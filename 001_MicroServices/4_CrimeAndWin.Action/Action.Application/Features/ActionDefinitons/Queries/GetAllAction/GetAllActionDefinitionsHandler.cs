using Action.Application.DTOs.ActionDefinitionDTOs;
using Action.Domain.Entities;
using Action.Application.Mapping;
using Shared.Application.Abstractions.Messaging;
using Shared.Domain.Repository;

namespace Action.Application.Features.ActionDefinitons.Queries.GetAllAction
{
    public sealed class GetAllActionDefinitionsHandler
        : IRequestHandler<GetAllActionDefinitionsQuery, List<ResultActionDefinitionDTO>>
    {
        private readonly IReadRepository<ActionDefinition> _read;
        private readonly ActionMapper _mapper;

        public GetAllActionDefinitionsHandler(IReadRepository<ActionDefinition> read, ActionMapper mapper)
        {
            _read = read;
            _mapper = mapper;
        }

        public async Task<List<ResultActionDefinitionDTO>> Handle(GetAllActionDefinitionsQuery request, CancellationToken cancellationToken)
        {
            var list = _read.GetAll(tracking: false)
                            .OrderBy(a => a.DisplayName)
                            .ToList();

            return _mapper.ToResultDtoList(list).ToList();
        }
    }
}



