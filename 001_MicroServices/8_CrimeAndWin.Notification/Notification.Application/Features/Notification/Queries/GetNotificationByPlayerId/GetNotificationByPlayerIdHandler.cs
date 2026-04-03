using Notification.Application.Mapping;
using Shared.Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using Notification.Application.DTOs;
using Shared.Domain.Repository;

namespace Notification.Application.Features.Notification.Queries.GetNotificationByPlayerId
{
    public class GetNotificationByPlayerIdHandler : IRequestHandler<GetNotificationByPlayerIdQuery, List<ResultNotificationDTO>>
    {
        private readonly IReadRepository<Domain.Entities.Notification> _readRepository;
        private readonly NotificationMapper _mapper;

        public GetNotificationByPlayerIdHandler(IReadRepository<Domain.Entities.Notification> readRepository, NotificationMapper mapper)
        {
            _readRepository = readRepository;
            _mapper = mapper;
        }

        public async Task<List<ResultNotificationDTO>> Handle(GetNotificationByPlayerIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _readRepository
                .GetWhere(x => x.PlayerId == request.PlayerId, tracking: false)
                .OrderByDescending(x => x.CreatedAtUtc)
                .ToListAsync(cancellationToken);

            return _mapper.ToResultDtoList(data).ToList();
        }
    }
}



