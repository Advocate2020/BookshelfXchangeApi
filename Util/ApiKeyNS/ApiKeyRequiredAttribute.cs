using BookXChangeApi.Util.DeveloperAuthorizationNS;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookXChangeApi.Util.ApiKeyNS
{
    /// <summary>
    ///     Controller endpoints secured with this attribute require an ApiKey passed as a header.
    ///     These endpoints can only be accessed if the ApiKey is valid.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class ApiKeyRequired : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var apiKey = (string?)context.HttpContext.Request.Headers["ApiKey"]; // Get the Api Key from the request header.

            if (string.IsNullOrEmpty(apiKey))
            {
                context.Result = ErrorResponse.CustomError("ApiKey header missing.", 401);

                return;
            }

            // The apiKey will be compared against the hash stored in Secrets Manager.
            var isValid = DeveloperAuthorization.IsApiKeyValid(apiKey);

            if (!isValid)
            {
                context.Result = ErrorResponse.CustomError("ApiKey invalid.", 401);
                return; // Code execution is aborted.
            }

            await next(); // Code execution continues.
        }
    }
}