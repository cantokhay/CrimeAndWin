using Action.Application.DTOs.ActionDefinitionDTOs.Admin;
using Action.Domain.Entities;
using MediatR;
using Shared.Domain.Repository;

namespace Action.Application.Features.ActionDefinitons.Queries.GetActionDefinitionByIdAsAdmin
{
    public sealed class GetActionDefinitionByIdAsAdminHandler
           : IRequestHandler<GetActionDefinitionByIdAsAdminQuery, AdminResultActionDefinitionDTO?>
    {
        private readonly IReadRepository<ActionDefinition> _read;

        public GetActionDefinitionByIdAsAdminHandler(IReadRepository<ActionDefinition> read)
        {
            _read = read;
        }

        public async Task<AdminResultActionDefinitionDTO?> Handle(GetActionDefinitionByIdAsAdminQuery request, CancellationToken ct)
        {
            var a = await _read.GetByIdAsync(request.id.ToString(), tracking: false);
            if (a is null) return null;

            return new AdminResultActionDefinitionDTO
            {
                Id = a.Id,
                Code = a.Code,
                DisplayName = a.DisplayName,
                Description = a.Description,
                MinPower = a.Requirements.MinPower,
                EnergyCost = a.Requirements.EnergyCost,
                PowerGain = a.Rewards.PowerGain,
                ItemDrop = a.Rewards.ItemDrop,
                MoneyGain = a.Rewards.MoneyGain,
                IsActive = a.IsActive,
                CreatedAtUtc = a.CreatedAtUtc,
                UpdatedAtUtc = a.UpdatedAtUtc
            };
        }
    }
}
