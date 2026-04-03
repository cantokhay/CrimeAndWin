using Shared.Application.Abstractions.Messaging;

namespace Identity.Application.Features.Role.Commands.DeleteRole
{
    public record DeleteRoleCommand(Guid id) : IRequest<bool>;
}


