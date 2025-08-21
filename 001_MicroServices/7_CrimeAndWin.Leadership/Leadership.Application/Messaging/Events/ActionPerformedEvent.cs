namespace Leadership.Application.Messaging.Concrete
{
    public class ActionPerformedEvent
    {
        public Guid PlayerId { get; set; }
        public Guid GameWorldId { get; set; }
        public Guid? SeasonId { get; set; }
        public int RankPointsDelta { get; set; }
    }
}
