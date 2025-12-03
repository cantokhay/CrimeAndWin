using Leadership.Domain.VOs;
using MediatR;
using Shared.Domain.Repository;
using Shared.Domain.Time;

namespace Leadership.Application.Features.LeaderboardEntry.Commands.AdminCreateLeaderboardEntry
{
    public sealed class AdminCreateLeaderboardEntryCommandHandler
            : IRequestHandler<AdminCreateLeaderboardEntryCommand, Guid>
    {
        private readonly IWriteRepository<Domain.Entities.LeaderboardEntry> _write;
        private readonly IDateTimeProvider _time;

        public AdminCreateLeaderboardEntryCommandHandler(
            IWriteRepository<Domain.Entities.LeaderboardEntry> write,
            IDateTimeProvider time)
        {
            _write = write;
            _time = time;
        }

        public async Task<Guid> Handle(AdminCreateLeaderboardEntryCommand request, CancellationToken cancellationToken)
        {
            var d = request.createLeaderboardEntryDTO;

            var entity = new Domain.Entities.LeaderboardEntry
            {
                Id = Guid.NewGuid(),
                LeaderboardId = d.LeaderboardId,
                PlayerId = d.PlayerId,
                Rank = new Rank
                {
                    RankPoints = d.RankPoints,
                    Position = d.Position
                },
                IsActive = d.IsActive,
                CreatedAtUtc = _time.UtcNow,
                IsDeleted = false
            };

            await _write.AddAsync(entity);
            await _write.SaveAsync();

            return entity.Id;
        }
    }
}
