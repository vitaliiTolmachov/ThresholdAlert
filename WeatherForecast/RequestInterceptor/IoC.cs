﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
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
            return services;
        }

        public static IApplicationBuilder UseThresholdMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ThresholdMiddleware>();
        }
    }
}
