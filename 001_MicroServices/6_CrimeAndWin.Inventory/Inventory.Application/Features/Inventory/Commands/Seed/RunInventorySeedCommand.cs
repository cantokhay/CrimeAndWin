using Shared.Application.Abstractions.Messaging;

namespace Inventory.Application.Features.Inventory.Commands.Seed
{
    public sealed record RunInventorySeedCommand(int Count) : IRequest<Unit>;
}


