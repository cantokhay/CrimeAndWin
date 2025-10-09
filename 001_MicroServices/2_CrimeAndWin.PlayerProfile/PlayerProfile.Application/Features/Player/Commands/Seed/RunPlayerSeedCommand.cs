using MediatR;

namespace PlayerProfile.Application.Features.Player.Commands.Seed
{
    public sealed record RunPlayerSeedCommand(int Count) : IRequest<Unit>;
}
