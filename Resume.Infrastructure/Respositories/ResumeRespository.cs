using Microsoft.EntityFrameworkCore;
using Resume.Infrastructure.Models;

namespace Resume.Infrastructure.Respositories;

public class ResumeRespository : IRepository
{
    private readonly ResumeContext _context;

    public ResumeRespository(ResumeContext context)
    {
        _context = context;
    }
    public bool AddPerson(Person person)
    {
        try
        {
            _context.Add(person);
            return _context.SaveChanges() > 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error", ex.Message);
        }
        return false;
    }
    public bool AddJob(Job job, Person person)
    {
        try
        {
            if (_context.Jobs.Any(x => x.Id == job.Id) is false)
            {
                _context.Jobs.Add(job);
                _context.SaveChanges();
            }
            person.Jobs ??= [];
            person.Jobs.Add(job);
            _context.Persons.Update(person);
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error", ex.Message);
        }
        return false;
    }

    public async Task<IReadOnlyCollection<Job>> GetPersonJobBetweenDates(Person person, DateTime start, DateTime end)
    {
        return (await _context.Persons
            .Where(p => p.Id == person.Id)
            .SelectMany(p => p.Jobs)
            .Where(j => j.StartDate >= start && j.EndDate <= end)
            .ToListAsync())
            .AsReadOnly();

    }

    public async Task<IReadOnlyList<Person>> GetPersonsBy(Job job)
    {
        return await _context.Persons.Include(y => y.Jobs)
            .Where(p => p.Jobs.Any(j => j.Id == job.Id))
            .Where(p => p.Jobs.Where(x => x.EndDate == null) != null)
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Person>> GetPersonsWithJobs(Job job)
    {
        return (await _context.Persons
            .Where(x => x.Jobs.Any(y => y.Name == job.Name))
            .ToListAsync())
            .AsReadOnly();
    }
}