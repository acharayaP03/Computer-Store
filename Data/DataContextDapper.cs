
using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ComputerStore.Data;

public class DataContextDapper
{
    private readonly IConfiguration _configuration;

    public DataContextDapper(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IEnumerable<T> LoadData<T>(string sqlCommand)
    {
        IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        return connection.Query<T>(sqlCommand);
    }
    public T LoadDataSingle<T>(string sqlCommand)
    {
        IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        return connection.QuerySingle<T>(sqlCommand);
    }

    public bool ExecuteSql(string sqlCommand, object parameters = null)
    {
        using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        return connection.Execute(sqlCommand, parameters) > 0;
    }
    public int ExecuteSqlWithRowCount(string sqlCommand, object parameters = null)
    {
        using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        return connection.Execute(sqlCommand, parameters);
    }

}