using Inventory.Application.DTOs.InventoryDTOs.Admin;
using MediatR;

namespace Inventory.Application.Features.Inventory.Queries.GetInventoryByIdAsAdmin
{
    public sealed record GetInventoryByIdAsAdminQuery(Guid id) : IRequest<AdminResultInventoryDTO?>;
}
