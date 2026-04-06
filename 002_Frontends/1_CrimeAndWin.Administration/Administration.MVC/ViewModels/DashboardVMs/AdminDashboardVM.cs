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
        public double AveragePlayerHeat { get; set; } // New

        // 3. Economy Stats
        public decimal TotalCashInEconomy { get; set; }
        public decimal TotalBlackMoneyInEconomy { get; set; } // New
        public int TotalTransactionsToday { get; set; }

        // 4. Action Stats
        public int TotalActionsToday { get; set; }
        public double AverageActionSuccessRate { get; set; }
        public int ActiveRaidsToday { get; set; } // New

        // 5. Recent Lists
        public List<RecentActionLogVM> RecentActions { get; set; } = new();
        public List<RecentSagaFailureVM> RecentFailures { get; set; } = new();

        // 6. Chart Data (JSON strings for Chart.js)
        public string ActionVolumeChartData { get; set; } 
        public string SuccessRateChartData { get; set; }  
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
