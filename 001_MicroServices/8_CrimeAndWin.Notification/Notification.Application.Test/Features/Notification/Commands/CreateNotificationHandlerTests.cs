using AutoMapper;
using FluentAssertions;
using Moq;
using Notification.Application.Features.Notification.Commands.CreateNotification;
using Notification.Domain.VOs;
using Shared.Domain.Repository;
using Xunit;

namespace Notification.Application.Test.Features.Notification.Commands
{
    public class CreateNotificationHandlerTests
    {
        private readonly Mock<IWriteRepository<Domain.Entities.Notification>> _mockWrite;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CreateNotificationHandler _handler;

        public CreateNotificationHandlerTests()
        {
            _mockWrite = new Mock<IWriteRepository<Domain.Entities.Notification>>();
            _mockMapper = new Mock<IMapper>();
            _handler = new CreateNotificationHandler(_mockWrite.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ValidRequest_CreatesNotificationWithContent()
        {
            // Arrange
            var playerId = Guid.NewGuid();
            var title = "You won!";
            var message = "Congratulations on your first crime!";
            var type = "System";

            var command = new CreateNotificationCommand 
            { 
                PlayerId = playerId,
                Title = title,
                Message = message,
                Type = type
            };

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockWrite.Verify(x => x.AddAsync(It.Is<Domain.Entities.Notification>(n => 
                n.PlayerId == playerId && 
                n.Content.Title == title && 
                n.Content.Message == message && 
                n.Content.Type == type)), Times.Once);

            _mockWrite.Verify(x => x.SaveAsync(), Times.Once);
        }
    }
}
