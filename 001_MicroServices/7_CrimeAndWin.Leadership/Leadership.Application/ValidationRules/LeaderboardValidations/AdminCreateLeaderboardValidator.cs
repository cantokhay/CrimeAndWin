using FluentValidation;
using Leadership.Application.DTOs.LeaderboardDTOs.Admin;

namespace Leadership.Application.ValidationRules.LeaderboardValidations
{
    public sealed class AdminCreateLeaderboardValidator : AbstractValidator<AdminCreateLeaderboardDTO>
    {
        public AdminCreateLeaderboardValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(128);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(512);
        }
    }
}
