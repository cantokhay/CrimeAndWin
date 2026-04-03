using Leadership.Application.Mapping;
using Shared.Application.Abstractions.Messaging;
using Shared.Domain.Repository;

namespace Leadership.Application.Features.Leaderboard.Commands.CreateLeaderboard
{
    public class CreateLeaderboardHandler : IRequestHandler<CreateLeaderboardCommand, Guid>
    {
        private readonly IWriteRepository<Domain.Entities.Leaderboard> _writeRepo;
        private readonly LeadershipMapper _mapper;

        public CreateLeaderboardHandler(IWriteRepository<Domain.Entities.Leaderboard> writeRepo, LeadershipMapper mapper)
        {
            _writeRepo = writeRepo; _mapper = mapper;
        }


        public async Task<Guid> Handle(CreateLeaderboardCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.ToEntity(request.Dto);
            entity.Id = Guid.NewGuid();
            entity.CreatedAtUtc = DateTime.UtcNow;
            await _writeRepo.AddAsync(entity);
            await _writeRepo.SaveAsync();
            return entity.Id;
        }
    }
}


