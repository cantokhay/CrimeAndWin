using GameWorld.Application.DTOs.GameWorldDTOs;
using Shared.Application.Abstractions.Messaging;

namespace GameWorld.Application.Features.GameWorld.Queries.GetListGameWorld
{
    public record GetGameWorldListQuery() : IRequest<IReadOnlyList<ResultGameWorldDTO>>;
}



