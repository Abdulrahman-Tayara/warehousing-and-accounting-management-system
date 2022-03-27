using Microsoft.AspNetCore.Mvc;
using wms.Dto.Responses.Common;

namespace wms.Dto;

public class MyActionResult<T>
{
    public ActionResult<BaseResponse<T>> Value { get; }
}