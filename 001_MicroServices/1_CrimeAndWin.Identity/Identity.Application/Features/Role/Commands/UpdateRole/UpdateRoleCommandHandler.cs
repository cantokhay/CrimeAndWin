using MediatR;
using Shared.Domain.Repository;
using Shared.Domain.Time;

namespace Identity.Application.Features.Role.Commands.UpdateRole
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, bool>
    {
        private readonly IWriteRepository<Domain.Entities.Role> _writeRepository;
        private readonly IReadRepository<Domain.Entities.Role> _readRepository;
        private readonly IDateTimeProvider _time;

        public UpdateRoleCommandHandler(IWriteRepository<Domain.Entities.Role> writeRepository, IReadRepository<Domain.Entities.Role> readRepository, IDateTimeProvider time)
        {
            _writeRepository = writeRepository;
            _readRepository = readRepository;
            _time = time;
        }

        public async Task<bool> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var dto = request.updateRoleDTO;
            var role = await _readRepository.GetByIdAsync(dto.Id.ToString());
            if (role == null) return false;

            role.Name = dto.Name;
            role.NormalizedName = dto.NormalizedName;
            role.Description = dto.Description;
            role.UpdatedAtUtc = _time.UtcNow;

            var result = _writeRepository.Update(role);
            await _writeRepository.SaveAsync();
            return result;
        }
    }
}
