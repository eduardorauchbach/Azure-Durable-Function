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
    internal static class SampleOrchestrations
    {
        private const string Sample1Step = "Sample 1";
        private const string Sample2Step = "Sample 2";
        private const string Sample3Step = "Sample 3";

        [FunctionName("Sample_Orchestrator")]
        public static async Task<string> RunOrchestrator(
        [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var data = context.GetInput<Dictionary<string, object>>();
            var status = new OrchestrationInformation(Sample1Step, Sample2Step, Sample3Step);

            int seed1 = await context.CallActivityAsync<int>("durable-seed", data);
            data["Seed1"] = seed1;

            status.SetStatus(context, Sample1Step, OrchestrationStatus.Running);
            status.SetMessage(context, Sample1Step, $"Seed: {seed1}");
            var delay1 = await context.CallActivityAsync<int>("durable-sample1", data);
            status.SetMessage(context, Sample1Step, $"Seed: {seed1} | Delay time: {delay1} ms");
            status.SetStatus(context, Sample1Step, OrchestrationStatus.Finished);

            int seed2 = await context.CallActivityAsync<int>("durable-seed", data);
            data["Seed2"] = seed2;

            status.SetStatus(context, Sample2Step, OrchestrationStatus.Running);
            status.SetMessage(context, Sample2Step, $"Seed: {seed2}");
            var delay2 = await context.CallActivityAsync<int>("durable-sample2", data);
            status.SetMessage(context, Sample2Step, $"Seed: {seed2} | Delay time: {delay2} ms");
            status.SetStatus(context, Sample2Step, OrchestrationStatus.Finished);

            var seed3 = await context.CallActivityAsync<int>("durable-seed", data);
            data["Seed3"] = seed3;

            status.SetStatus(context, Sample3Step, OrchestrationStatus.Running);
            status.SetMessage(context, Sample3Step, $"Seed: {seed3}");
            var delay3 = await context.CallActivityAsync<int>("durable-sample3", data);
            status.SetMessage(context, Sample3Step, $"Seed: {seed3} | Delay time: {delay3} ms");
            status.SetStatus(context, Sample3Step, OrchestrationStatus.Finished);

            return $"Total Delay time: {delay1 + delay2 + delay3} ms";
        }
    }
}
