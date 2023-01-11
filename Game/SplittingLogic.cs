using System;

namespace LiveSplit.Sonic2Absolute
{
    partial class Sonic2AbsoluteComponent
    {
        private bool Start()
        {
            bool RunStartedSaveFile, RunStartedNoSaveFile, RunStartedNGP;

            RunStartedSaveFile = watchers.State.Old == 5 && watchers.State.Current == 7;
            RunStartedNoSaveFile = watchers.State.Current == 4 && watchers.StartIndicator.Changed && watchers.StartIndicator.Current == 1;
            RunStartedNGP = watchers.State.Current == 6 && watchers.StartIndicator.Changed && watchers.StartIndicator.Current == 1 && watchers.ZoneSelectOnGameComplete.Current == 0;

            return (settings.StartCleanSave && (RunStartedSaveFile || RunStartedNoSaveFile)) || (settings.StartNewGamePlus && RunStartedNGP);
        }

        private bool Split()
        {
            if (watchers.Act.Current == watchers.Act.Old + 1)
                return settings["c" + ((int)watchers.Act.Old + 1).ToString()];
            else if (watchers.Act.Current == (Acts)(-1) && watchers.Act.Current != watchers.Act.Old)
                return settings.c20;
            else return false;
        }

        bool Reset()
        {
            if (!settings.Reset)
                return false;

            return watchers.State.Old == 0 && (watchers.State.Current == 4 || watchers.State.Current == 5);
        }

        bool IsLoading()
        {
            return false;
        }

        private TimeSpan? GameTime()
        {
            return null;
        }
    }
}