namespace FarmDash.Models;

public class Farm
{
    public int Id { get; set; }
    public int FarmID { get; set; }
    public string Name { get; set; } = "";
    public string Location { get; set; } = "";
    public string Description { get; set; } = "";
    public string Animal { get; set; } = "";
    // Death and Sick rate is per 1000 animals on the farm.
    public double DeathRate { get; set; }
    public double SickRate { get; set; }
    public string State { get; set; } = "";
    public DateTime Created { get; set; } =  DateTime.UtcNow;
}