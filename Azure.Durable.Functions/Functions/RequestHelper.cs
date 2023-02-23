using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azure.Durable.Functions.Functions
{
    internal static class RequestHelper
    {
        public static string GetOperationID(this HttpRequest req)
        {
            return !string.IsNullOrEmpty(req.Query["OperationID"]) ? Convert.ToString(req.Query["OperationID"]) : Guid.NewGuid().ToString();
        }

        public static int GetSeed(this HttpRequest req)
        {
            return !string.IsNullOrEmpty(req.Query["Seed"]) ? Convert.ToInt32(req.Query["Seed"]) : new Random().Next();
        }
    }
}