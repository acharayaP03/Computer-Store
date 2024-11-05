
using System;
using System.Data;
using ComputerStore.Data;
using ComputerStore.Models;
using ComputerStore.Utils;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

class Program 
{
    private static readonly ILogger<Program> _logger = AppLogger.GetLogger<Program>();

    static void Main()
    {

        try{
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
                .Build();

            DataContextDapper dataConextDapper = new(configuration);
            DataContextEntity dataContextEntity = new(configuration);

            _logger.LogInformation("Starting Application, trying to connect to the database");


            Computer computer = new()
            {
                Motherboard = "ZG4506",
                CPUCores = 8,
                HasWifi = true,
                HasLTE = false,
                VideoCard = "RTX 3090",
                ReleaseDate = new DateTime(2024, 1, 1),
                Price = 2000.99m
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



