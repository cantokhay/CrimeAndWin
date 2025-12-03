using MediatR;
using Shared.Domain.Repository;

namespace Leadership.Application.Features.LeaderboardEntry.Commands.AdminDeleteLeaderboardEntry
{
    public sealed class AdminDeleteLeaderboardEntryCommandHandler
            : IRequestHandler<AdminDeleteLeaderboardEntryCommand, bool>
    {
        private readonly IWriteRepository<Domain.Entities.LeaderboardEntry> _write;

        public AdminDeleteLeaderboardEntryCommandHandler(IWriteRepository<Domain.Entities.LeaderboardEntry> write)
        {
            _write = write;
        }

        public async Task<bool> Handle(AdminDeleteLeaderboardEntryCommand request, CancellationToken cancellationToken)
        {
            var ok = await _write.RemoveAsync(request.id.ToString());
            await _write.SaveAsync();
            return ok;
        }
    }
}
