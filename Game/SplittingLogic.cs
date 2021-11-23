using System;
using System.Diagnostics;
using System.Linq;

namespace LiveSplit.Sonic2Absolute
{
    class SplittingLogic
    {
        private Process game;
        private Watchers watchers;

        public event EventHandler OnStartTrigger;
        public event EventHandler<Acts> OnSplitTrigger;
        public event EventHandler OnResetTrigger;

        public void Update()
        {
            if (!VerifyOrHookGameProcess()) return;
            watchers.UpdateAll(game);
            watchers.Act.Update(watchers.LevelID, watchers.ZoneIndicator);
            Start();
            ResetLogic();
            Split();
        }

        void Start()
        {
            if ((watchers.State.Old == 5 && watchers.State.Current == 7) ||
                (watchers.State.Current == 4 && watchers.StartIndicator.Changed && watchers.StartIndicator.Current == 1))
                this.OnStartTrigger?.Invoke(this, EventArgs.Empty);
        }

        void ResetLogic()
        {
            if (watchers.State.Old == 0 && (watchers.State.Current == 4 || watchers.State.Current == 5)) this.OnResetTrigger?.Invoke(this, EventArgs.Empty);
        }

        void Split()
        {
            if (watchers.Act.Changed && watchers.Act.Current == Acts.Ending) { this.OnSplitTrigger?.Invoke(this, Acts.DeathEgg); }
            else if (watchers.Act.Old == watchers.Act.Current - 1) { this.OnSplitTrigger?.Invoke(this, watchers.Act.Old); }
        }

        bool VerifyOrHookGameProcess()
        {
            if (!(game == null || game.HasExited)) return true;
            foreach (var process in new string[] { "Sonic2Absolute" })
            {
                game = Process.GetProcessesByName(process).OrderByDescending(x => x.StartTime).FirstOrDefault(x => !x.HasExited);
                if (game == null) continue;
                try { watchers = new Watchers(game); }
                catch { game = null; return false; }
                return true;
            }
            return false;
        }
    }
}
