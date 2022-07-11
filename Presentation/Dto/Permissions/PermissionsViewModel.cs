using Application.Common.Mappings;
using wms.Dto.Common;
using wms.Dto.Policies;

namespace wms.Dto.Permissions;

public class PermissionsViewModel : IViewModel, IMapFrom<Application.Common.Security.Permissions>
{
    public bool AllPermissions { get; set; }
    public bool None { get; set; }

    public IList<PolicyViewModel> Policies { get; set; }
}