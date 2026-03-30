using MediatR;
using Shared.Domain.Repository;
using Shared.Domain.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.UserRole.Commands.UpdateUserRole
{
    public class UpdateUserRoleCommandHandler : IRequestHandler<UpdateUserRoleCommand, bool>
    {
        private readonly IWriteRepository<Domain.Entities.UserRole> _writeRepository;
        private readonly IReadRepository<Domain.Entities.UserRole> _readRepository;
        private readonly IDateTimeProvider _time;

        public UpdateUserRoleCommandHandler(
            IWriteRepository<Domain.Entities.UserRole> writeRepository,
            IReadRepository<Domain.Entities.UserRole> readRepository,
            IDateTimeProvider time)
        {
            _writeRepository = writeRepository;
            _readRepository = readRepository;
            _time = time;
        }

        public async Task<bool> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
        {
            var dto = request.updateUserRoleDTO;
            var entity = await _readRepository.GetByIdAsync(dto.Id.ToString());
            if (entity == null) return false;

            entity.UserId = dto.UserId;
            entity.RoleId = dto.RoleId;
            entity.UpdatedAtUtc = _time.UtcNow;

            var result = _writeRepository.Update(entity);
            await _writeRepository.SaveAsync();
            return result;
        }
    }
}
