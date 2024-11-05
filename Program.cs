
using System;
using System.Data;
using System.Text.Json;
using ComputerStore.Data;
using ComputerStore.Models;
using ComputerStore.Utils;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

class Program 
{
    private static readonly ILogger<Program> _logger = AppLogger.GetLogger<Program>();

    static void Main()
    {
        _logger.LogInformation("Starting Application, trying to connect to the database");
        try{
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
                .Build();

            DataContextDapper dataConextDapper = new(configuration);

            // convert to camel case. This is not needed if we use Newtonsoft.Json
            // JsonSerializerOptions options = new () { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
 
            string readFromJson = File.ReadAllText("Computers.json");
            // IEnumerable<Computer>? computer = JsonSerializer.Deserialize<IEnumerable<Computer>>(readFromJson, options); // System.Text.Json
            IEnumerable<Computer>? computer = JsonConvert.DeserializeObject<IEnumerable<Computer>>(readFromJson); // Newtonsoft.Json


            /**
            * This is how we can serialize the object to json
            * this is how its done with Newtonsoft 
            */

            JsonSerializerSettings settings = new ()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            string serializedJson = JsonConvert.SerializeObject(computer, settings);
            File.WriteAllText("computers.txt", serializedJson);

            // load all to Data base
            if(computer is not null)
            {
                foreach(var c in computer)
                {
                   string insertCommand = "INSERT INTO ComputerStoreAppSchema.Computer (Motherboard, CPUCores, HasWifi, HasLTE, VideoCard, ReleaseDate, Price) " +
                       "VALUES (@Motherboard, @CPUCores, @HasWifi, @HasLTE, @VideoCard, @ReleaseDate, @Price); " +
                       "SELECT CAST(SCOPE_IDENTITY() as int)";

                    dataConextDapper.ExecuteSqlWithRowCount(insertCommand, c);

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



