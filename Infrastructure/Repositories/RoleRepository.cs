using Application.Common.Security;
using Application.Exceptions;
using Application.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Infrastructure.Extensions;
using Infrastructure.Persistence.Database.Models;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly IMapper _mapper;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public RoleRepository(RoleManager<ApplicationRole> roleManager, IMapper mapper)
    {
        _roleManager = roleManager;
        _mapper = mapper;
    }

    public async Task<SaveAction<Task<Role>>> CreateAsync(Role role)
    {
        var applicationRole = _mapper.Map<Role, ApplicationRole>(role);

        var result = await _roleManager.CreateAsync(applicationRole);

        if (result.Succeeded)
            return () => Task.FromResult(_mapper.Map<ApplicationRole, Role>(applicationRole));

        throw new Exception(result.GetErrorsAsString());
    }

    public async Task<Role> Update(Role role)
    {
        var applicationRole = _mapper.Map<Role, ApplicationRole>(role);

        var result = await _roleManager.UpdateAsync(applicationRole);

        if (result.Succeeded)
            return _mapper.Map<ApplicationRole, Role>(applicationRole);

        throw new Exception(result.GetErrorsAsString());
    }

    public async Task<Role> FindByIdAsync(int id)
    {
        var role = await _roleManager.FindByIdAsync(id.ToString());

        if (role == null)
        {
            throw new NotFoundException();
        }

        return _mapper.Map<ApplicationRole, Role>(role);
    }

    public IQueryable<Role> GetAll()
    {
        return _roleManager.Roles.ProjectTo<Role>(_mapper.ConfigurationProvider);
    }

    public async Task DeleteAsync(int id)
    { 
        var role =  await _roleManager.FindByIdAsync(id.ToString());

        if (role == null)
        {
            throw new NotFoundException();
        }

        var result = await _roleManager.DeleteAsync(role);

        if (result.Succeeded)
            return;

        throw new Exception(result.GetErrorsAsString());
    }
}