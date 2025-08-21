using Inventory.Domain.VOs;
using Shared.Domain;

namespace Inventory.Domain.Entities
{
    public sealed class Item : BaseEntity
    {
        public Guid InventoryId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public ItemStats Stats { get; set; }
        public ItemValue Value { get; set; }

        public Inventory Inventory { get; set; }
    }
}
