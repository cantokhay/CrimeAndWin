using GameWorld.Application.DTOs.SeasonDTOs;
using MediatR;

namespace GameWorld.Application.Features.Season.Commands.UpdateSeason
{
    public record UpdateSeasonCommand(
        Guid SeasonId,
        Guid GameWorldId,
        int SeasonNumber,
        DateTime StartUtc,
        DateTime EndUtc,
        bool IsActive) : IRequest<UpdateSeasonDTO>;
}