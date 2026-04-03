using Identity.Application.DTOs.RoleDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Identity.Application.Features.Role.Commands.UpdateRole
{
    public record UpdateRoleCommand(UpdateRoleDTO updateRoleDTO) : IRequest<bool>;
}


