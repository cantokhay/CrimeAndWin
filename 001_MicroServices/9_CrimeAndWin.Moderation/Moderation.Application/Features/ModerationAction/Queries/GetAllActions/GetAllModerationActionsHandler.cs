using Moderation.Application.Mapping;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Moderation.Application.DTOs.ModerationActionDTOs;
using Shared.Domain.Repository;

namespace Moderation.Application.Features.ModerationAction.Queries.GetAllActions
{
    public sealed class GetAllModerationActionsHandler(
            IReadRepository<Domain.Entities.ModerationAction> readRepo,
            ModerationMapper mapper)
            : IRequestHandler<GetAllModerationActionsQuery, List<ResultModerationActionDTO>>
    {
        public async ValueTask<List<ResultModerationActionDTO>> Handle(GetAllModerationActionsQuery request, CancellationToken cancellationToken)
        {
            var actions = await readRepo.Table
                .AsNoTracking()
                .OrderByDescending(x => x.ActionDateUtc)
                .ToListAsync(cancellationToken);

            return mapper.ToResultDtoList(actions).ToList();
        }
    }
}


