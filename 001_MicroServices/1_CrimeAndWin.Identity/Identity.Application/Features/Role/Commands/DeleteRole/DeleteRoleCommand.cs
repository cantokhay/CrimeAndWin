using MediatR;

namespace Identity.Application.Features.Role.Commands.DeleteRole
{
    public record DeleteRoleCommand(Guid id) : IRequest<bool>;
}
