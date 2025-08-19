using Identity.Application.DTOs.UserDTOs;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.Domain.Repository;

namespace Identity.Application.Features.User.Commands.RegisterUser
{
    public sealed class RegisterUserHandler : IRequestHandler<RegisterUserCommand, AppUserDTO>
    {
        private readonly IWriteRepository<AppUser> _write;

        public RegisterUserHandler(IWriteRepository<AppUser> write) => _write = write;

        public async Task<AppUserDTO> Handle(RegisterUserCommand request, CancellationToken ct)
        {
            var hasher = new PasswordHasher<AppUser>();

            var user = new AppUser
            {
                Id = Guid.NewGuid(),
                UserName = request.UserName,
                NormalizedUserName = request.UserName.ToUpperInvariant(),
                Email = request.Email,
                NormalizedEmail = request.Email.ToUpperInvariant(),
                EmailConfirmed = false,
                SecurityStamp = Guid.NewGuid().ToString("N"),
                ConcurrencyStamp = Guid.NewGuid().ToString("N"),
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = true,
                AccessFailedCount = 0,
                CreatedAtUtc = DateTime.UtcNow,
                IsDeleted = false
            };

            user.PasswordHash = hasher.HashPassword(user, request.Password);

            await _write.AddAsync(user);
            await _write.SaveAsync();

            return new AppUserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed
            };
        }
    }
}
