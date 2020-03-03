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
        public static ActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "request")] HttpRequest req,
            [CosmosDB(databaseName: "doc", collectionName: "doc", ConnectionStringSetting = "connection")]out dynamic item,
            ILogger log)
        {
            log.LogInformation("DocService function POST /request");
            string requestBody = "";
            try
            {
                requestBody = new StreamReader(req.Body).ReadToEnd();
            }
            catch
            {
                log.LogWarning("TODO: Better exception handling");
            }
            finally
            {
                dynamic data = JsonConvert.DeserializeObject(requestBody);
                var obj = new Models.StatusObj { };
                obj.Id = Guid.NewGuid().ToString();
                obj.PartId = "shard";
                obj.Body = requestBody;

                item = obj;
            }

            return string.IsNullOrEmpty(requestBody)
                ? new BadRequestObjectResult("error")
                : (ActionResult)new OkObjectResult($"{item.Id}");
        }
    }
}
