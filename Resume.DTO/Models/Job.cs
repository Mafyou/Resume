namespace Resume.Data.Models;

public class Job
{
    public int Id { get; set; }
    public string Position { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int CompanyId { get; set; }
    public Company? Company { get; set; }
}