using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Serilog;



namespace stockManagementClean.API.Utilities
{
    public class GlobalModelValidator : ActionFilterAttribute
    {
        //private ILogger<GlobalModelValidator> _logger;

        public GlobalModelValidator()
        {
           // _logger = logger;
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if(context.HttpContext.Response.StatusCode !=200)
            {
                Log.Logger.Error($"error happened at {context.HttpContext.GetEndpoint().DisplayName.ToString()}");
            }
            Log.Logger.Information($"Operation {context.HttpContext.GetEndpoint().DisplayName.ToString()} Successful");


        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userInput = context.HttpContext.Request.Body;
            if (!context.ModelState.IsValid)
            {
                Log.Logger.Error($"Errors: {context.ModelState} Has Happened at {context.Controller.GetType().Name} and {context.ActionDescriptor.DisplayName}");
            }
        }
    }
}
