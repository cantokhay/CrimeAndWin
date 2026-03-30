using Economy.Application.Features.Wallet.Commands.WithdrawMoney;
using FluentValidation;

namespace Economy.Application.ValidationRules.WalletValidations
{
    public class WithdrawMoneyValidator : AbstractValidator<WithdrawMoneyCommand>
    {
        public WithdrawMoneyValidator()
        {
            RuleFor(x => x.Amount).GreaterThan(0);
            RuleFor(x => x.CurrencyType).NotEmpty();
            RuleFor(x => x.Reason).NotEmpty();
        }
    }
}
