using Action.Application.DTOs;
using Action.Domain.Entities;
using AutoMapper;
using MediatR;
using Shared.Domain.Repository;

namespace Action.Application.Features.ActionDefinitons.Queries.GetAllAction
{
    public sealed class GetAllActionDefinitionsHandler
        : IRequestHandler<GetAllActionDefinitionsQuery, List<ResultActionDefinitionDTO>>
    {
        private readonly IReadRepository<ActionDefinition> _read;
        private readonly IMapper _mapper;

        public GetAllActionDefinitionsHandler(IReadRepository<ActionDefinition> read, IMapper mapper)
        {
            _read = read;
            _mapper = mapper;
        }

        public async Task<List<ResultActionDefinitionDTO>> Handle(GetAllActionDefinitionsQuery request, CancellationToken cancellationToken)
        {
            var list = _read.GetAll(tracking: false)
                            .OrderBy(a => a.DisplayName)
                            .ToList();

            return _mapper.Map<List<ResultActionDefinitionDTO>>(list);
        }
    }
}
