using AutoMapper;
using GameWorld.Application.DTOs.GameWorldDTOs;
using GameWorld.Domain.VOs;
using MediatR;
using Shared.Domain.Repository;

namespace GameWorld.Application.Features.GameWorld.Commands.CreateGameWorld
{
    public class CreateGameWorldCommandHandler : IRequestHandler<CreateGameWorldCommand, CreateGameWorldDTO>
    {
        private readonly IWriteRepository<Domain.Entities.GameWorld> _repo;
        private readonly IMapper _mapper;

        public CreateGameWorldCommandHandler(IWriteRepository<Domain.Entities.GameWorld> repo, IMapper mapper)
        {
            _repo = repo; _mapper = mapper;
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

            return _mapper.Map<CreateGameWorldDTO>(entity);
        }
    }
}