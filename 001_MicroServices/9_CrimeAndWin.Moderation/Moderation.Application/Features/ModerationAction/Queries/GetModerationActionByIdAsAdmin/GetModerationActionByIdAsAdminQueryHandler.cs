using MediatR;
using Moderation.Application.DTOs.ModerationActionDTOs.Admin;
using Shared.Domain.Repository;

namespace Moderation.Application.Features.ModerationAction.Queries.GetModerationActionByIdAsAdmin
{
    public sealed class GetModerationActionByIdAsAdminQueryHandler
            : IRequestHandler<GetModerationActionByIdAsAdminQuery, AdminResultModerationActionDTO?>
    {
        private readonly IReadRepository<Domain.Entities.ModerationAction> _read;

        public GetModerationActionByIdAsAdminQueryHandler(IReadRepository<Domain.Entities.ModerationAction> read)
        {
            _read = read;
        }

        public async Task<AdminResultModerationActionDTO?> Handle(GetModerationActionByIdAsAdminQuery request, CancellationToken cancellationToken)
        {
            var m = await _read.GetByIdAsync(request.id.ToString(), tracking: false);
            if (m is null) return null;

            return new AdminResultModerationActionDTO
            {
                Id = m.Id,
                PlayerId = m.PlayerId,
                ActionType = m.ActionType,
                Reason = m.Reason,
                ActionDateUtc = m.ActionDateUtc,
                ExpiryDateUtc = m.ExpiryDateUtc,
                ModeratorId = m.ModeratorId,
                IsActive = m.IsActive,
                CreatedAtUtc = m.CreatedAtUtc,
                UpdatedAtUtc = m.UpdatedAtUtc
            };
        }
    }
}
