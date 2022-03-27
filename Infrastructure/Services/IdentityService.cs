using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Extensions;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services;

public class IdentityService
{
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationIdentityUser> _userManager;

    public IdentityService(IMapper mapper, UserManager<ApplicationIdentityUser> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<User> CreateUserAsync(User user)
    {
        var identityUser = _mapper.Map<User, ApplicationIdentityUser>(user);

        var result = await _userManager.CreateAsync(identityUser);

        if (result.Succeeded)
            return _mapper.Map<ApplicationIdentityUser, User>(identityUser);

        throw new Exception(result.GetErrorsAsString());
    }

    public async Task<User> FindUserByIdAsync(int id)
    {
        var identityUser = await _userManager.FindByIdAsync(id.ToString());

        if (identityUser == null)
            throw new NotFoundException();

        return _mapper.Map<ApplicationIdentityUser, User>(identityUser);
    }
    
}