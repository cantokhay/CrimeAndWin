using MediatR;
using Moderation.Application.DTOs.ModerationActionDTOs.Admin;

namespace Moderation.Application.Features.ModerationAction.Commands.AdminUpdateModerationAction
{
    public sealed record AdminUpdateModerationActionCommand(AdminUpdateModerationActionDTO updateModerationActionDTO) : IRequest<bool>;
}
