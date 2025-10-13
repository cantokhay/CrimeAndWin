using MediatR;
using Moderation.Application.Messaging.Abstract;
using Shared.Domain.Repository;

namespace Moderation.Application.Features.ModerationAction.Commands.LiftRestriction
{
    public class LiftRestrictionHandler : IRequestHandler<LiftRestrictionCommand, bool>
    {
        private readonly IReadRepository<Domain.Entities.ModerationAction> _readRepo;
        private readonly IWriteRepository<Domain.Entities.ModerationAction> _writeRepo;
        private readonly IEventPublisher _publisher;

        public LiftRestrictionHandler(IReadRepository<Domain.Entities.ModerationAction> readRepo, IWriteRepository<Domain.Entities.ModerationAction> writeRepo, IEventPublisher publisher)
        {
            _readRepo = readRepo;
            _writeRepo = writeRepo;
            _publisher = publisher;
        }

        public async Task<bool> Handle(LiftRestrictionCommand request, CancellationToken ct)
        {
            var active = _readRepo.GetWhere(x => x.PlayerId == request.Dto.PlayerId && x.IsActive).ToList();
            if (!active.Any()) return false;

            foreach (var a in active)
            {
                a.IsActive = false;
                a.ExpiryDateUtc = DateTime.UtcNow;
            }
            _writeRepo.UpdateRange(active);
            var saved = await _writeRepo.SaveAsync() > 0;

            if (saved)
            {
                await _publisher.PublishAsync(new Messaging.Concrete.IntegrationEvents.PlayerRestrictionLiftedIntegrationEvent(
                    request.Dto.PlayerId, request.Dto.ModeratorId, DateTime.UtcNow));
            }
            return saved;
        }
    }
}
