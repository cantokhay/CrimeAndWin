using FluentAssertions;
using Moq;
using PlayerProfile.Application.Features.Player.Queries.GetByIdPlayer;
using PlayerProfile.Application.Mapping;
using Shared.Domain.Repository;
using Shared.Domain.Time;
using Xunit;

namespace PlayerProfile.Application.Test.Features.Player.Energy
{
    public class PlayerEnergyRegenTests
    {
        private readonly Mock<IReadRepository<Domain.Entities.Player>> _mockRead;
        private readonly Mock<IWriteRepository<Domain.Entities.Player>> _mockWrite;
        private readonly Mock<IDateTimeProvider> _mockClock;
        private readonly PlayerProfileMapper _mapper;
        private readonly GetByIdPlayerQueryHandler _handler;

        public PlayerEnergyRegenTests()
        {
            _mockRead = new Mock<IReadRepository<Domain.Entities.Player>>();
            _mockWrite = new Mock<IWriteRepository<Domain.Entities.Player>>();
            _mockClock = new Mock<IDateTimeProvider>();
            _mapper = new PlayerProfileMapper();
            
            _handler = new GetByIdPlayerQueryHandler(
                _mockRead.Object, 
                _mockWrite.Object, 
                _mapper, 
                _mockClock.Object);
        }

        [Theory]
        [InlineData(0, 50, 50)]     // No time passed, energy should remain same
        [InlineData(1, 48, 50)]     // 1 min passed, 2 regent (48+2=50)
        [InlineData(10, 20, 40)]    // 10 mins passed, 2 regen (20+20=40)
        [InlineData(100, 10, 100)]  // 100 mins passed, 2 regen (10+200 capped at 100)
        [InlineData(10, 100, 100)]  // 10 mins passed but already at max, stays 100
        public async Task Handle_EnergyCalculation_CalculatesCorrectRegen(int minutesPassed, int startingEnergy, int expectedEnergy)
        {
            // Arrange
            var playerId = Guid.NewGuid();
            var now = DateTime.UtcNow;
            var lastCalc = now.AddMinutes(-minutesPassed);

            var player = new Domain.Entities.Player 
            { 
                Id = playerId, 
                Energy = new Domain.VOs.Energy(startingEnergy, 100, 2), // 2 regen per minute
                LastEnergyCalcUtc = lastCalc 
            };

            _mockClock.Setup(x => x.UtcNow).Returns(now);
            _mockRead.Setup(x => x.GetByIdAsync(playerId.ToString(), true)).ReturnsAsync(player);

            var query = new GetByIdPlayerQuery(playerId);

            // Act
            await _handler.Handle(query, CancellationToken.None);

            // Assert
            player.Energy.Current.Should().Be(expectedEnergy);
            
            if (minutesPassed > 0 && startingEnergy < 100)
            {
                _mockWrite.Verify(x => x.Update(player), Times.Once);
                _mockWrite.Verify(x => x.SaveAsync(), Times.Once);
                player.LastEnergyCalcUtc.Should().Be(now);
            }
            else
            {
                // If no regen happened, we shouldn't save for performance
                _mockWrite.Verify(x => x.Update(player), Times.Never);
            }
        }
    }
}
