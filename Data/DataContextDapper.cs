using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PartyOn.Models;
using Microsoft.Extensions.Configuration;

namespace PartyOn.Data{
    public class DataContextDapper{

        private string _connectionString = "Server=localhost;Database=DotnetCourseDatabase;TrustServerCertificate=true;Trusted_Connection=true";
        private readonly IConfiguration? _config;

        public DataContextDapper(IConfiguration config)
        {
            _config = config;
            _connectionString = _config?.GetConnectionString("DefaultConnection") ?? "Default Connection String";
        } 


        public IEnumerable<T> LoadData<T>(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.Query<T>(sql);
        }

        public T LoadDataSingle<T>(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.QuerySingle<T>(sql);
        }


        public bool ExecuteSql(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.Execute(sql) > 0;
        }

         public int ExecuteSqlWithRowCount(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.Execute(sql);
        }
    }
}

