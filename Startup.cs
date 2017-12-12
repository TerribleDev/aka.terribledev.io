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
        public static byte[] hello = System.Text.Encoding.UTF8.GetBytes("hello");
        public static byte[] fourOhFor = System.Text.Encoding.UTF8.GetBytes("404");
        public static int helloCount = hello.Count();
        public static byte[] favicon = System.IO.File.ReadAllBytes("favicon.ico");
        public static int faviconByteCount = favicon.Count();
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
			services.AddApplicationInsightsTelemetry();
            services.AddRouting();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.Use(async (context, next) => {
               
                if(context.Request.Host.Host.Equals("aka.terribledev.io", StringComparison.OrdinalIgnoreCase))
                {
                     //if we're targeting the forwarder, don't bother doing host calculations. Routes only
                    await next?.Invoke();
                    return;
                }
                var result = Routes.CalculateHostRedirect(context.Request.Host.Host);
                if(string.IsNullOrWhiteSpace(result))
                {
                    await next?.Invoke();
                    return;
                }
                context.Response.Redirect(result, true);

            });
            app.UseRouter(a=>{
                a.MapGet("favicon.ico", b=>
                {
                    b.Response.StatusCode = 200;
                    return b.Response.Body.WriteAsync(favicon, 0, faviconByteCount);
                });
                foreach(var route in Routes.RoutesDictionary)
                {
                    a.MapGet(route.Key, handler: async b=>{
                        b.Response.Redirect(route.Value, true);
                    });
                }

                    a.MapVerb("HEAD", "", async b =>{ 
                        b.Response.StatusCode = 200;
                    });
                    a.MapVerb("GET", "", async b => { 
                        b.Response.StatusCode = 200;
                        await b.Response.Body.WriteAsync(Startup.hello, 0, helloCount);
                    });
            });
            app.Use(async (context, next)=>{
               context.Response.StatusCode = 404;
               await context.Response.Body.WriteAsync(fourOhFor, 0, fourOhFor.Count());
            });

        }
    }
}
