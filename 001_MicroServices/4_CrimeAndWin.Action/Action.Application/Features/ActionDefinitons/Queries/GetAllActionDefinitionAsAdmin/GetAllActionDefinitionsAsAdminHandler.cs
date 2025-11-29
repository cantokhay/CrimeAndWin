using Action.Application.DTOs.ActionDefinitionDTOs.Admin;
using Action.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Repository;

namespace Action.Application.Features.ActionDefinitons.Queries.GetAllActionDefinitionAsAdmin
{
    public sealed class GetAllActionDefinitionsAsAdminHandler
            : IRequestHandler<GetAllActionDefinitionsAsAdminQuery, List<AdminResultActionDefinitionDTO>>
    {
        private readonly IReadRepository<ActionDefinition> _read;

        public GetAllActionDefinitionsAsAdminHandler(IReadRepository<ActionDefinition> read)
        {
            _read = read;
        }

        public async Task<List<AdminResultActionDefinitionDTO>> Handle(GetAllActionDefinitionsAsAdminQuery request, CancellationToken ct)
        {
            return await _read.GetAll(false)
                .Select(a => new AdminResultActionDefinitionDTO
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
                })
                .ToListAsync(ct);
        }
    }
}
