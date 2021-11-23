using LiveSplit.ComponentUtil;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace LiveSplit.Sonic2Absolute
{
    class Watchers : MemoryWatcherList
    {
        public MemoryWatcher<byte> LevelID { get; }
        public MemoryWatcher<uint> ZoneIndicator { get; }
        public MemoryWatcher<byte> State { get; }
        public MemoryWatcher<byte> StartIndicator { get; }

        public GameAct Act = new GameAct() { Current = Acts.EmeraldHillAct1, Old = Acts.EmeraldHillAct1 };


        public Watchers(Process game)
        {
            var scanner = new SignatureScanner(game, game.MainModuleWow64Safe().BaseAddress, game.MainModuleWow64Safe().ModuleMemorySize);
            IntPtr ptr;

            ptr = scanner.Scan(new SigScanTarget(2,
                                        "03 05 ????????",         // add eax,[SonicForever.exe+BF4E6C]
                                        "69 C8 C1000000"));       // imul ecx,eax,000000C1
            if (ptr == IntPtr.Zero) throw new Exception();
            this.LevelID =  new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr)));
            this.ZoneIndicator = new MemoryWatcher<uint>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 4));

            ptr = scanner.Scan(new SigScanTarget(2,
                            "8B 80 ????????",      // mov eax,[eax+SonicForever.exe+90FAAC]
                            "89 04 95 ????????",   // mov [edx*4+SonicForever.exe+1234F00],eax
                            "E9 870C0000"));       // jmp SonicForever.exe+2AAA6
            if (ptr == IntPtr.Zero) throw new Exception();
            this.State = new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 0x9D8));

            ptr = scanner.Scan(new SigScanTarget(2,
                "8B 80 ????????",      // mov eax,[eax+SonicForever.exe+90FAAC]
                "89 04 95 ????????",   // mov [edx*4+SonicForever.exe+1234F00],eax
                "E9 BE130000"));       // jmp SonicForever.exe+2AAA6
            if (ptr == IntPtr.Zero) throw new Exception();
            this.StartIndicator = new MemoryWatcher<byte>(new DeepPointer((IntPtr)game.ReadValue<int>(ptr) + 0x9D8));

            this.AddRange(this.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Where(p => !p.GetIndexParameters().Any()).Select(p => p.GetValue(this, null) as MemoryWatcher).Where(p => p != null));
        }
    }

    class FakeMemoryWatcher<T>
    {
        public T Current { get; set; }
        public T Old { get; set; }
        public bool Changed { get; }
        public FakeMemoryWatcher(T old, T current)
        {
            this.Old = old;
            this.Current = current;
            this.Changed = !old.Equals(current);
        }
    }

    class GameAct
    {
        public Acts Current { get; set; }
        public Acts Old { get; set; }
        public bool Changed { get; set; }
        public void Update(MemoryWatcher<byte> LevelID, MemoryWatcher<uint> ZoneIndicate)
        {
            this.Old = this.Current;
            this.Current = (ZoneIndicator)ZoneIndicate.Current == ZoneIndicator.Ending ? Acts.Ending : (ZoneIndicator)ZoneIndicate.Current == ZoneIndicator.Zones ? (Acts)LevelID.Current : this.Old;
            this.Changed = !this.Old.Equals(this.Current);
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
