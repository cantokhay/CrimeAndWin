using AutoMapper;
using MediatR;
using Moderation.Application.Messaging.Abstract;
using Moderation.Domain.VOs;
using Shared.Domain.Repository;

namespace Moderation.Application.Features.Report.Commands.CreateReport
{
    public class CreateReportHandler : IRequestHandler<CreateReportCommand, Guid>
    {
        private readonly IWriteRepository<Domain.Entities.Report> _writeRepo;
        private readonly IMapper _mapper;
        private readonly IEventPublisher _publisher;

        public CreateReportHandler(IWriteRepository<Domain.Entities.Report> writeRepo, IMapper mapper, IEventPublisher publisher)
        {
            _writeRepo = writeRepo;
            _mapper = mapper;
            _publisher = publisher;
        }

        public async Task<Guid> Handle(CreateReportCommand request, CancellationToken ct)
        {
            var entity = _mapper.Map<Domain.Entities.Report>(request.Dto);
            entity.Reason = new ReportReason(request.Dto.Reason);
            entity.IsResolved = false;
            entity.CreatedAtUtc = DateTime.UtcNow;

            await _writeRepo.AddAsync(entity);
            await _writeRepo.SaveAsync();

            await _publisher.PublishAsync(new Messaging.Concrete.IntegrationEvents.ReportCreatedIntegrationEvent(
                entity.Id, entity.ReporterId, entity.ReportedPlayerId, entity.Reason.Value, entity.CreatedAtUtc));

            return entity.Id;
        }
    }
}
