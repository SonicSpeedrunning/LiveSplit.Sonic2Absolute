namespace LiveSplit.Sonic2Absolute
{
    enum GameVersion
    {
        v1_0_0_and_1_0_1 = 0x4475000,
        v1_0_2 = 0x4477000
    }

    enum StartTrigger
    {
        NewGame,
        NewGamePlus
    }

    enum ZoneIndicator : uint
    {
        MainMenu = 0x6E69614D,
        Zones = 0x656E6F5A,
        Ending = 0x69646E45
    }

    enum Acts : byte
    {
        EmeraldHillAct1 = 0,
        EmeraldHillAct2 = 1,
        ChemicalPlantAct1 = 2,
        ChemicalPlantAct2 = 3,
        AquaticRuinAct1 = 4,
        AquaticRuinAct2 = 5,
        CasinoNightAct1 = 6,
        CasinoNightAct2 = 7,
        HillTopAct1 = 8,
        HillTopAct2 = 9,
        MysticCaveAct1 = 10,
        MysticCaveAct2 = 11,
        OilOceanAct1 = 12,
        OilOceanAct2 = 13,
        MetropolisAct1 = 14,
        MetropolisAct2 = 15,
        MetropolisAct3 = 16,
        SkyChase = 17,
        WingFortress = 18,
        DeathEgg = 19,
        Ending = 20
    }
}
