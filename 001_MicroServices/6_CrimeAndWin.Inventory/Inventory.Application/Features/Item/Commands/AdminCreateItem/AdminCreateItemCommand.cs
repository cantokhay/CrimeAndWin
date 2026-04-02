using Inventory.Application.DTOs.ItemDTOs.Admin;
using Mediator;

namespace Inventory.Application.Features.Item.Commands.AdminCreateItem
{
    public sealed record AdminCreateItemCommand(AdminCreateItemDTO createItemDTO) : IRequest<Guid>;
}

