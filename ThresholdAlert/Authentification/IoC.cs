using Authentification.Middleware;
using Authentification.Services;
using DataAccess.Extension;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CustomAuthentification
{
    public static class IoC
    {
        public static IServiceCollection AddCustomAuthorization(
            this IServiceCollection services)
        {
            services.RegisterDbContext();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
