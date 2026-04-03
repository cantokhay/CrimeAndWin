using Mediator;
using Notification.Domain.VOs;
using Shared.Domain.Repository;

namespace Notification.Application.Features.Notification.Commands.CreateNotification
{
    public class CreateNotificationHandler
            : IRequestHandler<CreateNotificationCommand, Guid>
    {
        private readonly IWriteRepository<Domain.Entities.Notification> _writeRepository;

        public CreateNotificationHandler(IWriteRepository<Domain.Entities.Notification> writeRepository)
        {
            _writeRepository = writeRepository;
        }

        public async ValueTask<Guid> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.Notification
            {
                PlayerId = request.PlayerId,
                Content = new NotificationContent(request.Title, request.Message, request.Type),
                CreatedAtUtc = DateTime.UtcNow
            };

            await _writeRepository.AddAsync(entity);
            await _writeRepository.SaveAsync();
            return entity.Id;
        }
    }
}

