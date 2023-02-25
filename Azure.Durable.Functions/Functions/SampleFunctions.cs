using Azure.Durable.Functions.Functions.Auxiliar;
using Azure.Durable.Functions.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Rauchtech.Logging;
using System;
using System.Threading.Tasks;

namespace Azure.Durable.Functions.Functions
{
    internal class SampleFunctions
    {
        private readonly ICustomLog<SampleFunctions> _logger;
        private readonly ISampleService _sampleService;

        public SampleFunctions(ICustomLog<SampleFunctions> logger, ISampleService sampleService)
        {
            _logger = logger;
            _sampleService = sampleService;
        }

        //Todo: OPEN API
        [FunctionName("sample1")]
        public async Task<int?> Sample1(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req)
        {
            var operationID = req.GetOperationID();
            var seed = req.GetSeed();

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

        [FunctionName("sample2")]
        public async Task<int?> Sample2(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req)
        {
            var operationID = req.GetOperationID();
            var seed = req.GetSeed();

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

        [FunctionName("sample3")]
        public async Task<int?> Sample3(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req)
        {
            var operationID = req.GetOperationID();
            var seed = req.GetSeed();

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
