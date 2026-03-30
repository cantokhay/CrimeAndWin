using Leadership.Domain.VOs;
using MediatR;
using Shared.Domain.Repository;
using Shared.Domain.Time;

namespace Leadership.Application.Features.LeaderboardEntry.Commands.AdminUpdateLeaderboardEntry
{
    public sealed class AdminUpdateLeaderboardEntryCommandHandler
            : IRequestHandler<AdminUpdateLeaderboardEntryCommand, bool>
    {
        private readonly IReadRepository<Domain.Entities.LeaderboardEntry> _read;
        private readonly IWriteRepository<Domain.Entities.LeaderboardEntry> _write;
        private readonly IDateTimeProvider _time;

        public AdminUpdateLeaderboardEntryCommandHandler(
            IReadRepository<Domain.Entities.LeaderboardEntry> read,
            IWriteRepository<Domain.Entities.LeaderboardEntry> write,
            IDateTimeProvider time)
        {
            _read = read;
            _write = write;
            _time = time;
        }

        public async Task<bool> Handle(AdminUpdateLeaderboardEntryCommand request, CancellationToken cancellationToken)
        {
            var d = request.updateLeaderboardEntryDTO;

            var entity = await _read.GetByIdAsync(d.Id.ToString(), tracking: true);
            if (entity is null) return false;

            entity.LeaderboardId = d.LeaderboardId;
            entity.PlayerId = d.PlayerId;
            entity.Rank = new Rank
            {
                RankPoints = d.RankPoints,
                Position = d.Position
            };
            entity.IsActive = d.IsActive;
            entity.UpdatedAtUtc = _time.UtcNow;

            var ok = _write.Update(entity);
            await _write.SaveAsync();

            return ok;
        }
    }
}
