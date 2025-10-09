using MediatR;
using Moderation.Application.DTOs.ModerationActionDTOs;

namespace Moderation.Application.Features.ModerationAction.Queries
{
    public record GetActionsByPlayerIdQuery(Guid PlayerId) : IRequest<List<ResultModerationActionDTO>>;
}
