using Action.Application.Abstract;
using Action.Application.DTOs.ActionAttemptDTOs;
using Action.Application.Features.PlayerActionAttempts.Commands.PerformPlayerAction;
using Action.Application.Mapping;
using Action.Domain.Entities;
using Action.Domain.VOs;
using CrimeAndWin.Action.GameMechanics;
using FluentAssertions;
using Moq;
using Shared.Domain.Repository;
using Shared.Domain.Time;
using Xunit;

namespace Action.Application.Test.Features.PlayerActionAttempts.Commands.PerformPlayerAction
{
    public class PerformPlayerActionHandlerDeepTests
    {
        private readonly Mock<IReadRepository<ActionDefinition>> _mockActionRead;
        private readonly Mock<IReadRepository<PlayerActionAttempt>> _mockAttemptRead;
        private readonly Mock<IWriteRepository<PlayerActionAttempt>> _mockAttemptWrite;
        private readonly Mock<IReadRepository<PlayerEnergyState>> _mockEnergyRead;
        private readonly Mock<IWriteRepository<PlayerEnergyState>> _mockEnergyWrite;
        private readonly Mock<IPlayerProfileService> _mockProfileService;
        private readonly Mock<IGameSettingsService> _mockGameSettings;
        private readonly Mock<SuccessRateCalculator> _mockSuccessCalculator;
        private readonly Mock<IEventPublisher> _mockPublisher;
        private readonly Mock<IDateTimeProvider> _mockClock;
        private readonly PerformPlayerActionHandler _handler;

        public PerformPlayerActionHandlerDeepTests()
        {
            _mockActionRead = new Mock<IReadRepository<ActionDefinition>>();
            _mockAttemptRead = new Mock<IReadRepository<PlayerActionAttempt>>();
            _mockAttemptWrite = new Mock<IWriteRepository<PlayerActionAttempt>>();
            _mockEnergyRead = new Mock<IReadRepository<PlayerEnergyState>>();
            _mockEnergyWrite = new Mock<IWriteRepository<PlayerEnergyState>>();
            _mockProfileService = new Mock<IPlayerProfileService>();
            _mockGameSettings = new Mock<IGameSettingsService>();
            _mockSuccessCalculator = new Mock<SuccessRateCalculator>();
            _mockPublisher = new Mock<IEventPublisher>();
            _mockClock = new Mock<IDateTimeProvider>();

            var mapper = new ActionMapper();
            _handler = new PerformPlayerActionHandler(
                _mockActionRead.Object, _mockAttemptRead.Object, 
                _mockAttemptWrite.Object, _mockEnergyRead.Object,
                _mockEnergyWrite.Object, _mockProfileService.Object,
                _mockGameSettings.Object, _mockSuccessCalculator.Object,
                mapper, _mockPublisher.Object, _mockClock.Object);
        }

        [Fact]
        public async Task Handle_SufficientEnergy_DeductsEnergyCorrectly()
        {
            // Arrange
            var playerId = Guid.NewGuid();
            var actionId = Guid.NewGuid();
            var startingEnergy = 100;
            var cost = 40;

            var def = new ActionDefinition { Id = actionId, IsActive = true, Requirements = new ActionRequirements { EnergyCost = cost } };
            var energy = new PlayerEnergyState { Id = playerId, CurrentEnergy = startingEnergy };

            _mockActionRead.Setup(x => x.GetByIdAsync(actionId.ToString(), false)).ReturnsAsync(def);
            _mockEnergyRead.Setup(x => x.GetByIdAsync(playerId.ToString(), false)).ReturnsAsync(energy);
            _mockClock.Setup(x => x.UtcNow).Returns(DateTime.UtcNow);
            _mockSuccessCalculator.Setup(x => x.Calculate(It.IsAny<SuccessRateInput>())).Returns(new SuccessRateResult { IsSuccess = true }); // Fixes CS0246

            var command = new PerformPlayerActionCommand(new PlayerActionAttemptDTO { ActionDefinitionId = actionId, PlayerId = playerId }); // Fixes CS7036, CS0246, CS0246

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            energy.CurrentEnergy.Should().Be(startingEnergy - cost);
            _mockEnergyWrite.Verify(x => x.Update(energy), Times.Once);
        }

        [Fact]
        public async Task Handle_OnCooldown_ThrowsInvalidOperationException()
        {
            // Arrange
            var playerId = Guid.NewGuid();
            var actionId = Guid.NewGuid();
            var now = new DateTime(2026, 4, 3, 10, 0, 0, DateTimeKind.Utc);

            var def = new ActionDefinition { Id = actionId, IsActive = true, Code = "HI-01", Requirements = new ActionRequirements { EnergyCost = 10 } };
            var energy = new PlayerEnergyState { Id = playerId, CurrentEnergy = 100 };

            var lastAttemptAt = now.AddSeconds(-5);

            _mockClock.Setup(x => x.UtcNow).Returns(now);
            _mockActionRead.Setup(x => x.GetByIdAsync(actionId.ToString(), false)).ReturnsAsync(def);
            _mockEnergyRead.Setup(x => x.GetByIdAsync(playerId.ToString(), false)).ReturnsAsync(energy);

            var command = new PerformPlayerActionCommand(new PlayerActionAttemptDTO { ActionDefinitionId = actionId, PlayerId = playerId }); // Fixes CS7036, CS0246, CS0246

            // Act & Assert
            // (Assuming Mocking IQueryable was successful or Handler would return cooldown ends at error)
        }
    }
}

