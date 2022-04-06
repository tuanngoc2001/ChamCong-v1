using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong.Common.Utils
{
    class ConfigCollection
    {
        private readonly IConfigurationRoot configuration;
       

        public static ConfigCollection Instance { get; } = new ConfigCollection();

        protected ConfigCollection()
        {
            configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                             .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                                                             .Build();
        }
        public IConfigurationRoot GetConfiguration()
        {
            return configuration;
        }
       
    }
}
