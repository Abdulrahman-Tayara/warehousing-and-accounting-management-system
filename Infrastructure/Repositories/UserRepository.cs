using Application.Exceptions;
using Application.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Infrastructure.Extensions;
using Infrastructure.Persistence.Database.Models;
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
        return Task.CompletedTask;
    }

    public async Task<SaveAction<Task<User>>> CreateAsync(User user)
    {
        var identityUser = _mapper.Map<User, ApplicationIdentityUser>(user);

        var result = await _userManager.CreateAsync(identityUser, user.PasswordHash);

        if (result.Succeeded)
            return () => Task.FromResult(_mapper.Map<ApplicationIdentityUser, User>(identityUser));

        throw new Exception(result.GetErrorsAsString());
    }

    public IQueryable<User> GetAll(GetAllOptions<User>? options = default)
    {
        return _userManager.Users
            .ProjectTo<User>(_mapper.ConfigurationProvider);
    }

    public Task<User> FindByIdAsync(int id, FindOptions? options = default)
    {
        return _userManager.FindByIdAsync(id.ToString())
            .ContinueWith(task =>
            {
                if (task.Result == null)
                    throw new NotFoundException("user", id);

                return _mapper.Map<User>(task.Result);
            });
    }

    public Task<User> FindIncludedByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(int id)
    {
        var model = await _userManager.FindByIdAsync(id.ToString());

        if (model == null)
        {
            throw new NotFoundException();
        }

        await _userManager.DeleteAsync(model);
    }

    public async Task<User> Update(User user)
    {
        var model =  await _userManager.FindByIdAsync(user.Id.ToString());

        model.UserName = user.UserName;
        
        var result = await _userManager.UpdateAsync(model);

        if (!result.Succeeded)
        {
            throw new Exception(result.GetErrorsAsString());
        }

        return _mapper.Map<User>(model);
    }
}