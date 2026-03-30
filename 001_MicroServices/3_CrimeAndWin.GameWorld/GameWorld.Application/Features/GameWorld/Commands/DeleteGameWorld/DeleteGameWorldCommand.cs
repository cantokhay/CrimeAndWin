using MediatR;

namespace GameWorld.Application.Features.GameWorld.Commands.DeleteGameWorld
{
    public sealed record DeleteGameWorldCommand(Guid Id) : IRequest<bool>;
}
