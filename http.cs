using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace aka.terribledev.io
{

    public static class http
    {
        public static Dictionary<string, string> RoutesDictionary = new Dictionary<string, string>()
        {
            ["docker101"] = "https://blog.terribledev.io/Getting-started-with-docker-containers/",
            ["dockerfortran"] = "https://github.com/TerribleDev/Fortran-docker-mvc",
            ["blog"] = "https://blog.terribledev.io",
            ["github"] = "https://github.com/terribledev",
            ["git-cheatsheet"] = "https://github.com/TerribleDev/Git-CheatSheet",
            ["feedback"] = "https://docs.google.com/forms/d/e/1FAIpQLSetozvuoSVTOb_lTH0CvQhYMzsXGggGQQdEEq041uQpJlOxVg/viewform?usp=sf_link",
            ["wineodistro"] = "https://docs.google.com/forms/d/e/1FAIpQLSf7caM9mjS9H1graJ9BnT1sRkUV2cyGF1dJVUCnMV0f2NXu4A/viewform?usp=sf_link",
            ["react-samples"] = "https://github.com/terribledev/react-samples",
            ["react-sample"] = "https://github.com/terribledev/react-samples",
            ["things-to-know"] = "https://github.com/TerribleDev/Things-to-know",
            ["webpack/timereport"] = "https://gist.github.com/TerribleDev/8677821c3e174659250df1f6bba9d7c3",
            ["web/perflist"] = "https://github.com/TerribleDev/WebPerformanceChecklist",
            ["web/lazyimages"] = "https://gist.github.com/TerribleDev/4c0e45faf4dca080f5a6eac702995e0d",
            ["web/jsmobileconf19"] = "https://1drv.ms/p/s!ApmZsxwTwlbwgYgp4R33QLD5fpeVxA",
            ["redbordertool"] = "https://gist.github.com/TerribleDev/51049146e00b36b0d8643f5e09d21ea8",
            ["fun"] = "https://www.youtube.com/watch?v=dQw4w9WgXcQ",
            ["lunch"] = "https://tparnell-lunch.herokuapp.com/",
        };
        [FunctionName("http")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "{*path}")] HttpRequest req,
            ILogger log,
            string path)
        {
            log.LogInformation($"redirect triggered: {DateTime.Now}");
                var name = path?.ToString().Trim('/') ?? string.Empty;
                if (string.Equals(name, "livecheck"))
                {
                    req.HttpContext.Response.GetTypedHeaders().CacheControl = new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
                    {
                        Private = true,
                        NoCache = true,
                        NoStore = true,
                        MustRevalidate = true
                    };
                    return new OkObjectResult("livecheck");

                }
                req.HttpContext.Response.GetTypedHeaders().CacheControl = new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
                {
                    Public = true,
                    MaxAge = TimeSpan.FromDays(1)
                };
                if (!RoutesDictionary.TryGetValue(name.ToLowerInvariant(), out string result))
                {
                    var stringBuilder = new StringBuilder();
                    stringBuilder.AppendLine("Hi! ^_^");
                    stringBuilder.AppendLine("Paths:");
                    stringBuilder.AppendJoin('\n', RoutesDictionary.Keys);
                    return new NotFoundObjectResult(stringBuilder.ToString());
                }
                else
                {
                    return new RedirectResult(result);
                }
        }
    }
}
