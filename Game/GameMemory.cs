using System;
using System.Linq;
using System.Reflection;
using LiveSplit.ComponentUtil;

namespace LiveSplit.Sonic2Absolute
{
    partial class Watchers
    {
        // Watchers
        private MemoryWatcher<byte> LevelID { get; set; }
        public MemoryWatcher<byte> StartIndicator { get; private set; }
        public MemoryWatcher<byte> State { get; private set; }
        public MemoryWatcher<byte> ZoneSelectOnGameComplete { get; private set; }
        public MemoryWatcher<ZoneIndicator> ZoneIndicator { get; private set; }

        // Fake watchers
        public FakeMemoryWatcher<Acts> Act { get; private set; }


        public Watchers()
        {
            Act = new FakeMemoryWatcher<Acts>(() => ZoneIndicator.Current == Sonic2Absolute.ZoneIndicator.Ending ? (Acts)(-1) : ZoneIndicator.Current == Sonic2Absolute.ZoneIndicator.Zones ? (Acts)LevelID.Current : Act.Old);
            GameProcess = new ProcessHook("Sonic2Absolute");
        }

        public void Update()
        {
            WatcherList.UpdateAll(game);
            Act.Update();
        }

        /// <summary>
        /// This function is essentially equivalent of the init descriptor in script-based autosplitters.
        /// Everything you want to be executed when the game gets hooked needs to be put here.
        /// The main purpose of this function is to perform sigscanning and get memory addresses and offsets
        /// needed by the autosplitter.
        /// </summary>
        private void GetAddresses()
        {
            // This game is 32bit only
            if (game.Is64Bit())
                throw new NotSupportedException();

            SignatureScanner Scanner = game.SigScanner();
            IntPtr ptr = IntPtr.Zero;            
            IntPtr pointerPath(int offset1, int offset2, int offset3) => (IntPtr)new DeepPointer(ptr + offset1, offset2).Deref<int>(game) + offset3;

            ptr = Scanner.ScanOrThrow(new SigScanTarget(14, "3D ???????? 0F 87 ???????? FF 24 85 ???????? A1") { OnFound = (p, s, addr) => p.ReadPointer(addr) });
            State = new MemoryWatcher<byte>(pointerPath(0x4 * 89, 8, 0x9D8));
            LevelID = new MemoryWatcher<byte>(pointerPath(0x4 * 123, 1, 0));
            StartIndicator = new MemoryWatcher<byte>(pointerPath(0x4 * 30, 8, 0x9D8));
            ZoneSelectOnGameComplete = new MemoryWatcher<byte>(pointerPath(0x4 * 91, 8, 0x9D8));
            ZoneIndicator = new MemoryWatcher<ZoneIndicator>(Scanner.ScanOrThrow(new SigScanTarget(7, "69 F8 ???????? B8 ????????") { OnFound = (p, s, addr) => p.ReadPointer(addr) }));

            WatcherList = new MemoryWatcherList();
            WatcherList
                .AddRange(GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(p => !p.GetIndexParameters().Any())
                .Select(p => p.GetValue(this, null) as MemoryWatcher)
                .Where(p => p != null));
        }
    }
}