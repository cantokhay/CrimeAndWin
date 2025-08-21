using MediatR;

namespace Inventory.Application.Features.Inventory.Commands
{
    public sealed record CreateInventoryCommand(
        Guid PlayerId
        ) 
        : IRequest<bool>;
}
