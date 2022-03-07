using Npgsql;

namespace Todo.Web.Utilities
{
    public static class DataUtility
    {
        //public static (string host, int port, string user, string password) GetAllSmptInfo()
        //{
        //    string hostString = GetSmtpHost();
        //    string passwordString = GetSmtpPassword();
        //    int port = GetSmtpPort();
        //    string userString = GetSmtpUser();
        //    return (hostString, port, userString, passwordString);
        //}
        public static string GetSmtpHost()
        {
            string output = Environment.GetEnvironmentVariable("SMTP_HOST");
            return output;
        }
        public static string GetSmtpPassword()
        {
            string output = Environment.GetEnvironmentVariable("SMTP_PASSWORD");
            return output;
        }
        public static int GetSmtpPort()
        {
            string outputString = Environment.GetEnvironmentVariable("SMTP_PORT");
            int port = int.Parse(outputString);
            return port;
        }

        public static string GetSmtpUser()
        {
            string output = Environment.GetEnvironmentVariable("SMTP_USER");
            return output;
        }

        public static string GetGmailHost()
        {
            string output = Environment.GetEnvironmentVariable("GMAIL_HOST");
            return output;
        }

        public static string GetGmailPassword()
        {
            string output = Environment.GetEnvironmentVariable("GMAIL_PASSWORD");
            return output;
        }
        public static string GetConnectionString(IConfiguration _config)
        {
            var connectionString = _config.GetConnectionString("PgSqlDb");
            var databaseUrl = Environment.GetEnvironmentVariable("PgSqlDb");
            return string.IsNullOrEmpty(databaseUrl) ? connectionString : GetDatabase(databaseUrl);
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
                SslMode = SslMode.Prefer,
                TrustServerCertificate = true
            };
            return builder.ToString();

        }
    }
}
