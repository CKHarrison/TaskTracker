using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoDataLibrary.Utilities;

namespace TodoDataLibrary.Database
{
    public class PgSqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _config;

        public PgSqlDataAccess(IConfiguration config)
        {
            _config = config;
        }

        public List<T> LoadData<T, U>(string sqlStatement,
                                     U parameters,
                                     string connectionStringName,
                                     bool isStoredProcedure = false)
        {
            string connectionString = ConnectionStringUtil.GetConnectionString();
            CommandType command = CommandType.Text;
            if (isStoredProcedure == true)
            {
                command = CommandType.StoredProcedure;
            }
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                return connection.Query<T>(sqlStatement, parameters, commandType: command).ToList();
            }
        }

        public void SaveData<T>(string sqlStatement, T parameters, string connectionStringName, bool isStoredProcedure = false)
        {
            string connectionString = ConnectionStringUtil.GetConnectionString();
            CommandType command = CommandType.Text;
            if (isStoredProcedure == true)
            {
                command = CommandType.StoredProcedure;
            }
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Execute(sqlStatement, parameters, commandType: command);
            }
        }
    }
}
