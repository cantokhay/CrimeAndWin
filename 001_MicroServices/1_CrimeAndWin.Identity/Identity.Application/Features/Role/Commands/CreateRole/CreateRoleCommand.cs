using Identity.Application.DTOs.RoleDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Identity.Application.Features.Role.Commands.CreateRole
{
    public record CreateRoleCommand(CreateRoleDTO createRoleDTO) : IRequest<Guid>;
}


