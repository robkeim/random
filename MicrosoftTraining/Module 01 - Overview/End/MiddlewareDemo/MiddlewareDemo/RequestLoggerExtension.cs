using Owin;

namespace MiddlewareDemo
{
    public static class RequestLoggerExtension
    {
        public static IAppBuilder UseRequestLogger(this IAppBuilder builder)
        {
            return builder.Use<RequestLoggerMiddleware>(builder);
        }
    }
}
