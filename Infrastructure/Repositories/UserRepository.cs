using Application.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Infrastructure.Extensions;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManager<ApplicationIdentityUser> _userManager;
    private readonly IMapper _mapper;

    public UserRepository(UserManager<ApplicationIdentityUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public Task SaveChanges()
    {
        throw new NotImplementedException();
    }

    public async Task<User> CreateAsync(User user)
    {
        var identityUser = _mapper.Map<User, ApplicationIdentityUser>(user);

        var result = await _userManager.CreateAsync(identityUser);

        if (result.Succeeded)
            return _mapper.Map<ApplicationIdentityUser, User>(identityUser);

        throw new Exception(result.GetErrorsAsString());
    }

    public IEnumerable<User> GetAllAsync(Func<User, bool>? filter = default)
    {
        if (filter == null)
        {
            return _userManager.Users
                .ProjectTo<User>(_mapper.ConfigurationProvider);
        }

        return _userManager.Users
            .Where(model => filter(_mapper.Map<User>(model)))
            .ProjectTo<User>(_mapper.ConfigurationProvider);
    }
}