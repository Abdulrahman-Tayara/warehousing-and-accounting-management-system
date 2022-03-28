using System.Reflection;
using Application.Common.Mappings;
using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using wms.Dto.Requests.Users;

namespace wms.Controllers;

public class UsersController : ApiControllerBase
{
    private ILogger<UsersController> _logger;
    private IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public UsersController(ILogger<UsersController> logger, IMapper mapper, IUserRepository userRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _userRepository = userRepository;
    }

    [HttpPost]
    public Task<User> CreateUser(CreateUserRequest request)
    {
        var mapFromTypes = Assembly.GetExecutingAssembly().GetExportedTypes()
            .Where(t => t.GetInterfaces().Any(i =>
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
            .Select(t => t.Name)
            .ToList();
        
        _logger.LogInformation(mapFromTypes.Count.ToString());
        
        var user = _mapper.Map<User>(new ApplicationIdentityUser()
        {
            Id = 1,
            UserName = "Abd",
            PasswordHash = "123"
        });
        
        _logger.LogInformation("User {} {}", user.UserName, user.PasswordHash);

        return Task.Run<User>(() => user);
        // return _userRepository.CreateAsync(new User()
        // {
        //     PasswordHash = request.Password,
        //     UserName = request.Username
        // });
    }
}