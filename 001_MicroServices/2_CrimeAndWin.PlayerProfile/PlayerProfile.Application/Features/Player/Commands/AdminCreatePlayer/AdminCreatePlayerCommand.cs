using MediatR;
using PlayerProfile.Application.DTOs.PlayerDTOs.Admin;

namespace PlayerProfile.Application.Features.Player.Commands.AdminCreatePlayer
{
    public sealed record AdminCreatePlayerCommand(AdminCreatePlayerDTO createPlayerDTO) : IRequest<Guid>;
}
