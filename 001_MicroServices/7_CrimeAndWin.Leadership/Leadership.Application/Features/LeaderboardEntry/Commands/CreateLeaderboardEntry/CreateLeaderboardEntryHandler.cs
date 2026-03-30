using Leadership.Domain.VOs;
using MediatR;
using Shared.Domain.Repository;

namespace Leadership.Application.Features.LeaderboardEntry.Commands.CreateLeaderboardEntry
{
    public class CreateLeaderboardEntryHandler : IRequestHandler<CreateLeaderboardEntryCommand, Guid>
    {
        private readonly IReadRepository<Domain.Entities.LeaderboardEntry> _readRepo;
        private readonly IWriteRepository<Domain.Entities.LeaderboardEntry> _writeRepo;


        public CreateLeaderboardEntryHandler(IReadRepository<Domain.Entities.LeaderboardEntry> readRepo, IWriteRepository<Domain.Entities.LeaderboardEntry> writeRepo)
        { _readRepo = readRepo; _writeRepo = writeRepo; }


        public async Task<Guid> Handle(CreateLeaderboardEntryCommand request, CancellationToken cancellationToken)
        {
            var existing = await _readRepo.GetSingleAsync(x => x.LeaderboardId == request.LeaderboardId && x.PlayerId == request.Dto.PlayerId);
            if (existing is null)
            {
                var entity = new Domain.Entities.LeaderboardEntry
                {
                    Id = Guid.NewGuid(),
                    LeaderboardId = request.LeaderboardId,
                    PlayerId = request.Dto.PlayerId,
                    Rank = new Rank { RankPoints = request.Dto.RankPoints, Position = request.Dto.Position },
                    IsActive = request.Dto.IsActive,
                    CreatedAtUtc = DateTime.UtcNow
                };
                await _writeRepo.AddAsync(entity);
                await _writeRepo.SaveAsync();
                return entity.Id;
            }
            else
            {
                existing.Rank = existing.Rank with { RankPoints = request.Dto.RankPoints, Position = request.Dto.Position };
                existing.IsActive = request.Dto.IsActive;
                existing.UpdatedAtUtc = DateTime.UtcNow;
                _writeRepo.Update(existing);
                await _writeRepo.SaveAsync();
                return existing.Id;
            }
        }
    }
}
