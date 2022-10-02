using Authentification.Models;
using Authentification.Services;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Authentification.Middleware

{
    public class BasicAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public BasicAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserService userService)
        {
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(context.Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':', 2);
                var username = credentials[0];
                var password = credentials[1];

                var userDto = await userService.GetUserNameAsync(username, password);

                // authenticate credentials with user service and attach user to http context
                context.Items["User"] = new User()
                {
                    Id = userDto.Id,
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    Username = username,
                    Password = password
                };
            }
            catch (Exception)
            {

                // do nothing if invalid auth header
                // user is not attached to context so request won't have access to secure routes
            }

            await _next(context);
        }
    }
}
