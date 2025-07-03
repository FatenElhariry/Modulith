using Serilog.Context;

namespace API.Application.Middlewares
{
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _headerName;

        public CorrelationIdMiddleware(RequestDelegate next, string headerName = "X-Correlation-ID")
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _headerName = headerName;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var correlationId = GetCorrelationId(context);

            using (LogContext.PushProperty("CorrelationId", correlationId))
            {
                await _next(context);
            }
        }

        private string GetCorrelationId(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue(_headerName, out var correlationIdValues) )
            {
                return correlationIdValues.FirstOrDefault() ?? Guid.NewGuid().ToString();
            }
            var correlationId = Guid.NewGuid().ToString();
            context.Request.Headers[_headerName] = correlationId;
            return correlationId;
        }
    }
}
