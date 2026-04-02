using FluentValidation;
using Shared.Infrastructure.Validation;

namespace Shared.Infrastructure.Validation.SharedVOs;

// Ortak para birimi dogrulamasi
public class MoneyValidator : AbstractValidator<(decimal Amount, string CurrencyType)>
{
    public MoneyValidator()
    {
        RuleFor(x => x.Amount).NotEqual(0);
        RuleFor(x => x.CurrencyType).NotEmpty().MaximumLength(3).MatchesRegex("^[A-Z]{3}$", "Döviz Tipi");
    }
}

// Ortak tarih araligi dogrulamasi
public class DateRangeValidator : AbstractValidator<(DateTime Start, DateTime End)>
{
    public DateRangeValidator()
    {
        RuleFor(x => x.Start).NotEmpty();
        RuleFor(x => x.End).NotEmpty().GreaterThan(x => x.Start).WithMessage("Bitiş tarihi başlangıçtan sonra olmalıdır.");
    }
}
