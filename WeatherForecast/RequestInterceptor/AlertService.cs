using Azure.Messaging.ServiceBus;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using DbContext = DataAccess.DbContext;

namespace RequestInterceptor
{
    public interface IAlertService
    {
        Task TrackActivityAsync(string hostName);
    }

    internal class AlertService : IAlertService
    {
        private readonly ServiceBusClient _client;
        private readonly DbContext _dbContext;

        // connection string to your Service Bus namespace
        static string connectionString = "Endpoint=sb://ns-threshold.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=gQ9wWd5WFsSx6lRjnWp/kbFMqfTJa2IOkwFIQR8jx6I=";

        // name of your Service Bus queue
        static string queueName = "alerts";

        public AlertService(ServiceBusClient client, DbContext dbContext)
        {
            _client = client;
            _dbContext = dbContext;
        }

        public async Task TrackActivityAsync(string hostName)
        {
            var now = DateTime.UtcNow;
            var hostActivity = await _dbContext.HostActivities
                .Include(x => x.Threshold)
                .FirstAsync(x => x.HostName == hostName &&
                                 x.Month == now.Month &&
                                 x.Year == now.Year)
                .ConfigureAwait(false);

            hostActivity.CallsMade++;
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            await CheckThresholdAsync(hostActivity.Threshold, hostActivity.CallsMade).ConfigureAwait(false);
        }

        private async Task CheckThresholdAsync(Threshold threshold, long callsMade)
        {
            var currentPercentage = callsMade * 100 / threshold.MaxCalls;

            if (currentPercentage < threshold.NotificationLevel)
                return;

            var sender = _client.CreateSender(queueName);

            var message = new ServiceBusMessage(threshold.HostName);

            // send the message
            await sender.SendMessageAsync(message).ConfigureAwait(false);
        }
    }
}
