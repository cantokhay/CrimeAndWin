using MediatR;
using Shared.Domain.Repository;
using Shared.Domain.Time;

namespace Identity.Application.Features.UserToken.Commands.CreateUserToken
{
    public class CreateUserTokenCommandHandler : IRequestHandler<CreateUserTokenCommand, Guid>
    {
        private readonly IWriteRepository<Domain.Entities.UserToken> _writeRepository;
        private readonly IDateTimeProvider _time;

        public CreateUserTokenCommandHandler(IWriteRepository<Domain.Entities.UserToken> writeRepository, IDateTimeProvider time)
        {
            _writeRepository = writeRepository;
            _time = time;
        }

        public async Task<Guid> Handle(CreateUserTokenCommand request, CancellationToken cancellationToken)
        {
            var dto = request.createUserTokenDTO;

            var entity = new Domain.Entities.UserToken
            {
                Id = Guid.NewGuid(),
                UserId = dto.UserId,
                LoginProvider = dto.LoginProvider,
                Name = dto.Name,
                Value = dto.Value,
                CreatedAtUtc = _time.UtcNow
            };

            await _writeRepository.AddAsync(entity);
            await _writeRepository.SaveAsync();

            return entity.Id;
        }
    }
}
