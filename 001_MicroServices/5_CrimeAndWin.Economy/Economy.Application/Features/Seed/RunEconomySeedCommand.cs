using MediatR;

namespace Economy.Application.Features.Seed
{
    public sealed record RunEconomySeedCommand(int Count) : IRequest<Unit>;
}
