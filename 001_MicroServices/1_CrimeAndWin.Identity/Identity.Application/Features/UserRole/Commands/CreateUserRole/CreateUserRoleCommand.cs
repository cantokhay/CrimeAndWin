using Identity.Application.DTOs.UserRoleDTOs.Admin;
using Mediator;

namespace Identity.Application.Features.UserRole.Commands.CreateUserRole
{
    public record CreateUserRoleCommand(CreateUserRoleDTO createUserRoleDTO) : IRequest<Guid>;
}

