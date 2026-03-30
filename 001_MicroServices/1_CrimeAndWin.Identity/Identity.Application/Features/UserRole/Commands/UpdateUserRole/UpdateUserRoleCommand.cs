using Identity.Application.DTOs.UserRoleDTOs.Admin;
using MediatR;

namespace Identity.Application.Features.UserRole.Commands.UpdateUserRole
{
    public record UpdateUserRoleCommand(UpdateUserRoleDTO updateUserRoleDTO) : IRequest<bool>;
}
