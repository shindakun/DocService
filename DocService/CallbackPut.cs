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
        public static ActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "callback/{id}")] HttpRequest req, string id,
            [CosmosDB(databaseName: "doc", collectionName: "doc", Id = "{id}", PartitionKey = "shard", ConnectionStringSetting = "connection")] Models.StatusObj statusObj,
            [CosmosDB(databaseName: "doc", collectionName: "doc", ConnectionStringSetting = "connection")]out dynamic item,
            ILogger log)
        {
            log.LogInformation("DocService function PUT /callback");
            string requestBody = "";
            try
            {
                requestBody = new StreamReader(req.Body).ReadToEnd();
            }
            catch
            {
                log.LogWarning("TODO: Better exception handling");
            }

            dynamic data = JsonConvert.DeserializeObject(requestBody);
            statusObj.Status = data.status;
            statusObj.Detail = data.detail;

            item = statusObj;

            return string.IsNullOrEmpty(requestBody)
                ? new BadRequestObjectResult("error")
                : (ActionResult)new NoContentResult();
        }
    }
}
