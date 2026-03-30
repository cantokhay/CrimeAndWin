using Identity.Application.DTOs.RoleDTOs.Admin;
using MediatR;

namespace Identity.Application.Features.Role.Commands.CreateRole
{
    public record CreateRoleCommand(CreateRoleDTO createRoleDTO) : IRequest<Guid>;
}
