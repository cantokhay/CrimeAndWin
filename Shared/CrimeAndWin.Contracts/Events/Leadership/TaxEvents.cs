namespace CrimeAndWin.Contracts.Commands.Economy
{
    public record AwardTaxCommand
    {
        public Guid CorrelationId { get; init; }
        public Guid LeaderPlayerId { get; init; }
        public decimal TaxAmount { get; init; }
        public string DistrictName { get; init; } = string.Empty;
    }
}

namespace CrimeAndWin.Contracts.Events.Leadership
{
    public record DistrictTaxCalculatedEvent
    {
        public Guid CorrelationId { get; init; }
        public Guid DistrictId { get; init; }
        public Guid LeaderId { get; init; }
        public decimal Amount { get; init; }
    }
}
