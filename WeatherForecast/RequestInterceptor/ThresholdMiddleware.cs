using Azure.Messaging.ServiceBus;
using DataAccess;
using Microsoft.AspNetCore.Http;

namespace RequestInterceptor
{
    public class ThresholdMiddleware
    {
        private readonly RequestDelegate _next;

        public ThresholdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // IMessageWriter is injected into InvokeAsync
        public async Task InvokeAsync(
            HttpContext httpContext,
            IAlertService service)
        {
            await _next(httpContext).ConfigureAwait(false);

            await service.TrackActivityAsync(httpContext.Request.Host.Value).ConfigureAwait(false);

        }

    }
}