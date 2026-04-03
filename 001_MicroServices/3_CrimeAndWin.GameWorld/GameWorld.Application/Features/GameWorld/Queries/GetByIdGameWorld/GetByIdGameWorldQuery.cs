using GameWorld.Application.DTOs.GameWorldDTOs;
using Shared.Application.Abstractions.Messaging;

namespace GameWorld.Application.Features.GameWorld.Queries.GetByIdGameWorld
{
    public record GetGameWorldByIdQuery(Guid Id) : IRequest<ResultGameWorldDTO>;
}



