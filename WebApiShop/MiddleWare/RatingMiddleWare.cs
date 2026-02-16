using Services;
using Entities;

namespace WebApiShop.MiddleWare
{
    public class RatingMiddleWare
    {
        RequestDelegate _next;

        public RatingMiddleWare(RequestDelegate requestDelegate)
        {
            _next = requestDelegate;
        }

        public async Task Invoke(HttpContext httpContext, IRatingService ratingService) // Change parameter from HttpContent to HttpContext
        {
            Rating rating = new Rating();
            rating.Host = httpContext.Request.Host.Host;
            rating.Method = httpContext.Request.Method;
            rating.Path = httpContext.Request.Path;
            rating.Referer = httpContext.Request.Headers.Referer;
            rating.UserAgent = httpContext.Request.Headers.UserAgent;
            rating.RecordDate = DateTime.Now;
            await ratingService.AddRating(rating);
            await _next(httpContext);
        }
    }

    public static class RatingMiddleWareExtensions
    {
        public static IApplicationBuilder UseRating(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RatingMiddleWare>();
        }
    }
}
