using LiveSplit.Sonic2Absolute;
using LiveSplit.Model;
using LiveSplit.UI.Components;
using System;
using System.Reflection;

[assembly: ComponentFactory(typeof(Factory))]

namespace LiveSplit.Sonic2Absolute
{
    public class Factory : IComponentFactory
    {
        public string ComponentName => "Sonic 2 Absolute - Autosplitter";
        public string Description => "Automatic splitting and IGT timing";
        public ComponentCategory Category => ComponentCategory.Control;
        public string UpdateName => this.ComponentName;
        public string UpdateURL => "https://raw.githubusercontent.com/SonicSpeedrunning/LiveSplit.Sonic2Absolute/master/";
        public Version Version => Assembly.GetExecutingAssembly().GetName().Version;
        public string XMLURL => this.UpdateURL + "Components/LiveSplit.Sonic2Absolute.xml";
        public IComponent Create(LiveSplitState state) { return new Component(state); }
    }
}
