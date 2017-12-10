using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Bitcoin.Middleware
{
    public class BitcoinDaemonAuthorizationMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IConfigurationSection settings; 

        public BitcoinDaemonAuthorizationMiddleware(RequestDelegate next, IConfigurationSection settings)
        {
            this.settings = settings;
            this.next = next;
        }
  

        public async Task Invoke(HttpContext context)
        {


            string authHeader = context.Request.Headers["Authorization"];
            if (!string.IsNullOrEmpty(authHeader))
            {
                bool isHeaderValid = ValidateCredentials(authHeader);
                if (isHeaderValid)
                {
                    await next.Invoke(context);
                    return;
                }

            }
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Unauthorized");

        }

        private bool ValidateCredentials(string authHeader)
        {
            var userName = settings.GetValue<string>("RpcUsername");
            var password = settings.GetValue<string>("RpcPassword");
            return authHeader == "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(userName + ":" + password));
        }
    }
    public static class SimpleHeaderAuthorizationMiddlewareExtension
    {
        public static IApplicationBuilder UseBitcoinDaemonAuthorizationMiddleware(this IApplicationBuilder app, IConfigurationSection settings)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.UseMiddleware<BitcoinDaemonAuthorizationMiddleware>(settings);
        }
    }
}