using Resume.Application.Queries;

namespace Resume.Application.Handlers;

public class GetPersonsByCompanyHandler : IRequestHandler<GetPersonsByCompanyQuery, IReadOnlyList<Person>>
{
    private readonly IRepository _repository;
    public GetPersonsByCompanyHandler(IRepository repository)
    {
        _repository = repository;
    }
    public Task<IReadOnlyList<Person>> Handle(GetPersonsByCompanyQuery request, CancellationToken cancellationToken)
    {
        return _repository.GetPersonsByCompany(new Company { Name = request.Company.Name });
    }
}