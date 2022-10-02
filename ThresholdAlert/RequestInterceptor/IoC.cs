using Azure.Messaging.ServiceBus;
using DataAccess.Extension;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace RequestInterceptor
{
    public static class IoC
    {
        public static IServiceCollection AddThresholdMiddleware(
            this IServiceCollection services)
        {
            services.AddSingleton<ServiceBusClient>(sp =>
            {
                // connection string to your Service Bus namespace
                var connectionString =
                    "Endpoint=sb://ns-threshold.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=gQ9wWd5WFsSx6lRjnWp/kbFMqfTJa2IOkwFIQR8jx6I=";

                // name of your Service Bus queue
                var queueName = "alerts";

                return new ServiceBusClient(connectionString);

            });
            services.AddScoped<IAlertService, AlertService>();
            services.RegisterDbContext();
            return services;
        }
    }
}
