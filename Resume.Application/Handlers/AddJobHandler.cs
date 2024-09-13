

namespace Resume.Application.Handlers;

public class AddJobHandler : IRequestHandler<AddJobCommand, bool>
{
    private readonly IRepository _repository;
    public AddJobHandler(IRepository repository)
    {
        _repository = repository;
    }
    public async Task<bool> Handle(AddJobCommand request, CancellationToken cancellationToken)
    {
        var modelJob = new Job
        {
            Name = request.job.Name,
            Position = request.job.Position,
            StartDate = request.job.StartDate,
            EndDate = request.job.EndDate
        };
        var modelPerson = new Person
        {
            Name = request.person.Name,
            BirthDate = request.person.BirthDay
        };
        return _repository.AddJob(modelJob, modelPerson);
    }
}