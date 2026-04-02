using Identity.Application.DTOs.RoleDTOs.Admin;
using Mediator;

namespace Identity.Application.Features.Role.Commands.UpdateRole
{
    public record UpdateRoleCommand(UpdateRoleDTO updateRoleDTO) : IRequest<bool>;
}

