using Economy.Domain.Entities;
using Economy.Domain.VOs;
using FluentValidation;
using Shared.Infrastructure.Validation;

namespace Economy.Application.ValidationRules.EntityValidations;

public class WalletEntityValidator : BaseEntityValidator<Wallet>
{
    public WalletEntityValidator()
    {
        RuleFor(x => x.PlayerId).NotEmpty();
        RuleFor(x => x.Balance).GreaterThanOrEqualTo(0).WithMessage("Cüzdan bakiyesi negatif olamaz.");
    }
}

public class TransactionEntityValidator : BaseEntityValidator<Transaction>
{
    public TransactionEntityValidator()
    {
        RuleFor(x => x.WalletId).NotEmpty();
        RuleFor(x => x.Money).NotNull().SetValidator(new MoneyValidator());
        RuleFor(x => x.Reason).NotNull().SetValidator(new TransactionReasonValidator());
    }
}

public class MoneyValidator : AbstractValidator<Money>
{
    public MoneyValidator()
    {
        RuleFor(x => x.Amount).NotEqual(0).WithMessage("İşlem tutarı sıfır olamaz.");
        RuleFor(x => x.CurrencyType).NotEmpty().MaximumLength(3).MatchesRegex("^[A-Z]{3}$", "Döviz Tipi");
    }
}

public class TransactionReasonValidator : AbstractValidator<TransactionReason>
{
    public TransactionReasonValidator()
    {
        RuleFor(x => x.ReasonCode).NotEmpty().MaximumLength(50).MatchesRegex("^[A-Z_]+$", "İşlem Kodu");
        RuleFor(x => x.Description).MaximumLength(255);
    }
}

