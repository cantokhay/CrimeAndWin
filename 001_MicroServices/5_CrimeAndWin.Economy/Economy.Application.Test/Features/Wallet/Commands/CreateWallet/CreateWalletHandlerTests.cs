using System.Linq.Expressions;
using Economy.Application.Features.Wallet.Commands.CreateWallet;
using FluentAssertions;
using Moq;
using Shared.Domain.Repository;
using Xunit;

namespace Economy.Application.Test.Features.Wallet.Commands.CreateWallet
{
    public class CreateWalletHandlerTests
    {
        private readonly Mock<IWriteRepository<Domain.Entities.Wallet>> _mockWalletWrite;
        private readonly Mock<IReadRepository<Domain.Entities.Wallet>> _mockWalletRead;
        private readonly CreateWalletHandler _handler;

        public CreateWalletHandlerTests()
        {
            _mockWalletWrite = new Mock<IWriteRepository<Domain.Entities.Wallet>>();
            _mockWalletRead = new Mock<IReadRepository<Domain.Entities.Wallet>>();
            _handler = new CreateWalletHandler(_mockWalletWrite.Object, _mockWalletRead.Object);
        }

        [Fact]
        public async Task Handle_WalletAlreadyExists_ReturnsFalse()
        {
            // Arrange
            var playerId = Guid.NewGuid();
            var existingWallet = new Domain.Entities.Wallet { PlayerId = playerId };
            _mockWalletRead.Setup(x => x.GetSingleAsync(It.IsAny<Expression<Func<Domain.Entities.Wallet, bool>>>(), true))
                .ReturnsAsync(existingWallet);

            var command = new CreateWalletCommand { PlayerId = playerId };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeFalse();
            _mockWalletWrite.Verify(x => x.AddAsync(It.IsAny<Domain.Entities.Wallet>()), Times.Never);
        }

        [Fact]
        public async Task Handle_NewWallet_ReturnsTrue()
        {
            // Arrange
            var playerId = Guid.NewGuid();
            _mockWalletRead.Setup(x => x.GetSingleAsync(It.IsAny<Expression<Func<Domain.Entities.Wallet, bool>>>(), true))
                .ReturnsAsync((Domain.Entities.Wallet)null!);
            
            _mockWalletWrite.Setup(x => x.SaveAsync()).ReturnsAsync(1);

            var command = new CreateWalletCommand { PlayerId = playerId };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeTrue();
            _mockWalletWrite.Verify(x => x.AddAsync(It.Is<Domain.Entities.Wallet>(w => w.PlayerId == playerId)), Times.Once);
            _mockWalletWrite.Verify(x => x.SaveAsync(), Times.Once);
        }
    }
}
