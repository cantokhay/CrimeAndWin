using MediatR;

namespace Action.Application.Features.ActionDefinitons.Commands.Seed
{
    public sealed record RunActionSeedCommand(int Count) : IRequest<Unit>;
}
