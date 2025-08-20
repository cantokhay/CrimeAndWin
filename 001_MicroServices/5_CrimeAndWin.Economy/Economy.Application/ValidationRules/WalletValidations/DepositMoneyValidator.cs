using Economy.Application.Features.Wallet.Commands;
using FluentValidation;

namespace Economy.Application.ValidationRules.WalletValidations
{
    public class DepositMoneyValidator : AbstractValidator<DepositMoneyCommand>
    {
        public DepositMoneyValidator()
        {
            RuleFor(x => x.Amount).GreaterThan(0);
            RuleFor(x => x.CurrencyType).NotEmpty();
            RuleFor(x => x.Reason).NotEmpty();
        }
    }
}
