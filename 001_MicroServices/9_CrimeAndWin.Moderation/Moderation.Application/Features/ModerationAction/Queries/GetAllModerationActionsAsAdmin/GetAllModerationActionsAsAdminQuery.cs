using MediatR;
using Moderation.Application.DTOs.ModerationActionDTOs.Admin;

namespace Moderation.Application.Features.ModerationAction.Queries.GetAllModerationActionsAsAdmin
{
    public sealed record GetAllModerationActionsAsAdminQuery() : IRequest<List<AdminResultModerationActionDTO>>;
}
