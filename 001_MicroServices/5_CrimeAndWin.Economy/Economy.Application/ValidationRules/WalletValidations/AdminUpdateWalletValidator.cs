using Economy.Application.DTOs.WalletDTOs.Admin;
using FluentValidation;

namespace Economy.Application.ValidationRules.WalletValidations
{
    public sealed class AdminUpdateWalletValidator : AbstractValidator<AdminUpdateWalletDTO>
    {
        public AdminUpdateWalletValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.PlayerId).NotEmpty();
            RuleFor(x => x.Balance).GreaterThanOrEqualTo(0);
        }
    }
}
