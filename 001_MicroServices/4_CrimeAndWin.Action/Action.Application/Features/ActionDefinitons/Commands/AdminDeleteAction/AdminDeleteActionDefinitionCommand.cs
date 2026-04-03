using Shared.Application.Abstractions.Messaging;

namespace Action.Application.Features.ActionDefinitons.Commands.AdminDeleteAction
{
    public sealed record AdminDeleteActionDefinitionCommand(Guid id) : IRequest<bool>;
}


