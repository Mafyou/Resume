using Microsoft.EntityFrameworkCore;

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
            if (_context.Persons.FirstOrDefault(x => x.Name == person.Name) is not null)
            {
                return false;
            }
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
            var realPerson = _context.Persons.Where(x => x.Name == person.Name);
            if (realPerson is null || realPerson.Count() is not 1)
            {
                return false;
            }
            var personDb = realPerson.First();
            if (job.Company is null)
            {
                return false;
            }
            if (_context.Companies.FirstOrDefault(x => x.Name == job.Company.Name) is null)
            {
                _context.Companies.Add(job.Company);
                _context.SaveChanges();
            }
            if (_context.Jobs.FirstOrDefault(x => x.Company.Id == job.CompanyId) is null)
            {
                _context.Jobs.Add(job);
                _context.SaveChanges();
                personDb.Jobs = [job];
            }
            else
            {
                personDb.Jobs.Add(job);
            }
            _context.Persons.Update(personDb);
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error", ex.Message);
        }
        return false;
    }

    public async Task<IReadOnlyList<Job>> GetPersonJobBetweenDates(Person person, DateTime start, DateTime end)
    {
        return (await _context.Persons
            .Include(x => x.Jobs)
            .ThenInclude(y => y.Company)
            .Where(p => p.Name == person.Name)
            .SelectMany(p => p.Jobs)
            .Where(j => j.StartDate >= start && j.EndDate <= end)
            .ToListAsync())
            .AsReadOnly();

    }

    public async Task<IReadOnlyList<Person>> GetPersonsByCompany(Company company)
    {
        return (await _context.Persons
            .Include(y => y.Jobs)
            .ThenInclude(z => z.Company)
            .Where(x => x.Jobs.Any(j => j.Company.Name == company.Name))
            .OrderBy(p => p.Name)
            .ToListAsync())
            .AsReadOnly();
    }

    public async Task<IReadOnlyList<Person>> GetPersonsWithJobs()
    {
        return (await _context.Persons
            .Include(x => x.Jobs)
            .ThenInclude(y => y.Company)
            .OrderBy(p => p.Name)
            .ToListAsync())
            .AsReadOnly();
    }
}