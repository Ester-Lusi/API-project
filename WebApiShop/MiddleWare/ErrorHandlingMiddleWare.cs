namespace WebApiShop.MiddleWare
{
    public class ErrorHandlingMiddleWare
    {
        RequestDelegate _next;
        ILogger<ErrorHandlingMiddleWare> _logger;

        public ErrorHandlingMiddleWare(RequestDelegate requestDelegate, ILogger<ErrorHandlingMiddleWare> logger)
        {
            _next = requestDelegate;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                httpContext.Response.StatusCode = 500;
                _logger.LogError(ex + "call Stack:" + ex.StackTrace);
            }
        }
    }

    public static class ErrorHandlingMiddleWareExtensions
    {
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleWare>();
        }
    }
}
