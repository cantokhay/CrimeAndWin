using AutoMapper;
using MediatR;
using Shared.Domain.Repository;

namespace Leadership.Application.Features.Leaderboard.Commands.CreateLeaderboard
{
    public class CreateLeaderboardHandler : IRequestHandler<CreateLeaderboardCommand, Guid>
    {
        private readonly IWriteRepository<Domain.Entities.Leaderboard> _writeRepo;
        private readonly IMapper _mapper;


        public CreateLeaderboardHandler(IWriteRepository<Domain.Entities.Leaderboard> writeRepo, IMapper mapper)
        {
            _writeRepo = writeRepo; _mapper = mapper;
        }


        public async Task<Guid> Handle(CreateLeaderboardCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Domain.Entities.Leaderboard>(request.Dto);
            entity.Id = Guid.NewGuid();
            entity.CreatedAtUtc = DateTime.UtcNow;
            await _writeRepo.AddAsync(entity);
            await _writeRepo.SaveAsync();
            return entity.Id;
        }
    }
}
