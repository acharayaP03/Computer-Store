
using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace ComputerStore.Data;

public class DataContextDapper
{
    private readonly  string _connectionString = "Server=localhost;Database=ComputerStore;Trusted_Connection=True;TrustServerCertificate=True;";

    public IEnumerable<T> LoadData<T>(string sqlCommand)
    {
        IDbConnection connection = new SqlConnection(_connectionString);
        return connection.Query<T>(sqlCommand);
    }
    public T LoadDataSingle<T>(string sqlCommand)
    {
        IDbConnection connection = new SqlConnection(_connectionString);
        return connection.QuerySingle<T>(sqlCommand);
    }

    public bool ExecuteSql(string sqlCommand, object parameters = null)
    {
        using IDbConnection connection = new SqlConnection(_connectionString);
        return connection.Execute(sqlCommand, parameters) > 0;
    }
    public int ExecuteSqlWithRowCount(string sqlCommand, object parameters = null)
    {
        using IDbConnection connection = new SqlConnection(_connectionString);
        return connection.Execute(sqlCommand, parameters);
    }

}