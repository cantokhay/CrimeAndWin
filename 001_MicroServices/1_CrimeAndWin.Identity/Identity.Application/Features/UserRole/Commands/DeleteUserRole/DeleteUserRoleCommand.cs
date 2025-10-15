using MediatR;

namespace Identity.Application.Features.UserRole.Commands.DeleteUserRole
{
    public record DeleteUserRoleCommand(Guid id) : IRequest<bool>;
}
