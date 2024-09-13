namespace Resume.Infrastructure.Models;

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public List<Job> Jobs { get; set; }
}