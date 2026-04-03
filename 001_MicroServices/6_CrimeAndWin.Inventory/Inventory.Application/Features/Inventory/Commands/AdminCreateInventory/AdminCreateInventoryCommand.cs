using Inventory.Application.DTOs.InventoryDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Inventory.Application.Features.Inventory.Commands.AdminCreateInventory
{
    public sealed record AdminCreateInventoryCommand(AdminCreateInventoryDTO createInventoryDTO) : IRequest<Guid>;
}


