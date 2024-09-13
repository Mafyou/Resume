using MediatR;
using Resume.Infrastructure.Abstractions;
using Resume.Infrastructure.Models;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(o =>
{
    o.RegisterServicesFromAssembly(typeof(AddPersonCommand).Assembly);
});

builder.Services.AddInfrastructure(builder.Configuration); // Ensure AddInfrastructure is defined in YourNamespace.Infrastructure
builder.Services.AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

/// <summary>
/// Sauvegardent une nouvelle personne. Attention, seules les personnes de moins de 150 ans peuvent être enregistrées. Sinon, renvoyez une erreur.
/// </summary>
app.MapPost("/AddPerson", async (PersonDTO person, [FromServices] ISender sender) =>
{
    var maxAllowedBirthDate = DateTime.Now.AddYears(-150);
    if (person.BirthDay <= maxAllowedBirthDate)
        return Results.BadRequest("Trop vieux");
    var isAdded = await sender.Send(new AddPersonCommand(person));
    return Results.Ok($"Person saved {isAdded}");
});

/// <summary>
/// Permettent d'ajouter un emploi à une personne avec une date de début et de fin d'emploi. Pour le poste actuellement occupé, la date de fin n'est pas obligatoire. Une personne peut avoir plusieurs emplois aux dates qui se chevauchent.
/// </summary>
app.MapPost("/AddJob", async (PersonJobWrapperDTO wrapper, [FromServices] ISender sender) =>
{
    var isAdded = await sender.Send(new AddJobCommand(wrapper.job, wrapper.person));
    return Results.Ok($"Added job {isAdded}");
});

/// <summary>
/// Renvoient toutes les personnes enregistrées par ordre alphabétique, et indiquent également leur âge et leur(s) emploi(s) actuel(s).
/// </summary>
//app.MapGet("/GetPersonsWithJobs", (JobDTO job, [FromServices] IRepository repo) =>
//{
//    var model = new Job
//    {
//        Name = job.Name
//    };
//    return Results.Ok(repo.GetPersonsBy(model));
//});

/// <summary>
/// Renvoient toutes les personnes ayant travaillé pour une entreprise donnée.
/// </summary>
app.MapPost("/GetPersonsBy", (JobDTO job, [FromServices] IRepository repo) =>
{
    var model = new Job
    {
        Name = job.Name
    };
    return Results.Ok(repo.GetPersonsBy(model));
});

/// <summary>
/// Renvoient tous les emplois d'une personne entre deux plages de dates.
/// </summary>
//app.MapGet("/GetPersonJobBetweenDates", (PersonJobDateWrapperDTO dto, [FromServices] IRepository repo) =>
//{
//    var model = new Person
//    {
//        Name = dto.person.Name,
//        BirthDate = dto.person.BirthDay
//    };
//    return Results.Ok(repo.GetPersonJobBetweenDates(model, dto.StartDate, dto.EndDate));
//});

app.Run();

record PersonJobWrapperDTO(PersonDTO person, JobDTO job);
record PersonJobDateWrapperDTO(PersonDTO person, DateTime StartDate, DateTime EndDate);