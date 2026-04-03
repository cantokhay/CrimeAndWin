using Action.Application.DTOs.ActionAttemptDTOs;
using Action.Domain.Entities;
using Action.Application.Mapping;
using Shared.Application.Abstractions.Messaging;
using Shared.Domain.Repository;

namespace Action.Application.Features.PlayerActionAttempts.Queries.GetAllPlayerAttempts
{
    public sealed class GetAllPlayerActionAttemptsHandler
        : IRequestHandler<GetAllPlayerActionAttemptsQuery, List<ResultPlayerActionAttemptDTO>>
    {
        private readonly IReadRepository<PlayerActionAttempt> _read;
        private readonly ActionMapper _mapper;

        public GetAllPlayerActionAttemptsHandler(IReadRepository<PlayerActionAttempt> read, ActionMapper mapper)
        {
            _read = read;
            _mapper = mapper;
        }

        public async Task<List<ResultPlayerActionAttemptDTO>> Handle(GetAllPlayerActionAttemptsQuery request, CancellationToken cancellationToken)
        {
            var list = _read.GetAll(tracking: false)
                            .OrderByDescending(a => a.AttemptedAtUtc)
                            .ToList();

            return _mapper.ToResultDtoList(list).ToList();
        }
    }
}



