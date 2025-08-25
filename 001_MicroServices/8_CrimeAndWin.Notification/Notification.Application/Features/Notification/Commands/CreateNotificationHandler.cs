using AutoMapper;
using MediatR;
using Notification.Domain.VOs;
using Shared.Domain.Repository;

namespace Notification.Application.Features.Notification.Commands
{
    public class CreateNotificationHandler
            : IRequestHandler<CreateNotificationCommand, Guid>
    {
        private readonly IWriteRepository<Domain.Entities.Notification> _writeRepository;
        private readonly IMapper _mapper;

        public CreateNotificationHandler(IWriteRepository<Domain.Entities.Notification> writeRepository, IMapper mapper)
        {
            _writeRepository = writeRepository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
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
