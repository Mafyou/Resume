using FluentValidation;

namespace Resume.Application.Validators;

public class PersonValidator : AbstractValidator<PersonDTO>
{
    public PersonValidator()
    {
        RuleFor(x => x.Name).NotEmpty()
            .WithMessage("Name is required.");
        // Get the current date
        var maxAllowedBirthDate = DateTime.Now.AddYears(150);
        RuleFor(x => x.BirthDay).Must(x => x < maxAllowedBirthDate).
            WithMessage("Trop vieux.");
    }
}