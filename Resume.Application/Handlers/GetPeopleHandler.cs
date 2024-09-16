
using Resume.Application.Queries;
using Resume.DTO.Extensions;

namespace Resume.Application.Handlers;

public class GetPeopleHandler : IRequestHandler<GetPeopleQuery, IReadOnlyList<PersonDTO>>
{
    private readonly IRepository _repository;

    public GetPeopleHandler(IRepository repository)
    {
        _repository = repository;
    }
    public async Task<IReadOnlyList<PersonDTO>> Handle(GetPeopleQuery request, CancellationToken cancellationToken)
    {
        return (await _repository.GetPersonsWithJobs()).ToDTOs();
    }
}