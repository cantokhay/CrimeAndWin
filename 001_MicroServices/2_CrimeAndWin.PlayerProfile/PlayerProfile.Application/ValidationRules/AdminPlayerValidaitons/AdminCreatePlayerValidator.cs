using FluentValidation;
using PlayerProfile.Application.DTOs.PlayerDTOs.Admin;

namespace PlayerProfile.Application.ValidationRules.AdminPlayerValidaitons
{
    public sealed class AdminCreatePlayerValidator : AbstractValidator<AdminCreatePlayerDTO>
    {
        public AdminCreatePlayerValidator()
        {
            RuleFor(x => x.AppUserId).NotEmpty();
            RuleFor(x => x.DisplayName).NotEmpty().MaximumLength(30);
            RuleFor(x => x.EnergyMax).GreaterThan(0);
            RuleFor(x => x.Power).GreaterThanOrEqualTo(0);
        }
    }
}
