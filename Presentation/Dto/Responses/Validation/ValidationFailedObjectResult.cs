using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace wms.Dto.Responses.Validation;

public class ValidationFailedObjectResult : ObjectResult
{
    public ValidationFailedObjectResult(ModelStateDictionary modelState)
        : base(new ValidationFailedResponse(modelState))
    {
        StatusCode = StatusCodes.Status422UnprocessableEntity;
    }
}