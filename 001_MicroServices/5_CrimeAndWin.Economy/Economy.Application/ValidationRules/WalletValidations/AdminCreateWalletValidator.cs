using Economy.Application.DTOs.WalletDTOs.Admin;
using FluentValidation;

namespace Economy.Application.ValidationRules.WalletValidations
{
    public sealed class AdminCreateWalletValidator : AbstractValidator<AdminCreateWalletDTO>
    {
        public AdminCreateWalletValidator()
        {
            RuleFor(x => x.PlayerId).NotEmpty();
            RuleFor(x => x.Balance).GreaterThanOrEqualTo(0);
        }
    }
}
