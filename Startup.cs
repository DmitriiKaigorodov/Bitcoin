using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Bitcoin.Core;
using Bitcoin.Core.Repositories;
using Bitcoin.Core.Services;
using Bitcoin.Core.Settings;
using Bitcoin.Middleware;
using Bitcoin.Persistent;
using Bitcoin.Persistent.Repositories;
using Bitcoin.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bitcoin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddAutoMapper();
            var connectionString = Configuration.GetConnectionString("Default");
            services.AddDbContext<BitcoinDbContext>(opt => opt.UseSqlServer(connectionString));
            services.Configure<BitcoinDaemonSettings>(Configuration.GetSection("BitcoinDaemon"));
            services.AddScoped<IRpcService, RpcService>();
            services.AddScoped<IBitcoinService, BitcoinService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IWalletService, WalletService>();        
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            ConfigureRepositories(services);
        }

        public void ConfigureRepositories(IServiceCollection services)
        {
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IWalletRepository,WalletRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseWhen( context => context.Request.Path.StartsWithSegments("/api"),
            appBuilder =>
            {
                appBuilder.UseBitcoinDaemonAuthorizationMiddleware(Configuration.GetSection("BitcoinDaemon"));
            });

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
