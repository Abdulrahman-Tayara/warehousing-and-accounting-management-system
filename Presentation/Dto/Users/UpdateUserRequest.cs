using Application.Commands.Users.UpdateUser;
using Application.Common.Mappings;

namespace wms.Dto.Users;

public class UpdateUserRequest : IMapFrom<UpdateUserCommand>
{
    public string Username { get; set; }    
}