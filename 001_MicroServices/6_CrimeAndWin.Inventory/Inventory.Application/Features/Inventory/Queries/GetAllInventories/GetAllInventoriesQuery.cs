using Inventory.Application.DTOs.InventoryDTOs;
using MediatR;

namespace Inventory.Application.Features.Inventory.Queries.GetAllInventories
{
    public sealed record GetAllInventoriesQuery() : IRequest<List<ResultInventoryDTO>>;
}
