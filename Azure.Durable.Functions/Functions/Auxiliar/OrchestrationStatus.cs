using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System.Collections.Generic;
using System.Linq;

namespace Azure.Durable.Functions.Functions.Auxiliar
{
    internal class OrchestrationInformation
    {
        public OrchestrationInformation(params string[] steps)
        {
            Steps = steps.Select(x => new OrchestrationStep(x, OrchestrationStatus.None)).ToList();
        }

        public void SetStatus(IDurableOrchestrationContext context, string step, OrchestrationStatus status)
        {
            Steps.First(x => x.Step == step).SetStatus(status);
            context.SetCustomStatus(Steps);
        }
        public void SetMessage(IDurableOrchestrationContext context, string step, string message)
        {
            Steps.First(x => x.Step == step).SetMessage(message);
            context.SetCustomStatus(Steps);
        }

        public List<OrchestrationStep> Steps { get; }
    }
    internal class OrchestrationStep
    {
        public OrchestrationStep(string name, OrchestrationStatus internalStatus)
        {
            Step = name;
            InternalStatus = internalStatus;
        }

        public void SetStatus(OrchestrationStatus status)
        {
            InternalStatus = status;
        }
        public void SetMessage(string message)
        {
            InternalMessage = message;
        }

        public string Step { get; }

        public string Message => InternalMessage;
        public string Status => InternalStatus.ToString();

        private string InternalMessage { get; set; }
        private OrchestrationStatus InternalStatus { get; set; }
    }
    internal enum OrchestrationStatus
    {
        None,
        Running,
        Finished,
    }
}
