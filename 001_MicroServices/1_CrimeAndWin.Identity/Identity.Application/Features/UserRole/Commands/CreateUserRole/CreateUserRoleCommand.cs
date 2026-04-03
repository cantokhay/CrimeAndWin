using Identity.Application.DTOs.UserRoleDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Identity.Application.Features.UserRole.Commands.CreateUserRole
{
    public record CreateUserRoleCommand(CreateUserRoleDTO createUserRoleDTO) : IRequest<Guid>;
}


