namespace MyShuttle.Api.Filters
{
    using System;
    using System.Net;
    using System.Net.Http.Headers;
    using System.Web.Http.Filters;

    internal class NoCacheAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Response.StatusCode == HttpStatusCode.OK
                && !actionExecutedContext.Response.Headers.Contains("CacheControl"))
            {
                actionExecutedContext.Response.Headers.CacheControl = new CacheControlHeaderValue { NoCache = true };
                actionExecutedContext.Response.Headers.Pragma.ParseAdd("no-cache");

                if (!actionExecutedContext.Response.Content.Headers.Contains("Expires"))
                {
                    actionExecutedContext.Response.Content.Headers.Expires = DateTimeOffset.UtcNow.AddDays(-1);
                }
            }
        }
    }
}