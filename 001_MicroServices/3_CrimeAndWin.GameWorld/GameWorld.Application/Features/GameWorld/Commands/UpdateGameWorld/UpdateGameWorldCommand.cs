using GameWorld.Application.DTOs.GameWorldDTOs;
using MediatR;

namespace GameWorld.Application.Features.GameWorld.Commands.UpdateGameWorld
{
    public record UpdateGameWorldCommand(Guid GameWorldId, int MaxEnergy, int RegenRatePerHour) : IRequest<UpdateGameWorldDTO>;
}
