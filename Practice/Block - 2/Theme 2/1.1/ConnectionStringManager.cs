using System;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;

namespace _1._1
{
    class ConnectionStringManager
    {
        public string ConnectionString { get; }

        public ConnectionStringManager(string connectionStringName = "DefaultConnection",
            string environmentVariableName = "DefaultConnection")
        {
            var preConfig = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory());
            var config = preConfig.AddJsonFile("appsettings.json")
                .Build();
            ConnectionString = string.Format(
                config.GetConnectionString(connectionStringName)
            );
        }
    }
}
