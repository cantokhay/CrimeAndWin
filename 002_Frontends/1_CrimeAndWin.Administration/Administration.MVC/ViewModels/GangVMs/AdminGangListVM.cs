namespace Administration.MVC.ViewModels.GangVMs
{
    public class AdminGangListVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Tag { get; set; }
        public string LeaderName { get; set; }
        public int MemberCount { get; set; }
        public decimal TotalRespect { get; set; }
        public decimal VaultBlackBalance { get; set; }
        public decimal VaultCashBalance { get; set; }
        public bool IsActive { get; set; }
    }
}
