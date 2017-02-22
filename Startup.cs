using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Routing;

namespace aka.terribledev.io
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseRouter(a=>{
                a.MapGet("docker101", handler: async b=>{
                    b.Response.Redirect("https://github.com/TerribleDev/intro-to-docker/", true);
                });
                a.MapGet("dockerfortran", handler: async b=>{
                    b.Response.Redirect("https://github.com/TerribleDev/Fortran-docker-mvc", true);
                });
                a.MapGet("blog", handler: async b=>{
                    b.Response.Redirect("https://blog.terribledev.io/", true);
                });
                a.MapGet("github", handler: async b=>{
                    b.Response.Redirect("https://github.com/TerribleDev/", true);
                });
            });
        }
    }
}
