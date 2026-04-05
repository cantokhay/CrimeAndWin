using Identity.Application.DTOs.UserDTOs;
using Identity.Domain.Entities;
using Shared.Application.Abstractions.Messaging;
using Microsoft.AspNetCore.Identity;
using Shared.Domain.Repository;

namespace Identity.Application.Features.User.Commands.RegisterUser
{
    public sealed class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, AppUserDTO>
    {
        private readonly IWriteRepository<AppUser> _write;
        private readonly Shared.Infrastructure.Mail.IMailService _mail;

        public RegisterUserCommandHandler(IWriteRepository<AppUser> write, Shared.Infrastructure.Mail.IMailService mail)
        {
            _write = write;
            _mail = mail;
        }

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
                IsDeleted = false,
                IsApproved = false,
                ActivationToken = Guid.NewGuid().ToString("N")
            };

            user.PasswordHash = hasher.HashPassword(user, request.Password);

            await _write.AddAsync(user);
            await _write.SaveAsync();

            // Send Activation Email
            var activationLink = $"http://localhost:6001/api/Auth/ConfirmEmail?email={user.Email}&token={user.ActivationToken}";
            var mailBody = $@"
                <h1>Welcome to CrimeAndWin</h1>
                <p>Hello {user.UserName}, please activate your account by clicking the link below:</p>
                <p><a href='{activationLink}'>Activate My Account</a></p>
                <br/>
                <p>After email confirmation, our team will review and approve your account.</p>";

            await _mail.SendEmailAsync(user.Email, "Activate Your CrimeAndWin Account", mailBody);

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


