namespace wms.Dto.Responses.Common;

public class NoDataResponse : BaseResponse<object>
{
    public NoDataResponse(string? message) : base(new ResponseMetaData() {message = message}, null)
    {
    }
}