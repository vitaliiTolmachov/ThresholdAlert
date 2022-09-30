using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DataAccess
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContextFactory()
        {

        }

        private readonly IConfiguration Configuration;

        public AppDbContextFactory(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public AppDbContext CreateDbContext(string[] args)
        {

            string filePath = (new Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath;

            IConfiguration Configuration = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(filePath))
                .AddJsonFile("appSettings.json")
                .Build();


            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("db"));

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
