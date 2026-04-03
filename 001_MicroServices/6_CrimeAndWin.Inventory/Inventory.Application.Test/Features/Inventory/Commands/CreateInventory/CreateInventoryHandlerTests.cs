using System.Linq.Expressions;
using FluentAssertions;
using Inventory.Application.Features.Inventory.Commands.CreateInventory;
using Moq;
using Shared.Domain.Repository;
using Shared.Domain.Time;
using Xunit;

namespace Inventory.Application.Test.Features.Inventory.Commands.CreateInventory
{
    public class CreateInventoryHandlerTests
    {
        private readonly Mock<IWriteRepository<Domain.Entities.Inventory>> _mockWrite;
        private readonly Mock<IReadRepository<Domain.Entities.Inventory>> _mockRead;
        private readonly Mock<IDateTimeProvider> _mockTime;
        private readonly CreateInventoryHandler _handler;

        public CreateInventoryHandlerTests()
        {
            _mockWrite = new Mock<IWriteRepository<Domain.Entities.Inventory>>();
            _mockRead = new Mock<IReadRepository<Domain.Entities.Inventory>>();
            _mockTime = new Mock<IDateTimeProvider>();
            _handler = new CreateInventoryHandler(_mockWrite.Object, _mockTime.Object, _mockRead.Object);
        }

        [Fact]
        public async Task Handle_InventoryAlreadyExists_ReturnsFalse()
        {
            // Arrange
            var playerId = Guid.NewGuid();
            var existingInventory = new Domain.Entities.Inventory { PlayerId = playerId };
            _mockRead.Setup(x => x.GetSingleAsync(It.IsAny<Expression<Func<Domain.Entities.Inventory, bool>>>(), true))
                .ReturnsAsync(existingInventory);

            var command = new CreateInventoryCommand(playerId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeFalse();
            _mockWrite.Verify(x => x.AddAsync(It.IsAny<Domain.Entities.Inventory>()), Times.Never);
        }

        [Fact]
        public async Task Handle_NewInventory_ReturnsTrue()
        {
            // Arrange
            var playerId = Guid.NewGuid();
            _mockRead.Setup(x => x.GetSingleAsync(It.IsAny<Expression<Func<Domain.Entities.Inventory, bool>>>(), true))
                .ReturnsAsync((Domain.Entities.Inventory)null!);

            _mockTime.Setup(x => x.UtcNow).Returns(DateTime.UtcNow);
            _mockWrite.Setup(x => x.SaveAsync()).ReturnsAsync(1);

            var command = new CreateInventoryCommand(playerId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeTrue();
            _mockWrite.Verify(x => x.AddAsync(It.Is<Domain.Entities.Inventory>(i => i.PlayerId == playerId)), Times.Once);
            _mockWrite.Verify(x => x.SaveAsync(), Times.Once);
        }
    }
}

