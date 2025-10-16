using MediatR;
using PlayerProfile.Application.DTOs.PlayerDTOs.Admin;

namespace PlayerProfile.Application.Features.Player.Commands.AdminUpdatePlayer
{
    public sealed record AdminUpdatePlayerCommand(AdminUpdatePlayerDTO updatePlayerDTO) : IRequest<bool>;
}
