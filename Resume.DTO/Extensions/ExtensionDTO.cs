using Resume.Data.DTO;
using Resume.Data.Models;

namespace Resume.DTO.Extensions;

public static class ExtensionDTO
{
    public static PersonDTO ToDTO(this Person person)
    {
        if (person is null) return null;
        return new PersonDTO(person.Name, person.BirthDate, person.Jobs.ToDTOs());
    }
    public static IReadOnlyList<PersonDTO> ToDTOs(this IReadOnlyList<Person> persons)
    {
        return persons.Select(p => p.ToDTO()).ToList().AsReadOnly();
    }
    public static JobDTO ToDTO(this Job job)
    {
        if (job is null) return null;
        return new JobDTO(job.Position, job.StartDate, job.EndDate, job.Company.ToDTO());
    }
    public static IReadOnlyList<JobDTO> ToDTOs(this IReadOnlyList<Job> jobs)
    {
        return jobs.Select(p => p.ToDTO()).ToList().AsReadOnly();
    }
    public static CompanyDTO ToDTO(this Company company)
    {
        return new CompanyDTO(company.Name);
    }
}