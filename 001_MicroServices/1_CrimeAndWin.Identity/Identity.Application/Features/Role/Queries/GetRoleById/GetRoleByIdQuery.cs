using Identity.Application.DTOs.RoleDTOs.Admin;
using Mediator;

namespace Identity.Application.Features.Role.Queries.GetRoleById
{
    public record GetRoleByIdQuery(Guid id) : IRequest<ResultRoleDTO>;
}

