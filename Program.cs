
using System;
using ComputerStore.Models;

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




