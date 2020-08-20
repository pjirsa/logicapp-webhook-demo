using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;

namespace logicdemo
{
    public static class MyQueueTrigger
    {

        public static HttpClient client = new HttpClient();

        [FunctionName("MyQueueTrigger")]
        public static async Task Run([ServiceBusTrigger("messages", Connection = "ServiceBusConnection")]ProcessRequest item, ILogger log)
        {

            log.LogInformation($"C# Queue trigger function processed: {item.data}");
            log.LogInformation("Starting long-running process.");
            Thread.Sleep(TimeSpan.FromSeconds(15));
            ProcessResponse result = new ProcessResponse { data = "some result data" };

            await item.callbackUrl.PostJsonAsync(result);

            // await client.PostAsJsonAsync<ProcessResponse>(item.callbackUrl, result);
            log.LogInformation("Callback sent. Have a nice day!");
        }
    }
}
