using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

namespace FactoryAPIProject.Filters
{
    public class TokenValidationFilter : IAsyncActionFilter
    {
        private readonly List<string> _tokens;

        public TokenValidationFilter(List<string> tokens)
        {
            _tokens = tokens;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var authorizationHeader = context.HttpContext.Request.Headers["Authorization"];

            if (!StringValues.IsNullOrEmpty(authorizationHeader))
            {
                var headerValue = new StringSegment(authorizationHeader);
                if (headerValue.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                {
                    var token = headerValue.Substring("Bearer ".Length).Trim();
                    if (_tokens.Contains(token))
                    {
                        context.Result = new UnauthorizedResult();
                    }
                }
            }
            await next();
        }
    }
}
