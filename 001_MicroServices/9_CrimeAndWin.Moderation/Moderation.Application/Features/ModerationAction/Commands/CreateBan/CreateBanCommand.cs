using MediatR;
using Moderation.Application.DTOs.ModerationActionDTOs;

namespace Moderation.Application.Features.ModerationAction.Commands.CreateBan
{
    public record CreateBanCommand(CreateBanDTO Dto) : IRequest<Guid>;
}
