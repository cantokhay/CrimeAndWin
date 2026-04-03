using Shared.Application.Abstractions.Messaging;

namespace GameWorld.Application.Features.GameWorld.Commands.Seed
{
    public sealed record RunGameWorldSeedCommand(int Count) : IRequest<Unit>;

}



