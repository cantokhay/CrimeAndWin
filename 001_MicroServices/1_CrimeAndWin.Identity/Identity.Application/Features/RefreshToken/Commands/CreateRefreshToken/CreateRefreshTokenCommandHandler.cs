using MediatR;
using Shared.Domain.Repository;
using Shared.Domain.Time;

namespace Identity.Application.Features.RefreshToken.Commands.CreateRefreshToken
{
    public class CreateRefreshTokenCommandHandler : IRequestHandler<CreateRefreshTokenCommand, Guid>
    {
        private readonly IWriteRepository<Domain.Entities.RefreshToken> _writeRepository;
        private readonly IDateTimeProvider _time;

        public CreateRefreshTokenCommandHandler(IWriteRepository<Domain.Entities.RefreshToken> writeRepository, IDateTimeProvider time)
        {
            _writeRepository = writeRepository;
            _time = time;
        }

        public async Task<Guid> Handle(CreateRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var dto = request.createRefreshTokenDTO;

            var entity = new Domain.Entities.RefreshToken
            {
                Id = Guid.NewGuid(),
                UserId = dto.UserId,
                Token = dto.Token,
                ExpiresAtUtc = dto.ExpiresAtUtc,
                RevokedAtUtc = dto.RevokedAtUtc,
                ReplacedByToken = dto.ReplacedByToken,
                CreatedAtUtc = _time.UtcNow
            };

            await _writeRepository.AddAsync(entity);
            await _writeRepository.SaveAsync();

            return entity.Id;
        }
    }
}
