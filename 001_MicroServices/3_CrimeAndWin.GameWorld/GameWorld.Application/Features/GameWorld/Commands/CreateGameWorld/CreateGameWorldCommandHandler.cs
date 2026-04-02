using GameWorld.Application.Mapping;
using GameWorld.Application.DTOs.GameWorldDTOs;
using GameWorld.Domain.VOs;
using Mediator;
using Shared.Domain.Repository;

namespace GameWorld.Application.Features.GameWorld.Commands.CreateGameWorld
{
    public class CreateGameWorldCommandHandler : IRequestHandler<CreateGameWorldCommand, CreateGameWorldDTO>
    {
        private readonly IWriteRepository<Domain.Entities.GameWorld> _repo;
        private readonly GameWorldMapper _mapper;

        public CreateGameWorldCommandHandler(IWriteRepository<Domain.Entities.GameWorld> repo, GameWorldMapper mapper)
        {
            _repo = repo; _mapper = mapper;
        }

        public async ValueTask<CreateGameWorldDTO> Handle(CreateGameWorldCommand request, CancellationToken ct)
        {
            var entity = new Domain.Entities.GameWorld
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Rule = new GameRule(request.MaxEnergy, request.RegenRatePerHour),
                Seasons = new List<Domain.Entities.Season>(),
                CreatedAtUtc = DateTime.UtcNow
            };

            await _repo.AddAsync(entity);
            await _repo.SaveAsync();

            return _mapper.ToCreateDto(entity);
        }
    }
}

