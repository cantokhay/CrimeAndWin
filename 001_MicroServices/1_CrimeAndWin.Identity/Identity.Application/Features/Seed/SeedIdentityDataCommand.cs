using Mediator;

namespace Identity.Application.Features.Seed
{
    public sealed record SeedIdentityDataCommand() : IRequest<string>;
}

