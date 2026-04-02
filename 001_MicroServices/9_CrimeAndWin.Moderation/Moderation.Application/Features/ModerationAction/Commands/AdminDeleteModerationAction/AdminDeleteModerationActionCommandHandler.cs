using Mediator;
using Shared.Domain.Repository;

namespace Moderation.Application.Features.ModerationAction.Commands.AdminDeleteModerationAction
{
    public sealed class AdminDeleteModerationActionCommandHandler
            : IRequestHandler<AdminDeleteModerationActionCommand, bool>
    {
        private readonly IWriteRepository<Domain.Entities.ModerationAction> _write;

        public AdminDeleteModerationActionCommandHandler(IWriteRepository<Domain.Entities.ModerationAction> write)
        {
            _write = write;
        }

        public async ValueTask<bool> Handle(AdminDeleteModerationActionCommand request, CancellationToken cancellationToken)
        {
            var ok = await _write.RemoveAsync(request.id.ToString());
            await _write.SaveAsync();
            return ok;
        }
    }
}

