namespace Resume.Application.Queries;

public record GetPeopleQuery() : IRequest<IReadOnlyList<PersonDTO>>;
