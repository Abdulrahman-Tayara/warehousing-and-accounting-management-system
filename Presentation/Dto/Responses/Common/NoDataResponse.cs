namespace wms.Dto.Responses.Common;

public class NoDataResponse : BaseResponse<object>
{
    public NoDataResponse(string? message = default) : base(new ResponseMetaData() {message = message}, null)
    {
    }
}