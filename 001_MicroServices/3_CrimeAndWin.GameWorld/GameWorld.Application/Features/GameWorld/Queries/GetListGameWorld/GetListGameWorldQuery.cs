using GameWorld.Application.DTOs.GameWorldDTOs;
using MediatR;

namespace GameWorld.Application.Features.GameWorld.Queries.GetListGameWorld
{
    public record GetGameWorldListQuery() : IRequest<IReadOnlyList<ResultGameWorldDTO>>;
}
