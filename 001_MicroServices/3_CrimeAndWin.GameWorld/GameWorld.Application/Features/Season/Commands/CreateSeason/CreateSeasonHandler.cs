using AutoMapper;
using GameWorld.Application.DTOs.SeasonDTOs;
using GameWorld.Domain.VOs;
using MediatR;
using Shared.Domain.Repository;

namespace GameWorld.Application.Features.Season.Commands.CreateSeason
{
    public class AddSeasonHandler : IRequestHandler<CreateSeasonCommand, CreateSeasonDTO>
    {
        private readonly IReadRepository<Domain.Entities.GameWorld> _gwReadRepo;
        private readonly IWriteRepository<Domain.Entities.Season> _seasonWriteRepo;
        private readonly IMapper _mapper;

        public AddSeasonHandler(IReadRepository<Domain.Entities.GameWorld> gwReadRepo, IWriteRepository<Domain.Entities.Season> seasonWriteRepo, IMapper mapper)
        {
            _gwReadRepo = gwReadRepo; _seasonWriteRepo = seasonWriteRepo; _mapper = mapper;
        }

        public async Task<CreateSeasonDTO> Handle(CreateSeasonCommand request, CancellationToken ct)
        {
            var gw = await _gwReadRepo.GetByIdAsync(request.GameWorldId.ToString());
            if (gw is null) throw new KeyNotFoundException("Game World not found.");

            var season = new Domain.Entities.Season
            {
                Id = Guid.NewGuid(),
                GameWorldId = gw.Id,
                SeasonNumber = request.SeasonNumber,
                DateRange = new DateRange(request.StartUtc, request.EndUtc),
                IsActive = request.IsActive,
                CreatedAtUtc = DateTime.UtcNow
            };

            await _seasonWriteRepo.AddAsync(season);
            await _seasonWriteRepo.SaveAsync();

            return _mapper.Map<CreateSeasonDTO>(season);
        }
    }
}
