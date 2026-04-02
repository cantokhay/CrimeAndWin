using Inventory.Domain.Entities;
using Inventory.Domain.VOs;
using Inventory.Domain.Enums;
using FluentValidation;
using Shared.Infrastructure.Validation;

namespace Inventory.Application.ValidationRules.EntityValidations;

public class InventoryEntityValidator : BaseEntityValidator<Inventory.Domain.Entities.Inventory>
{
    public InventoryEntityValidator()
    {
        RuleFor(x => x.PlayerId).NotEmpty();
        RuleFor(x => x.Capacity).Positive();
    }
}

public class ItemEntityValidator : BaseEntityValidator<Item>
{
    public ItemEntityValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Stats).SetValidator(new ItemStatsValidator());
        RuleFor(x => x.Value).SetValidator(new ItemValueValidator());
    }
}

public class ItemStatsValidator : AbstractValidator<ItemStats>
{
    public ItemStatsValidator()
    {
        RuleFor(x => x.Damage).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Defense).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Power).GreaterThanOrEqualTo(0);
    }
}

public class ItemValueValidator : AbstractValidator<ItemValue>
{
    public ItemValueValidator()
    {
        RuleFor(x => x.Price).PositiveCurrency();
        RuleFor(x => x.Rarity).InRange(1, 5);
        RuleFor(x => x.CurrencyType).IsInEnum();
    }
}
