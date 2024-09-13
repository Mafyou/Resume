namespace Resume.Application.Handlers;

public class AddPersonHandler : IRequestHandler<AddPersonCommand, bool>
{
    private readonly IRepository _repository;

    public AddPersonHandler(IRepository repository)
    {
        _repository = repository;
    }
    public async Task<bool> Handle(AddPersonCommand request, CancellationToken cancellationToken)
    {
        var model = new Person
        {
            Name = request.person.Name,
            BirthDate = request.person.BirthDay
        };
        return _repository.AddPerson(model);
    }
}