using MediatR;
using PlayerProfile.Application.Features.Player.DTOs;

namespace PlayerProfile.Application.Features.Player.Commands.CreatePlayer
{
    public sealed record CreatePlayerCommand(
        Guid AppUserId, string DisplayName, string AvatarKey,
        int Power, int Defense, int Agility, int Luck,
        int EnergyCurrent, int EnergyMax, int EnergyRegenPerMinute) : IRequest<CreatePlayerDTO>;
}
