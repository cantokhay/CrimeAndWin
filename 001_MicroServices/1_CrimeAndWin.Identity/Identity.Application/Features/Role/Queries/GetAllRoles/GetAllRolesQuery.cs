using Identity.Application.DTOs.RoleDTOs.Admin;
using MediatR;

namespace Identity.Application.Features.Role.Queries.GetAllRoles
{
    public record GetAllRolesQuery() : IRequest<List<ResultRoleDTO>>;
}
