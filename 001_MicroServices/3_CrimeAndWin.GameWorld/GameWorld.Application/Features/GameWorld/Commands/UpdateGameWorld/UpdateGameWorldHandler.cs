using AutoMapper;
using GameWorld.Application.Abstract;
using GameWorld.Application.DTOs.GameWorldDTOs;
using MediatR;
using Shared.Domain.Repository;
using GameWorld.Domain.VOs;

namespace GameWorld.Application.Features.GameWorld.Commands.UpdateGameWorld
{
    public class UpdateGameWorldHandler : IRequestHandler<UpdateGameWorldCommand, UpdateGameWorldDTO>
    {
        private readonly IReadRepository<Domain.Entities.GameWorld> _readRepo;
        private readonly IWriteRepository<Domain.Entities.GameWorld> _writeRepo;
        private readonly IMapper _mapper;
        private readonly IEventBus _bus;

        public UpdateGameWorldHandler(IReadRepository<Domain.Entities.GameWorld> readRepo, IWriteRepository<Domain.Entities.GameWorld> writeRepo, IMapper mapper, IEventBus bus)
        {
            _readRepo = readRepo;
            _writeRepo = writeRepo;
            _mapper = mapper;
            _bus = bus;
        }

        public async Task<UpdateGameWorldDTO> Handle(UpdateGameWorldCommand request, CancellationToken ct)
        {
            var gw = await _readRepo.GetByIdAsync(request.GameWorldId.ToString());
            if (gw is null) throw new KeyNotFoundException("GameWorld not found.");

            gw.Rule = new GameRule(request.MaxEnergy, request.RegenRatePerHour);
            gw.UpdatedAtUtc = DateTime.UtcNow;

            _writeRepo.Update(gw);
            await _writeRepo.SaveAsync();

            await _bus.PublishAsync("gameworld.rules.updated", new
            {
                GameWorldId = gw.Id,
                gw.Rule.MaxEnergy,
                gw.Rule.RegenRatePerHour,
                OccurredUtc = DateTime.UtcNow
            }, ct);

            return _mapper.Map<UpdateGameWorldDTO>(gw);
        }
    }
}
