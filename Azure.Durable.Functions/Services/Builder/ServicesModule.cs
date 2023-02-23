using Azure.Durable.Functions.Services.Code;
using Microsoft.Extensions.DependencyInjection;

namespace Azure.Durable.Functions.Services
{
    public static class ServicesModule
    {
        public static IServiceCollection RegisterServicesModule(this IServiceCollection services)
        {
            services.AddScoped<ISampleService, SampleService>();

            return services;
        }
    }
}
