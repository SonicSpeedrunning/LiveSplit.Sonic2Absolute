using System;
using System.Threading;

namespace LiveSplit.Sonic2Absolute
{
    class SplittingLogic
    {
        private Watchers watchers;

        public event EventHandler OnStartTrigger;
        public event EventHandler<Acts> OnSplitTrigger;
        public event EventHandler OnResetTrigger;

        public void Update()
        {
            if (!VerifyOrHookGameProcess()) return;
            watchers.Update();
            Start();
            ResetLogic();
            Split();
        }

        void Start()
        {
            if ((watchers.RunStartedSaveFile.Current && !watchers.RunStartedSaveFile.Old) || (watchers.RunStartedNoSaveFile.Current && !watchers.RunStartedNoSaveFile.Old))
                this.OnStartTrigger?.Invoke(this, EventArgs.Empty);
        }

        void ResetLogic()
        {
            if (watchers.StartingNewGame.Current && !watchers.StartingNewGame.Old) this.OnResetTrigger?.Invoke(this, EventArgs.Empty);
        }

        void Split()
        {
            if (watchers.Act.Changed && watchers.Act.Current == Acts.Ending) { this.OnSplitTrigger?.Invoke(this, Acts.DeathEgg); }
            else if (watchers.Act.Old == watchers.Act.Current - 1) { this.OnSplitTrigger?.Invoke(this, watchers.Act.Old); }
        }

        bool VerifyOrHookGameProcess()
        {
            if (watchers != null && watchers.IsGameHooked) return true;
            try { watchers = new Watchers(); } catch { Thread.Sleep(500); return false; }
            return true;
        }
    }
}
