using Action.Application.DTOs.ActionAttemptDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Action.Application.Features.PlayerActionAttempts.Commands.AdminUpdatePlayerActionAttempt
{
    public sealed record AdminUpdatePlayerActionAttemptCommand(AdminUpdatePlayerActionAttemptDTO Dto) : IRequest<bool>;
}


