using Identity.Application.DTOs.UserRoleDTOs.Admin;
using MediatR;

namespace Identity.Application.Features.UserRole.Queries.GetAllUserRoles
{
    public record GetAllUserRolesQuery() : IRequest<List<ResultUserRoleDTO>>;
}
