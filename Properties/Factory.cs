using LiveSplit.Model;
using LiveSplit.Sonic2Absolute;
using LiveSplit.UI.Components;
using System;
using System.Reflection;

[assembly: ComponentFactory(typeof(Sonic2AbsoluteFactory))]

namespace LiveSplit.Sonic2Absolute
{
    public class Sonic2AbsoluteFactory : IComponentFactory
    {
        public string ComponentName => "Sonic 2 Absolute - Autosplitter";
        public string Description => "Autosplitter";
        public ComponentCategory Category => ComponentCategory.Control;
        public string UpdateName => this.ComponentName;
        public string UpdateURL => "https://raw.githubusercontent.com/SonicSpeedrunning/LiveSplit.Sonic2Absolute/master/";
        public Version Version => Assembly.GetExecutingAssembly().GetName().Version;
        public string XMLURL => this.UpdateURL + "Components/LiveSplit.Sonic2Absolute.xml";
        public IComponent Create(LiveSplitState state) => new Sonic2AbsoluteComponent(state);
    }
}
