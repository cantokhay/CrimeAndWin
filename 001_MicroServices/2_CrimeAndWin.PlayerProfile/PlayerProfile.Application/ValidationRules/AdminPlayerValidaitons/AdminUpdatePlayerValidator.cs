using FluentValidation;
using PlayerProfile.Application.DTOs.PlayerDTOs.Admin;

namespace PlayerProfile.Application.ValidationRules.AdminPlayerValidaitons
{
    public sealed class AdminUpdatePlayerValidator : AbstractValidator<AdminUpdatePlayerDTO>
    {
        public AdminUpdatePlayerValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.DisplayName).NotEmpty().MaximumLength(30);
            RuleFor(x => x.EnergyMax).GreaterThan(0);
            RuleFor(x => x.Power).GreaterThanOrEqualTo(0);
        }
    }
}
