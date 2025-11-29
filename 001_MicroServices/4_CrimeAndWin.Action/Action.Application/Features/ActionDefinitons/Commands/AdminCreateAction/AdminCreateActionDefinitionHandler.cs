using Action.Domain.Entities;
using Action.Domain.VOs;
using MediatR;
using Shared.Domain.Repository;
using Shared.Domain.Time;

namespace Action.Application.Features.ActionDefinitons.Commands.AdminCreateAction
{
    public sealed class AdminCreateActionDefinitionHandler
            : IRequestHandler<AdminCreateActionDefinitionCommand, Guid>
    {
        private readonly IWriteRepository<ActionDefinition> _write;
        private readonly IDateTimeProvider _time;

        public AdminCreateActionDefinitionHandler(IWriteRepository<ActionDefinition> write, IDateTimeProvider time)
        {
            _write = write;
            _time = time;
        }

        public async Task<Guid> Handle(AdminCreateActionDefinitionCommand request, CancellationToken ct)
        {
            var d = request.createActionDefinitionDTO;

            var entity = new ActionDefinition
            {
                Id = Guid.NewGuid(),
                Code = d.Code,
                DisplayName = d.DisplayName,
                Description = d.Description,
                Requirements = new ActionRequirements(d.MinPower, d.EnergyCost),
                Rewards = new ActionRewards(d.PowerGain, d.ItemDrop, d.MoneyGain),
                IsActive = d.IsActive,
                CreatedAtUtc = _time.UtcNow
            };

            await _write.AddAsync(entity);
            await _write.SaveAsync();
            return entity.Id;
        }
    }
}
