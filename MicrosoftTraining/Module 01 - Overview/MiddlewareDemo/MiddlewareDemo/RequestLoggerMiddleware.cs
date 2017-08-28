using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Owin.Logging;
using Owin;

namespace MiddlewareDemo
{
    using AppFunc = Func<IDictionary<string, object>, Task>;

    public class RequestLoggerMiddleware
    {
        private readonly ILogger _logger;
        AppFunc _next;
        public RequestLoggerMiddleware(AppFunc next, IAppBuilder app)
        {
            _next = next;
            _logger = app.CreateLogger<RequestLoggerMiddleware>();
        }
        public async Task Invoke(IDictionary<string, object> environment)
        {
            _logger.WriteInformation($"Handling request: {environment["owin.RequestPath"]}");
            await _next(environment);
            _logger.WriteInformation("Finished handling request.");

        }
    }
}
