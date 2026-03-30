using Action.Application.DTOs.ActionAttemptDTOs.Admin;
using Action.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Repository;

namespace Action.Application.Features.PlayerActionAttempts.Queries.GetAllPlayerActionAttemptsAsAdmin
{
    public sealed class GetAllPlayerActionAttemptsAsAdminHandler
            : IRequestHandler<GetAllPlayerActionAttemptsAsAdminQuery, List<AdminResultPlayerActionAttemptDTO>>
    {
        private readonly IReadRepository<PlayerActionAttempt> _read;

        public GetAllPlayerActionAttemptsAsAdminHandler(IReadRepository<PlayerActionAttempt> read)
        {
            _read = read;
        }

        public async Task<List<AdminResultPlayerActionAttemptDTO>> Handle(GetAllPlayerActionAttemptsAsAdminQuery request, CancellationToken ct)
        {
            return await _read.GetAll(false)
                .Select(x => new AdminResultPlayerActionAttemptDTO
                {
                    Id = x.Id,
                    PlayerId = x.PlayerId,
                    ActionDefinitionId = x.ActionDefinitionId,
                    SuccessRate = x.PlayerActionResults.SuccessRate,
                    OutcomeType = x.PlayerActionResults.OutcomeType.ToString(),
                    CreatedAtUtc = x.CreatedAtUtc,
                    UpdatedAtUtc = x.UpdatedAtUtc
                })
                .ToListAsync(ct);
        }
    }
}
