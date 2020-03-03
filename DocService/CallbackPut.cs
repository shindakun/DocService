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
    public static class CallbackPut
    {
        [FunctionName("CallbackPut")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "callback/{id}")] HttpRequest req, string id,
            ILogger log)
        {
            log.LogInformation("DocService function PUT /callback");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            string status = data?.status;
            string detail = data?.detail;

            // should check for issues with body object
            string responseMessage = string.IsNullOrEmpty(id)
                ? "id not found or missing"
                : $"{status}, {detail}, {id}";

            return new OkObjectResult(responseMessage);
        }
    }
}
