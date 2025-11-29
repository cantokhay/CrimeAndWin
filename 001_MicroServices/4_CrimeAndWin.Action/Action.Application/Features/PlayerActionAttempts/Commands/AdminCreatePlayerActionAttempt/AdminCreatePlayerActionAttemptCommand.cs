using Action.Application.DTOs.ActionAttemptDTOs.Admin;
using MediatR;

namespace Action.Application.Features.PlayerActionAttempts.Commands.AdminCreatePlayerActionAttempt
{
    public sealed record AdminCreatePlayerActionAttemptCommand(AdminCreatePlayerActionAttemptDTO Dto) : IRequest<Guid>;
}
