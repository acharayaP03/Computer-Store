
using System;
using System.Data;
using ComputerStore.Data;
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

        try{
            DataContextDapper dataConextDapper = new();
            _logger.LogInformation("Starting Application, trying to connect to the database");


            Computer computer = new()
            {
                Motherboard = "ASUS",
                CPUCores = 8,
                HasWifi = true,
                HasLTE = false,
                VideoCard = "NVIDIA",
                ReleaseDate = new DateTime(2020, 1, 1),
                Price = 1500.99m
            };

            string sqlCommand = "SELECT GETDATE()";
            string insertCommand = "INSERT INTO ComputerStoreAppSchema.Computer (Motherboard, CPUCores, HasWifi, HasLTE, VideoCard, ReleaseDate, Price) " +
                               "VALUES (@Motherboard, @CPUCores, @HasWifi, @HasLTE, @VideoCard, @ReleaseDate, @Price); " +
                               "SELECT CAST(SCOPE_IDENTITY() as int)";

            DateTime dateTimeNow = dataConextDapper.LoadDataSingle<DateTime>(sqlCommand);
            _logger.LogInformation($"Connection is valid. Current date and time is {dateTimeNow}");
            
            int result = dataConextDapper.ExecuteSqlWithRowCount(insertCommand, computer);

            Console.WriteLine(insertCommand);
            _logger.LogInformation($"Computer inserted with ID {result}");



            string selectAllComputerCommand = @"
                SELECT
                    Computer.Motherboard,
                    Computer.CPUCores,
                    Computer.HasWifi,
                    Computer.HasLTE,
                    Computer.VideoCard,
                    Computer.ReleaseDate,
                    Computer.Price
                FROM ComputerStoreAppSchema.Computer";

            IEnumerable<Computer> computers = dataConextDapper.LoadData<Computer>(selectAllComputerCommand);
            
            foreach (Computer c in computers)
            {
                Console.WriteLine($"Motherboard: {c.Motherboard}");
                Console.WriteLine($"CPUCores: {c.CPUCores}");
                Console.WriteLine($"HasWifi: {c.HasWifi}");
                Console.WriteLine($"HasLTE: {c.HasLTE}");
                Console.WriteLine($"VideoCard: {c.VideoCard}");
                Console.WriteLine($"ReleaseDate: {c.ReleaseDate}");
                Console.WriteLine($"Price: {c.Price}");
                Console.WriteLine();
            }
            
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



