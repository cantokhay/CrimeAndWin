using Inventory.Application.DTOs.ItemDTOs.Admin;

namespace Inventory.Application.DTOs.InventoryDTOs.Admin
{
    public sealed class AdminResultInventoryDTO
    {
        public Guid Id { get; set; }
        public Guid PlayerId { get; set; }
        public List<AdminResultItemDTO> Items { get; set; } = new();
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? UpdatedAtUtc { get; set; }
    }
}
