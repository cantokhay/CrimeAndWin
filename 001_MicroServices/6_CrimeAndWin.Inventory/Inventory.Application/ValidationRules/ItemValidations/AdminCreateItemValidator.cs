using FluentValidation;
using Inventory.Application.DTOs.ItemDTOs.Admin;

namespace Inventory.Application.ValidationRules.ItemValidations
{
    public sealed class AdminCreateItemValidator : AbstractValidator<AdminCreateItemDTO>
    {
        public AdminCreateItemValidator()
        {
            RuleFor(x => x.InventoryId).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().MaximumLength(128);
            RuleFor(x => x.Quantity).GreaterThanOrEqualTo(1);
            RuleFor(x => x.Damage).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Defense).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Power).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Amount).GreaterThanOrEqualTo(0);
            RuleFor(x => (int)x.Currency).IsInEnum();
        }
    }
}
