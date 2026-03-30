using FluentValidation;
using GameWorld.Application.Features.Season.Commands.CreateSeason;

namespace GameWorld.Application.ValidationRules.SeasonValidations
{
    public class CreateSeasonValidator : AbstractValidator<CreateSeasonCommand>
    {
        public CreateSeasonValidator()
        {
            RuleFor(x => x.GameWorldId).NotEmpty();
            RuleFor(x => x.SeasonNumber).GreaterThan(0);
            RuleFor(x => x.StartUtc).LessThan(x => x.EndUtc).WithMessage("Start Date < End Date olmalı");
        }
    }
}
