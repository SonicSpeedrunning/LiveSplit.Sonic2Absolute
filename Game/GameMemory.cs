using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using LiveSplit.ComponentUtil;

namespace LiveSplit.Sonic2Absolute
{
    class Watchers : MemoryWatcherList
    {
        // Game process
        private readonly Process game;
        public bool IsGameHooked => !(game == null || game.HasExited);

        // Imported game data
        private MemoryWatcher<byte> LevelID { get; }
        private MemoryWatcher<uint> ZoneIndicator { get; }
        private MemoryWatcher<byte> State { get; }
        private MemoryWatcher<byte> StartIndicator { get; }
        private MemoryWatcher<byte> ZoneSelectOnGameComplete { get; }

        // Fake MemoryWatchers: used to convert game data into more easily readable formats
        public FakeMemoryWatcher<Acts> Act = new FakeMemoryWatcher<Acts>(Acts.EmeraldHillAct1, Acts.EmeraldHillAct1);
        public FakeMemoryWatcher<bool> RunStartedSaveFile = new FakeMemoryWatcher<bool>(false, false);
        public FakeMemoryWatcher<bool> RunStartedNoSaveFile = new FakeMemoryWatcher<bool>(false, false);
        public FakeMemoryWatcher<bool> StartingNewGame = new FakeMemoryWatcher<bool>(false, false);
        public FakeMemoryWatcher<bool> RunStartedNGP = new FakeMemoryWatcher<bool>(false, false);

        public Watchers()
        {
            foreach (var process in new string[] { "Sonic2Absolute" })
            {
                game = Process.GetProcessesByName(process).OrderByDescending(x => x.StartTime).FirstOrDefault(x => !x.HasExited);
                if (game == null) continue;
            }
            if (game == null) throw new Exception("Couldn't connect to the game!");

            var scanner = new SignatureScanner(game, game.MainModuleWow64Safe().BaseAddress, game.MainModuleWow64Safe().ModuleMemorySize);
            IntPtr ptr;
            
            switch ((GameVersion)game.MainModuleWow64Safe().ModuleMemorySize)
            {
                case GameVersion.v1_0_0_and_1_0_1:
                    ptr = scanner.Scan(new SigScanTarget(2,
                        "03 05 ????????",         // add eax,[SonicForever.exe+BF4E6C]
                        "69 C8 C1000000"));       // imul ecx,eax,000000C1
                    if (ptr == IntPtr.Zero) throw new Exception("Couldn't find address for the following variables: LevelID, ZoneIndicator");
                    this.LevelID = new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr)));
                    this.ZoneIndicator = new MemoryWatcher<uint>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 4));

                    ptr = scanner.Scan(new SigScanTarget(2,
                        "8B 80 ????????",      // mov eax,[eax+SonicForever.exe+90FAAC]
                        "89 04 95 ????????",   // mov [edx*4+SonicForever.exe+1234F00],eax
                        "E9 870C0000"));       // jmp SonicForever.exe+2AAA6
                    if (ptr == IntPtr.Zero) throw new Exception("Couldn't find address for the following variable: State");
                    this.State = new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 0x9D8));

                    ptr = scanner.Scan(new SigScanTarget(2,
                        "8B 80 ????????",      // mov eax,[eax+SonicForever.exe+90FAAC]
                        "89 04 95 ????????",   // mov [edx*4+SonicForever.exe+1234F00],eax
                        "E9 BE130000"));       // jmp SonicForever.exe+2AAA6
                    if (ptr == IntPtr.Zero) throw new Exception("Couldn't find address for the following variable: StartIndicator");
                    this.StartIndicator = new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 0x9D8));

                    ptr = scanner.Scan(new SigScanTarget(2,
                        "8B 80 ????????",      // mov eax,[eax+SonicForever.exe+90FAAC]
                        "89 04 95 ????????",   // mov [edx*4+SonicForever.exe+1234F00],eax
                        "E9 570C0000"));       // jmp SonicForever.exe+2AAA6
                    if (ptr == IntPtr.Zero) throw new Exception("Couldn't find address for the following variable: ZoneSelectOnGameComplete");
                    this.ZoneSelectOnGameComplete = new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 0x9D8));
                    break;

                case GameVersion.v1_0_2:
                    this.LevelID = new MemoryWatcher<byte>(new DeepPointer(game.MainModuleWow64Safe().BaseAddress + 0x141B6CC));
                    this.ZoneIndicator = new MemoryWatcher<uint>(new DeepPointer(game.MainModuleWow64Safe().BaseAddress + 0x15F6050));
                    this.State = new MemoryWatcher<byte>(new DeepPointer(game.MainModuleWow64Safe().BaseAddress + 0x131169C));
                    this.StartIndicator = new MemoryWatcher<byte>(new DeepPointer(game.MainModuleWow64Safe().BaseAddress + 0x131171C));
                    this.ZoneSelectOnGameComplete = new MemoryWatcher<byte>(new DeepPointer(game.MainModuleWow64Safe().BaseAddress + 0x13116A4));
                    break;

                default:
                    throw new Exception("Game version unknown");
            }

            this.AddRange(this.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Where(p => !p.GetIndexParameters().Any()).Select(p => p.GetValue(this, null) as MemoryWatcher).Where(p => p != null));
        }

        public void Update()
        {
            this.UpdateAll(game);

            this.RunStartedSaveFile.Old = this.RunStartedSaveFile.Current;
            this.RunStartedSaveFile.Current = this.State.Old == 5 && this.State.Current == 7;
            this.RunStartedSaveFile.Changed = !this.RunStartedSaveFile.Old.Equals(this.RunStartedSaveFile.Current);

            this.RunStartedNoSaveFile.Old = this.RunStartedNoSaveFile.Current;
            this.RunStartedNoSaveFile.Current = this.State.Current == 4 && this.StartIndicator.Changed && this.StartIndicator.Current == 1;
            this.RunStartedNoSaveFile.Changed = !this.RunStartedNoSaveFile.Old.Equals(this.RunStartedNoSaveFile.Current);

            this.RunStartedNGP.Old = this.RunStartedNGP.Current;
            this.RunStartedNGP.Current = this.State.Current == 6 && this.StartIndicator.Changed && this.StartIndicator.Current == 1 && this.ZoneSelectOnGameComplete.Current == 0;
            this.RunStartedNGP.Changed = !this.RunStartedNGP.Old.Equals(this.RunStartedNGP.Current);

            this.Act.Old = this.Act.Current;
            this.Act.Current = (ZoneIndicator)this.ZoneIndicator.Current == Sonic2Absolute.ZoneIndicator.Ending ? Acts.Ending : (ZoneIndicator)ZoneIndicator.Current == Sonic2Absolute.ZoneIndicator.Zones ? (Acts)LevelID.Current : this.Act.Old;
            this.Act.Changed = !this.Act.Old.Equals(this.Act.Current);

            this.StartingNewGame.Old = this.StartingNewGame.Current;
            this.StartingNewGame.Current = this.State.Old == 0 && (this.State.Current == 4 || this.State.Current == 5);
            this.StartingNewGame.Changed = !this.StartingNewGame.Old.Equals(this.StartingNewGame.Current);
        }
    }

    class FakeMemoryWatcher<T>
    {
        public T Current { get; set; }
        public T Old { get; set; }
        public bool Changed { get; set; }
        public FakeMemoryWatcher(T old, T current)
        {
            this.Old = old;
            this.Current = current;
            this.Changed = !old.Equals(current);
        }
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
