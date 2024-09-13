namespace Resume.Application.Commands;

public sealed record AddPersonCommand(PersonDTO person) : IRequest<bool>;