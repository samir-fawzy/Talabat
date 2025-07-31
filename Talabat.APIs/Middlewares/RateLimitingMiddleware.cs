using System.Data;
using System.Diagnostics.Metrics;
using System.Threading.Tasks;

namespace Talabat.APIs.Middlewares
{
    public class RateLimitingMiddleware
    {
        private static int counter = 0;
        private static DateTime lastRequest = DateTime.Now;
        // during 10 seconds , allow 10 requests
        private readonly RequestDelegate next;
        private readonly ILogger logger;

        public RateLimitingMiddleware(RequestDelegate next, ILogger<RateLimitingMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if(DateTime.Now.Subtract(lastRequest).Seconds > 10)
            {
                counter = 1;
                lastRequest = DateTime.Now;
                await next(context);
            }
            else
            {
                if(counter < 5)
                {
                    counter++;
                    await next(context);
                }
                else
                {
                   await context.Response.WriteAsync("Rate limit exceeded. Please try again later.");
                }
            }

        }
    }
}
