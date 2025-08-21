using Inventory.Application.DTOs.VODTOs;

namespace Inventory.Application.DTOs.ItemDTOs
{
    public sealed class ResultItemDTO
    {
        public Guid Id { get; set; }
        public Guid InventoryId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public ResultItemStatsDTO Stats { get; set; }
        public ResultItemValueDTO Value { get; set; }
    }
}
