using Shared.Application.Abstractions.Messaging;

namespace Identity.Application.Features.Seed
{
    public sealed record SeedIdentityDataCommand() : IRequest<string>;
}


