using DataAccess.Entities;
using DataAccess.Entities.Configurations;
using DataAccess.Extension;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

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

            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("db"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new HostActivityConfiguration());
            modelBuilder.ApplyConfiguration(new ThresholdConfiguration());
            
            modelBuilder.SeedDefaultDataToDb();
        }

        public virtual DbSet<Threshold> Thresholds { get; set; }

        public virtual DbSet<HostActivity> HostActivities { get; set; }
    }
}
