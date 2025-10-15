using MediatR;
using Shared.Domain.Repository;
using Shared.Domain.Time;

namespace Identity.Application.Features.UserClaim.Commands.UpdateUserClaim
{
    public class UpdateUserClaimCommandHandler : IRequestHandler<UpdateUserClaimCommand, bool>
    {
        private readonly IWriteRepository<Domain.Entities.UserClaim> _writeRepository;
        private readonly IReadRepository<Domain.Entities.UserClaim> _readRepository;
        private readonly IDateTimeProvider _time;

        public UpdateUserClaimCommandHandler(
            IWriteRepository<Domain.Entities.UserClaim> writeRepository,
            IReadRepository<Domain.Entities.UserClaim> readRepository,
            IDateTimeProvider time)
        {
            _writeRepository = writeRepository;
            _readRepository = readRepository;
            _time = time;
        }

        public async Task<bool> Handle(UpdateUserClaimCommand request, CancellationToken cancellationToken)
        {
            var dto = request.updateUserClaimDTO;
            var entity = await _readRepository.GetByIdAsync(dto.Id.ToString());
            if (entity == null) return false;

            entity.UserId = dto.UserId;
            entity.ClaimType = dto.ClaimType;
            entity.ClaimValue = dto.ClaimValue;
            entity.UpdatedAtUtc = _time.UtcNow;

            var result = _writeRepository.Update(entity);
            await _writeRepository.SaveAsync();
            return result;
        }
    }
}
