using Action.Application.DTOs.ActionAttemptDTOs;
using Shared.Application.Abstractions.Messaging;

namespace Action.Application.Features.PlayerActionAttempts.Queries.GetAllPlayerAttempts
{
    public sealed record GetAllPlayerActionAttemptsQuery()
        : IRequest<List<ResultPlayerActionAttemptDTO>>;
}


