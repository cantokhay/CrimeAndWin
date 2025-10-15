using Identity.Domain.Entities;
using MediatR;
using Shared.Domain.Repository;
using Shared.Domain.Time;

namespace Identity.Application.Features.User.Commands.UpdateAppUser
{
    public class UpdateAppUserCommandHandler : IRequestHandler<UpdateAppUserCommand, bool>
    {
        private readonly IWriteRepository<AppUser> _writeRepository;
        private readonly IReadRepository<AppUser> _readRepository;
        private readonly IDateTimeProvider _time;

        public UpdateAppUserCommandHandler(
            IWriteRepository<AppUser> writeRepository,
            IReadRepository<AppUser> readRepository,
            IDateTimeProvider time)
        {
            _writeRepository = writeRepository;
            _readRepository = readRepository;
            _time = time;
        }

        public async Task<bool> Handle(UpdateAppUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _readRepository.GetByIdAsync(request.updateAppUserDTO.Id.ToString());
            if (user == null) return false;

            user.UserName = request.updateAppUserDTO.UserName;
            user.Email = request.updateAppUserDTO.Email;
            user.PhoneNumber = request.updateAppUserDTO.PhoneNumber;
            user.LockoutEnabled = request.updateAppUserDTO.LockoutEnabled;
            user.UpdatedAtUtc = _time.UtcNow;

            var updated = _writeRepository.Update(user);
            await _writeRepository.SaveAsync();
            return updated;
        }
    }
}
