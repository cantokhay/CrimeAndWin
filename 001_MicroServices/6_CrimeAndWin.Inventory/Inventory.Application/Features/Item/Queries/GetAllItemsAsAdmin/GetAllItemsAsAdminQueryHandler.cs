using Inventory.Application.DTOs.ItemDTOs.Admin;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Repository;

namespace Inventory.Application.Features.Item.Queries.GetAllItemsAsAdmin
{
    public sealed class GetAllItemsAsAdminQueryHandler
            : IRequestHandler<GetAllItemsAsAdminQuery, List<AdminResultItemDTO>>
    {
        private readonly IReadRepository<Domain.Entities.Item> _read;

        public GetAllItemsAsAdminQueryHandler(IReadRepository<Domain.Entities.Item> read)
        {
            _read = read;
        }

        public async Task<List<AdminResultItemDTO>> Handle(GetAllItemsAsAdminQuery request, CancellationToken ct)
        {
            return await _read.GetAll(false)
                .Select(i => new AdminResultItemDTO
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
                })
                .ToListAsync(ct);
        }
    }
}
