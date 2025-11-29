using Action.Domain.Entities;
using Action.Domain.Enums;
using Action.Domain.VOs;
using MediatR;
using Shared.Domain.Repository;
using Shared.Domain.Time;

namespace Action.Application.Features.PlayerActionAttempts.Commands.AdminCreatePlayerActionAttempt
{
    public sealed class AdminCreatePlayerActionAttemptHandler
            : IRequestHandler<AdminCreatePlayerActionAttemptCommand, Guid>
    {
        private readonly IWriteRepository<PlayerActionAttempt> _write;
        private readonly IDateTimeProvider _time;

        public AdminCreatePlayerActionAttemptHandler(IWriteRepository<PlayerActionAttempt> write, IDateTimeProvider time)
        {
            _write = write;
            _time = time;
        }

        public async Task<Guid> Handle(AdminCreatePlayerActionAttemptCommand request, CancellationToken ct)
        {
            var d = request.Dto;
            var outcome = d.SuccessRate >= 0.5 ? OutcomeType.Success : OutcomeType.Fail;

            var entity = new PlayerActionAttempt
            {
                Id = Guid.NewGuid(),
                PlayerId = d.PlayerId,
                ActionDefinitionId = d.ActionDefinitionId,
                PlayerActionResults = new PlayerActionResult(d.SuccessRate, outcome),
                CreatedAtUtc = _time.UtcNow
            };

            await _write.AddAsync(entity);
            await _write.SaveAsync();
            return entity.Id;
        }
    }
}
