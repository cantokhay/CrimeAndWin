using Action.Application.DTOs.ActionDefinitionDTOs;
using Action.Domain.Entities;
using Action.Application.Mapping;
using Mediator;
using Shared.Domain.Repository;

namespace Action.Application.Features.ActionDefinitons.Commands.CreateAction
{
    public sealed class CreateActionDefinitionHandler
            : IRequestHandler<CreateActionDefinitionCommand, CreateActionDefinitionDTO>
    {
        private readonly IWriteRepository<ActionDefinition> _write;
        private readonly ActionMapper _mapper;

        public CreateActionDefinitionHandler(IWriteRepository<ActionDefinition> write, ActionMapper mapper)
        {
            _write = write;
            _mapper = mapper;
        }

        public async Task<CreateActionDefinitionDTO> Handle(CreateActionDefinitionCommand cmd, CancellationToken ct)
        {
            var entity = _mapper.ToEntity(cmd.Request);
            entity.Id = Guid.NewGuid();
            entity.CreatedAtUtc = DateTime.UtcNow;

            await _write.AddAsync(entity);
            await _write.SaveAsync();

            return _mapper.ToCreateDto(entity);
        }
    }
}


