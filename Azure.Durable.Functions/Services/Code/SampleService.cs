using Rauchtech.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azure.Durable.Functions.Services.Code
{
    internal class SampleService : ISampleService
    {
        ICustomLog<SampleService> _logger;

        public SampleService(ICustomLog<SampleService> logger)
        {
            _logger = logger;
        }

        [LogAspect]
        public async Task<int> SampleMethod1(int seed)
        {
            var delay = new Random(seed).Next(1000, 10000);
            await Task.Delay(delay);

            return delay;
        }

        [LogAspect]
        public async Task<int> SampleMethod2(int seed)
        {
            var delay = new Random(seed).Next(1000, 10000);
            await Task.Delay(delay);

            return delay;
        }

        [LogAspect]
        public async Task<int> SampleMethod3(int seed)
        {
            var delay = new Random(seed).Next(1000, 10000);
            await Task.Delay(delay);

            return delay;
        }
    }
}
