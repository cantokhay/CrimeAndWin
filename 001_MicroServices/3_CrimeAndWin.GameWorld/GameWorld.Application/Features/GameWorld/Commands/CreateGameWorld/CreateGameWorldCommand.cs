using GameWorld.Application.DTOs.GameWorldDTOs;
using Shared.Application.Abstractions.Messaging;

namespace GameWorld.Application.Features.GameWorld.Commands.CreateGameWorld
{
    public record CreateGameWorldCommand(string Name, int MaxEnergy, int RegenRatePerHour) : IRequest<CreateGameWorldDTO>;
}



