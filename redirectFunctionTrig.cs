
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace TerribleDev
{
    public static class redirectFunction
    {
        public static Dictionary<string, string> RoutesDictionary = new Dictionary<string, string>()
        {
            ["docker101"] = "https://blog.terribledev.io/Getting-started-with-docker-containers/",
            ["dockerfortran"] = "https://github.com/TerribleDev/Fortran-docker-mvc",
            ["blog"] = "https://blog.terribledev.io",
            ["github"] = "https://github.com/terribledev",
            ["git-cheatsheet"] = "https://github.com/TerribleDev/Git-CheatSheet",
            ["janus-intro"] = "https://janus-vistaprint.github.io/intro-deck/",
            ["jenkins-groovy"] = "https://jenkinsci.github.io/job-dsl-plugin/",
            ["feedback"] = "https://docs.google.com/forms/d/e/1FAIpQLSetozvuoSVTOb_lTH0CvQhYMzsXGggGQQdEEq041uQpJlOxVg/viewform?usp=sf_link",
            ["wineodistro"] = "https://docs.google.com/forms/d/e/1FAIpQLSf7caM9mjS9H1graJ9BnT1sRkUV2cyGF1dJVUCnMV0f2NXu4A/viewform?usp=sf_link",
            ["react-samples"] = "https://github.com/terribledev/react-samples",
            ["react-sample"] = "https://github.com/terribledev/react-samples"
        };

        public static class redirectFunctionTrig
        {
            [FunctionName("redirectFunctionTrig")]
            public static IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "{path}")]
            HttpRequest req, 
            ILogger log,
            string path)
            {
                var name = path.ToString().TrimEnd('/');
                if(string.Equals(name, "livecheck")) 
                {
                    return new OkObjectResult("Hi! ^_^");
                }
                if (!RoutesDictionary.TryGetValue(name, out string result))
                {
                    return new NotFoundObjectResult("hello");
                }
                else
                {
                    return new RedirectResult(result);
                }
            }
        }
    }
}