using Azure.Messaging.ServiceBus;
using Messages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using DbContext = DataAccess.DbContext;

namespace RequestInterceptor
{
    public interface IAlertService
    {
        Task TrackActivityAsync(string hostName, long userId);
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

        public async Task TrackActivityAsync(string hostName, long userId)
        {
            var threshold = await _dbContext.Thresholds.AsNoTracking()
                .FirstOrDefaultAsync(x => x.HostName == hostName &&
                                     x.UserIdId == userId);
            //TODO: Business logic exception should be raised
            if (threshold == null)
                return;

            var now = DateTime.UtcNow;
            var hostActivity = await _dbContext.HostActivities
                .FirstAsync(x => x.ThresholdId == threshold.ThresholdId &&
                                 x.Month == now.Month &&
                                 x.Year == now.Year &&
                                 x.UserId == userId)
                .ConfigureAwait(false);

            hostActivity.CallsMade++;
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            await CheckThresholdAsync(hostActivity.ThresholdId, hostActivity.CallsMade).ConfigureAwait(false);
        }

        private async Task CheckThresholdAsync(long thresholdId, long callsMade)
        {
            var threshold = await _dbContext.Thresholds
                .FirstOrDefaultAsync(x => x.ThresholdId == thresholdId)
                .ConfigureAwait(false);

            if (threshold == null)
                return;

            var currentPercentage = callsMade * 100 / threshold.MaxCalls;

            if (threshold.IsAlertSent || currentPercentage < threshold.NotificationLevel)
                return;

            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == threshold.UserIdId);

            await using (var sender = _client.CreateSender(queueName))
            {
                var messageBody = new AlertMessage
                {
                    UserFirstName = user.FirstName,
                    UserLastName = user.LastName,
                    UserName = user.Username,
                    RequestedHost = threshold.HostName,
                    Percentage = threshold.NotificationLevel,
                    CallLimit = threshold.MaxCalls
                };
                var json = JsonConvert.SerializeObject(messageBody);
                var message = new ServiceBusMessage(json);

                // send the message
                await sender.SendMessageAsync(message).ConfigureAwait(false);

                threshold.IsAlertSent = true;
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
        }
    }
}
