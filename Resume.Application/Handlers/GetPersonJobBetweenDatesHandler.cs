using Resume.Application.Queries;
using Resume.DTO.Extensions;

namespace Resume.Application.Handlers;

public class GetPersonJobBetweenDatesHandler : IRequestHandler<GetPersonJobBetweenDatesQuery, IReadOnlyList<JobDTO>>
{
    private readonly IRepository _repository;
    public GetPersonJobBetweenDatesHandler(IRepository repository)
    {
        _repository = repository;
    }
    public async Task<IReadOnlyList<JobDTO>> Handle(GetPersonJobBetweenDatesQuery request, CancellationToken cancellationToken)
    {
        var jobs = await _repository.GetPersonJobBetweenDates(new Person { Name = request.PersonName }, request.StartDate, request.EndDate);
        return jobs.ToDTOs();
    }
}