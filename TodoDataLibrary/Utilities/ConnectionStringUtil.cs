using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoDataLibrary.Utilities
{
    internal static class ConnectionStringUtil
    {
        public static string GetConnectionString()
        {
            var localConnectionString = "Server = 127.0.0.1; Port = 5432; Database = TodoAppDB; User Id = postgres; Password = password;";
            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            return string.IsNullOrEmpty(databaseUrl) ? localConnectionString : GetDatabase(databaseUrl);
        }
        public static string GetDatabase(string databaseUrl)
        {
            //IDictionary evVars = Environment.GetEnvironmentVariables();
            //string output = Environment.GetEnvironmentVariable("PgSqlDb");

            var database = new Uri(databaseUrl);
            var userInfo = database.UserInfo.Split(':');

            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = database.Host,
                Port = database.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = database.LocalPath.Trim('/'),
                TrustServerCertificate = true,
            };
            return builder.ToString();

        }
    }
}
