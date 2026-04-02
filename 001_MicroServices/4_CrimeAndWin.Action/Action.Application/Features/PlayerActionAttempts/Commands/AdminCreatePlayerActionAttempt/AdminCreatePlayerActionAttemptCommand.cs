using Action.Application.DTOs.ActionAttemptDTOs.Admin;
using Mediator;

namespace Action.Application.Features.PlayerActionAttempts.Commands.AdminCreatePlayerActionAttempt
{
    public sealed record AdminCreatePlayerActionAttemptCommand(AdminCreatePlayerActionAttemptDTO Dto) : IRequest<Guid>;
}

