using MediatR;

namespace Inventory.Application.Features.Item.Commands.AdminDeleteItem
{
    public sealed record AdminDeleteItemCommand(Guid id) : IRequest<bool>;
}
