using Microsoft.EntityFrameworkCore;
namespace Resume.Infrastructure.Context;

public class ResumeContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<Company> Companies { get; set; }
}