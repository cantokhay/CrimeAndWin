using GameWorld.Application.DTOs.GameWorldDTOs;
using Mediator;

namespace GameWorld.Application.Features.GameWorld.Queries.GetListGameWorld
{
    public record GetGameWorldListQuery() : IRequest<IReadOnlyList<ResultGameWorldDTO>>;
}


