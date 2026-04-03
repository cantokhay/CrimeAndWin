using Shared.Application.Abstractions.Messaging;

namespace Inventory.Application.Features.Inventory.Commands.AdminDeleteInventory
{
    public sealed record AdminDeleteInventoryCommand(Guid id) : IRequest<bool>;
}


