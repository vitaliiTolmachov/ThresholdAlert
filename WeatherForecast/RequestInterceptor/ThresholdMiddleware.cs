using Authentification.Models;
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

            var user = (User)httpContext.Items["User"];
            if (user == null) return;

            await service.TrackActivityAsync(httpContext.Request.Host.Value, user.Id).ConfigureAwait(false);

        }

    }
}