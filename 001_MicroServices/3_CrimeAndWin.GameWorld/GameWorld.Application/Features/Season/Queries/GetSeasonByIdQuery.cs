using GameWorld.Application.DTOs.SeasonDTOs;
using MediatR;

namespace Season.Application.Features.Season.Queries
{
    public record GetSeasonByIdQuery(Guid Id) : IRequest<ResultSeasonDTO>;
}