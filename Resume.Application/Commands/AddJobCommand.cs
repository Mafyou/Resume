namespace Resume.Application.Commands;

public sealed record AddJobCommand(PersonDTO person) : IRequest<bool>;