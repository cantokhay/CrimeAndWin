using Identity.Application.DTOs.UserRoleDTOs.Admin;
using Mediator;

namespace Identity.Application.Features.UserRole.Queries.GetUserRoleById
{
    public record GetUserRoleByIdQuery(Guid id) : IRequest<ResultUserRoleDTO>;
}

