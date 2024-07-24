using Shared.Exceptions;
using Shared.Extensions;
using System.Net;

namespace API.Middlewares
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomMiddleware> _logger;
        private readonly IConfiguration _config;

        public CustomMiddleware(RequestDelegate next, ILogger<CustomMiddleware> logger, IConfiguration config)
        {
            _next = next;
            _logger = logger;
            _config = config;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                if (!context.Request.Headers.TryGetValue("X-Correlation-ID", out var correlationId))
                {
                    correlationId = Guid.NewGuid().ToString();
                    context.Request.Headers["X-Correlation-ID"] = correlationId;
                }

                context.Response.OnStarting(() =>
                {
                    context.Response.Headers["X-Correlation-ID"] = correlationId;
                    return Task.CompletedTask;
                });

                using (_logger.BeginScope(new Dictionary<string, object> { { "CorrelationId", correlationId } }))
                {
                    await _next(context);
                }
            }
            catch (CustomException ex)
            {
                _logger.LogError($"Error Message: {ex}\n Error Detail: {ex.InnerException?.ToString()}");
                await context.Response.WriteAsJsonAsync(new ResponseModel   
                {
                    Status = false,
                    StatusCode = (int)ex.StatusCode,
                    Message = ex.Message
                });
            }
        }
    }


}
