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
            [CosmosDB(databaseName: "doc", collectionName: "doc", Id = "{id}", PartitionKey = "shard", ConnectionStringSetting = "connection")] Models.StatusObj statusObj,
            ILogger log)
        {
            log.LogInformation("DocService function GET /status");

            string json = JsonConvert.SerializeObject(statusObj);
            log.LogInformation(json);

            return json == "null" 
                ? new BadRequestObjectResult("error or id not found")
                : (ActionResult)new OkObjectResult($"{json}");
        }
    }
}