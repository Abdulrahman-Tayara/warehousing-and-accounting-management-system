using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace wms.Dto.Responses.Validation;

public class BadRequestObjectResult : ObjectResult
{
    public BadRequestObjectResult(ModelStateDictionary modelState)
        : base(new BadRequestResponse(modelState))
    {
        StatusCode = StatusCodes.Status400BadRequest;
    }
}