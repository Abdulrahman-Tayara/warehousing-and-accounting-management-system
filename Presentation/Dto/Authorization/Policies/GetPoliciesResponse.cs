using Application.Common.Security;

namespace wms.Dto.Authorization.Policies;

public class GetPoliciesResponse
{
    public IEnumerable<Method> Methods { get; set; }
    public IEnumerable<Resource> Resources { get; set; }
}