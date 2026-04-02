using System;
using System.Collections.Generic;

namespace CrimeAndWin.Action.GameMechanics;

public static class EnergyConstants
{
    // Her seviyede max enerji = BaseMaxEnergy + (Level * EnergyPerLevel)
    public const int BaseMaxEnergy   = 100;
    public const int EnergyPerLevel  = 5;    // Seviye başına +5 max enerji

    // Zaman bazlı yenileme
    public const int RefillAmountPerTick = 1;         // Her tick'te +1 enerji
    public const int RefillIntervalSeconds = 180;     // Her 3 dakikada bir tick

    // Item hızlandırma çarpanları (item tipi → saniye cinsinden azaltma)
    public static readonly Dictionary<string, int> ItemRefillBonus = new()
    {
        { "EnergyDrink_Small",  60  },   // 1 dakika erken dolar
        { "EnergyDrink_Large",  180 },   // 3 dakika erken dolar
        { "VIPBoost",           600 },   // 10 dakika erken dolar
    };
}

