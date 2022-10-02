using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    internal static class DbSeed
    {
        internal static ModelBuilder SeedDefaultDataToDb(
            this ModelBuilder modelBuilder)
        {
            #region Threshold
            //TODO: Use userId instead of defaultThreshold
            var hostName = "localhost:6054";

            var defaultUser = new User()
            {
                Id = 1,
                FirstName = "Admin",
                LastName = "Admin",
                Username = "test",
                Password = "test"

            };

            var defaultThreshold = new Threshold
            {
                ThresholdId = 1,
                HostName = hostName,
                MaxCalls = 10,
                NotificationLevel = 50,
                UserIdId = defaultUser.Id
            };

            modelBuilder.Entity<Threshold>().HasData(defaultThreshold);

            #endregion


            #region HostActivity

            modelBuilder.Entity<HostActivity>().HasData(
                   new HostActivity
                   {
                       HostActivityId = 1,
                       CallsMade = 0,
                       Month = 9,
                       Year = 2022,
                       ThresholdId = defaultThreshold.ThresholdId,
                       UserId = defaultUser.Id
                   },
                   new HostActivity
                   {
                       HostActivityId = 2,
                       CallsMade = 0,
                       Month = 10,
                       Year = 2022,
                       ThresholdId = defaultThreshold.ThresholdId,
                       UserId = defaultUser.Id
                   });

            #endregion

            #region User

            modelBuilder.Entity<User>().HasData(defaultUser); 

            #endregion

            return modelBuilder;
        }
    }
}
