using Action.Application.Abstract;
using Action.Domain.Entities;
using AutoMapper;
using CrimeAndWin.Contracts.Events.Action;
using MediatR;
using Shared.Domain.Repository;

namespace Action.Application.Features.PlayerActionAttempts.Commands.PerformPlayerAction
{
    public sealed class PerformPlayerActionHandler
        : IRequestHandler<PerformPlayerActionCommand, Guid>
    {
        private readonly IReadRepository<ActionDefinition> _actionRead;
        private readonly IWriteRepository<PlayerActionAttempt> _attemptWrite;
        private readonly IMapper _mapper;
        private readonly IEventPublisher _publisher;

        public PerformPlayerActionHandler(
            IReadRepository<ActionDefinition> actionRead,
            IWriteRepository<PlayerActionAttempt> attemptWrite,
            IMapper mapper,
            IEventPublisher publisher)
        {
            _actionRead = actionRead;
            _attemptWrite = attemptWrite;
            _mapper = mapper;
            _publisher = publisher;
        }

        public async Task<Guid> Handle(PerformPlayerActionCommand request, CancellationToken ct)
        {
            var def = await _actionRead.GetByIdAsync(request.Request.ActionDefinitionId.ToString());
            if (def is null || !def.IsActive) throw new InvalidOperationException("Action not available.");

            var attempt = _mapper.Map<PlayerActionAttempt>(request.Request);
            attempt.Id = Guid.NewGuid();
            attempt.CorrelationId = Guid.NewGuid();
            attempt.AttemptedAtUtc = DateTime.UtcNow;
            attempt.CreatedAtUtc = DateTime.UtcNow;

            await _attemptWrite.AddAsync(attempt);
            await _attemptWrite.SaveAsync();

            await _publisher.PublishAsync(new CrimeCompletedEvent
            {
                CorrelationId = attempt.CorrelationId,
                PlayerId = attempt.PlayerId,
                ActionId = attempt.Id,
                ActionType = def.Code,
                IsSuccess = true, // Simplified assuming creation success implies business success
                MoneyReward = def.Rewards.MoneyGain,
                ExpReward = def.Rewards.PowerGain,
                EnergyCost = def.Requirements.EnergyCost,
                ItemRewardId = def.Rewards.ItemDrop ? Guid.NewGuid() : null // random item if bool is true
            }, ct);

            return attempt.Id;
        }
    }
}
