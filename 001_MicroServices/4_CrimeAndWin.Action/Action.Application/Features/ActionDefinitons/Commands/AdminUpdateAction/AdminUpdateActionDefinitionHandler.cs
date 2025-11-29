using Action.Domain.Entities;
using Action.Domain.VOs;
using MediatR;
using Shared.Domain.Repository;
using Shared.Domain.Time;

namespace Action.Application.Features.ActionDefinitons.Commands.AdminUpdateAction
{
    public sealed class AdminUpdateActionDefinitionHandler
            : IRequestHandler<AdminUpdateActionDefinitionCommand, bool>
    {
        private readonly IReadRepository<ActionDefinition> _read;
        private readonly IWriteRepository<ActionDefinition> _write;
        private readonly IDateTimeProvider _time;

        public AdminUpdateActionDefinitionHandler(
            IReadRepository<ActionDefinition> read,
            IWriteRepository<ActionDefinition> write,
            IDateTimeProvider time)
        {
            _read = read;
            _write = write;
            _time = time;
        }

        public async Task<bool> Handle(AdminUpdateActionDefinitionCommand request, CancellationToken ct)
        {
            var d = request.updateActionDefinitionDTO;
            var entity = await _read.GetByIdAsync(d.Id.ToString(), tracking: true);
            if (entity is null) return false;

            entity.Code = d.Code;
            entity.DisplayName = d.DisplayName;
            entity.Description = d.Description;
            entity.Requirements = new ActionRequirements(d.MinPower, d.EnergyCost);
            entity.Rewards = new ActionRewards(d.PowerGain, d.ItemDrop, d.MoneyGain);
            entity.IsActive = d.IsActive;
            entity.UpdatedAtUtc = _time.UtcNow;

            var ok = _write.Update(entity);
            await _write.SaveAsync();
            return ok;
        }
    }
}
