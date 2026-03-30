using MediatR;
using Shared.Domain.Repository;

namespace PlayerProfile.Application.Features.Player.Commands.AdminDeletePlayer
{
    public sealed class AdminDeletePlayerCommandHandler : IRequestHandler<AdminDeletePlayerCommand, bool>
    {
        private readonly IWriteRepository<Domain.Entities.Player> _write;

        public AdminDeletePlayerCommandHandler(IWriteRepository<Domain.Entities.Player> write)
        {
            _write = write;
        }

        public async Task<bool> Handle(AdminDeletePlayerCommand request, CancellationToken cancellationToken)
        {
            var result = await _write.RemoveAsync(request.Id.ToString());
            await _write.SaveAsync();
            return result;
        }
    }
}
