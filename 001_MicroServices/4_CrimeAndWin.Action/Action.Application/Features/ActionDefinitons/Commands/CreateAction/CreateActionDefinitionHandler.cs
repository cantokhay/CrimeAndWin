using Action.Application.DTOs;
using Action.Domain.Entities;
using AutoMapper;
using MediatR;
using Shared.Domain.Repository;

namespace Action.Application.Features.ActionDefinitons.Commands.CreateAction
{
    public sealed class CreateActionDefinitionHandler
            : IRequestHandler<CreateActionDefinitionCommand, CreateActionDefinitionDTO>
    {
        private readonly IWriteRepository<ActionDefinition> _write;
        private readonly IMapper _mapper;

        public CreateActionDefinitionHandler(IWriteRepository<ActionDefinition> write, IMapper mapper)
        {
            _write = write;
            _mapper = mapper;
        }

        public async Task<CreateActionDefinitionDTO> Handle(CreateActionDefinitionCommand cmd, CancellationToken ct)
        {
            var entity = _mapper.Map<ActionDefinition>(cmd.Request);
            entity.Id = Guid.NewGuid();
            entity.CreatedAtUtc = DateTime.UtcNow;

            await _write.AddAsync(entity);
            await _write.SaveAsync();

            return _mapper.Map<CreateActionDefinitionDTO>(entity);
        }
    }
}
