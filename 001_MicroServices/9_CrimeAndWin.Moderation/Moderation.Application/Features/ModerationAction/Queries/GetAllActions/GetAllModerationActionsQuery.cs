using MediatR;
using Moderation.Application.DTOs.ModerationActionDTOs;

namespace Moderation.Application.Features.ModerationAction.Queries.GetAllActions
{
    public sealed record GetAllModerationActionsQuery() : IRequest<List<ResultModerationActionDTO>>;
}
