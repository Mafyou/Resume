namespace Resume.Application.Commands;

public sealed record AddJobCommand(JobDTO job, PersonDTO person) : IRequest<bool>;