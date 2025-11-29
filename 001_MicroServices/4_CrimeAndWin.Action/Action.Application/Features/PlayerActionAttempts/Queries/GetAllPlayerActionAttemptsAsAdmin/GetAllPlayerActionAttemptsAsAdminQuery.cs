using Action.Application.DTOs.ActionAttemptDTOs.Admin;
using MediatR;

namespace Action.Application.Features.PlayerActionAttempts.Queries.GetAllPlayerActionAttemptsAsAdmin
{
    public sealed record GetAllPlayerActionAttemptsAsAdminQuery() : IRequest<List<AdminResultPlayerActionAttemptDTO>>;
}
