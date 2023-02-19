using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azure.Durable.Functions.Configurations
{
    /// <summary>
    /// Model to IOptions to load from the config
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Aplication name to be displayed at the logs
        /// </summary>
        public string ApplicationName { get; set; }
    }
}
