using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Server
{
    public class PostgreSQLConfiguration
    {
        public PostgreSQLConfiguration(string connectionString, string connectionString2)
        {
            ConnectionString = connectionString;
            ConnectionString2 = connectionString2;
        }
        public string ConnectionString { get; set; }
        public string ConnectionString2 { get; set; }
    }
}
