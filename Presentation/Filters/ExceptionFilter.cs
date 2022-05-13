using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Domain.Exceptions;
using wms.Dto.Common.Responses;
using StatusCodes = Microsoft.AspNetCore.Http.StatusCodes;

namespace wms.Filters;

public class ExceptionFilter : ExceptionFilterAttribute
{
    private readonly ILogger<ExceptionFilter> _logger;

    private readonly IHostEnvironment _hostEnvironment;

    private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionMap;

    public ExceptionFilter(IHostEnvironment hostEnvironment, ILogger<ExceptionFilter> logger)
    {
        _hostEnvironment = hostEnvironment;
        _logger = logger;
        _exceptionMap = new Dictionary<Type, Action<ExceptionContext>>
        {
            {typeof(ProductMinLevelExceededException), HandleProductMinLevelExceededException}
        };
    }

    public override void OnException(ExceptionContext context)
    {
        Type type = context.Exception.GetType();

        if (_exceptionMap.ContainsKey(type)) // Exception is any of the types declared in the dictionary.
        {
            _exceptionMap[type].Invoke(context);
        }
        else if (context.Exception is BaseException)
        {
            HandleCustomException(context);
        }
        else // Exception is unknown
        {
            HandleUnknownException(context);
        }

        base.OnException(context);
    }

    private void HandleUnknownException(ExceptionContext context)
    {
        
        if (_hostEnvironment.IsDevelopment() || _hostEnvironment.IsStaging())
        {
            _logger.LogError(context.Exception, null);
        }

        string message = $"Something went wrong, an unknown error occured, please try again later.";

        var responseBody = new NoDataResponse(message);

        context.Result = new ObjectResult(responseBody) {StatusCode = StatusCodes.Status500InternalServerError};

        context.ExceptionHandled = true;
    }

    private void HandleCustomException(ExceptionContext context)
    {
        BaseException exception = (BaseException) context.Exception;

        var responseBody = new NoDataResponse(exception.Message);

        context.Result = new ObjectResult(responseBody) {StatusCode = exception.Code};

        context.ExceptionHandled = true;
    }

    private void HandleProductMinLevelExceededException(ExceptionContext context)
    {
        var exception = context.Exception as ProductMinLevelExceededException;

        var responseBody = new BaseResponse<IList<int>>(
            new ResponseMetaData {message = exception!.Message}, exception.ProductsWithExceededMinLevel);

        context.Result = new ObjectResult(responseBody) {StatusCode = exception.Code};

        context.ExceptionHandled = true;
    }
}