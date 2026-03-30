using Shared.Domain;

namespace Inventory.Domain.Entities
{
    public sealed class Inventory : BaseEntity
    {
        public Guid PlayerId { get; set; }
        public List<Item> Items { get; set; }
    }
}
