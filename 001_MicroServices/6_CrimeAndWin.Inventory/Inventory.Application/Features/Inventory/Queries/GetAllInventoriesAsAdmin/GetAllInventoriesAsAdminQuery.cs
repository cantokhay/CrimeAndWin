using Inventory.Application.DTOs.InventoryDTOs.Admin;
using MediatR;

namespace Inventory.Application.Features.Inventory.Queries.GetAllInventoriesAsAdmin
{
    public sealed record GetAllInventoriesAsAdminQuery() : IRequest<List<AdminResultInventoryDTO>>;
}
