using Action.Application.Abstract;
using Action.Application.DTOs.ActionAttemptDTOs;
using Action.Application.Features.PlayerActionAttempts.Commands.PerformPlayerAction;
using Action.Application.Mapping;
using Action.Domain.Entities;
using Action.Domain.VOs;
using CrimeAndWin.Action.GameMechanics;
using CrimeAndWin.Contracts.Events.Action;
using FluentAssertions;
using MassTransit;
using Moq;
using Shared.Domain.Repository;
using Shared.Domain.Time;
using Xunit;

namespace Action.Application.Test.Features.PlayerActionAttempts.Commands.PerformPlayerAction
{
    public class PerformPlayerActionHandlerDeepTests
    {
        private readonly Mock<IReadRepository<ActionDefinition>> _mockActionRead;
        private readonly Mock<IWriteRepository<PlayerActionAttempt>> _mockAttemptWrite;
        private readonly Mock<IPlayerProfileService> _mockProfileService;
        private readonly Mock<IPublishEndpoint> _mockPublishEndpoint;
        private readonly PerformPlayerActionHandler _handler;

        public PerformPlayerActionHandlerDeepTests()
        {
            _mockActionRead = new Mock<IReadRepository<ActionDefinition>>();
            _mockAttemptWrite = new Mock<IWriteRepository<PlayerActionAttempt>>();
            _mockProfileService = new Mock<IPlayerProfileService>();
            _mockPublishEndpoint = new Mock<IPublishEndpoint>();

            _handler = new PerformPlayerActionHandler(
                _mockAttemptWrite.Object,
                _mockActionRead.Object, 
                _mockProfileService.Object,
                _mockPublishEndpoint.Object);
        }

        [Fact]
        public async Task Handle_ActionNotFound_ThrowsException()
        {
            var actionId = Guid.NewGuid();
            var playerId = Guid.NewGuid();

            _mockActionRead.Setup(x => x.GetByIdAsync(actionId.ToString(), false)).ReturnsAsync((ActionDefinition)null!);

            var command = new PerformPlayerActionCommand(new PlayerActionAttemptDTO { ActionDefinitionId = actionId, PlayerId = playerId });

            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
