using Microsoft.Data.SqlClient;
using RUTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RUTS.Repositories
{
    public abstract class RepositoryBase
    {
        private RepositoryBase()
        {

        }

        private static string _databaseName = "RUTS";

        private static string _server = "172.16.1.94"; //(localdb)\\mssqllocaldb
        private static string _username = "sa";
        private static string _pass = "P@ssw0rd";


        private static SqlConnection _connection;

        private static string _connectionString;

        public static string GetConnectionString()
        {
            string connectionString = "Server=" + _server + ";database=" + _databaseName + ";User Id="+_username + ";Password=" +_pass+ ";Trust Server Certificate=True;Encrypt=false;";
            return connectionString;
        }

        public static SqlConnection GetConnection()
        {

            if (_connection == null)
            {
                _connectionString = "Server=" + _server + ";database=" + _databaseName + ";Trusted_Connection=True;";
                _connection = new SqlConnection(_connectionString);
                _connection.Open();
            }

            return _connection;
        }
    }
}
