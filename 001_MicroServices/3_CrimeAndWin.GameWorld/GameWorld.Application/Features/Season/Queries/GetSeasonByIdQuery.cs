using GameWorld.Application.DTOs.SeasonDTOs;
using Mediator;

namespace Season.Application.Features.Season.Queries
{
    public record GetSeasonByIdQuery(Guid Id) : IRequest<ResultSeasonDTO>;
}

