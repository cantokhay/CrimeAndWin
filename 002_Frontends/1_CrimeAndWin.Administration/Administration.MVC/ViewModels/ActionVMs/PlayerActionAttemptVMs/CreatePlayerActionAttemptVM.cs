namespace Administration.MVC.ViewModels.ActionVMs.PlayerActionAttemptVMs
{
    public class CreatePlayerActionAttemptVM
    {
        public Guid PlayerId { get; set; }
        public Guid ActionDefinitionId { get; set; }
        public double SuccessRate { get; set; } // 0-1 arası
    }
}
