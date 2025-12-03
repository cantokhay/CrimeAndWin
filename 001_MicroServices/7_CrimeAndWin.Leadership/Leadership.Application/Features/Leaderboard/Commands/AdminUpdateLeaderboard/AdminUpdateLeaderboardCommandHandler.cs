using MediatR;
using Shared.Domain.Repository;
using Shared.Domain.Time;

namespace Leadership.Application.Features.Leaderboard.Commands.AdminUpdateLeaderboard
{
    public sealed class AdminUpdateLeaderboardCommandHandler
           : IRequestHandler<AdminUpdateLeaderboardCommand, bool>
    {
        private readonly IReadRepository<Domain.Entities.Leaderboard> _read;
        private readonly IWriteRepository<Domain.Entities.Leaderboard> _write;
        private readonly IDateTimeProvider _time;

        public AdminUpdateLeaderboardCommandHandler(
            IReadRepository<Domain.Entities.Leaderboard> read,
            IWriteRepository<Domain.Entities.Leaderboard> write,
            IDateTimeProvider time)
        {
            _read = read;
            _write = write;
            _time = time;
        }

        public async Task<bool> Handle(AdminUpdateLeaderboardCommand request, CancellationToken cancellationToken)
        {
            var d = request.updateLeaderboardDTO;
            var entity = await _read.GetByIdAsync(d.Id.ToString(), tracking: true);
            if (entity is null) return false;

            entity.Name = d.Name;
            entity.Description = d.Description;
            entity.GameWorldId = d.GameWorldId;
            entity.SeasonId = d.SeasonId;
            entity.IsSeasonal = d.IsSeasonal;
            entity.UpdatedAtUtc = _time.UtcNow;

            var ok = _write.Update(entity);
            await _write.SaveAsync();

            return ok;
        }
    }
}
