using AppCore.Dto;
using FluentValidation;

namespace AppCore.Validators;

public class AddressDtoValidator : AbstractValidator<AddressDto>
{
    public AddressDtoValidator()
    {
        RuleFor(x => x.Street)
            .NotEmpty().WithMessage("Ulica jest wymagana.");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("Miasto jest wymagane.");

        RuleFor(x => x.PostalCode)
            .NotEmpty().WithMessage("Kod pocztowy jest wymagany.");

        RuleFor(x => x.Country)
            .NotEmpty().WithMessage("Kraj jest wymagany.");

        RuleFor(x => x.Type)
            .IsInEnum().WithMessage("Nieprawidłowy typ adresu.");
    }
}
