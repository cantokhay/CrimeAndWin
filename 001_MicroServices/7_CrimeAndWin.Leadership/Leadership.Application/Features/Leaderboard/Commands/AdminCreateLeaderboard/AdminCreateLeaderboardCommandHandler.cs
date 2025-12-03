using MediatR;
using Shared.Domain.Repository;
using Shared.Domain.Time;

namespace Leadership.Application.Features.Leaderboard.Commands.AdminCreateLeaderboard
{
    public sealed class AdminCreateLeaderboardCommandHandler
            : IRequestHandler<AdminCreateLeaderboardCommand, Guid>
    {
        private readonly IWriteRepository<Domain.Entities.Leaderboard> _write;
        private readonly IDateTimeProvider _time;

        public AdminCreateLeaderboardCommandHandler(
            IWriteRepository<Domain.Entities.Leaderboard> write,
            IDateTimeProvider time)
        {
            _write = write;
            _time = time;
        }

        public async Task<Guid> Handle(AdminCreateLeaderboardCommand request, CancellationToken cancellationToken)
        {
            var d = request.createLeaderboardDTO;

            var entity = new Domain.Entities.Leaderboard
            {
                Id = Guid.NewGuid(),
                Name = d.Name,
                Description = d.Description,
                GameWorldId = d.GameWorldId,
                SeasonId = d.SeasonId,
                IsSeasonal = d.IsSeasonal,
                CreatedAtUtc = _time.UtcNow,
                Entries = new List<Domain.Entities.LeaderboardEntry>()
            };

            await _write.AddAsync(entity);
            await _write.SaveAsync();

            return entity.Id;
        }
    }
}
