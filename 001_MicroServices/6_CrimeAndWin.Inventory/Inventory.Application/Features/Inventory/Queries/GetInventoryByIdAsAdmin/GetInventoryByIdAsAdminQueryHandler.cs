using Inventory.Application.DTOs.InventoryDTOs.Admin;
using Inventory.Application.DTOs.ItemDTOs.Admin;
using MediatR;
using Shared.Domain.Repository;

namespace Inventory.Application.Features.Inventory.Queries.GetInventoryByIdAsAdmin
{
    public sealed class GetInventoryByIdAsAdminQueryHandler
            : IRequestHandler<GetInventoryByIdAsAdminQuery, AdminResultInventoryDTO?>
    {
        private readonly IReadRepository<Domain.Entities.Inventory> _read;

        public GetInventoryByIdAsAdminQueryHandler(IReadRepository<Domain.Entities.Inventory> read)
        {
            _read = read;
        }

        public async Task<AdminResultInventoryDTO?> Handle(GetInventoryByIdAsAdminQuery request, CancellationToken ct)
        {
            var inv = await _read.GetByIdAsync(request.id.ToString(), false);
            if (inv is null) return null;

            return new AdminResultInventoryDTO
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
            };
        }
    }
}
