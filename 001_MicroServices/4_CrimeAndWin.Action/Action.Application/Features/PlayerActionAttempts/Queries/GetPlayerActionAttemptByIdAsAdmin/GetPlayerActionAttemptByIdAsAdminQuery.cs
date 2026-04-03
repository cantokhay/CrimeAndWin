using Action.Application.DTOs.ActionAttemptDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Action.Application.Features.PlayerActionAttempts.Queries.GetPlayerActionAttemptByIdAsAdmin
{
    public sealed record GetPlayerActionAttemptByIdAsAdminQuery(Guid id) : IRequest<AdminResultPlayerActionAttemptDTO?>;
}


