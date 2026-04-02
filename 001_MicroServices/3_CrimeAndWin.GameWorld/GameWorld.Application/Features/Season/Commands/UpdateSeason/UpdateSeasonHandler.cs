using GameWorld.Application.Mapping;
using GameWorld.Application.DTOs.SeasonDTOs;
using GameWorld.Domain.VOs;
using Mediator;
using Shared.Domain.Repository;

namespace GameWorld.Application.Features.Season.Commands.UpdateSeason
{
    public sealed class UpdateSeasonHandler(
            IWriteRepository<Domain.Entities.Season> writeRepo,
            IReadRepository<Domain.Entities.Season> readRepo,
            GameWorldMapper mapper)
            : IRequestHandler<UpdateSeasonCommand, UpdateSeasonDTO>
    {
        public async ValueTask<UpdateSeasonDTO> Handle(UpdateSeasonCommand request, CancellationToken cancellationToken)
        {
            var entity = await readRepo.GetByIdAsync(request.SeasonId.ToString());
            if (entity is null)
                throw new KeyNotFoundException("Season bulunamadı.");

            entity.GameWorldId = request.GameWorldId;
            entity.SeasonNumber = request.SeasonNumber;
            entity.DateRange = new DateRange(request.StartUtc, request.EndUtc);
            entity.IsActive = request.IsActive;
            entity.UpdatedAtUtc = DateTime.UtcNow;

            writeRepo.Update(entity);
            await writeRepo.SaveAsync();

            return mapper.ToUpdateDto(entity);
        }
    }
}

