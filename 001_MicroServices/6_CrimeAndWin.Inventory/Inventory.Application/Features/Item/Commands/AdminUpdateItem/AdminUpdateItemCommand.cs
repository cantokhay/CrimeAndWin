using Inventory.Application.DTOs.ItemDTOs.Admin;
using MediatR;

namespace Inventory.Application.Features.Item.Commands.AdminUpdateItem
{
    public sealed record AdminUpdateItemCommand(AdminUpdateItemDTO updateItemDTO) : IRequest<bool>;
}
