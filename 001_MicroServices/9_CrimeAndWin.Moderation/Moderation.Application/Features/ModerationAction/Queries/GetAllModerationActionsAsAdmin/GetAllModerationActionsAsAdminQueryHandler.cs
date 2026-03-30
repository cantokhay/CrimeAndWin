using MediatR;
using Microsoft.EntityFrameworkCore;
using Moderation.Application.DTOs.ModerationActionDTOs.Admin;
using Shared.Domain.Repository;

namespace Moderation.Application.Features.ModerationAction.Queries.GetAllModerationActionsAsAdmin
{
    public sealed class GetAllModerationActionsAsAdminQueryHandler
            : IRequestHandler<GetAllModerationActionsAsAdminQuery, List<AdminResultModerationActionDTO>>
    {
        private readonly IReadRepository<Domain.Entities.ModerationAction> _read;

        public GetAllModerationActionsAsAdminQueryHandler(IReadRepository<Domain.Entities.ModerationAction> read)
        {
            _read = read;
        }

        public async Task<List<AdminResultModerationActionDTO>> Handle(GetAllModerationActionsAsAdminQuery request, CancellationToken cancellationToken)
        {
            return await _read.GetAll(false)
                .Select(m => new AdminResultModerationActionDTO
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
                })
                .ToListAsync(cancellationToken);
        }
    }
}
