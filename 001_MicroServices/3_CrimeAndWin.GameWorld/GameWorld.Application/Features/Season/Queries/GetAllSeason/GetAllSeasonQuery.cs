using GameWorld.Application.DTOs.SeasonDTOs;
using Shared.Application.Abstractions.Messaging;

namespace GameWorld.Application.Features.Season.Queries.GetAllSeason
{
    public sealed record GetAllSeasonsQuery() : IRequest<List<ResultSeasonDTO>>;
}



