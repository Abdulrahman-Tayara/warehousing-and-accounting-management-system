namespace wms.Dto.Common.Responses;

public class NoDataResponse : BaseResponse<object>
{
    public NoDataResponse(string? message = default) : base(new ResponseMetaData() {message = message}, null)
    {
    }
}