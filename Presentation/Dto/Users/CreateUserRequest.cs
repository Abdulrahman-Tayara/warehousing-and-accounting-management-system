using Application.Commands.Users.CreateUser;
using Application.Common.Mappings;

namespace wms.Dto.Users;

public class CreateUserRequest : IMapFrom<CreateUserCommand>
{
    public string Username { get; set; }

    public string Password { get; set; }
}