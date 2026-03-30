using MediatR;
using Shared.Domain.Repository;
using Shared.Domain.Time;

namespace Identity.Application.Features.Role.Commands.CreateRole
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Guid>
    {
        private readonly IWriteRepository<Domain.Entities.Role> _writeRepository;
        private readonly IDateTimeProvider _time;

        public CreateRoleCommandHandler(IWriteRepository<Domain.Entities.Role> writeRepository, IDateTimeProvider time)
        {
            _writeRepository = writeRepository;
            _time = time;
        }

        public async Task<Guid> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var dto = request.createRoleDTO;

            var role = new Domain.Entities.Role
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                NormalizedName = dto.NormalizedName,
                Description = dto.Description,
                CreatedAtUtc = _time.UtcNow
            };

            await _writeRepository.AddAsync(role);
            await _writeRepository.SaveAsync();

            return role.Id;
        }
    }
}
