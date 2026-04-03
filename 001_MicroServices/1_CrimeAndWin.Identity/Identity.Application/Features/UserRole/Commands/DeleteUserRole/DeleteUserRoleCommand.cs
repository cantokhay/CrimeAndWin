using Shared.Application.Abstractions.Messaging;

namespace Identity.Application.Features.UserRole.Commands.DeleteUserRole
{
    public record DeleteUserRoleCommand(Guid id) : IRequest<bool>;
}


