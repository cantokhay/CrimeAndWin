using MediatR;
using Shared.Domain.Repository;
using Shared.Domain.Time;

namespace Identity.Application.Features.UserLogin.Commands.UpdateUserLogin
{
    public class UpdateUserLoginCommandHandler : IRequestHandler<UpdateUserLoginCommand, bool>
    {
        private readonly IWriteRepository<Domain.Entities.UserLogin> _writeRepository;
        private readonly IReadRepository<Domain.Entities.UserLogin> _readRepository;
        private readonly IDateTimeProvider _time;

        public UpdateUserLoginCommandHandler(
            IWriteRepository<Domain.Entities.UserLogin> writeRepository,
            IReadRepository<Domain.Entities.UserLogin> readRepository,
            IDateTimeProvider time)
        {
            _writeRepository = writeRepository;
            _readRepository = readRepository;
            _time = time;
        }

        public async Task<bool> Handle(UpdateUserLoginCommand request, CancellationToken cancellationToken)
        {
            var dto = request.updateUserLoginDTO;
            var entity = await _readRepository.GetByIdAsync(dto.Id.ToString());
            if (entity == null) return false;

            entity.UserId = dto.UserId;
            entity.LoginProvider = dto.LoginProvider;
            entity.ProviderKey = dto.ProviderKey;
            entity.ProviderDisplayName = dto.ProviderDisplayName;
            entity.UpdatedAtUtc = _time.UtcNow;

            var result = _writeRepository.Update(entity);
            await _writeRepository.SaveAsync();
            return result;
        }
    }
}
