using Inventory.Application.DTOs.ItemDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Inventory.Application.Features.Item.Commands.AdminUpdateItem
{
    public sealed record AdminUpdateItemCommand(AdminUpdateItemDTO updateItemDTO) : IRequest<bool>;
}


