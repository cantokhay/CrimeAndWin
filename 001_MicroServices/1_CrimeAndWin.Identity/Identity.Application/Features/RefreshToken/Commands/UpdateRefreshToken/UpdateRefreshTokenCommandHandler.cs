using MediatR;
using Shared.Domain.Repository;
using Shared.Domain.Time;

namespace Identity.Application.Features.RefreshToken.Commands.UpdateRefreshToken
{
    public class UpdateRefreshTokenCommandHandler : IRequestHandler<UpdateRefreshTokenCommand, bool>
    {
        private readonly IWriteRepository<Domain.Entities.RefreshToken> _writeRepository;
        private readonly IReadRepository<Domain.Entities.RefreshToken> _readRepository;
        private readonly IDateTimeProvider _time;

        public UpdateRefreshTokenCommandHandler(
            IWriteRepository<Domain.Entities.RefreshToken> writeRepository,
            IReadRepository<Domain.Entities.RefreshToken> readRepository,
            IDateTimeProvider time)
        {
            _writeRepository = writeRepository;
            _readRepository = readRepository;
            _time = time;
        }

        public async Task<bool> Handle(UpdateRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var dto = request.updateRefreshTokenDTO;
            var entity = await _readRepository.GetByIdAsync(dto.Id.ToString());
            if (entity == null) return false;

            entity.UserId = dto.UserId;
            entity.Token = dto.Token;
            entity.ExpiresAtUtc = dto.ExpiresAtUtc;
            entity.RevokedAtUtc = dto.RevokedAtUtc;
            entity.ReplacedByToken = dto.ReplacedByToken;
            entity.UpdatedAtUtc = _time.UtcNow;

            var result = _writeRepository.Update(entity);
            await _writeRepository.SaveAsync();
            return result;
        }
    }
}
