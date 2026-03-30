using FluentValidation;
using Leadership.Application.DTOs.LeaderboardDTOs;

namespace Leadership.Application.ValidationRules.LeaderboardValidations
{
    public class CreateLeaderboardDTOValidator : AbstractValidator<CreateLeaderboardDTO>
    {
        public CreateLeaderboardDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Description).MaximumLength(500);
            RuleFor(x => x.IsSeasonal).NotNull();
            // GameWorldId/SeasonId opsiyonel, seasonal'a göre UI kontrol edilebilir
        }
    }
}
