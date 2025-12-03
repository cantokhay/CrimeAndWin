using Inventory.Domain.Enums;

namespace Inventory.Application.DTOs.ItemDTOs.Admin
{
    public sealed class AdminUpdateItemDTO
    {
        public Guid Id { get; set; }
        public Guid InventoryId { get; set; }
        public string Name { get; set; } = null!;
        public int Quantity { get; set; }

        public int Damage { get; set; }
        public int Defense { get; set; }
        public int Power { get; set; }

        public decimal Amount { get; set; }
        public CurrencyType Currency { get; set; }
    }
}
