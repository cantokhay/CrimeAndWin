using Identity.Application.DTOs.UserRoleDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Identity.Application.Features.UserRole.Queries.GetUserRoleById
{
    public record GetUserRoleByIdQuery(Guid id) : IRequest<ResultUserRoleDTO>;
}


