using OAuthTest.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace OAuthTest.Filters
{
    public class ControllerExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is AppException exception)
            {
                context.Result = exception.GetResponse();
            }
        }
    }
}
