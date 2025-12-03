using Inventory.Application.DTOs.ItemDTOs.Admin;
using MediatR;
using Shared.Domain.Repository;

namespace Inventory.Application.Features.Item.Queries.GetItemByIdAsAdmin
{
    public sealed class GetItemByIdAsAdminQueryHandler
            : IRequestHandler<GetItemByIdAsAdminQuery, AdminResultItemDTO?>
    {
        private readonly IReadRepository<Domain.Entities.Item> _read;

        public GetItemByIdAsAdminQueryHandler(IReadRepository<Domain.Entities.Item> read)
        {
            _read = read;
        }

        public async Task<AdminResultItemDTO?> Handle(GetItemByIdAsAdminQuery request, CancellationToken ct)
        {
            var i = await _read.GetByIdAsync(request.id.ToString(), tracking: false);
            if (i is null) return null;

            return new AdminResultItemDTO
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
            };
        }
    }
}
