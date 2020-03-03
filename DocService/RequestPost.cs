using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace DocService
{
    public static class RequestPost
    {
        [FunctionName("RequestPost")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "request")] HttpRequest req,
            [CosmosDB(databaseName: "doc", collectionName: "doc", ConnectionStringSetting = "connection")]IAsyncCollector<Models.StatusObj> item,
            ILogger log)
        {
            log.LogInformation("DocService function POST /request");

            var obj = new Models.StatusObj { };
            string requestBody = "";
            // string result = "";
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
                obj.Id = Guid.NewGuid().ToString();
                obj.PartId = "shard";
                obj.Body = requestBody;

                await item.AddAsync(obj);

                // ******************
                // using var c = new HttpClient();
                // var post = await c.PostAsJsonAsync("https://webhook.site/b0972ceb-0ef6-4066-8bd6-ab67f26d500c", $"{{\"body\":{requestBody}, \"callback\":\"/callback/{obj.Id}\"}}");
                // result = await post.Content.ReadAsStringAsync();
                // log.LogInformation(result);
                // ******************
            }

            return string.IsNullOrEmpty(requestBody)
                ? new BadRequestObjectResult("error")
                : (ActionResult)new OkObjectResult($"{obj.Id}");
        }
    }
}
