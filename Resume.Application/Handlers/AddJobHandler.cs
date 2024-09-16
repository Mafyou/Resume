

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
        var modelPerson = new Person
        {
            Name = request.person.Name,
            BirthDate = request.person.BirthDay
        };
        var areAdded = new List<bool>();
        foreach (var job in request.person.Jobs)
        {
            var modelJob = new Job
            {
                Position = job.Position,
                StartDate = job.StartDate,
                EndDate = job.EndDate,
                Company = new Company { Name = job.Company.Name }
            };
            areAdded.Add(_repository.AddJob(modelJob, modelPerson));
        }
        return !areAdded.Any(x => !x);
    }
}