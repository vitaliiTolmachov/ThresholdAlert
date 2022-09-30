using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbContext(DbContextOptions<DbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string filePath = (new Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath;

            IConfiguration Configuration = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(filePath))
                .AddJsonFile("dbSettings.json")
                .Build();

            Configuration.GetConnectionString("db");
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("db"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var hostName = "localhost:6054";
            var threshold = new Threshold
            {
                ThresholdId = 1,
                HostName = hostName,
                MaxCalls = 10,
                NotificationLevel = 50
            };

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

            modelBuilder.Entity<Threshold>().HasData(threshold);



            modelBuilder.Entity<HostActivity>()
                .HasOne(x => x.Threshold)
                .WithMany(x => x.HostActivities);
        }

        public virtual DbSet<Threshold> Thresholds { get; set; }

        public virtual DbSet<HostActivity> HostActivities { get; set; }
    }
}
