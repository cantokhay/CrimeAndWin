using MediatR;

namespace Inventory.Application.Features.Inventory.Commands.CreateInventory
{
    public sealed record CreateInventoryCommand(
        Guid PlayerId
        ) 
        : IRequest<bool>;
}
