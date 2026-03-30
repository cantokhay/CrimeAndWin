using GameWorld.Application.DTOs.SeasonDTOs;
using MediatR;

namespace GameWorld.Application.Features.Season.Queries.GetAllSeason
{
    public sealed record GetAllSeasonsQuery() : IRequest<List<ResultSeasonDTO>>;
}
