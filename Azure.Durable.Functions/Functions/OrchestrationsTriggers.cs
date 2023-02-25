using Azure.Durable.Functions.Configurations;
using Azure.Durable.Functions.Functions.Auxiliar;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Rauchtech.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Azure.Durable.Functions.Functions
{
    internal class OrchestrationsTriggers
    {
        private readonly ICustomLog<OrchestrationsTriggers> _logger;
        private readonly string _durableFunctionUrl;

        public OrchestrationsTriggers(ICustomLog<OrchestrationsTriggers> logger, IOptions<AppSettings> options)
        {
            _logger = logger;
            _durableFunctionUrl = options.Value.StatusURL;
        }

        [FunctionName("Sample_Orchestration_HttpStart")]
        public async Task<IActionResult> DisassembleStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "process-sample-stage")] HttpRequest req,
            [DurableClient] IDurableOrchestrationClient starter)
        {
            var operationID = req.GetOperationID();
            var seed = !string.IsNullOrEmpty(req.Query["Seed"]) ? req.Query["Seed"].ToString() : null;

            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                { "OperationID", operationID},
                { "Seed", seed},
            };

            string instanceId = await starter.StartNewAsync("Sample_Orchestrator", data);

            _logger.AddKey("OperationID", operationID);
            _logger.LogCustom(LogLevel.Information, message: $"Started orchestration with ID = '{instanceId}'.");

            string host = req.Headers.ContainsKey("X-ORIGINAL-HOST") ? req.Headers["X-ORIGINAL-HOST"].ToString() : req.Host.ToString();
            string requestProtocol = req.Headers.ContainsKey("X-FORWARDED-PROTO") ? req.Headers["X-FORWARDED-PROTO"].ToString() : req.Scheme;
            string checkStatusLocation = string.Format(_durableFunctionUrl, requestProtocol, host, instanceId, req.Query.ContainsKey("subscription-key") ? req.Query["subscription-key"] : req.Headers["Ocp-Apim-Subscription-Key"]);

            string message = $"Your submission has been received. To get the status, go to: {checkStatusLocation}";

            ActionResult response = new AcceptedResult(checkStatusLocation, message);

            req.HttpContext.Response.Headers.Add("retry-after", "30");

            return response;
        }
    }
}
