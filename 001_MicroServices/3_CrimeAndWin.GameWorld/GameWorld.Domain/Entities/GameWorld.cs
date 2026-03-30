using GameWorld.Domain.VOs;
using Shared.Domain;

namespace GameWorld.Domain.Entities
{
    public class GameWorld : BaseEntity
    {
        public string Name { get; set; }
        public GameRule Rule { get; set; }
        public ICollection<Season> Seasons { get; set; }
    }
}
