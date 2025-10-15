using MediatR;
using Shared.Domain.Repository;
using Shared.Domain.Time;

namespace Identity.Application.Features.UserClaim.Commands.CreateUserClaim
{
    public class CreateUserClaimCommandHandler : IRequestHandler<CreateUserClaimCommand, Guid>
    {
        private readonly IWriteRepository<Domain.Entities.UserClaim> _writeRepository;
        private readonly IDateTimeProvider _time;

        public CreateUserClaimCommandHandler(IWriteRepository<Domain.Entities.UserClaim> writeRepository, IDateTimeProvider time)
        {
            _writeRepository = writeRepository;
            _time = time;
        }

        public async Task<Guid> Handle(CreateUserClaimCommand request, CancellationToken cancellationToken)
        {
            var dto = request.createUserClaimDTO;

            var entity = new Domain.Entities.UserClaim
            {
                Id = Guid.NewGuid(),
                UserId = dto.UserId,
                ClaimType = dto.ClaimType,
                ClaimValue = dto.ClaimValue,
                CreatedAtUtc = _time.UtcNow
            };

            await _writeRepository.AddAsync(entity);
            await _writeRepository.SaveAsync();

            return entity.Id;
        }
    }
}
