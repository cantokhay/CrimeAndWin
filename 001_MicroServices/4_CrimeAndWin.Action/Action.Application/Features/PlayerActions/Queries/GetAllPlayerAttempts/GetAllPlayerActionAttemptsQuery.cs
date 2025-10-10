using Action.Application.DTOs;
using MediatR;

namespace Action.Application.Features.PlayerActions.Queries.GetAllPlayerAttempts
{
    public sealed record GetAllPlayerActionAttemptsQuery()
        : IRequest<List<ResultPlayerActionAttemptDTO>>;
}
