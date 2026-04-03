using Inventory.Application.DTOs.InventoryDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Inventory.Application.Features.Inventory.Commands.AdminUpdateInventory
{
    public sealed record AdminUpdateInventoryCommand(AdminUpdateInventoryDTO updateInventoryDTO) : IRequest<bool>;
}


