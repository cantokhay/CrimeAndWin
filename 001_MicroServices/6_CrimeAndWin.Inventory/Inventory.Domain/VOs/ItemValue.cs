using Inventory.Domain.Enums;

namespace Inventory.Domain.VOs
{
    public readonly record struct ItemValue
        (
        decimal Amount, 
        CurrencyType Currency
        );
}
