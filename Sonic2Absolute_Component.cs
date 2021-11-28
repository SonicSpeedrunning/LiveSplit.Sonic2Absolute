using System;
using System.Windows.Forms;
using System.Xml;
using LiveSplit.Model;
using LiveSplit.UI;
using LiveSplit.UI.Components;

namespace LiveSplit.Sonic2Absolute
{
    class Component : LogicComponent
    {
        public override string ComponentName => "Sonic 2 Absolute - Autosplitter";
        private Settings Settings { get; set; }
        private readonly TimerModel timer;
        private readonly System.Timers.Timer update_timer;
        private readonly SplittingLogic SplittingLogic;

        public Component(LiveSplitState state)
        {
            timer = new TimerModel { CurrentState = state };
            Settings = new Settings();
            Settings.OnbtnSetSplits_Click += OnSetSplits;

            SplittingLogic = new SplittingLogic();
            SplittingLogic.OnStartTrigger += OnStartTrigger;
            SplittingLogic.OnSplitTrigger += OnSplitTrigger;
            SplittingLogic.OnResetTrigger += OnResetTrigger;

            update_timer = new System.Timers.Timer() { Interval = 15, Enabled = true, AutoReset = false };
            update_timer.Elapsed += delegate { SplittingLogic.Update(); update_timer.Start(); };
        }

        void OnStartTrigger(object sender, StartTrigger type)
        {
            if (timer.CurrentState.CurrentPhase != TimerPhase.NotRunning) return;
            switch (type)
            {
                case StartTrigger.NewGame: if (Settings.RunStart) timer.Start(); break;
                case StartTrigger.NewGamePlus: if (Settings.RunStartNGP) timer.Start(); break;
            }
        }

        void OnSplitTrigger(object sender, Acts type)
        {
            if (timer.CurrentState.CurrentPhase != TimerPhase.Running) return;
            switch (type)
            {
                case Acts.EmeraldHillAct1: if (Settings.EH1) timer.Split(); break;
                case Acts.EmeraldHillAct2: if (Settings.EH2) timer.Split(); break;
                case Acts.ChemicalPlantAct1: if (Settings.CP1) timer.Split(); break;
                case Acts.ChemicalPlantAct2: if (Settings.CP2) timer.Split(); break;
                case Acts.AquaticRuinAct1: if (Settings.AR1) timer.Split(); break;
                case Acts.AquaticRuinAct2: if (Settings.AR2) timer.Split(); break;
                case Acts.CasinoNightAct1: if (Settings.CN1) timer.Split(); break;
                case Acts.CasinoNightAct2: if (Settings.CN2) timer.Split(); break;
                case Acts.HillTopAct1: if (Settings.HT1) timer.Split(); break;
                case Acts.HillTopAct2: if (Settings.HT2) timer.Split(); break;
                case Acts.MysticCaveAct1: if (Settings.MC1) timer.Split(); break;
                case Acts.MysticCaveAct2: if (Settings.MC2) timer.Split(); break;
                case Acts.OilOceanAct1: if (Settings.OO1) timer.Split(); break;
                case Acts.OilOceanAct2: if (Settings.OO2) timer.Split(); break;
                case Acts.MetropolisAct1: if (Settings.MZ1) timer.Split(); break;
                case Acts.MetropolisAct2: if (Settings.MZ2) timer.Split(); break;
                case Acts.MetropolisAct3: if (Settings.MZ3) timer.Split(); break;
                case Acts.SkyChase: if (Settings.SCZ) timer.Split(); break;
                case Acts.WingFortress: if (Settings.WFZ) timer.Split(); break;
                case Acts.DeathEgg: if (Settings.DEZ) timer.Split(); break;
            }
        }

        void OnResetTrigger(object sender, EventArgs e)
        {
            if (timer.CurrentState.CurrentPhase == TimerPhase.Running && Settings.Reset) timer.Reset();
        }

        public override void Dispose()
        {
            Settings.Dispose();
            update_timer?.Dispose();
        }

        public void OnSetSplits(object sender, EventArgs e)
        {
            var question = MessageBox.Show("This will set up your splits according to your selected autosplitting options.\n" +
                                            "WARNING: Any existing PB recorded for the current layout will be deleted.\n\n" +
                                            "Do you want to continue?", "Livesplit - Sonic 2 Absolute", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (question == DialogResult.No) return;
            timer.CurrentState.Run.Clear();
            if (Settings.EH1) timer.CurrentState.Run.AddSegment("Emerald Hill - Act 1");
            if (Settings.EH2) timer.CurrentState.Run.AddSegment("Emerald Hill - Act 1");
            if (Settings.CP1) timer.CurrentState.Run.AddSegment("Chemical Plant - Act 1");
            if (Settings.CP2) timer.CurrentState.Run.AddSegment("Chemical Plant - Act 2");
            if (Settings.AR1) timer.CurrentState.Run.AddSegment("Aquatic Ruin - Act 1");
            if (Settings.AR2) timer.CurrentState.Run.AddSegment("Aquatic Ruin - Act 2");
            if (Settings.CN1) timer.CurrentState.Run.AddSegment("Casino Night- Act 1");
            if (Settings.CN2) timer.CurrentState.Run.AddSegment("Casino Night- Act 2");
            if (Settings.HT1) timer.CurrentState.Run.AddSegment("Hill Top - Act 1");
            if (Settings.HT2) timer.CurrentState.Run.AddSegment("Hill Top - Act 2");
            if (Settings.MC1) timer.CurrentState.Run.AddSegment("Mystic Cave - Act 1");
            if (Settings.MC2) timer.CurrentState.Run.AddSegment("Mystic Cave - Act 2");
            if (Settings.OO1) timer.CurrentState.Run.AddSegment("Oil Ocean - Act 1");
            if (Settings.OO2) timer.CurrentState.Run.AddSegment("Oil Ocean - Act 2");
            if (Settings.MZ1) timer.CurrentState.Run.AddSegment("Metropolis - Act 1");
            if (Settings.MZ2) timer.CurrentState.Run.AddSegment("Metropolis - Act 2");
            if (Settings.MZ3) timer.CurrentState.Run.AddSegment("Metropolis - Act 3");
            if (Settings.SCZ) timer.CurrentState.Run.AddSegment("Sky Chase");
            if (Settings.WFZ) timer.CurrentState.Run.AddSegment("Wing Fortress");
            if (Settings.DEZ) timer.CurrentState.Run.AddSegment("Death Egg");
            if (timer.CurrentState.Run.Count == 0) timer.CurrentState.Run.AddSegment("");
        }

        public override XmlNode GetSettings(XmlDocument document) { return this.Settings.GetSettings(document); }

        public override Control GetSettingsControl(LayoutMode mode) { return this.Settings; }

        public override void SetSettings(XmlNode settings) { this.Settings.SetSettings(settings); }

        public override void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode) { }
    }
}
