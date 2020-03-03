using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DocService
{
    public static class RequestPost
    {
        [FunctionName("RequestPost")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "request")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("DocService function POST /request");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            string body = data?.body;

            string responseMessage = string.IsNullOrEmpty(body)
                ? "body object not found"
                : $"{body}";

            return new OkObjectResult(responseMessage);
        }
    }
}
