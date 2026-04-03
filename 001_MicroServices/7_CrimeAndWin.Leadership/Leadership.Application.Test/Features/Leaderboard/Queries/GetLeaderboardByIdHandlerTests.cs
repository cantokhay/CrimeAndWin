using FluentAssertions;
using Leadership.Application.Features.Leaderboard.Queries.GetLeaderbordById;
using Leadership.Application.Mapping;
using Leadership.Domain.Entities;
using Moq;
using Shared.Domain.Repository;
using Xunit;

namespace Leadership.Application.Test.Features.Leaderboard.Queries
{
    public class GetLeaderboardByIdHandlerTests
    {
        private readonly Mock<IReadRepository<Domain.Entities.Leaderboard>> _mockRead;
        private readonly LeadershipMapper _mapper;
        private readonly GetLeaderboardByIdHandler _handler;

        public GetLeaderboardByIdHandlerTests()
        {
            _mockRead = new Mock<IReadRepository<Domain.Entities.Leaderboard>>();
            _mapper = new LeadershipMapper();
            _handler = new GetLeaderboardByIdHandler(_mockRead.Object, _mapper);
        }

        [Fact]
        public async Task Handle_LeaderboardNotFound_ReturnsNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockRead.Setup(x => x.GetByIdAsync(id.ToString(), true)).ReturnsAsync((Domain.Entities.Leaderboard)null!);

            var query = new GetLeaderboardByIdQuery(id);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task Handle_ValidLeaderboard_ReturnsMappedDto()
        {
            // Arrange
            var id = Guid.NewGuid();
            var leaderboard = new Domain.Entities.Leaderboard
            {
                Id = id,
                Name = "Top Players",
                Entries = new List<LeaderboardEntry>
                {
                    new LeaderboardEntry
                    {
                        PlayerId = Guid.NewGuid(),
                        Rank = new Domain.VOs.Rank { RankPoints = 1000, Position = 1 }
                    }
                }
            };

            _mockRead.Setup(x => x.GetByIdAsync(id.ToString(), true)).ReturnsAsync(leaderboard);

            var query = new GetLeaderboardByIdQuery(id);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result!.Id.Should().Be(id);
            result.Name.Should().Be("Top Players");
            result.Entries.Should().HaveCount(1);
            result.Entries[0].PlayerId.Should().Be(leaderboard.Entries.First().PlayerId);
            result.Entries[0].RankPoints.Should().Be(1000);
            result.Entries[0].Position.Should().Be(1);
        }
    }
}

