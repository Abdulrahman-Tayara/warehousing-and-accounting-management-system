using Application.Common.Security;
using Application.Exceptions;
using Application.Services.Identity;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence.Database.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services;

public class IdentityService : IIdentityService
{
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationIdentityUser> _userManager;
    private readonly ApplicationUserClaimsPrincipalFactory _userClaimsPrincipalFactory;
    private readonly IAuthorizationService _authorizationService;

    public IdentityService(IMapper mapper, UserManager<ApplicationIdentityUser> userManager,
        IAuthorizationService authorizationService,
        ApplicationUserClaimsPrincipalFactory userClaimsPrincipalFactory)
    {
        _mapper = mapper;
        _userManager = userManager;
        _authorizationService = authorizationService;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
    }

    public async Task<bool> CheckPasswordAsync(User user, string password)
    {
        var identityUser = await _userManager.FindByIdAsync(user.Id.ToString());

        return await _userManager.CheckPasswordAsync(identityUser, password);
    }

    public async Task<User> FindUserByNameAsync(string username)
    {
        var identityUser = await _userManager.FindByNameAsync(username);

        if (identityUser == null)
            throw new NotFoundException();

        return _mapper.Map<ApplicationIdentityUser, User>(identityUser);
    }

    public async Task<bool> AuthorizeAsync(int userId, string policyName)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        
        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);
        
        var result = await _authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

    public Task<bool> AuthorizeAsync(int userId, IList<Policy> policies)
    {
        return AuthorizeAsync(userId, PolicyHelper.PoliciesToString(policies));
    }
}