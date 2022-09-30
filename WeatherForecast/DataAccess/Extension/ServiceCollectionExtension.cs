using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Extension
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection RegisterDbContext(
            this IServiceCollection services,
            IConfiguration Configuration)
        {
            //access the appsetting json file in your WebApplication File

            string filePath = Directory.GetCurrentDirectory();

            Configuration = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(filePath))
                .AddJsonFile("appSettings.json")
                .Build();

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("dbConnection")));
            return services;
        }

    }
}