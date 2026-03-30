namespace Moderation.Application.Messaging.Concrete
{
    public class IntegrationEvents
    {
        public record ReportCreatedIntegrationEvent(Guid ReportId, Guid ReporterId, Guid ReportedPlayerId, string Reason, DateTime CreatedAtUtc);
        public record ReportResolvedIntegrationEvent(Guid ReportId, Guid ResolvedByModeratorId, DateTime ResolvedAtUtc);

        public record PlayerBannedIntegrationEvent(Guid PlayerId, Guid ModeratorId, string Reason, DateTime ActionDateUtc, DateTime? ExpiryDateUtc);
        public record PlayerRestrictionLiftedIntegrationEvent(Guid PlayerId, Guid ModeratorId, DateTime ActionDateUtc);
    }
}
