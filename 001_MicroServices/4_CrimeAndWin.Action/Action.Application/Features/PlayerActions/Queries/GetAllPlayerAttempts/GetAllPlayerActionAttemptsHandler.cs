using Action.Application.DTOs;
using Action.Domain.Entities;
using AutoMapper;
using MediatR;
using Shared.Domain.Repository;

namespace Action.Application.Features.PlayerActions.Queries.GetAllPlayerAttempts
{
    public sealed class GetAllPlayerActionAttemptsHandler
        : IRequestHandler<GetAllPlayerActionAttemptsQuery, List<ResultPlayerActionAttemptDTO>>
    {
        private readonly IReadRepository<PlayerActionAttempt> _read;
        private readonly IMapper _mapper;

        public GetAllPlayerActionAttemptsHandler(IReadRepository<PlayerActionAttempt> read, IMapper mapper)
        {
            _read = read;
            _mapper = mapper;
        }

        public async Task<List<ResultPlayerActionAttemptDTO>> Handle(GetAllPlayerActionAttemptsQuery request, CancellationToken cancellationToken)
        {
            var list = _read.GetAll(tracking: false)
                            .OrderByDescending(a => a.AttemptedAtUtc)
                            .ToList();

            return _mapper.Map<List<ResultPlayerActionAttemptDTO>>(list);
        }
    }
}
