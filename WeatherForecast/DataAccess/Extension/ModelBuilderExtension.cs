using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Extension
{
    internal static class ModelBuilderExtension
    {
        internal static ModelBuilder SeedDefaultDataToDb(
            this ModelBuilder modelBuilder)
        {
            #region Threshold

            var hostName = "localhost:6054";
            var threshold = new Threshold
            {
                ThresholdId = 1,
                HostName = hostName,
                MaxCalls = 10,
                NotificationLevel = 50
            };

            modelBuilder.Entity<Threshold>().HasData(threshold);

            #endregion


            #region HostActivity

            modelBuilder.Entity<HostActivity>().HasData(
                   new HostActivity
                   {
                       HostActivityId = 1,
                       HostName = hostName,
                       CallsMade = 0,
                       Month = 9,
                       Year = 2022,
                       ThresholdId = threshold.ThresholdId
                   },
                   new HostActivity
                   {
                       HostActivityId = 2,
                       HostName = hostName,
                       CallsMade = 0,
                       Month = 10,
                       Year = 2022,
                       ThresholdId = threshold.ThresholdId
                   });

            #endregion

            return modelBuilder;
        }
    }
}
