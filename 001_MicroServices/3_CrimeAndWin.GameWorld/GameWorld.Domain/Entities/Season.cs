using GameWorld.Domain.VOs;
using Shared.Domain;

namespace GameWorld.Domain.Entities
{
    public class Season : BaseEntity
    {
        public Guid GameWorldId { get; set; }
        public int SeasonNumber { get; set; }
        public DateRange DateRange { get; set; }
        public bool IsActive { get; set; }
    }
}
