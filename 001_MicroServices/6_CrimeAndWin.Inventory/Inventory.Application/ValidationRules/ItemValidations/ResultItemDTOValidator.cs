using FluentValidation;
using Inventory.Application.DTOs.ItemDTOs;

namespace Inventory.Application.ValidationRules.ItemValidations
{
    public sealed class ResultItemDTOValidator : AbstractValidator<ResultItemDTO>
    {
        public ResultItemDTOValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.InventoryId).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().MaximumLength(128);
            RuleFor(x => x.Quantity).GreaterThanOrEqualTo(1);
            RuleFor(x => x.Stats).NotNull();
            RuleFor(x => x.Stats.Damage).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Stats.Defense).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Stats.Power).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Value).NotNull();
            RuleFor(x => x.Value.Amount).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Value.Currency).IsInEnum();
        }
    }
}
