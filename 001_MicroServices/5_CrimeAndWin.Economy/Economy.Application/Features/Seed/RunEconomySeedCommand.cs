using Shared.Application.Abstractions.Messaging;

namespace Economy.Application.Features.Seed
{
    public sealed record RunEconomySeedCommand(int Count) : IRequest<Unit>;
}


