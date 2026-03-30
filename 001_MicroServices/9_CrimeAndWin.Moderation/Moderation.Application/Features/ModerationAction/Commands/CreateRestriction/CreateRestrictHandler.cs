using AutoMapper;
using MediatR;
using Shared.Domain.Repository;

namespace Moderation.Application.Features.ModerationAction.Commands.CreateRestriction
{
    public class CreateRestrictHandler : IRequestHandler<CreateRestrictCommand, Guid>
    {
        private readonly IWriteRepository<Domain.Entities.ModerationAction> _writeRepo;
        private readonly IMapper _mapper;

        public CreateRestrictHandler(IWriteRepository<Domain.Entities.ModerationAction> writeRepo, IMapper mapper)
        {
            _writeRepo = writeRepo;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateRestrictCommand request, CancellationToken ct)
        {
            var entity = _mapper.Map<Domain.Entities.ModerationAction>(request.Dto);
            entity.ActionType = "Restrict";
            entity.IsActive = true;
            entity.ActionDateUtc = DateTime.UtcNow;

            await _writeRepo.AddAsync(entity);
            await _writeRepo.SaveAsync();
            return entity.Id;
        }
    }
}
