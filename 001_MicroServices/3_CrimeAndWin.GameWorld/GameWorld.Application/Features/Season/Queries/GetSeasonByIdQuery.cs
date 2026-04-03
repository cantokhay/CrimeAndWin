using GameWorld.Application.DTOs.SeasonDTOs;
using Shared.Application.Abstractions.Messaging;

namespace Season.Application.Features.Season.Queries
{
    public record GetSeasonByIdQuery(Guid Id) : IRequest<ResultSeasonDTO>;
}


