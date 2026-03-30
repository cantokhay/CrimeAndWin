using AutoMapper;
using MediatR;
using Moderation.Application.Messaging.Abstract;
using Shared.Domain.Repository;
using static Moderation.Application.Messaging.Concrete.IntegrationEvents;

namespace Moderation.Application.Features.ModerationAction.Commands.CreateBan
{
    public class CreateBanHandler : IRequestHandler<CreateBanCommand, Guid>
    {
        private readonly IWriteRepository<Domain.Entities.ModerationAction> _writeRepo;
        private readonly IMapper _mapper;
        private readonly IEventPublisher _publisher;

        public CreateBanHandler(IWriteRepository<Domain.Entities.ModerationAction> writeRepo, IMapper mapper, IEventPublisher publisher)
        {
            _writeRepo = writeRepo;
            _mapper = mapper;
            _publisher = publisher;
        }

        public async Task<Guid> Handle(CreateBanCommand request, CancellationToken ct)
        {
            var entity = _mapper.Map<Domain.Entities.ModerationAction>(request.Dto);
            entity.ActionType = "Ban";
            entity.IsActive = true;
            entity.ActionDateUtc = DateTime.UtcNow;

            await _writeRepo.AddAsync(entity);
            await _writeRepo.SaveAsync();

            await _publisher.PublishAsync(new PlayerBannedIntegrationEvent(
                entity.PlayerId, entity.ModeratorId, entity.Reason, entity.ActionDateUtc, entity.ExpiryDateUtc));

            return entity.Id;
        }
    }
}
