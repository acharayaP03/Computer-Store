
using System;
using System.Data;
using ComputerStore.Models;
using ComputerStore.Utils;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

class Program 
{
    private static readonly ILogger<Program> _logger = AppLogger.GetLogger<Program>();

    static void Main()
    {
        _logger.LogInformation("Application started");

        string connectionString = "Server=localhost;Database=ComputerStore;Trusted_Connection=True;TrustServerCertificate=True;";

        try{
            
            _logger.LogInformation("Starting Application, trying to connect to the database");

            using IDbConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string sqlCommand = "SELECT GETDATE()";

            DateTime dateTimeNow = connection.QuerySingle<DateTime>(sqlCommand);

            Computer computer = new Computer
            {
                Motherboard = "ASUS",
                CPUCores = 8,
                HasWifi = true,
                HasLTE = false,
                VideoCard = "NVIDIA",
                ReleaseDate = new DateTime(2020, 1, 1),
                Price = 1500.99m
            };

            _logger.LogInformation($"Connection is valid. Current date and time is {dateTimeNow}");

            Console.WriteLine($"Motherboard: {computer.Motherboard}");
            Console.WriteLine($"Computer {computer}");
        }
        catch(SqlException ex)
        {
            _logger.LogError(ex, "An error occurred while connecting to the database");
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "An error occurred");
        }

    }
}



