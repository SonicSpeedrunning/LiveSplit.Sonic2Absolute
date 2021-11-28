using System;
using System.Xml;
using System.Windows.Forms;

namespace LiveSplit.Sonic2Absolute
{
    public partial class Settings : UserControl
    {
        public bool RunStart { get; set; }
        public bool RunStartNGP { get; set; }
        public bool Reset { get; set; }
        public bool EH1 { get; set; }
        public bool EH2 { get; set; }
        public bool CP1 { get; set; }
        public bool CP2 { get; set; }
        public bool AR1 { get; set; }
        public bool AR2 { get; set; }
        public bool CN1 { get; set; }
        public bool CN2 { get; set; }
        public bool HT1 { get; set; }
        public bool HT2 { get; set; }
        public bool MC1 { get; set; }
        public bool MC2 { get; set; }
        public bool OO1 { get; set; }
        public bool OO2 { get; set; }
        public bool MZ1 { get; set; }
        public bool MZ2 { get; set; }
        public bool MZ3 { get; set; }
        public bool SCZ { get; set; }
        public bool WFZ { get; set; }
        public bool DEZ { get; set; }

        // Event Handlers
        public event EventHandler OnbtnSetSplits_Click;
        void btnSetSplits_Click(object sender, EventArgs e) { this.OnbtnSetSplits_Click?.Invoke(this, EventArgs.Empty); }


        public Settings()
        {
            InitializeComponent();

            // General settings
            this.chkRunStart.DataBindings.Add("Checked", this, "RunStart", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkNGPstart.DataBindings.Add("Checked", this, "RunStartNGP", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkReset.DataBindings.Add("Checked", this, "Reset", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkEH1.DataBindings.Add("Checked", this, "EH1", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkEH2.DataBindings.Add("Checked", this, "EH2", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkCP1.DataBindings.Add("Checked", this, "CP1", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkCP2.DataBindings.Add("Checked", this, "CP2", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkAR1.DataBindings.Add("Checked", this, "AR1", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkAR2.DataBindings.Add("Checked", this, "AR2", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkCN1.DataBindings.Add("Checked", this, "CN1", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkCN2.DataBindings.Add("Checked", this, "CN2", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkHT1.DataBindings.Add("Checked", this, "HT1", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkHT2.DataBindings.Add("Checked", this, "HT2", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkMC1.DataBindings.Add("Checked", this, "MC1", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkMC2.DataBindings.Add("Checked", this, "MC2", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkOO1.DataBindings.Add("Checked", this, "OO1", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkOO2.DataBindings.Add("Checked", this, "OO2", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkMZ1.DataBindings.Add("Checked", this, "MZ1", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkMZ2.DataBindings.Add("Checked", this, "MZ2", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkMZ3.DataBindings.Add("Checked", this, "MZ3", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkSCZ.DataBindings.Add("Checked", this, "SCZ", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkWFZ.DataBindings.Add("Checked", this, "WFZ", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkDEZ.DataBindings.Add("Checked", this, "DEZ", false, DataSourceUpdateMode.OnPropertyChanged);

            // Default Values
            this.RunStart = true;
            this.RunStartNGP = true;
            this.Reset = true;
            EH1 = EH2 = CP1 = CP2 = AR1 = AR2 = CN1 = CN2 = HT1 = HT2 = MC1 = MC2 = OO1 = OO2 = MZ1 = MZ2 = MZ3 = SCZ = WFZ = DEZ = true;
        }

        public XmlNode GetSettings(XmlDocument doc)
        {
            XmlElement settingsNode = doc.CreateElement("settings");
            settingsNode.AppendChild(ToElement(doc, "RunStart", this.RunStart));
            settingsNode.AppendChild(ToElement(doc, "RunStartNGP", this.RunStartNGP));
            settingsNode.AppendChild(ToElement(doc, "Reset", this.Reset));
            settingsNode.AppendChild(ToElement(doc, "EH1", this.EH1));
            settingsNode.AppendChild(ToElement(doc, "EH2", this.EH2));
            settingsNode.AppendChild(ToElement(doc, "CP1", this.CP1));
            settingsNode.AppendChild(ToElement(doc, "CP2", this.CP2));
            settingsNode.AppendChild(ToElement(doc, "AR1", this.AR1));
            settingsNode.AppendChild(ToElement(doc, "AR2", this.AR2));
            settingsNode.AppendChild(ToElement(doc, "CN1", this.CN1));
            settingsNode.AppendChild(ToElement(doc, "CN2", this.CN2));
            settingsNode.AppendChild(ToElement(doc, "HT1", this.HT1));
            settingsNode.AppendChild(ToElement(doc, "HT2", this.HT2));
            settingsNode.AppendChild(ToElement(doc, "MC1", this.MC1));
            settingsNode.AppendChild(ToElement(doc, "MC2", this.MC2));
            settingsNode.AppendChild(ToElement(doc, "OO1", this.OO1));
            settingsNode.AppendChild(ToElement(doc, "OO2", this.OO2));
            settingsNode.AppendChild(ToElement(doc, "MZ1", this.MZ1));
            settingsNode.AppendChild(ToElement(doc, "MZ2", this.MZ2));
            settingsNode.AppendChild(ToElement(doc, "MZ3", this.MZ3));
            settingsNode.AppendChild(ToElement(doc, "SCZ", this.SCZ));
            settingsNode.AppendChild(ToElement(doc, "WFZ", this.WFZ));
            settingsNode.AppendChild(ToElement(doc, "DEZ", this.DEZ));
            return settingsNode;
        }

        public void SetSettings(XmlNode settings)
        {
            this.RunStart = ParseBool(settings, "RunStart", true);
            this.RunStartNGP = ParseBool(settings, "RunStartNGP", true);
            this.Reset = ParseBool(settings, "Reset", true);
            this.EH1 = ParseBool(settings, "EH1", true);
            this.EH2 = ParseBool(settings, "EH2", true);
            this.CP1 = ParseBool(settings, "CP1", true);
            this.CP2 = ParseBool(settings, "CP2", true);
            this.AR1 = ParseBool(settings, "AR1", true);
            this.AR2 = ParseBool(settings, "AR2", true);
            this.CN1 = ParseBool(settings, "CN1", true);
            this.CN2 = ParseBool(settings, "CN2", true);
            this.HT1 = ParseBool(settings, "HT1", true);
            this.HT2 = ParseBool(settings, "HT2", true);
            this.MC1 = ParseBool(settings, "MC1", true);
            this.MC2 = ParseBool(settings, "MC2", true);
            this.OO1 = ParseBool(settings, "OO1", true);
            this.OO2 = ParseBool(settings, "OO2", true);
            this.MZ1 = ParseBool(settings, "MZ1", true);
            this.MZ2 = ParseBool(settings, "MZ2", true);
            this.MZ3 = ParseBool(settings, "MZ3", true);
            this.SCZ = ParseBool(settings, "SCZ", true);
            this.WFZ = ParseBool(settings, "WFZ", true);
            this.DEZ = ParseBool(settings, "DEZ", true);
        }

        static bool ParseBool(XmlNode settings, string setting, bool default_ = false)
        {
            bool val;
            return settings[setting] != null ? (Boolean.TryParse(settings[setting].InnerText, out val) ? val : default_) : default_;
        }

        static XmlElement ToElement<T>(XmlDocument document, string name, T value)
        {
            XmlElement str = document.CreateElement(name);
            str.InnerText = value.ToString();
            return str;
        }
    }
}
