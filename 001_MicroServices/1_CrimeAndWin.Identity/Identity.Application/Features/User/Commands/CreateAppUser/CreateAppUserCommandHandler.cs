using Identity.Domain.Entities;
using MediatR;
using Shared.Domain.Repository;
using Shared.Domain.Time;

namespace Identity.Application.Features.User.Commands.CreateAppUser
{
    public class CreateAppUserCommandHandler : IRequestHandler<CreateAppUserCommand, Guid>
    {
        private readonly IWriteRepository<AppUser> _writeRepository;
        private readonly IDateTimeProvider _time;

        public CreateAppUserCommandHandler(IWriteRepository<AppUser> writeRepository, IDateTimeProvider time)
        {
            _writeRepository = writeRepository;
            _time = time;
        }

        public async Task<Guid> Handle(CreateAppUserCommand request, CancellationToken cancellationToken)
        {
            var dto = request.createAppUserDTO;

            var user = new AppUser
            {
                Id = Guid.NewGuid(),
                UserName = dto.UserName,
                NormalizedUserName = dto.UserName.ToUpperInvariant(),
                Email = dto.Email,
                NormalizedEmail = dto.Email.ToUpperInvariant(),
                EmailConfirmed = false,
                PasswordHash = dto.PasswordHash,
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                PhoneNumber = dto.PhoneNumber,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                AccessFailedCount = 0,
                CreatedAtUtc = _time.UtcNow
            };

            await _writeRepository.AddAsync(user);
            await _writeRepository.SaveAsync();

            return user.Id;
        }
    }

}
