using Application.Commands.Authorization.Roles;
using Application.Common.Mappings;
using wms.Dto.Authorization.Permissions;

namespace wms.Dto.Authorization.Roles;

public class UpdateRoleRequest : IMapFrom<UpdateRoleCommand>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public PermissionsViewModel Permissions { get; set; }
}