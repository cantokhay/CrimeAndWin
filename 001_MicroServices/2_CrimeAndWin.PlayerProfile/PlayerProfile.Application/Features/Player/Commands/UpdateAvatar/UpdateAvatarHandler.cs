using MediatR;
using Shared.Domain.Repository;

namespace PlayerProfile.Application.Features.Player.Commands.UpdateAvatar
{
    public sealed class UpdateAvatarHandler(IWriteRepository<Domain.Entities.Player> writeRepo, IReadRepository<Domain.Entities.Player> readRepo) : IRequestHandler<UpdateAvatarCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateAvatarCommand r, CancellationToken ct)
        {
            var p = await readRepo.GetByIdAsync(r.PlayerId.ToString()) ?? throw new Exception(nameof(Domain.Entities.Player));
            p.AvatarKey = r.AvatarKey;
            writeRepo.Update(p);
            await writeRepo.SaveAsync();
            return Unit.Value;
        }
    }
}
