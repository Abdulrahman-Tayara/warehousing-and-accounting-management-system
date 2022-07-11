using Application.Commands.Roles;
using Application.Common.Mappings;
using wms.Dto.Permissions;

namespace wms.Dto.Roles;

public class CreateRoleRequest : IMapFrom<CreateRoleCommand>
{
    public string Name { get; set; }
    
    public PermissionsViewModel Permissions { get; set; }
}