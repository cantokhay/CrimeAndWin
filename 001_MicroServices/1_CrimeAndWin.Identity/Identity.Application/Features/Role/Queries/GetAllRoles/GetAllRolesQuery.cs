using Identity.Application.DTOs.RoleDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Identity.Application.Features.Role.Queries.GetAllRoles
{
    public record GetAllRolesQuery() : IRequest<List<ResultRoleDTO>>;
}


