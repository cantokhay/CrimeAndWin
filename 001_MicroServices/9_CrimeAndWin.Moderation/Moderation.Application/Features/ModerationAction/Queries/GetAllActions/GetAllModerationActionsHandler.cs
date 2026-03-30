using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moderation.Application.DTOs.ModerationActionDTOs;
using Shared.Domain.Repository;

namespace Moderation.Application.Features.ModerationAction.Queries.GetAllActions
{
    public sealed class GetAllModerationActionsHandler(
            IReadRepository<Domain.Entities.ModerationAction> readRepo,
            IMapper mapper)
            : IRequestHandler<GetAllModerationActionsQuery, List<ResultModerationActionDTO>>
    {
        public async Task<List<ResultModerationActionDTO>> Handle(GetAllModerationActionsQuery request, CancellationToken cancellationToken)
        {
            var actions = await readRepo.Table
                .AsNoTracking()
                .OrderByDescending(x => x.ActionDateUtc)
                .ToListAsync(cancellationToken);

            return mapper.Map<List<ResultModerationActionDTO>>(actions);
        }
    }
}
