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
    public static class StatusGet
    {
        [FunctionName("StatusGet")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "status/{id}")] HttpRequest req, string id,
            ILogger log)
        {
            log.LogInformation("DocService function GET /status");

            var obj = new Models.StatusObj
            {
                Status = "status",
                Detail = "detail",
                Body = "body",
            };

            string json = JsonConvert.SerializeObject(obj);

            string responseMessage = string.IsNullOrEmpty(id)
                ? "id not found or missing"
                : $"{id} {json}";

            return new OkObjectResult(responseMessage);
        }
    }
}
