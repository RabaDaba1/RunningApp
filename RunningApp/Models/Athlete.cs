namespace RunningApp.Models;

public class Athlete
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public ICollection<Result> Results { get; set; }
}