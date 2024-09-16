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
/// Sauvegardent une nouvelle personne. Attention, seules les personnes de moins de 150 ans peuvent �tre enregistr�es. Sinon, renvoyez une erreur.
/// </summary>
app.MapPost("/AddPerson", async (PersonDTO person, [FromServices] ISender sender) =>
{
    var maxAllowedBirthDate = DateTime.Now.AddYears(-150);
    if (person.BirthDay <= maxAllowedBirthDate)
        return Results.BadRequest("Trop vieux");
    var isAdded = await sender.Send(new AddPersonCommand(person));
    return isAdded ? Results.Ok("Person added") : Results.BadRequest("Person not added");
});

/// <summary>
/// Permettent d'ajouter un emploi � une personne avec une date de d�but et de fin d'emploi. Pour le poste actuellement occup�, la date de fin n'est pas obligatoire. Une personne peut avoir plusieurs emplois aux dates qui se chevauchent.
/// </summary>
app.MapPost("/AddJob", async (PersonDTO person, [FromServices] ISender sender) =>
{
    var isAdded = await sender.Send(new AddJobCommand(person));
    return isAdded ? Results.Ok("Job added") : Results.BadRequest("Job not added");
});

/// <summary>
/// Renvoient toutes les personnes enregistr�es par ordre alphab�tique, et indiquent �galement leur �ge et leur(s) emploi(s) actuel(s).
/// </summary>
app.MapGet("/GetPersonsWithJobs", async ([FromServices] ISender sender) =>
{
    return Results.Ok(await sender.Send(new GetPeopleQuery()));
});

/// <summary>
/// Renvoient toutes les personnes ayant travaill� pour une entreprise donn�e.
/// </summary>
app.MapPost("/GetPersonsByCompany", (CompanyDTO company, [FromServices] ISender sender) =>
{
    return Results.Ok(sender.Send(new GetPersonsByCompanyQuery(company)));
});

/// <summary>
/// Renvoient tous les emplois d'une personne entre deux plages de dates.
/// </summary>
app.MapPost("/GetPersonJobBetweenDates", (PersonJobDateWrapperDTO dto, [FromServices] ISender sernder) =>
{
    return Results.Ok(sernder.Send(new GetPersonJobBetweenDatesQuery(dto.PersonName, dto.StartDate, dto.EndDate)));
});

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetService<ResumeContext>();
    dbContext?.Database.EnsureCreated();
}

app.Run();

record PersonJobWrapperDTO(PersonDTO Person, JobDTO job);
record PersonJobDateWrapperDTO(string PersonName, DateTime StartDate, DateTime EndDate);