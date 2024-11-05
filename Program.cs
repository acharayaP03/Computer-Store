
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
            DataContextEntity dataContextEntity = new();

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

            // entity framework
            dataContextEntity.Add(computer);
            dataContextEntity.SaveChanges();

            // Accessing the data using Entity framework

            IEnumerable<Computer>? computersEntity = dataContextEntity.Computer?.ToList<Computer>();
            if(computersEntity is not null)
            {
              
                foreach (Computer c in computersEntity)
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



