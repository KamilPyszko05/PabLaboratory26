using AppCore.Dto;
using AppCore.Interfaces;
using AppCore.Models;
using FluentValidation;

namespace AppCore.Validators;

public class CreatePersonDtoValidator : AbstractValidator<CreatePersonDto>
{
    private readonly IGenericRepositoryAsync<Company> _companyRepository;

    public CreatePersonDtoValidator(IUnitOfWork unitOfWork)
    {
        _companyRepository = unitOfWork.Companies;

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Imię jest wymagane.")
            .MaximumLength(100).WithMessage("Imię nie może przekraczać 100 znaków.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Nazwisko jest wymagane.")
            .MaximumLength(200).WithMessage("Nazwisko nie może przekraczać 200 znaków.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email jest wymagany.")
            .EmailAddress().WithMessage("Nieprawidłowy format adresu email.");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Telefon jest wymagany.")
            .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Nieprawidłowy format numeru telefonu (E.164).");

        RuleFor(x => x.BirthDate)
            .Must(date => date == null || BeValidAge(date.Value))
            .WithMessage("Osoba musi mieć od 18 do 120 lat.");

        RuleFor(x => x.Gender)
            .IsInEnum().WithMessage("Nieprawidłowa wartość płci.");

        RuleFor(x => x.EmployerId)
            .MustAsync(EmployerExistsAsync)
            .WithMessage("Pracodawca o podanym identyfikatorze nie istnieje.")
            .When(x => x.EmployerId.HasValue);

        RuleFor(x => x.Address)
            .SetValidator(new AddressDtoValidator()!)
            .When(x => x.Address != null);
    }

    private static bool BeValidAge(DateTime birthDate)
    {
        var today = DateTime.Today;
        var age = today.Year - birthDate.Year;
        if (birthDate.Date > today.AddYears(-age)) age--;
        return age is >= 18 and <= 120;
    }

    private async Task<bool> EmployerExistsAsync(Guid? employerId, CancellationToken cancellationToken)
    {
        if (!employerId.HasValue) return true;
        var company = await _companyRepository.FindByIdAsync(employerId.Value);
        return company != null;
    }
}
