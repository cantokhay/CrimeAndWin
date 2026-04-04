using GameWorld.Application.Mapping;
using GameWorld.Application.DTOs.GameWorldDTOs;
using GameWorld.Domain.VOs;
using Shared.Application.Abstractions.Messaging;
using Shared.Domain.Repository;

namespace GameWorld.Application.Features.GameWorld.Commands.CreateGameWorld
{
    public class CreateGameWorldCommandHandler : IRequestHandler<CreateGameWorldCommand, CreateGameWorldDTO>
    {
        private readonly IWriteRepository<Domain.Entities.GameWorld> _repo;

        public CreateGameWorldCommandHandler(IWriteRepository<Domain.Entities.GameWorld> repo)
        {
            _repo = repo;
        }

        public async Task<CreateGameWorldDTO> Handle(CreateGameWorldCommand request, CancellationToken ct)
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

            return GameWorldMapper.ToCreateDto(entity);
        }
    }
}
