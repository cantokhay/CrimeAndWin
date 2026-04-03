using Identity.Application.DTOs.UserRoleDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Identity.Application.Features.UserRole.Commands.UpdateUserRole
{
    public record UpdateUserRoleCommand(UpdateUserRoleDTO updateUserRoleDTO) : IRequest<bool>;
}


