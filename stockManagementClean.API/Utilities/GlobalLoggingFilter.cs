using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
namespace stockManagementClean.API.Utilities
{    public class GlobalLoggingFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var request = context.HttpContext.Request;
            var method = request.Method;
            var path = request.Path;
            Log.Information("Incoming Request: {Method} {Path}", method, path);
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var response = context.HttpContext.Response;
            var statusCode = response.StatusCode;

            Log.Information("Outgoing Response: Status Code {StatusCode}", statusCode);
        }
    }

}
