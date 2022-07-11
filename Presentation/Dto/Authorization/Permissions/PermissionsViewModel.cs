using Application.Common.Mappings;
using wms.Dto.Authorization.Policies;
using wms.Dto.Common;

namespace wms.Dto.Authorization.Permissions;

public class PermissionsViewModel : IViewModel, IMapFrom<Application.Common.Security.Permissions>
{
    public bool AllPermissions { get; set; }
    public bool None { get; set; }

    public IList<PolicyViewModel> Policies { get; set; }
}