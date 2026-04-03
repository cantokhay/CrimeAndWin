using Identity.Application.DTOs.RoleDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Identity.Application.Features.Role.Queries.GetRoleById
{
    public record GetRoleByIdQuery(Guid id) : IRequest<ResultRoleDTO>;
}


