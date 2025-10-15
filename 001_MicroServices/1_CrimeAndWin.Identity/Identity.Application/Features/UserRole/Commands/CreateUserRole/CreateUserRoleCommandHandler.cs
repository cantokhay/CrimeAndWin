using MediatR;
using Shared.Domain.Repository;
using Shared.Domain.Time;

namespace Identity.Application.Features.UserRole.Commands.CreateUserRole
{
    public class CreateUserRoleCommandHandler : IRequestHandler<CreateUserRoleCommand, Guid>
    {
        private readonly IWriteRepository<Domain.Entities.UserRole> _writeRepository;
        private readonly IDateTimeProvider _time;

        public CreateUserRoleCommandHandler(IWriteRepository<Domain.Entities.UserRole> writeRepository, IDateTimeProvider time)
        {
            _writeRepository = writeRepository;
            _time = time;
        }

        public async Task<Guid> Handle(CreateUserRoleCommand request, CancellationToken cancellationToken)
        {
            var dto = request.createUserRoleDTO;

            var entity = new Domain.Entities.UserRole
            {
                Id = Guid.NewGuid(),
                UserId = dto.UserId,
                RoleId = dto.RoleId,
                CreatedAtUtc = _time.UtcNow
            };

            await _writeRepository.AddAsync(entity);
            await _writeRepository.SaveAsync();

            return entity.Id;
        }
    }
}
