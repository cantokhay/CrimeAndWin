using Identity.Application.DTOs.RoleDTOs.Admin;
using Mediator;

namespace Identity.Application.Features.Role.Commands.CreateRole
{
    public record CreateRoleCommand(CreateRoleDTO createRoleDTO) : IRequest<Guid>;
}

