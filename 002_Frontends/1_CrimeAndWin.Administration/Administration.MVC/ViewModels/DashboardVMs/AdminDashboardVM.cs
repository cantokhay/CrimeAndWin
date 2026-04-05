namespace Administration.MVC.ViewModels.DashboardVMs
{
    public class AdminDashboardVM
    {
        // 1. Service Health
        public int HealthyServiceCount { get; set; }
        public int TotalServiceCount { get; set; }

        // 2. Player Stats
        public int TotalPlayerCount { get; set; }
        public int NewPlayersToday { get; set; }

        // 3. Economy Stats
        public decimal TotalCashInEconomy { get; set; }
        public int TotalTransactionsToday { get; set; }

        // 4. Action Stats
        public int TotalActionsToday { get; set; }
        public double AverageActionSuccessRate { get; set; }

        // 5. Recent Lists
        public List<RecentActionLogVM> RecentActions { get; set; } = new();
        public List<RecentSagaFailureVM> RecentFailures { get; set; } = new();

        // 6. Chart Data (JSON strings for Chart.js)
        public string ActionVolumeChartData { get; set; } // Daily volume
        public string SuccessRateChartData { get; set; }  // Daily success %
    }

    public class RecentActionLogVM
    {
        public string PlayerName { get; set; }
        public string ActionName { get; set; }
        public bool IsSuccess { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class RecentSagaFailureVM
    {
        public string SagaType { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
