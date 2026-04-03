using FluentAssertions;
using Moq;
using Moderation.Application.Features.Report.Commands.CreateReport;
using Moderation.Application.Mapping;
using Moderation.Application.Messaging.Abstract;
using Shared.Domain.Repository;
using Xunit;
using Moderation.Application.DTOs.ReportDTOs;

namespace Moderation.Application.Test.Features.Report.Commands
{
    public class CreateReportHandlerTests
    {
        private readonly Mock<IWriteRepository<Domain.Entities.Report>> _mockWrite;
        private readonly Mock<IEventPublisher> _mockPublisher;
        private readonly ModerationMapper _mapper;
        private readonly CreateReportHandler _handler;

        public CreateReportHandlerTests()
        {
            _mockWrite = new Mock<IWriteRepository<Domain.Entities.Report>>();
            _mockPublisher = new Mock<IEventPublisher>();
            _mapper = new ModerationMapper();
            _handler = new CreateReportHandler(_mockWrite.Object, _mapper, _mockPublisher.Object);
        }

        [Fact]
        public async Task Handle_ValidReport_PersistenceAndEventIntegrity()
        {
            // Arrange
            var reporterId = Guid.NewGuid();
            var reportedId = Guid.NewGuid();
            var reason = "Cheat - Wallhack";

            var dto = new CreateReportDTO
            {
                ReporterId = reporterId,
                ReportedPlayerId = reportedId,
                Reason = reason
            };

            var command = new CreateReportCommand(dto);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeEmpty();

            // Verify persistence
            _mockWrite.Verify(x => x.AddAsync(It.Is<Domain.Entities.Report>(r =>
                r.ReporterId == reporterId &&
                r.ReportedPlayerId == reportedId &&
                r.Reason.Value == reason &&
                !r.IsResolved)), Times.Once);

            _mockWrite.Verify(x => x.SaveAsync(), Times.Once);

            _mockPublisher.Verify(x => x.PublishAsync(It.Is<Messaging.Concrete.IntegrationEvents.ReportCreatedIntegrationEvent>(e =>
                e.ReporterId == reporterId &&
                e.ReportedPlayerId == reportedId &&
                e.Reason == reason), null), Times.Once);
        }
    }
}

