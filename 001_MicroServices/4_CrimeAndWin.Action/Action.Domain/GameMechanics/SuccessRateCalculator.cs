using System;
using System.Collections.Generic;
using System.Linq;

namespace CrimeAndWin.Action.GameMechanics;

public class SuccessRateCalculator
{
    private readonly Random _random = new();

    public SuccessRateResult Calculate(SuccessRateInput input)
    {
        double baseRate      = DifficultyBaseRate.BaseSuccessRate[input.Difficulty];
        double levelBonus    = Math.Min(input.PlayerLevel * 0.005, 0.25);
        double equipBonus    = CalculateEquipmentBonus(input.EquippedItems);
        double variation     = (_random.NextDouble() * 0.10) - 0.05; // -0.05 ile +0.05

        double rawRate  = baseRate + levelBonus + equipBonus + variation;
        double finalRate = Math.Clamp(rawRate, 0.01, 0.99);

        return new SuccessRateResult
        {
            FinalRate       = finalRate,
            BaseRate        = baseRate,
            LevelBonus      = levelBonus,
            EquipmentBonus  = equipBonus,
            RandomVariation = variation,
            IsSuccess       = _random.NextDouble() < finalRate,
        };
    }

    private double CalculateEquipmentBonus(List<EquippedItem> items)
    {
        // Her item kendi SuccessBonus değerini taşır (Inventory'den gelir)
        // Toplam ekipman bonusu max %20 ile sınırlandırılır
        double total = items.Sum(i => i.SuccessBonus);
        return Math.Min(total, 0.20);
    }
}

public record SuccessRateInput
{
    public int PlayerLevel           { get; init; }
    public ActionDifficulty Difficulty { get; init; }
    public List<EquippedItem> EquippedItems { get; init; } = new();
}

public record SuccessRateResult
{
    public double FinalRate       { get; init; }
    public double BaseRate        { get; init; }
    public double LevelBonus      { get; init; }
    public double EquipmentBonus  { get; init; }
    public double RandomVariation { get; init; }
    public bool IsSuccess         { get; init; }
}

public record EquippedItem
{
    public Guid   ItemId       { get; init; }
    public string ItemType     { get; init; } = string.Empty;
    public double SuccessBonus { get; init; } // 0.05 = %5 bonus
}


