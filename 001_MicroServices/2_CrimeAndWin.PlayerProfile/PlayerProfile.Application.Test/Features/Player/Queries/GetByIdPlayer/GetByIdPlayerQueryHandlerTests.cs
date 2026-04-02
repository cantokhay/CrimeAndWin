using FluentAssertions;
using Moq;
using PlayerProfile.Application.DTOs.PlayerDTOs;
using PlayerProfile.Application.Features.Player.Queries.GetByIdPlayer;
using PlayerProfile.Application.Mapping;
using PlayerProfile.Domain.Entities;
using PlayerProfile.Domain.VOs;
using Shared.Domain.Repository;
using Shared.Domain.Time;
using Xunit;

namespace PlayerProfile.Application.Test.Features.Player.Queries.GetByIdPlayer
{
    public class GetByIdPlayerQueryHandlerTests
    {
        private readonly Mock<IReadRepository<Player>> _mockRead;
        private readonly Mock<IWriteRepository<Player>> _mockWrite;
        private readonly Mock<IDateTimeProvider> _mockClock;
        private readonly GetByIdPlayerQueryHandler _handler;

        public GetByIdPlayerQueryHandlerTests()
        {
            _mockRead = new Mock<IReadRepository<Player>>();
            _mockWrite = new Mock<IWriteRepository<Player>>();
            _mockClock = new Mock<IDateTimeProvider>();
            
            // Assuming Mapper is instantiatable or mocked
            var mapper = new PlayerProfileMapper();
            
            _handler = new GetByIdPlayerQueryHandler(
                _mockRead.Object, 
                _mockWrite.Object, 
                mapper, 
                _mockClock.Object);
        }

        [Fact]
        public async Task Handle_PlayerProfileNotFound_ReturnsNull()
        {
            // Arrange
            var query = new GetByIdPlayerQuery { Id = Guid.NewGuid() };
            _mockRead.Setup(x => x.GetByIdAsync(It.IsAny<string>(), true)).ReturnsAsync((Player)null!);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task Handle_PlayerNeedsEnergyRegen_UpdatesEnergy()
        {
            // Arrange
            var playerId = Guid.NewGuid();
            var now = new DateTime(2026, 4, 2, 22, 0, 0, DateTimeKind.Utc);
            var lastCalc = now.AddMinutes(-10); // 10 minutes passed

            var player = new Player 
            { 
                Id = playerId, 
                Energy = new Energy(50, 100, 2), // 2 regen per min
                LastEnergyCalcUtc = lastCalc 
            };

            _mockClock.Setup(x => x.UtcNow).Returns(now);
            _mockRead.Setup(x => x.GetByIdAsync(playerId.ToString(), true)).ReturnsAsync(player);

            var query = new GetByIdPlayerQuery { Id = playerId };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            // 50 + (10 * 2) = 70
            player.Energy.Current.Should().Be(70);
            player.LastEnergyCalcUtc.Should().Be(now);
            
            _mockWrite.Verify(x => x.Update(player), Times.Once);
            _mockWrite.Verify(x => x.SaveAsync(), Times.Once);
        }
    }
}
