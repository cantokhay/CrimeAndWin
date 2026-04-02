using Identity.Application.DTOs.UserRoleDTOs.Admin;
using Mediator;

namespace Identity.Application.Features.UserRole.Commands.UpdateUserRole
{
    public record UpdateUserRoleCommand(UpdateUserRoleDTO updateUserRoleDTO) : IRequest<bool>;
}

