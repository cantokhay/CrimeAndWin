using Inventory.Application.DTOs.InventoryDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Inventory.Application.Features.Inventory.Queries.GetAllInventoriesAsAdmin
{
    public sealed record GetAllInventoriesAsAdminQuery() : IRequest<List<AdminResultInventoryDTO>>;
}


