using Application.Common.Mappings;
using wms.Dto.Authorization.Roles;
using wms.Dto.Common;
using wms.Dto.Users;

namespace wms.Dto.Authorization.UserRoles;

public class UserRolesViewModel : IViewModel, IMapFrom<Application.Common.Security.UserRoles>
{
    public UserViewModel User { get; set; }
    
    public IList<RoleViewModel> Roles { get; set; }
}