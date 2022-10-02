using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DataAccess.Extension
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection RegisterDbContext(
            this IServiceCollection services)
        {
            //string filePath = (new Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath;
            string filePath = new Uri(Assembly.GetAssembly(typeof(DbContext)).CodeBase).AbsolutePath;

            var Configuration = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(filePath))
                .AddJsonFile("dbSettings.json")
                .Build();

            services.AddDbContext<DbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("db")));
            return services;
        }

    }
}