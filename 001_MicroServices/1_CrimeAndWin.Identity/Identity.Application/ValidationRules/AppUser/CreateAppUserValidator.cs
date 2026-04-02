using FluentValidation;
using Identity.Application.DTOs.UserDTOs.Admin;
using Identity.Domain.Entities;
using Shared.Infrastructure.Validation;

namespace Identity.Application.ValidationRules.AppUser
{
    // --- DTO VALIDATORS ---
    public class CreateAppUserValidator : AbstractValidator<CreateAppUserDTO>
    {
        public CreateAppUserValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Kullanıcı adı boş olamaz.")
                .MinimumLength(3).WithMessage("Kullanıcı adı en az 3 karakter olmalıdır.")
                .MaximumLength(20).WithMessage("Kullanıcı adı en fazla 20 karakter olmalıdır.")
                .MatchesRegex("^[a-zA-Z0-9_]+$", "Kullanıcı Adı");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta adresi boş olamaz.")
                .EmailAddress().WithMessage("Geçersiz e-posta adresi.")
                .MaximumLength(100);

            RuleFor(x => x.PasswordHash)
                .NotEmpty().WithMessage("Şifre boş olamaz.")
                .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır.");
        }
    }

    // --- ENTITY VALIDATORS ---
    public class AppUserEntityValidator : AbstractValidator<AppUser>
    {
        public AppUserEntityValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().MaximumLength(256);
            RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(256);
            RuleFor(x => x.PasswordHash).NotEmpty();
            RuleFor(x => x.SecurityStamp).NotEmpty();
        }
    }

    public class RefreshTokenEntityValidator : AbstractValidator<RefreshToken>
    {
        public RefreshTokenEntityValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Token).NotEmpty();
            RuleFor(x => x.ExpiresAtUtc).IsFuture();
            RuleFor(x => x.CreatedAtUtc).IsPast();
        }
    }
}
