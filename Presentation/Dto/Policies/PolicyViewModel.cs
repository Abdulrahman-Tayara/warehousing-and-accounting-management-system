using Application.Common.Mappings;
using Application.Common.Security;
using wms.Dto.Common;

namespace wms.Dto.Policies;

public class PolicyViewModel : IViewModel, IMapFrom<Policy>
{
    public Resource Resource { get; set; }
    
    public Method Method { get; set; }

    public string Name => Resource + "-" + Method;
}