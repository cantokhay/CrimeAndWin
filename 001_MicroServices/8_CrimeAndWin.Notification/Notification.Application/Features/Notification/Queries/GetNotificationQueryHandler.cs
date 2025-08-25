using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notification.Application.DTOs;
using Shared.Domain.Repository;

namespace Notification.Application.Features.Notification.Queries
{
    public class GetNotificationQueryHandler : IRequestHandler<GetNotificationQuery, List<ResultNotificationDTO>>
    {
        private readonly IReadRepository<Domain.Entities.Notification> _readRepository;
        private readonly IMapper _mapper;

        public GetNotificationQueryHandler(IReadRepository<Domain.Entities.Notification> readRepository, IMapper mapper)
        {
            _readRepository = readRepository;
            _mapper = mapper;
        }

        public async Task<List<ResultNotificationDTO>> Handle(GetNotificationQuery request, CancellationToken cancellationToken)
        {
            return await _readRepository
                .GetWhere(x => x.PlayerId == request.PlayerId, tracking: false)
                .OrderByDescending(x => x.CreatedAtUtc)
                .ProjectTo<ResultNotificationDTO>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
