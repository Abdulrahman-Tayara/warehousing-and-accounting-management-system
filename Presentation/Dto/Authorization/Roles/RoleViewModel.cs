using Application.Common.Mappings;
using Application.Common.Security;
using wms.Dto.Authorization.Permissions;
using wms.Dto.Common;

namespace wms.Dto.Authorization.Roles;

public class RoleViewModel : IViewModel, IMapFrom<Role>
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public PermissionsViewModel Permissions { get; set; }
}