using MediatR;
using Moderation.Application.Messaging.Abstract;
using Shared.Domain.Repository;

namespace Moderation.Application.Features.Report.Commands.ResolveReport
{
    public class ResolveReportHandler : IRequestHandler<ResolveReportCommand, bool>
    {
        private readonly IReadRepository<Domain.Entities.Report> _readRepo;
        private readonly IWriteRepository<Domain.Entities.Report> _writeRepo;
        private readonly IEventPublisher _publisher;

        public ResolveReportHandler(IReadRepository<Domain.Entities.Report> readRepo, IWriteRepository<Domain.Entities.Report> writeRepo, IEventPublisher publisher)
        {
            _readRepo = readRepo;
            _writeRepo = writeRepo;
            _publisher = publisher;
        }

        public async Task<bool> Handle(ResolveReportCommand request, CancellationToken ct)
        {
            var entity = await _readRepo.GetByIdAsync(request.ReportId.ToString(), tracking: true);
            if (entity is null) return false;

            entity.IsResolved = true;
            entity.ResolvedAtUtc = DateTime.UtcNow;
            entity.ResolvedByModeratorId = request.ModeratorId;

            _writeRepo.Update(entity);
            var saved = await _writeRepo.SaveAsync() > 0;

            if (saved)
            {
                await _publisher.PublishAsync(new Messaging.Concrete.IntegrationEvents.ReportResolvedIntegrationEvent(
                    entity.Id, request.ModeratorId, entity.ResolvedAtUtc!.Value));
            }
            return saved;
        }
    }
}
