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
        private Settings settings { get; set; }
        private readonly TimerModel timer;
        private readonly System.Timers.Timer update_timer;
        private readonly SplittingLogic SplittingLogic;

        public Component(LiveSplitState state)
        {
            timer = new TimerModel { CurrentState = state };
            settings = new Settings();

            SplittingLogic = new SplittingLogic();
            SplittingLogic.OnStartTrigger += OnStartTrigger;
            SplittingLogic.OnSplitTrigger += OnSplitTrigger;
            SplittingLogic.OnResetTrigger += OnResetTrigger;

            update_timer = new System.Timers.Timer() { Interval = 15, Enabled = true, AutoReset = false };
            update_timer.Elapsed += updateTimer_Tick;
        }

        void updateTimer_Tick(object sender, EventArgs e)
        {
            SplittingLogic.Update();
            update_timer.Start();
        }

        void OnStartTrigger(object sender, EventArgs e)
        {
            if (timer.CurrentState.CurrentPhase != TimerPhase.NotRunning) return;
            if (settings.RunStart) timer.Start();
        }

        void OnSplitTrigger(object sender, Acts type)
        {
            if (timer.CurrentState.CurrentPhase != TimerPhase.Running) return;
            switch (type)
            {
                case Acts.EmeraldHillAct1: if (settings.EH1) timer.Split(); break;
                case Acts.EmeraldHillAct2: if (settings.EH2) timer.Split(); break;
                case Acts.ChemicalPlantAct1: if (settings.CP1) timer.Split(); break;
                case Acts.ChemicalPlantAct2: if (settings.CP2) timer.Split(); break;
                case Acts.AquaticRuinAct1: if (settings.AR1) timer.Split(); break;
                case Acts.AquaticRuinAct2: if (settings.AR2) timer.Split(); break;
                case Acts.CasinoNightAct1: if (settings.CN1) timer.Split(); break;
                case Acts.CasinoNightAct2: if (settings.CN2) timer.Split(); break;
                case Acts.HillTopAct1: if (settings.HT1) timer.Split(); break;
                case Acts.HillTopAct2: if (settings.HT2) timer.Split(); break;
                case Acts.MysticCaveAct1: if (settings.MC1) timer.Split(); break;
                case Acts.MysticCaveAct2: if (settings.MC2) timer.Split(); break;
                case Acts.OilOceanAct1: if (settings.OO1) timer.Split(); break;
                case Acts.OilOceanAct2: if (settings.OO2) timer.Split(); break;
                case Acts.MetropolisAct1: if (settings.MZ1) timer.Split(); break;
                case Acts.MetropolisAct2: if (settings.MZ2) timer.Split(); break;
                case Acts.MetropolisAct3: if (settings.MZ3) timer.Split(); break;
                case Acts.SkyChase: if (settings.SCZ) timer.Split(); break;
                case Acts.WingFortress: if (settings.WFZ) timer.Split(); break;
                case Acts.DeathEgg: if (settings.DEZ) timer.Split(); break;
            }
        }

        void OnResetTrigger(object sender, EventArgs e)
        {
            if (timer.CurrentState.CurrentPhase == TimerPhase.Running && settings.Reset) timer.Reset();
        }

        public override void Dispose()
        {
            settings.Dispose();
            update_timer?.Dispose();
        }

        public override XmlNode GetSettings(XmlDocument document) { return this.settings.GetSettings(document); }

        public override Control GetSettingsControl(LayoutMode mode) { return this.settings; }

        public override void SetSettings(XmlNode settings) { this.settings.SetSettings(settings); }

        public override void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode) { }
    }
}
