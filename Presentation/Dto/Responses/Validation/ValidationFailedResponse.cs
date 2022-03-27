using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using wms.Dto.Responses.Common;

namespace wms.Dto.Responses.Validation;

public class ValidationFailedResponse : NoDataResponse
{
    public ValidationFailedResponse(ModelStateDictionary modelState) : base(MapModelStateToMessage(modelState))
    {
    }

    private static string MapModelStateToMessage(ModelStateDictionary modelState)
    {
        return modelState.Keys.Aggregate(
            "",
            (s, key) => modelState[key].Errors.Aggregate(
                s,
                (ss, modelError) => ss + (modelError.ErrorMessage + ' ')
            )
        )[..^1];
    }
}