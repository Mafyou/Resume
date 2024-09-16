namespace Resume.Application.Queries;

public record GetPersonsByCompanyQuery(CompanyDTO Company) : IRequest<IReadOnlyList<Person>>;