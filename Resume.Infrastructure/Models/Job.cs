namespace Resume.Infrastructure.Models;

public class Job
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Position { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}