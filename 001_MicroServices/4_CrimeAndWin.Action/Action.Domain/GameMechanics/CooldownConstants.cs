using System.Collections.Generic;

namespace CrimeAndWin.Action.GameMechanics;

public static class CooldownConstants
{
    // Aksiyon tipi → saniye cinsinden cooldown süresi
    public static readonly Dictionary<string, int> CooldownByActionType = new()
    {
        { "Pickpocket",        30  },
        { "Mugging",           120 },
        { "BankRobbery",       3600 },
        { "DrugDealing",       300 },
        { "Extortion",         600 },
        { "CarJacking",        900 },
        { "Assassination",     7200 },
        { "Smuggling",         1800 },
        { "Hacking",           450 },
        { "GangWar",           10800 },
    };

    public const int DefaultCooldownSeconds = 60; // Tanımsız tip için varsayılan
}


