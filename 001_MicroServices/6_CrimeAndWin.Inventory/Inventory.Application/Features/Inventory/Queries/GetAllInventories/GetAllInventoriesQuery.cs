using Inventory.Application.DTOs.InventoryDTOs;
using Shared.Application.Abstractions.Messaging;

namespace Inventory.Application.Features.Inventory.Queries.GetAllInventories
{
    public sealed record GetAllInventoriesQuery() : IRequest<List<ResultInventoryDTO>>;
}


