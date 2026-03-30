using MediatR;
using Shared.Domain.Repository;
using Shared.Domain.Time;

namespace Moderation.Application.Features.ModerationAction.Commands.AdminCreateModerationAction
{
    public sealed class AdminCreateModerationActionCommandHandler
            : IRequestHandler<AdminCreateModerationActionCommand, Guid>
    {
        private readonly IWriteRepository<Domain.Entities.ModerationAction> _write;
        private readonly IDateTimeProvider _time;

        public AdminCreateModerationActionCommandHandler(
            IWriteRepository<Domain.Entities.ModerationAction> write,
            IDateTimeProvider time)
        {
            _write = write;
            _time = time;
        }

        public async Task<Guid> Handle(AdminCreateModerationActionCommand request, CancellationToken cancellationToken)
        {
            var d = request.createModerationActionDTO;

            var entity = new Domain.Entities.ModerationAction
            {
                Id = Guid.NewGuid(),
                PlayerId = d.PlayerId,
                ActionType = d.ActionType,
                Reason = d.Reason,
                ActionDateUtc = d.ActionDateUtc == default ? _time.UtcNow : d.ActionDateUtc,
                ExpiryDateUtc = d.ExpiryDateUtc,
                ModeratorId = d.ModeratorId,
                IsActive = d.IsActive,
                CreatedAtUtc = _time.UtcNow,
                IsDeleted = false
            };

            await _write.AddAsync(entity);
            await _write.SaveAsync();

            return entity.Id;
        }
    }
}
