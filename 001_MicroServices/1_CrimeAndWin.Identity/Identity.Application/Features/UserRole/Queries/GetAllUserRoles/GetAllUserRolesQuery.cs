using Identity.Application.DTOs.UserRoleDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Identity.Application.Features.UserRole.Queries.GetAllUserRoles
{
    public record GetAllUserRolesQuery() : IRequest<List<ResultUserRoleDTO>>;
}


