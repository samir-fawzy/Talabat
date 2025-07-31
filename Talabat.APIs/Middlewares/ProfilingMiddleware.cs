using System.Diagnostics;

namespace Talabat.APIs.Middlewares
{
    public class ProfilingMiddleware
    {


        private readonly RequestDelegate next;
        private readonly ILogger<ProfilingMiddleware> logger;

        public ProfilingMiddleware(RequestDelegate Next,ILogger<ProfilingMiddleware> logger)
        {
            next = Next;
            this.logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            await next(context);
            stopWatch.Stop();
            logger.LogInformation($"Request {context.Request.Method} {context.Request.Path} executed in {stopWatch.ElapsedMilliseconds} ms");


        }
    }
}
