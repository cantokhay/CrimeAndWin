using Shared.Application.Abstractions.Messaging;
using Moderation.Application.DTOs.ModerationActionDTOs.Admin;

namespace Moderation.Application.Features.ModerationAction.Queries.GetModerationActionByIdAsAdmin
{
    public sealed record GetModerationActionByIdAsAdminQuery(Guid id) : IRequest<AdminResultModerationActionDTO?>;
}


