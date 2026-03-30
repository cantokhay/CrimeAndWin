using Identity.Application.DTOs.UserRoleDTOs.Admin;
using MediatR;

namespace Identity.Application.Features.UserRole.Commands.CreateUserRole
{
    public record CreateUserRoleCommand(CreateUserRoleDTO createUserRoleDTO) : IRequest<Guid>;
}
