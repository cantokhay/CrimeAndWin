using Inventory.Application.DTOs.InventoryDTOs.Admin;
using Inventory.Application.DTOs.ItemDTOs.Admin;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Repository;

namespace Inventory.Application.Features.Inventory.Queries.GetAllInventoriesAsAdmin
{
    public sealed class GetAllInventoriesAsAdminQueryHandler
            : IRequestHandler<GetAllInventoriesAsAdminQuery, List<AdminResultInventoryDTO>>
    {
        private readonly IReadRepository<Domain.Entities.Inventory> _read;

        public GetAllInventoriesAsAdminQueryHandler(IReadRepository<Domain.Entities.Inventory> read)
        {
            _read = read;
        }

        public async Task<List<AdminResultInventoryDTO>> Handle(GetAllInventoriesAsAdminQuery request, CancellationToken ct)
        {
            return await _read.GetAll(false)
                .Select(inv => new AdminResultInventoryDTO
                {
                    Id = inv.Id,
                    PlayerId = inv.PlayerId,
                    CreatedAtUtc = inv.CreatedAtUtc,
                    UpdatedAtUtc = inv.UpdatedAtUtc,
                    Items = inv.Items.Select(i => new AdminResultItemDTO
                    {
                        Id = i.Id,
                        InventoryId = i.InventoryId,
                        Name = i.Name,
                        Quantity = i.Quantity,
                        Damage = i.Stats.Damage,
                        Defense = i.Stats.Defense,
                        Power = i.Stats.Power,
                        Amount = i.Value.Amount,
                        Currency = i.Value.Currency,
                        CreatedAtUtc = i.CreatedAtUtc,
                        UpdatedAtUtc = i.UpdatedAtUtc
                    }).ToList()
                })
                .ToListAsync(ct);
        }
    }
}
