using System.Threading.Tasks;

namespace Azure.Durable.Functions.Services
{
    internal interface ISampleService
    {
        Task<int> SampleMethod1(int seed);
        Task<int> SampleMethod2(int seed);
        Task<int> SampleMethod3(int seed);
    }
}