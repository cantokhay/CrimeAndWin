using Action.Application.DTOs.ActionAttemptDTOs.Admin;
using MediatR;

namespace Action.Application.Features.PlayerActionAttempts.Queries.GetPlayerActionAttemptByIdAsAdmin
{
    public sealed record GetPlayerActionAttemptByIdAsAdminQuery(Guid id) : IRequest<AdminResultPlayerActionAttemptDTO?>;
}
