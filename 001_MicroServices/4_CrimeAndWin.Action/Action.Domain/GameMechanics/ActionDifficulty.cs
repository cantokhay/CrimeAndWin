using System.Collections.Generic;

namespace CrimeAndWin.Action.GameMechanics;

public enum ActionDifficulty
{
    VeryEasy   = 1,   // Baz oran: %90
    Easy       = 2,   // Baz oran: %75
    Medium     = 3,   // Baz oran: %55
    Hard       = 4,   // Baz oran: %35
    VeryHard   = 5,   // Baz oran: %15
    Legendary  = 6,   // Baz oran: %5
}

public static class DifficultyBaseRate
{
    public static readonly Dictionary<ActionDifficulty, double> BaseSuccessRate = new()
    {
        { ActionDifficulty.VeryEasy,  0.90 },
        { ActionDifficulty.Easy,      0.75 },
        { ActionDifficulty.Medium,    0.55 },
        { ActionDifficulty.Hard,      0.35 },
        { ActionDifficulty.VeryHard,  0.15 },
        { ActionDifficulty.Legendary, 0.05 },
    };
}

