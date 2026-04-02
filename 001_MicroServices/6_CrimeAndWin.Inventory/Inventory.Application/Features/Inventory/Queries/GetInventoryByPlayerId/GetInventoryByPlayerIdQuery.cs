using Inventory.Application.DTOs.InventoryDTOs;
using Mediator;

namespace Inventory.Application.Features.Inventory.Queries.GetInventoryByPlayerId
{
    public sealed record GetInventoryByPlayerIdQuery(Guid PlayerId) : IRequest<ResultInventoryDTO>;
}

