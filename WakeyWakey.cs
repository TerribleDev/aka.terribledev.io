// using System;
// using System.Net.Http;
// using System.Threading.Tasks;
// using Microsoft.Azure.WebJobs;
// using Microsoft.Azure.WebJobs.Host;
// using Microsoft.Extensions.Logging;

// namespace aka.terribledev.io
// {
//     public static class WakeyWakey
//     {
//         static HttpClient client = new HttpClient();
//         [FunctionName("WakeyWakey")]
//         public async static Task Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
//         {
//             log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
//             using (var result = await client.GetAsync("https://aka.terribledev.io", HttpCompletionOption.ResponseContentRead))
//             {
//                 if (!result.IsSuccessStatusCode)
//                 {
//                     log.LogCritical("Error waking up redirect", result);
//                 }
//                 var content = await result.Content.ReadAsStringAsync();
//                 log.LogInformation($"Recieved status code ${result.StatusCode} with body ${content}");
//             }
//         }
//     }
// }
