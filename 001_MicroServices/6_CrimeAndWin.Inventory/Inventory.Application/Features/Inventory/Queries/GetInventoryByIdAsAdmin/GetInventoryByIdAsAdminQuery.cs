using Inventory.Application.DTOs.InventoryDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Inventory.Application.Features.Inventory.Queries.GetInventoryByIdAsAdmin
{
    public sealed record GetInventoryByIdAsAdminQuery(Guid id) : IRequest<AdminResultInventoryDTO?>;
}


