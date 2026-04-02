using GameWorld.Application.DTOs.GameWorldDTOs;
using Mediator;

namespace GameWorld.Application.Features.GameWorld.Queries.GetByIdGameWorld
{
    public record GetGameWorldByIdQuery(Guid Id) : IRequest<ResultGameWorldDTO>;
}


