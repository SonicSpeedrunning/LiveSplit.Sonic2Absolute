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
                    throw new Exception("Unsupported game version!");
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
