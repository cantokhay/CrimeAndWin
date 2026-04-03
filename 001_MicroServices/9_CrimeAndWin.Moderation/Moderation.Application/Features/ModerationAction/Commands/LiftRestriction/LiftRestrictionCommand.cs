using Shared.Application.Abstractions.Messaging;
using Moderation.Application.DTOs.ModerationActionDTOs;

namespace Moderation.Application.Features.ModerationAction.Commands.LiftRestriction
{
    public record LiftRestrictionCommand(LiftRestrictionDTO Dto) : IRequest<bool>;
}


