using Action.Application.Abstract;
using Action.Domain.Entities;
using CrimeAndWin.Contracts.Events.Action;
using MassTransit;
using Shared.Application.Abstractions.Messaging;
using Shared.Domain.Repository;
using CrimeAndWin.Action.GameMechanics;

namespace Action.Application.Features.PlayerActionAttempts.Commands.PerformPlayerAction
{
    public class PerformPlayerActionHandler : IRequestHandler<PerformPlayerActionCommand, PerformPlayerActionResult>
    {
        private readonly IWriteRepository<PlayerActionAttempt> _attemptRepository;
        private readonly IReadRepository<ActionDefinition> _actionRepository;
        private readonly IPlayerProfileService _profileService;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly BribeCalculator _bribeCalculator;

        public PerformPlayerActionHandler(
            IWriteRepository<PlayerActionAttempt> attemptRepository,
            IReadRepository<ActionDefinition> actionRepository,
            IPlayerProfileService profileService,
            IPublishEndpoint publishEndpoint)
        {
            _attemptRepository = attemptRepository;
            _actionRepository = actionRepository;
            _profileService = profileService;
            _publishEndpoint = publishEndpoint;
            _bribeCalculator = new BribeCalculator();
        }

        public async Task<PerformPlayerActionResult> Handle(PerformPlayerActionCommand request, CancellationToken cancellationToken)
        {
            var actionDef = await _actionRepository.GetByIdAsync(request.Request.ActionDefinitionId.ToString());
            if (actionDef is null)
                throw new Exception("Action not found");

            var playerLevel = await _profileService.GetPlayerLevelAsync(request.Request.PlayerId);

            var playerProfile = new
            {
                HeatIndex = 0, // Replace with actual value
                RespectScore = 0, // Replace with actual value
            };

            bool isSuccess = false;
            double successChance = (double)actionDef.BaseSuccessRate;

            // SPECIAL LOGIC: Bribe Mechanics (Faz 4)
            if (actionDef.Type == Domain.Enums.ActionType.Bribe)
            {
                var bribeResult = _bribeCalculator.Calculate(playerProfile.HeatIndex, playerProfile.RespectScore, actionDef.BaseSuccessRate);
                successChance = bribeResult.SuccessProbability;

                // Random Roll
                var roll = new Random().NextDouble() * 100;
                isSuccess = roll <= successChance;
            }
            else
            {
                // Standard Success Logic
                var roll = new Random().NextDouble() * 100;
                isSuccess = roll <= successChance;
            }

            // Publish Event to start Saga
            await _publishEndpoint.Publish(new CrimeActionStartedEvent
            {
                CorrelationId = Guid.NewGuid(),
                PlayerId = request.Request.PlayerId,
                ActionId = request.Request.ActionDefinitionId,
            }, cancellationToken);

            return new PerformPlayerActionResult { IsSuccess = isSuccess, Message = isSuccess ? "Basari!" : "Basarisiz!" };
        }
    }
}
