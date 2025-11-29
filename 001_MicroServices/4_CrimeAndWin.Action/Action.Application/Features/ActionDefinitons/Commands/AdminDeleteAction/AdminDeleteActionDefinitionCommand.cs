using MediatR;

namespace Action.Application.Features.ActionDefinitons.Commands.AdminDeleteAction
{
    public sealed record AdminDeleteActionDefinitionCommand(Guid id) : IRequest<bool>;
}
