using GameWorld.Application.DTOs.SeasonDTOs;
using MediatR;

namespace GameWorld.Application.Features.Season.Commands.CreateSeason
{
    public record CreateSeasonCommand(Guid GameWorldId, int SeasonNumber, DateTime StartUtc, DateTime EndUtc, bool IsActive) : IRequest<CreateSeasonDTO>;
}
