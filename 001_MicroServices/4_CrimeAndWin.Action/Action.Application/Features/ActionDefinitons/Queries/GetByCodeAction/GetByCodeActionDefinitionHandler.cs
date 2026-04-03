using Action.Application.DTOs.ActionDefinitionDTOs;
using Action.Domain.Entities;
using Action.Application.Mapping;
using Shared.Application.Abstractions.Messaging;
using Shared.Domain.Repository;

namespace Action.Application.Features.ActionDefinitons.Queries.GetByCodeAction
{
    public sealed class GetByCodeActionDefinitionHandler
        : IRequestHandler<GetByCodeActionDefinitionQuery, ActionDefinitionDTO>
    {
        private readonly IReadRepository<ActionDefinition> _read;
        private readonly ActionMapper _mapper;

        public GetByCodeActionDefinitionHandler(IReadRepository<ActionDefinition> read, ActionMapper mapper)
        {
            _read = read;
            _mapper = mapper;
        }

        public async Task<ActionDefinitionDTO> Handle(GetByCodeActionDefinitionQuery request, CancellationToken ct)
        {
            var entity = await _read.GetSingleAsync(a => a.Code == request.Code);
            return _mapper.ToDto(entity);
        }
    }
}


