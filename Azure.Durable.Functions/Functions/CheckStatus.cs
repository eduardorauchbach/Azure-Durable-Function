using Azure.Durable.Functions.Configurations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Options;
using Rauchtech.Logging;
using System.Threading.Tasks;

namespace Azure.Durable.Functions.Functions
{
    public class CheckStatus
    {
        private readonly ICustomLog<CheckStatus> _logger;
        private readonly string _durableFunctionUrl;

        public CheckStatus(ICustomLog<CheckStatus> logger, IOptions<AppSettings> options)
        {
            _logger = logger;
            _durableFunctionUrl = options.Value.StatusURL;
        }

        [FunctionName("CheckStatus")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "status/{instanceId}")] HttpRequest req,
            [DurableClient] IDurableOrchestrationClient orchestrationClient,
            string instanceId)
        {
            var status = await orchestrationClient.GetStatusAsync(instanceId);

            if (status != null)

            {
                if (status.RuntimeStatus == OrchestrationRuntimeStatus.Running || status.RuntimeStatus == OrchestrationRuntimeStatus.Pending)
                {
                    string host = req.Headers.ContainsKey("X-ORIGINAL-HOST") ? req.Headers["X-ORIGINAL-HOST"].ToString() : req.Host.ToString();
                    string requestProtocol = req.Headers.ContainsKey("X-FORWARDED-PROTO") ? req.Headers["X-FORWARDED-PROTO"].ToString() : req.Scheme;
                    string checkStatusLocacion = string.Format(_durableFunctionUrl, requestProtocol, host, instanceId, req.Query["subscription-key"]);

                    ActionResult response = new AcceptedResult(checkStatusLocacion, status);

                    req.HttpContext.Response.Headers.Add("retry-after", "10");

                    return response;
                }
                else if (status.RuntimeStatus == OrchestrationRuntimeStatus.Completed)

                {
                    return new OkObjectResult(status);
                }
                else if (status.RuntimeStatus == OrchestrationRuntimeStatus.Failed ||

                status.RuntimeStatus == OrchestrationRuntimeStatus.Terminated ||

                status.RuntimeStatus == OrchestrationRuntimeStatus.Canceled)

                {
                    return new BadRequestObjectResult("Orchestration failed.");
                }
            }

            return new NotFoundObjectResult($"Something went wrong. Please check if your submission Id is correct. Submission '{instanceId}' not found.");
        }
    }

}
