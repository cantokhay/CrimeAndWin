namespace CrimeAndWin.Action.GameMechanics
{
    public class BribeCalculator
    {
        public BribeResult Calculate(decimal currentHeat, decimal respectScore, decimal bribeAmount)
        {
            // Base success start from 70%
            decimal baseSuccess = 70.0m;
            
            // Heat penalty: Each heat point reduces success by 0.5%
            decimal heatPenalty = currentHeat * 0.5m;
            
            // Respect bonus: Each 100 respect points increase success by 1%
            decimal respectBonus = respectScore / 100.0m;
            
            // Amount bonus: Bribing more than required gives a boost (optional/future)
            
            decimal finalChance = (baseSuccess - heatPenalty) + respectBonus;
            
            // Boundaries
            if (finalChance > 95) finalChance = 95; // Never 100%
            if (finalChance < 5) finalChance = 5;   // Always a tiny chance
            
            // Critical Limit: If heat is extreme (>90), police are scared to take bribes
            bool isPoliceScared = currentHeat > 90;
            if (isPoliceScared) 
            {
                finalChance = 5.0m; // Only 5% chance if you are too hot
            }

            return new BribeResult 
            { 
               SuccessProbability = (double)finalChance, 
               IsPoliceScared = isPoliceScared,
               RiskMultiplier = isPoliceScared ? 2.0 : 1.0
            };
        }
    }

    public class BribeResult
    {
        public double SuccessProbability { get; set; }
        public bool IsPoliceScared { get; set; }
        public double RiskMultiplier { get; set; }
    }
}
