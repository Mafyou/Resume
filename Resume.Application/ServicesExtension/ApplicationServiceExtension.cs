﻿using FluentValidation;
using Resume.Application.Validators;
using Resume.Data.DTO;

namespace Resume.Application.ServicesExtension;

public static class ApplicationServiceExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<PersonValidator>(); //fv => fv.RegisterValidatorsFromAssemblyContaining<PersonValidator>());
        services.AddScoped<IValidator<PersonDTO>, PersonValidator>();
        return services;
    }
}