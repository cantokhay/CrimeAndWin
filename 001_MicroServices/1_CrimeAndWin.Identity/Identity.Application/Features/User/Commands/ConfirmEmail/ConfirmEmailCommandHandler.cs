using Identity.Domain.Entities;
using Shared.Application.Abstractions.Messaging;
using Shared.Domain.Repository;

namespace Identity.Application.Features.User.Commands.ConfirmEmail
{
    public sealed class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, bool>
    {
        private readonly IWriteRepository<AppUser> _userWrite;
        private readonly IReadRepository<AppUser> _userRead;

        public ConfirmEmailCommandHandler(IWriteRepository<AppUser> userWrite, IReadRepository<AppUser> userRead)
        {
            _userWrite = userWrite;
            _userRead = userRead;
        }

        public async Task<bool> Handle(ConfirmEmailCommand request, CancellationToken ct)
        {
            var user = await _userRead.GetAsync(x => x.Email == request.Email && x.ActivationToken == request.Token);

            if (user == null)
            {
                // Hata: Yanlış token veya kullanıcı bulunamadı
                return false;
            }

            if (user.EmailConfirmed)
            {
                // Zaten onaylanmış
                return true;
            }

            user.EmailConfirmed = true;
            user.ActivationToken = null; // Token kullanıldı

            _userWrite.Update(user);
            await _userWrite.SaveAsync();

            return true;
        }
    }
}
