using wms.Dto.Responses.Common;
using wms.Dto.ViewModels;

namespace wms.Dto.Responses.Authentication;

public class AuthenticateResponseBody : BaseResponse<AuthenticatedUserViewModel>
{
    public AuthenticateResponseBody(AuthenticatedUserViewModel data)
        : base(new ResponseMetaData {message = "Authenticated successfully"}, data)
    {
    }
}