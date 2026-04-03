using Shared.Application.Abstractions.Messaging;

namespace GameWorld.Application.Features.GameWorld.Commands.DeleteGameWorld
{
    public sealed record DeleteGameWorldCommand(Guid Id) : IRequest<bool>;
}



