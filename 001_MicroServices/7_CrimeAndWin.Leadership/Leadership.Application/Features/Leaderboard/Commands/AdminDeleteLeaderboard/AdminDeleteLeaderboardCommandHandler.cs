using MediatR;
using Shared.Domain.Repository;

namespace Leadership.Application.Features.Leaderboard.Commands.AdminDeleteLeaderboard
{
    public sealed class AdminDeleteLeaderboardCommandHandler
            : IRequestHandler<AdminDeleteLeaderboardCommand, bool>
    {
        private readonly IWriteRepository<Domain.Entities.Leaderboard> _write;

        public AdminDeleteLeaderboardCommandHandler(IWriteRepository<Domain.Entities.Leaderboard> write)
        {
            _write = write;
        }

        public async Task<bool> Handle(AdminDeleteLeaderboardCommand request, CancellationToken cancellationToken)
        {
            var ok = await _write.RemoveAsync(request.id.ToString());
            await _write.SaveAsync();
            return ok;
        }
    }
}
