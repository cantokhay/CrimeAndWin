using GameWorld.Application.DTOs.SeasonDTOs;
using Mediator;

namespace GameWorld.Application.Features.Season.Queries.GetAllSeason
{
    public sealed record GetAllSeasonsQuery() : IRequest<List<ResultSeasonDTO>>;
}


