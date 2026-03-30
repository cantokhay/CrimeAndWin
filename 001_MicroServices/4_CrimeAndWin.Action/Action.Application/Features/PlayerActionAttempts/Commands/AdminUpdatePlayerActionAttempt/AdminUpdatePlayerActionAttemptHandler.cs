using Action.Domain.Entities;
using Action.Domain.Enums;
using Action.Domain.VOs;
using MediatR;
using Shared.Domain.Repository;
using Shared.Domain.Time;

namespace Action.Application.Features.PlayerActionAttempts.Commands.AdminUpdatePlayerActionAttempt
{
    public sealed class AdminUpdatePlayerActionAttemptHandler
            : IRequestHandler<AdminUpdatePlayerActionAttemptCommand, bool>
    {
        private readonly IReadRepository<PlayerActionAttempt> _read;
        private readonly IWriteRepository<PlayerActionAttempt> _write;
        private readonly IDateTimeProvider _time;

        public AdminUpdatePlayerActionAttemptHandler(
            IReadRepository<PlayerActionAttempt> read,
            IWriteRepository<PlayerActionAttempt> write,
            IDateTimeProvider time)
        {
            _read = read;
            _write = write;
            _time = time;
        }

        public async Task<bool> Handle(AdminUpdatePlayerActionAttemptCommand request, CancellationToken ct)
        {
            var d = request.Dto;
            var entity = await _read.GetByIdAsync(d.Id.ToString(), tracking: true);
            if (entity is null) return false;

            var outcome = d.SuccessRate >= 0.5 ? OutcomeType.Success : OutcomeType.Fail;

            entity.PlayerId = d.PlayerId;
            entity.ActionDefinitionId = d.ActionDefinitionId;
            entity.PlayerActionResults = new PlayerActionResult(d.SuccessRate, outcome);
            entity.UpdatedAtUtc = _time.UtcNow;

            var ok = _write.Update(entity);
            await _write.SaveAsync();
            return ok;
        }
    }
}
