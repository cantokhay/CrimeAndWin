using MediatR;
using Shared.Domain.Repository;
using Shared.Domain.Time;

namespace Moderation.Application.Features.ModerationAction.Commands.AdminUpdateModerationAction
{
    public sealed class AdminUpdateModerationActionCommandHandler
            : IRequestHandler<AdminUpdateModerationActionCommand, bool>
    {
        private readonly IReadRepository<Domain.Entities.ModerationAction> _read;
        private readonly IWriteRepository<Domain.Entities.ModerationAction> _write;
        private readonly IDateTimeProvider _time;

        public AdminUpdateModerationActionCommandHandler(
            IReadRepository<Domain.Entities.ModerationAction> read,
            IWriteRepository<Domain.Entities.ModerationAction> write,
            IDateTimeProvider time)
        {
            _read = read;
            _write = write;
            _time = time;
        }

        public async Task<bool> Handle(AdminUpdateModerationActionCommand request, CancellationToken cancellationToken)
        {
            var d = request.updateModerationActionDTO;

            var entity = await _read.GetByIdAsync(d.Id.ToString(), tracking: true);
            if (entity is null) return false;

            entity.PlayerId = d.PlayerId;
            entity.ActionType = d.ActionType;
            entity.Reason = d.Reason;
            entity.ActionDateUtc = d.ActionDateUtc;
            entity.ExpiryDateUtc = d.ExpiryDateUtc;
            entity.ModeratorId = d.ModeratorId;
            entity.IsActive = d.IsActive;
            entity.UpdatedAtUtc = _time.UtcNow;

            var ok = _write.Update(entity);
            await _write.SaveAsync();

            return ok;
        }
    }
}
