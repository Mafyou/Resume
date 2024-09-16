namespace Resume.Application.Queries;

public record GetPersonJobBetweenDatesQuery(string PersonName, DateTime StartDate, DateTime EndDate) : IRequest<IReadOnlyList<JobDTO>>;