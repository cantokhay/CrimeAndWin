namespace Administration.MVC.ViewModels.ActionVMs.PlayerActionAttemptVMs
{
    public class UpdatePlayerActionAttemptVM
    {
        public Guid Id { get; set; }
        public Guid PlayerId { get; set; }
        public Guid ActionDefinitionId { get; set; }
        public double SuccessRate { get; set; }
    }
}
