using Application.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Infrastructure.Extensions;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

    public IEnumerable<User> GetAll()
    {
        return _userManager.Users
            .ProjectTo<User>(_mapper.ConfigurationProvider);
    }

    public Task<User> FindByIdAsync(int id)
    {
        return _userManager.FindByIdAsync(id.ToString())
            .ContinueWith(task => _mapper.Map<User>(task.Result));
    }
}