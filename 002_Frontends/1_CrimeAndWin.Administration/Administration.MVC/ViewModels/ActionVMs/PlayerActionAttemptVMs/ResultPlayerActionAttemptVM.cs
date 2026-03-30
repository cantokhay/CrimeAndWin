namespace Administration.MVC.ViewModels.ActionVMs.PlayerActionAttemptVMs
{
    public class ResultPlayerActionAttemptVM
    {
        public Guid Id { get; set; }
        public Guid PlayerId { get; set; }
        public Guid ActionDefinitionId { get; set; }
        public double SuccessRate { get; set; }
        public string OutcomeType { get; set; } = null!;
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? UpdatedAtUtc { get; set; }
    }
}
