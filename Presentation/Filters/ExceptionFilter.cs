using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using wms.Dto.Responses.Common;

namespace wms.Filters;

public class ExceptionFilter : ExceptionFilterAttribute
{
    private readonly IHostEnvironment _hostEnvironment;

    private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionMap;

    public ExceptionFilter(IHostEnvironment hostEnvironment)
    {
        _hostEnvironment = hostEnvironment;
        _exceptionMap = new Dictionary<Type, Action<ExceptionContext>>
        {
            // {typeof(InvalidOperationException), HandleValidationException}
        };
    }

    public override void OnException(ExceptionContext context)
    {
        Type type = context.Exception.GetType();

        if (_exceptionMap.ContainsKey(type)) // Exception is any of the types declared in the dictionary.
        {
            _exceptionMap[type].Invoke(context);
        }
        else // Exception is unknown
        {
            HandleUnknownException(context);
        }

        base.OnException(context);
    }

    private void HandleUnknownException(ExceptionContext context)
    {
        string message;
        if (_hostEnvironment.IsDevelopment() || _hostEnvironment.IsStaging())
        {
            message = $"{context.Exception.Message} {context.Exception.StackTrace}";
        }
        else
        {
            message = $"Something went wrong, an unknown error occured, please try again later.";
        }

        var responseBody = new NoDataResponse(message);

        context.Result = new ObjectResult(responseBody) {StatusCode = StatusCodes.Status500InternalServerError};

        context.ExceptionHandled = true;
    }
}