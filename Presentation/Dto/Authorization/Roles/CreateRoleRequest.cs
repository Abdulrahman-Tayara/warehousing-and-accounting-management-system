using Application.Commands.Authorization.Roles;
using Application.Common.Mappings;
using wms.Dto.Authorization.Permissions;

namespace wms.Dto.Authorization.Roles;

public class CreateRoleRequest : IMapFrom<CreateRoleCommand>
{
    public string Name { get; set; }
    
    public PermissionsViewModel Permissions { get; set; }
}