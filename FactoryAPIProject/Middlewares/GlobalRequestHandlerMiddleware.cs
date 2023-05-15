using FactoryAPIProject.Services;
using System.Runtime.CompilerServices;

namespace FactoryAPIProject.Middlewares
{
    public class GlobalRequestHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string path = @"C:\\Users\\Casper\\Desktop\\GitHub Projects\\FactoryAPIProject\\FactoryAPIProject";
        private readonly IHttpContextAccessor httpContextAccessor;
        public GlobalRequestHandlerMiddleware(RequestDelegate next, IHttpContextAccessor httpContextAccessor)
        {
            _next = next;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task Invoke(HttpContext context)
        {
            using var fileStream = new FileStream($"{path}/Logs/logRequest.txt", FileMode.Append);

            using var streamWriter = new StreamWriter(fileStream);

            string name = httpContextAccessor.HttpContext.Request.HttpContext.User.Identity.Name;
            await streamWriter.WriteLineAsync($"{DateTime.UtcNow} - {context.Request.Method} {context.Request.Path} - [User Name : {name}]");

            await streamWriter.FlushAsync();

            await _next(context);
        }
    }
}