using MediatR;
using Shared.Domain.Repository;

namespace Notification.Application.Features.Notification.Commands.AdminDeleteNotification
{
    public sealed class AdminDeleteNotificationCommandHandler
            : IRequestHandler<AdminDeleteNotificationCommand, bool>
    {
        private readonly IWriteRepository<Domain.Entities.Notification> _write;

        public AdminDeleteNotificationCommandHandler(IWriteRepository<Domain.Entities.Notification> write)
        {
            _write = write;
        }

        public async Task<bool> Handle(AdminDeleteNotificationCommand request, CancellationToken cancellationToken)
        {
            var ok = await _write.RemoveAsync(request.id.ToString());
            await _write.SaveAsync();
            return ok;
        }
    }
}
