using System.Collections.Generic;

namespace aka.terribledev.io
{
    public static class Routes
    {
        public static Dictionary<string, string> RoutesDictionary = new Dictionary<string, string>(){
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

        public static string CalculateHostRedirect(string host)
        {
            if(host.StartsWith("github", System.StringComparison.OrdinalIgnoreCase))
            {
                return "https://github.com/terribledev";
            }
            if(host.StartsWith("bitbucket", System.StringComparison.OrdinalIgnoreCase))
            {
                return "https://bitbucket.org/TerribleDev/";
            }
            if(host.StartsWith("api", System.StringComparison.OrdinalIgnoreCase))
            {
                return "http://api.terribledev.io/";
            }
            if(host.StartsWith("resume", System.StringComparison.OrdinalIgnoreCase))
            {
                return "http://resume.terribledev.io/";
            }
            if(host.StartsWith("soupinsummer.co", System.StringComparison.OrdinalIgnoreCase))
            {
                return "https://about.terribledev.io";
            }
            if(host.Equals("tommyparnell.com", System.StringComparison.OrdinalIgnoreCase) || host.Equals("www.tommyparnell.com", System.StringComparison.OrdinalIgnoreCase))
            {
                return "https://about.terribledev.io";
            }
            if(host.Equals("tparnell.io", System.StringComparison.OrdinalIgnoreCase) || host.Equals("www.tparnell.io", System.StringComparison.OrdinalIgnoreCase))
            {
                return "https://about.terribledev.io";
            }
            if(host.Equals("terribledev.io", System.StringComparison.OrdinalIgnoreCase))
            {
                return "https://about.terribledev.io";
            }
            return string.Empty;
        }
    }
}
