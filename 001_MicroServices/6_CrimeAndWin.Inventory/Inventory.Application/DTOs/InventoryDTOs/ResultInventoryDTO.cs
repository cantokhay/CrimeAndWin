using Inventory.Application.DTOs.ItemDTOs;

namespace Inventory.Application.DTOs.InventoryDTOs
{
    public sealed class ResultInventoryDTO
    {
        public Guid Id { get; set; }
        public Guid PlayerId { get; set; }
        public List<ResultItemDTO> Items { get; set; }
    }
}
