using Shared.Application.Abstractions.Messaging;

namespace PlayerProfile.Application.Features.Player.Commands.AdminDeletePlayer
{
    public sealed record AdminDeletePlayerCommand(Guid Id) : IRequest<bool>;
}


