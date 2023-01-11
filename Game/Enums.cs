using System;

namespace LiveSplit.Sonic2Absolute
{
    enum ZoneIndicator : uint
    {
        MainMenu = 0x6E69614Du,
        Zones = 0x656E6F5Au,
        Ending = 0x69646E45u,
        SaveSelect = 0x65766153u,
    }

    enum Acts : int
    {
        EmeraldHill1 = 0,
        EmeraldHill2 = 1,
        ChemicalPlant1 = 2,
        ChemicalPlant2 = 3,
        AquaticRuin1 = 4,
        AquaticRuin2 = 5,
        CasinoNight1 = 6,
        CasinoNight2 = 7,
        HillTop1 = 8,
        HillTop2 = 9,
        MysticCave1 = 10,
        MysticCave2 = 11,
        OilOcean1 = 12,
        OilOcean2 = 13,
        Metropolis1 = 14,
        Metropolis2 = 15,
        Metropolis3 = 16,
        SkyChase = 17,
        WingFortress = 18,
        DeathEgg = 19,
    }
}
