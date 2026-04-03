using Shared.Application.Abstractions.Messaging;
using Moderation.Application.DTOs.ModerationActionDTOs;

namespace Moderation.Application.Features.ModerationAction.Commands.CreateRestriction
{
    public record CreateRestrictCommand(CreateRestrictDTO Dto) : IRequest<Guid>;
}


