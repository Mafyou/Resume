namespace Resume.Infrastructure.Abstractions;

public interface IRepository
{
    bool AddPerson(Person person);
    bool AddJob(Job job, Person person);
    Task<IReadOnlyList<Person>> GetPersonsWithJobs();
    Task<IReadOnlyList<Person>> GetPersonsByCompany(Company company);
    Task<IReadOnlyList<Job>> GetPersonJobBetweenDates(Person person, DateTime start, DateTime end);
}