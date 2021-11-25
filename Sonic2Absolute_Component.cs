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

            SplittingLogic = new SplittingLogic();
            SplittingLogic.OnStartTrigger += OnStartTrigger;
            SplittingLogic.OnSplitTrigger += OnSplitTrigger;
            SplittingLogic.OnResetTrigger += OnResetTrigger;

            update_timer = new System.Timers.Timer() { Interval = 15, Enabled = true, AutoReset = false };
            update_timer.Elapsed += UpdateTimer_Tick;
        }

        void UpdateTimer_Tick(object sender, EventArgs e)
        {
            SplittingLogic.Update();
            update_timer.Start();
        }

        void OnStartTrigger(object sender, EventArgs e)
        {
            if (timer.CurrentState.CurrentPhase != TimerPhase.NotRunning) return;
            if (Settings.RunStart) timer.Start();
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

        public override XmlNode GetSettings(XmlDocument document) { return this.Settings.GetSettings(document); }

        public override Control GetSettingsControl(LayoutMode mode) { return this.Settings; }

        public override void SetSettings(XmlNode settings) { this.Settings.SetSettings(settings); }

        public override void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode) { }
    }
}
