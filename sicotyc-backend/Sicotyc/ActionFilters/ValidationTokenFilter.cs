using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Sicotyc.ActionFilters
{
    public class ValidationTokenFilter : IActionFilter
    {
        private readonly ILoggerManager _logger;
        private readonly IAuthenticationManager _authManager;

        public ValidationTokenFilter(ILoggerManager logger, IAuthenticationManager authManager)
        {
            _logger = logger;
            _authManager = authManager; 

        }
        public async void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Headers.TryGetValue("x-token", out var tokenHeaderValue))
            {
                ResultProcess validToken = await _authManager.ValidateToken(tokenHeaderValue);

                if (validToken.Status != HttpStatusCode.OK)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }
                //else {
                //    context.Result = new OkResult();
                //    return;
                //}
            }

        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

    }
}
