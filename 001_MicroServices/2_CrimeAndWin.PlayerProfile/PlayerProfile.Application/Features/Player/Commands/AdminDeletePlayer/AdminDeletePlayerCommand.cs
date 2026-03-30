using MediatR;

namespace PlayerProfile.Application.Features.Player.Commands.AdminDeletePlayer
{
    public sealed record AdminDeletePlayerCommand(Guid Id) : IRequest<bool>;
}
