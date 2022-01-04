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
        public bool IsGameHooked => game != null && !game.HasExited;

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
            game = Process.GetProcessesByName("Sonic2Absolute").OrderByDescending(x => x.StartTime).FirstOrDefault(x => !x.HasExited);
            if (game == null) throw new Exception("Couldn't connect to the game!");

            switch ((GameVersion)game.MainModuleWow64Safe().ModuleMemorySize)
            {
                case GameVersion.v1_0_0_and_1_0_1:
                    this.LevelID = new MemoryWatcher<byte>(new DeepPointer(game.MainModuleWow64Safe().BaseAddress + 0x15F4454));
                    this.ZoneIndicator = new MemoryWatcher<uint>(new DeepPointer(game.MainModuleWow64Safe().BaseAddress + 0x15F4458));
                    this.State = new MemoryWatcher<byte>(new DeepPointer(game.MainModuleWow64Safe().BaseAddress + 0x130FAAC));
                    this.StartIndicator = new MemoryWatcher<byte>(new DeepPointer(game.MainModuleWow64Safe().BaseAddress + 0x130FB2C));
                    this.ZoneSelectOnGameComplete = new MemoryWatcher<byte>(new DeepPointer(game.MainModuleWow64Safe().BaseAddress + 0x130FAB4));
                    break;

                case GameVersion.v1_0_2:
                    this.LevelID = new MemoryWatcher<byte>(new DeepPointer(game.MainModuleWow64Safe().BaseAddress + 0x141B6CC));
                    this.ZoneIndicator = new MemoryWatcher<uint>(new DeepPointer(game.MainModuleWow64Safe().BaseAddress + 0x15F6050));
                    this.State = new MemoryWatcher<byte>(new DeepPointer(game.MainModuleWow64Safe().BaseAddress + 0x131169C));
                    this.StartIndicator = new MemoryWatcher<byte>(new DeepPointer(game.MainModuleWow64Safe().BaseAddress + 0x131171C));
                    this.ZoneSelectOnGameComplete = new MemoryWatcher<byte>(new DeepPointer(game.MainModuleWow64Safe().BaseAddress + 0x13116A4));
                    break;

                default:
                    var scanner = new SignatureScanner(game, game.MainModuleWow64Safe().BaseAddress, game.MainModuleWow64Safe().ModuleMemorySize);
                    IntPtr ptr;

                    ptr = scanner.Scan(new SigScanTarget(1,
                        "A3 ????????",      // mov [Sonic2Absolute.exe+141B6CC],eax  <---
                        "E8 ????????",      // call Sonic2Absolute.exe+B410
                        "33 D2")            // xor edx,edx
                    { OnFound = (p, s, addr) => (IntPtr)p.ReadValue<int>(addr) });
                    if (ptr == IntPtr.Zero) throw new Exception();
                    this.LevelID = new MemoryWatcher<byte>(new DeepPointer(ptr));

                    ptr = scanner.Scan(new SigScanTarget(7,
                        "69 F8 ????????")   // imul edi,eax,000000C1
                    { OnFound = (p, s, addr) => (IntPtr)p.ReadValue<int>(addr) });
                    if (ptr == IntPtr.Zero) throw new Exception();
                    this.ZoneIndicator = new MemoryWatcher<uint>(new DeepPointer(ptr));

                    ptr = scanner.Scan(new SigScanTarget(4,
                        "89 45 F8",     // mov [ebp-08],eax
                        "A1 ????????")  // mov eax,[Sonic2Absolute.exe+1310D8C]  <---
                    { OnFound = (p, s, addr) => (IntPtr)p.ReadValue<int>(addr) + 0x910 });
                    if (ptr == IntPtr.Zero) throw new Exception();
                    this.State = new MemoryWatcher<byte>(new DeepPointer(ptr));
                    this.StartIndicator = new MemoryWatcher<byte>(new DeepPointer(ptr + 0x80));
                    this.ZoneSelectOnGameComplete = new MemoryWatcher<byte>(new DeepPointer(ptr + 0x8));
                    break;
            }

            this.AddRange(this.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Where(p => !p.GetIndexParameters().Any()).Select(p => p.GetValue(this, null) as MemoryWatcher).Where(p => p != null));
        }

        public void Update()
        {
            this.UpdateAll(game);

            this.RunStartedSaveFile.Old = this.RunStartedSaveFile.Current;
            this.RunStartedSaveFile.Current = this.State.Old == 5 && this.State.Current == 7;

            this.RunStartedNoSaveFile.Old = this.RunStartedNoSaveFile.Current;
            this.RunStartedNoSaveFile.Current = this.State.Current == 4 && this.StartIndicator.Changed && this.StartIndicator.Current == 1;

            this.RunStartedNGP.Old = this.RunStartedNGP.Current;
            this.RunStartedNGP.Current = this.State.Current == 6 && this.StartIndicator.Changed && this.StartIndicator.Current == 1 && this.ZoneSelectOnGameComplete.Current == 0;

            this.Act.Old = this.Act.Current;
            this.Act.Current = (ZoneIndicator)this.ZoneIndicator.Current == Sonic2Absolute.ZoneIndicator.Ending ? Acts.Ending : (ZoneIndicator)ZoneIndicator.Current == Sonic2Absolute.ZoneIndicator.Zones ? (Acts)LevelID.Current : this.Act.Old;

            this.StartingNewGame.Old = this.StartingNewGame.Current;
            this.StartingNewGame.Current = this.State.Old == 0 && (this.State.Current == 4 || this.State.Current == 5);
        }
    }

    class FakeMemoryWatcher<T>
    {
        public T Current { get; set; }
        public T Old { get; set; }
        public bool Changed => !this.Old.Equals(this.Current);
        public FakeMemoryWatcher(T old, T current)
        {
            this.Old = old;
            this.Current = current;
        }
    }
}
