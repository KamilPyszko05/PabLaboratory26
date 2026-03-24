using AppCore.Dto;
using AppCore.Interfaces;
using AppCore.Models;
using FluentValidation;

namespace AppCore.Validators;

public class UpdatePersonDtoValidator : AbstractValidator<UpdatePersonDto>
{
    private readonly IGenericRepositoryAsync<Company> _companyRepository;

    public UpdatePersonDtoValidator(IUnitOfWork unitOfWork)
    {
        _companyRepository = unitOfWork.Companies;

        RuleFor(x => x.FirstName)
            .MaximumLength(100).WithMessage("Imię nie może przekraczać 100 znaków.")
            .When(x => x.FirstName != null);

        RuleFor(x => x.LastName)
            .MaximumLength(200).WithMessage("Nazwisko nie może przekraczać 200 znaków.")
            .When(x => x.LastName != null);

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Nieprawidłowy format adresu email.")
            .When(x => x.Email != null);

        RuleFor(x => x.Phone)
            .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Nieprawidłowy format numeru telefonu (E.164).")
            .When(x => x.Phone != null);

        RuleFor(x => x.BirthDate)
            .Must(date => date == null || BeValidAge(date.Value))
            .WithMessage("Osoba musi mieć od 18 do 120 lat.")
            .When(x => x.BirthDate.HasValue);

        RuleFor(x => x.Gender)
            .IsInEnum().WithMessage("Nieprawidłowa wartość płci.")
            .When(x => x.Gender.HasValue);

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
