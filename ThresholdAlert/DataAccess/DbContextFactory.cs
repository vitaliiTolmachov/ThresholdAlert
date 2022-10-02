using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DataAccess
{
    public class DbContextFactory : IDesignTimeDbContextFactory<DbContext>
    {
        public DbContextFactory()
        {

        }

        private readonly IConfiguration Configuration;

        public DbContextFactory(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public DbContext CreateDbContext(string[] args)
        {

            string filePath = (new Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath;

            IConfiguration Configuration = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(filePath))
                .AddJsonFile("dbSettings.json")
                .Build();


            var optionsBuilder = new DbContextOptionsBuilder<DbContext>();
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("db"));

            return new DbContext(optionsBuilder.Options);
        }
    }
}
