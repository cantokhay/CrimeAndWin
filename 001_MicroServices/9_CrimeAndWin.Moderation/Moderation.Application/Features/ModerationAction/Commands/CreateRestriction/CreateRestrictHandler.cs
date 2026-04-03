using Moderation.Application.Mapping;
using Shared.Application.Abstractions.Messaging;
using Shared.Domain.Repository;

namespace Moderation.Application.Features.ModerationAction.Commands.CreateRestriction
{
    public class CreateRestrictHandler : IRequestHandler<CreateRestrictCommand, Guid>
    {
        private readonly IWriteRepository<Domain.Entities.ModerationAction> _writeRepo;
        private readonly ModerationMapper _mapper;

        public CreateRestrictHandler(IWriteRepository<Domain.Entities.ModerationAction> writeRepo, ModerationMapper mapper)
        {
            _writeRepo = writeRepo;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateRestrictCommand request, CancellationToken ct)
        {
            var entity = _mapper.ToEntity(request.Dto);
            entity.ActionType = "Restrict";
            entity.IsActive = true;
            entity.ActionDateUtc = DateTime.UtcNow;

            await _writeRepo.AddAsync(entity);
            await _writeRepo.SaveAsync();
            return entity.Id;
        }
    }
}


