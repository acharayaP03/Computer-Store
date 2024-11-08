namespace ComputerStore.Models;
public class Computer
{
    public int ComputerId { get; set; }
    public string Motherboard { get; set; } = "";
    public int CPUCores { get; set; } = 0;
    public bool HasWifi { get; set; }
    public bool HasLTE { get; set; }
    public string VideoCard { get; set; } = "";
    public DateTime? ReleaseDate { get; set; }
    public decimal Price { get; set; }


    public override string ToString()
    {
        return $"Motherboard: {Motherboard}, CPU Cores: {CPUCores}, Has Wifi: {HasWifi}, Has LTE: {HasLTE}, Video Card: {VideoCard}, Release Date: {ReleaseDate}, Price: {Price}";
    }
}




