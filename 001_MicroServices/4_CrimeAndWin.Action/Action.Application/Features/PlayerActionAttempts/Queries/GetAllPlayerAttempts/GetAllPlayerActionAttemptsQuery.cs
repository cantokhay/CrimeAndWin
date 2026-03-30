using Action.Application.DTOs.ActionAttemptDTOs;
using MediatR;

namespace Action.Application.Features.PlayerActionAttempts.Queries.GetAllPlayerAttempts
{
    public sealed record GetAllPlayerActionAttemptsQuery()
        : IRequest<List<ResultPlayerActionAttemptDTO>>;
}
