using Action.Application.DTOs.ActionAttemptDTOs.Admin;
using Mediator;

namespace Action.Application.Features.PlayerActionAttempts.Commands.AdminUpdatePlayerActionAttempt
{
    public sealed record AdminUpdatePlayerActionAttemptCommand(AdminUpdatePlayerActionAttemptDTO Dto) : IRequest<bool>;
}

