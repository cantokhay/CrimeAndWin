using Action.Domain.Entities;
using MediatR;
using Shared.Domain.Repository;

namespace Action.Application.Features.PlayerActionAttempts.Commands.AdminDeletePlayerActionAttempt
{
    public sealed class AdminDeletePlayerActionAttemptHandler
           : IRequestHandler<AdminDeletePlayerActionAttemptCommand, bool>
    {
        private readonly IWriteRepository<PlayerActionAttempt> _write;

        public AdminDeletePlayerActionAttemptHandler(IWriteRepository<PlayerActionAttempt> write)
        {
            _write = write;
        }

        public async Task<bool> Handle(AdminDeletePlayerActionAttemptCommand request, CancellationToken ct)
        {
            var ok = await _write.RemoveAsync(request.Id.ToString());
            await _write.SaveAsync();
            return ok;
        }
    }
}
