using AutoMapper;
using GameWorld.Application.DTOs.SeasonDTOs;
using GameWorld.Domain.VOs;
using MediatR;
using Shared.Domain.Repository;

namespace GameWorld.Application.Features.Season.Commands.UpdateSeason
{
    public sealed class UpdateSeasonHandler(
            IWriteRepository<Domain.Entities.Season> writeRepo,
            IReadRepository<Domain.Entities.Season> readRepo,
            IMapper mapper)
            : IRequestHandler<UpdateSeasonCommand, UpdateSeasonDTO>
    {
        public async Task<UpdateSeasonDTO> Handle(UpdateSeasonCommand request, CancellationToken cancellationToken)
        {
            var entity = await readRepo.GetByIdAsync(request.SeasonId.ToString());
            if (entity is null)
                throw new KeyNotFoundException("Season bulunamadı.");

            entity.SeasonNumber = request.SeasonNumber;
            entity.DateRange = new DateRange(request.StartUtc, request.EndUtc);
            entity.IsActive = request.IsActive;
            entity.UpdatedAtUtc = DateTime.UtcNow;

            writeRepo.Update(entity);
            await writeRepo.SaveAsync();

            return mapper.Map<UpdateSeasonDTO>(entity);
        }
    }
}
