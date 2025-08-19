using AutoMapper;
using MediatR;
using PlayerProfile.Application.DTOs.PlayerDTOs;
using Shared.Domain.Repository;
using Shared.Domain.Time;

namespace PlayerProfile.Application.Features.Player.Queries.GetByIdPlayer
{
    public sealed class GetByIdPlayerHandler(
        IReadRepository<Domain.Entities.Player> readRepo, IWriteRepository<Domain.Entities.Player> writeRepo, IMapper mapper, IDateTimeProvider clock)
        : IRequestHandler<GetByIdPlayerQuery, ResultPlayerDTO?>
    {
        public async Task<ResultPlayerDTO?> Handle(GetByIdPlayerQuery q, CancellationToken ct)
        {
            var p = await readRepo.GetByIdAsync(q.Id.ToString());
            if (p is null) return null;

            var minutes = (int)(clock.UtcNow - p.LastEnergyCalcUtc).TotalMinutes;
            if (minutes > 0 && p.Energy.Current < p.Energy.Max)
            {
                var gained = Math.Min(minutes * p.Energy.RegenPerMinute, p.Energy.Max - p.Energy.Current);
                p.Energy = p.Energy with { Current = p.Energy.Current + gained };
                p.LastEnergyCalcUtc = clock.UtcNow;
                writeRepo.Update(p);
                await writeRepo.SaveAsync();
            }

            return mapper.Map<ResultPlayerDTO>(p);
        }
    }
}
