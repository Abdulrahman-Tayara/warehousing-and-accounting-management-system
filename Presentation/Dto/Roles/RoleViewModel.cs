using Application.Common.Mappings;
using Application.Common.Security;
using wms.Dto.Common;
using wms.Dto.Permissions;

namespace wms.Dto.Roles;

public class RoleViewModel : IViewModel, IMapFrom<Role>
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public PermissionsViewModel Permissions { get; set; }
}