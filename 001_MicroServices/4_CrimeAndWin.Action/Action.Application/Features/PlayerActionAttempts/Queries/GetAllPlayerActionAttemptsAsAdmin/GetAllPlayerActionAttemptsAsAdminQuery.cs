using Action.Application.DTOs.ActionAttemptDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Action.Application.Features.PlayerActionAttempts.Queries.GetAllPlayerActionAttemptsAsAdmin
{
    public sealed record GetAllPlayerActionAttemptsAsAdminQuery() : IRequest<List<AdminResultPlayerActionAttemptDTO>>;
}


