using Action.Application.DTOs;
using Action.Domain.Entities;
using AutoMapper;
using MediatR;
using Shared.Domain.Repository;

namespace Action.Application.Features.ActionDefinitons.Queries.GetByCodeAction
{
    public sealed class GetByCodeActionDefinitionHandler
        : IRequestHandler<GetByCodeActionDefinitionQuery, ActionDefinitionDTO>
    {
        private readonly IReadRepository<ActionDefinition> _read;
        private readonly IMapper _mapper;

        public GetByCodeActionDefinitionHandler(IReadRepository<ActionDefinition> read, IMapper mapper)
        {
            _read = read;
            _mapper = mapper;
        }

        public async Task<ActionDefinitionDTO> Handle(GetByCodeActionDefinitionQuery request, CancellationToken ct)
        {
            var entity = await _read.GetSingleAsync(a => a.Code == request.Code);
            return _mapper.Map<ActionDefinitionDTO>(entity);
        }
    }
}
