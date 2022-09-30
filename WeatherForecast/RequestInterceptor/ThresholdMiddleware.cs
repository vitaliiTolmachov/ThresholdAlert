using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Http;

namespace RequestInterceptor
{
    public class ThresholdMiddleware
    {
        private readonly RequestDelegate _next;

        // connection string to your Service Bus namespace
        static string connectionString = "Endpoint=sb://ns-threshold.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=gQ9wWd5WFsSx6lRjnWp/kbFMqfTJa2IOkwFIQR8jx6I=";

        // name of your Service Bus queue
        static string queueName = "alerts";


        public ThresholdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // IMessageWriter is injected into InvokeAsync
        public async Task InvokeAsync(HttpContext httpContext, ServiceBusClient client)
        {
            await _next(httpContext);

            var sender = client.CreateSender(queueName);

            var message = new ServiceBusMessage(httpContext.Request.Host.Value);

            // send the message
            await sender.SendMessageAsync(message);

        }

    }
}