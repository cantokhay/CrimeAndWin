using Inventory.Application.DTOs.InventoryDTOs;
using MediatR;

namespace Inventory.Application.Features.Inventory.Queries.GetInventoryByPlayerId
{
    public sealed record GetInventoryByPlayerIdQuery(Guid PlayerId) : IRequest<ResultInventoryDTO>;
}
