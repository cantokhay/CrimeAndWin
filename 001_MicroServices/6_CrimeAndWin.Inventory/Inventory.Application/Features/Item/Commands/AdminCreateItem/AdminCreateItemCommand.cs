using Inventory.Application.DTOs.ItemDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Inventory.Application.Features.Item.Commands.AdminCreateItem
{
    public sealed record AdminCreateItemCommand(AdminCreateItemDTO createItemDTO) : IRequest<Guid>;
}


