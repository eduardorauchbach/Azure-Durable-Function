using Azure.Durable.Functions;
using Azure.Durable.Functions.Configurations;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Rauchtech.Logging;

[assembly: FunctionsStartup(typeof(Startup))]

namespace Azure.Durable.Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var executionContextOptions = builder.Services.BuildServiceProvider().GetService<IOptions<ExecutionContextOptions>>().Value;
            var appDirectory = executionContextOptions.AppDirectory;

            var config = new ConfigurationBuilder()
                    .SetBasePath(appDirectory)
                    .AddEnvironmentVariables()
                    .Build();

            builder.Services.Configure<AppSettings>(config);
            builder.Services.AddOptions();

            ConfigureServices(builder.Services);
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.RegisterCustomLogs();
        }
    }
}