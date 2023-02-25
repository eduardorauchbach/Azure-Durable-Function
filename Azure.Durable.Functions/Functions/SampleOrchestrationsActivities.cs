using Azure.Durable.Functions.Configurations;
using Azure.Durable.Functions.Functions.Auxiliar;
using Azure.Durable.Functions.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Rauchtech.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Azure.Durable.Functions.Functions
{
    internal class SampleOrchestrationsActivities
    {
        private readonly ICustomLog<SampleOrchestrationsActivities> _logger;
        private readonly ISampleService _sampleService;

        public SampleOrchestrationsActivities(ICustomLog<SampleOrchestrationsActivities> logger, IOptions<AppSettings> options, ISampleService sampleService)
        {
            _logger = logger;
            _sampleService = sampleService;
        }

        [FunctionName("durable-seed")]
        public int DurableSeed([ActivityTrigger] IDurableActivityContext context)
        {
            var data = context.GetInput<Dictionary<string, string>>();
            var seed = data["Seed"] != null ? Convert.ToInt32(data["Seed"]) : (int?)null;

            return seed ?? new Random().Next(1000, 10000);
        }

        [FunctionName("durable-sample1")]
        public async Task<int?> DurableSample1([ActivityTrigger] IDurableActivityContext context)
        {
            var data = context.GetInput<Dictionary<string, string>>();
            var operationID = data["OperationID"];
            int seed = Convert.ToInt32(data["Seed1"]);

            int? delayTime;

            try
            {
                _logger.AddKey("OperationID", operationID);
                _logger.LogCustom(LogLevel.Information, message: CustomLogMessages.Begin);

                delayTime = await _sampleService.SampleMethod1(seed);

                _logger.LogCustom(LogLevel.Information, message: CustomLogMessages.Finish);
            }
            catch (Exception ex)
            {
                _logger.LogCustom(LogLevel.Error, LogType.Dashboard, exception: ex);
                throw;
            }

            return delayTime;
        }

        [FunctionName("durable-sample2")]
        public async Task<int?> DurableSample2([ActivityTrigger] IDurableActivityContext context)
        {
            var data = context.GetInput<Dictionary<string, string>>();
            var operationID = data["OperationID"];
            int seed = Convert.ToInt32(data["Seed2"]);

            int? delayTime;

            try
            {
                _logger.AddKey("OperationID", operationID);
                _logger.LogCustom(LogLevel.Information, message: CustomLogMessages.Begin);

                delayTime = await _sampleService.SampleMethod2(seed);

                _logger.LogCustom(LogLevel.Information, message: CustomLogMessages.Finish);
            }
            catch (Exception ex)
            {
                _logger.LogCustom(LogLevel.Error, LogType.Dashboard, exception: ex);
                throw;
            }

            return delayTime;
        }

        [FunctionName("durable-sample3")]
        public async Task<int?> DurableSample3([ActivityTrigger] IDurableActivityContext context)
        {
            var data = context.GetInput<Dictionary<string, string>>();
            var operationID = data["OperationID"];
            int seed = Convert.ToInt32(data["Seed3"]);

            int? delayTime;

            try
            {
                _logger.AddKey("OperationID", operationID);
                _logger.LogCustom(LogLevel.Information, message: CustomLogMessages.Begin);

                delayTime = await _sampleService.SampleMethod3(seed);

                _logger.LogCustom(LogLevel.Information, message: CustomLogMessages.Finish);
            }
            catch (Exception ex)
            {
                _logger.LogCustom(LogLevel.Error, LogType.Dashboard, exception: ex);
                throw;
            }

            return delayTime;
        }
    }
}
