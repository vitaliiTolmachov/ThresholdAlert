using Azure.Messaging.ServiceBus;
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
                .FirstAsync(x => x.HostName == hostName &&
                                 x.Month == now.Month &&
                                 x.Year == now.Year)
                .ConfigureAwait(false);

            hostActivity.CallsMade++;
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            await CheckThresholdAsync(hostActivity.ThresholdId, hostActivity.CallsMade).ConfigureAwait(false);
        }

        private async Task CheckThresholdAsync(long thresholdId, long callsMade)
        {
            var threshold = await _dbContext.Thresholds
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ThresholdId == thresholdId)
                .ConfigureAwait(false);

            if (threshold == null)
                return;

            var currentPercentage = callsMade * 100 / threshold.MaxCalls;

            if (currentPercentage < threshold.NotificationLevel)
                return;

            await using (var sender = _client.CreateSender(queueName))
            {              

                var message = new ServiceBusMessage(threshold.HostName);

                // send the message
                await sender.SendMessageAsync(message).ConfigureAwait(false); 
            }
        }
    }
}
