using MediatR;
using Shared.Domain.Repository;
using Shared.Domain.Time;

namespace Identity.Application.Features.UserLogin.Commands.CreateUserLogin
{
    public class CreateUserLoginCommandHandler : IRequestHandler<CreateUserLoginCommand, Guid>
    {
        private readonly IWriteRepository<Domain.Entities.UserLogin> _writeRepository;
        private readonly IDateTimeProvider _time;

        public CreateUserLoginCommandHandler(IWriteRepository<Domain.Entities.UserLogin> writeRepository, IDateTimeProvider time)
        {
            _writeRepository = writeRepository;
            _time = time;
        }

        public async Task<Guid> Handle(CreateUserLoginCommand request, CancellationToken cancellationToken)
        {
            var dto = request.createUserLoginDTO;

            var entity = new Domain.Entities.UserLogin
            {
                Id = Guid.NewGuid(),
                UserId = dto.UserId,
                LoginProvider = dto.LoginProvider,
                ProviderKey = dto.ProviderKey,
                ProviderDisplayName = dto.ProviderDisplayName,
                CreatedAtUtc = _time.UtcNow
            };

            await _writeRepository.AddAsync(entity);
            await _writeRepository.SaveAsync();

            return entity.Id;
        }
    }
}
