using Action.Application.DTOs.ActionAttemptDTOs.Admin;
using Action.Domain.Entities;
using MediatR;
using Shared.Domain.Repository;

namespace Action.Application.Features.PlayerActionAttempts.Queries.GetPlayerActionAttemptByIdAsAdmin
{
    public sealed class GetPlayerActionAttemptByIdAsAdminHandler
           : IRequestHandler<GetPlayerActionAttemptByIdAsAdminQuery, AdminResultPlayerActionAttemptDTO?>
    {
        private readonly IReadRepository<PlayerActionAttempt> _read;

        public GetPlayerActionAttemptByIdAsAdminHandler(IReadRepository<PlayerActionAttempt> read)
        {
            _read = read;
        }

        public async Task<AdminResultPlayerActionAttemptDTO?> Handle(GetPlayerActionAttemptByIdAsAdminQuery request, CancellationToken ct)
        {
            var x = await _read.GetByIdAsync(request.id.ToString(), tracking: false);
            if (x is null) return null;

            return new AdminResultPlayerActionAttemptDTO
            {
                Id = x.Id,
                PlayerId = x.PlayerId,
                ActionDefinitionId = x.ActionDefinitionId,
                SuccessRate = x.PlayerActionResults.SuccessRate,
                OutcomeType = x.PlayerActionResults.OutcomeType.ToString(),
                CreatedAtUtc = x.CreatedAtUtc,
                UpdatedAtUtc = x.UpdatedAtUtc
            };
        }
    }
}
