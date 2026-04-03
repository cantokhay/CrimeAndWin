using Shared.Application.Abstractions.Messaging;

namespace PlayerProfile.Application.Features.Player.Commands.Seed
{
    public sealed record RunPlayerSeedCommand(int Count) : IRequest<Unit>;
}


