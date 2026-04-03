using Moderation.Application.Mapping;
using Shared.Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using Moderation.Application.DTOs.ModerationActionDTOs;
using Shared.Domain.Repository;

namespace Moderation.Application.Features.ModerationAction.Queries.GetActionsByPlayerId
{
    public class GetActionsByPlayerIdHandler : IRequestHandler<GetActionsByPlayerIdQuery, List<ResultModerationActionDTO>>
    {
        private readonly IReadRepository<Domain.Entities.ModerationAction> _readRepo;
        private readonly ModerationMapper _mapper;

        public GetActionsByPlayerIdHandler(IReadRepository<Domain.Entities.ModerationAction> readRepo, ModerationMapper mapper)
        {
            _readRepo = readRepo;
            _mapper = mapper;
        }

        public async Task<List<ResultModerationActionDTO>> Handle(GetActionsByPlayerIdQuery request, CancellationToken ct)
        {
            var data = await _readRepo
                .GetWhere(x => x.PlayerId == request.PlayerId, tracking: false)
                .OrderByDescending(x => x.ActionDateUtc)
                .ToListAsync(ct);

            return _mapper.ToResultDtoList(data).ToList();
        }
    }
}



