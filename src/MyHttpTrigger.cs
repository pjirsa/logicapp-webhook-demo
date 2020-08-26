using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace logicdemo
{
    public static class MyHttpTrigger
    {
        [FunctionName("MyHttpTrigger")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            [ServiceBus("messages", Connection = "ServiceBusConnection")] out ProcessRequest process,
            ILogger log)
        {
            log.LogInformation("Webhook request from Logic Apps received.");

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            string callbackUrl = data?.callbackUrl;

            //This will drop a message in a queue that QueueTrigger will pick up
            process = new ProcessRequest { callbackUrl = callbackUrl, data = "some data" };
            return new AcceptedResult();
        }
        
    }

}
