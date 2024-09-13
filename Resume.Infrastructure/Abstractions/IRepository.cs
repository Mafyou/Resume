namespace Resume.Infrastructure.Abstractions;

public interface IRepository
{
    bool AddPerson(Person person);
    bool AddJob(Job job, Person person);
    Task<IReadOnlyList<Person>> GetPersonsWithJobs(Job job);
    Task<IReadOnlyList<Person>> GetPersonsBy(Job job);
    Task<IReadOnlyCollection<Job>> GetPersonJobBetweenDates(Person person, DateTime start, DateTime end);
}