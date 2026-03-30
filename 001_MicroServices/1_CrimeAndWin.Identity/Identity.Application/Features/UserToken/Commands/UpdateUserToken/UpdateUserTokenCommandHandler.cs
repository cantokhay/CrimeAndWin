using MediatR;
using Shared.Domain.Repository;
using Shared.Domain.Time;

namespace Identity.Application.Features.UserToken.Commands.UpdateUserToken
{
    public class UpdateUserTokenCommandHandler : IRequestHandler<UpdateUserTokenCommand, bool>
    {
        private readonly IWriteRepository<Domain.Entities.UserToken> _writeRepository;
        private readonly IReadRepository<Domain.Entities.UserToken> _readRepository;
        private readonly IDateTimeProvider _time;

        public UpdateUserTokenCommandHandler(
            IWriteRepository<Domain.Entities.UserToken> writeRepository,
            IReadRepository<Domain.Entities.UserToken> readRepository,
            IDateTimeProvider time)
        {
            _writeRepository = writeRepository;
            _readRepository = readRepository;
            _time = time;
        }

        public async Task<bool> Handle(UpdateUserTokenCommand request, CancellationToken cancellationToken)
        {
            var dto = request.updateUserTokenDTO;
            var entity = await _readRepository.GetByIdAsync(dto.Id.ToString());
            if (entity == null) return false;

            entity.UserId = dto.UserId;
            entity.LoginProvider = dto.LoginProvider;
            entity.Name = dto.Name;
            entity.Value = dto.Value;
            entity.UpdatedAtUtc = _time.UtcNow;

            var result = _writeRepository.Update(entity);
            await _writeRepository.SaveAsync();
            return result;
        }
    }
}
