namespace Resume.Infrastructure.ServicesExtension;

public static class InfrastructreServiceExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSqlite<ResumeContext>(configuration.GetConnectionString("MainConnection"));
        services.AddScoped<IRepository, ResumeRespository>();
        return services;
    }
}