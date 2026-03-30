using Economy.Application.DTOs.TransactionDTOs.Admin;
using FluentValidation;

namespace Economy.Application.ValidationRules.TransactionValidations
{
    public sealed class AdminCreateTransactionValidator : AbstractValidator<AdminCreateTransactionDTO>
    {
        public AdminCreateTransactionValidator()
        {
            RuleFor(x => x.WalletId).NotEmpty();
            RuleFor(x => x.Amount).NotEqual(0);
            RuleFor(x => x.CurrencyType).NotEmpty();
            RuleFor(x => x.ReasonCode).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}
