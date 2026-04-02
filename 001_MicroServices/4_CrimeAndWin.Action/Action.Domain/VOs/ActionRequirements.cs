namespace Action.Domain.VOs
{
    public sealed record ActionRequirements
    {
        public int MinPower { get; init; }
        public int EnergyCost { get; init; }
        public int DifficultyLevel { get; init; } = 3; // Default to Medium

        public ActionRequirements() { }
        
        public ActionRequirements(int MinPower, int EnergyCost)
        {
            this.MinPower = MinPower;
            this.EnergyCost = EnergyCost;
        }

        public ActionRequirements(int MinPower, int EnergyCost, int DifficultyLevel)
        {
            this.MinPower = MinPower;
            this.EnergyCost = EnergyCost;
            this.DifficultyLevel = DifficultyLevel;
        }
    }
}

