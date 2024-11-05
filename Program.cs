
using System;
using System.Data;
using ComputerStore.Models;
using Dapper;
using Microsoft.Data.SqlClient;

string connectionString = "Server=localhost;Database=ComputerStore;Trusted_Connection=True;TrustServerCertificate=True;";

//Test if connection string is valid
IDbConnection connection = new SqlConnection(connectionString);

string sqlCommand = "SELECT GETDATE()";

DateTime dateTimeNow = connection.QuerySingle<DateTime>(sqlCommand);

Console.WriteLine($"Connection is valid. Current date and time is {dateTimeNow}");

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

Console.WriteLine($"Motherboard: {computer.Motherboard}");
Console.WriteLine($"Computer {computer}");




