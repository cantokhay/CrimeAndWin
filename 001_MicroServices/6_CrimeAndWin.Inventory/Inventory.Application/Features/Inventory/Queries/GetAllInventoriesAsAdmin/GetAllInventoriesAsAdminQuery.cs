using Inventory.Application.DTOs.InventoryDTOs.Admin;
using Mediator;

namespace Inventory.Application.Features.Inventory.Queries.GetAllInventoriesAsAdmin
{
    public sealed record GetAllInventoriesAsAdminQuery() : IRequest<List<AdminResultInventoryDTO>>;
}

