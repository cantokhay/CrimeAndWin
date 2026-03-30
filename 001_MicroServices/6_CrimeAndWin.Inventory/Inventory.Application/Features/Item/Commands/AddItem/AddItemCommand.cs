using Inventory.Domain.Enums;
using MediatR;

namespace Inventory.Application.Features.Item.Commands.AddItem
{
    public sealed record AddItemCommand(
        Guid InventoryId,
        string Name,
        int Quantity,
        int Damage,
        int Defense,
        int Power,
        decimal Amount,
        CurrencyType Currency) : IRequest<bool>;
}
