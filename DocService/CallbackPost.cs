using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;

namespace DocService
{
    public static class CallbackPost
    {
        [FunctionName("CallbackPost")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "callback/{id}")] HttpRequest req, string id,
            ILogger log)
        {
            log.LogInformation("DocService function POST /callback");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            log.LogInformation($"{id} {requestBody}");

            return string.IsNullOrEmpty(requestBody)
                ? new BadRequestObjectResult("error")
                : (ActionResult)new NoContentResult();
        }
    }
}
