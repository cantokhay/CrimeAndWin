using MediatR;
using Moderation.Application.DTOs.ModerationActionDTOs.Admin;

namespace Moderation.Application.Features.ModerationAction.Commands.AdminCreateModerationAction
{
    public sealed record AdminCreateModerationActionCommand(AdminCreateModerationActionDTO createModerationActionDTO) : IRequest<Guid>;
}
