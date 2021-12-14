namespace LiveSplit.Sonic2Absolute
{
    partial class Settings
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkReset = new System.Windows.Forms.CheckBox();
            this.chkRunStart = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkDEZ = new System.Windows.Forms.CheckBox();
            this.chkWFZ = new System.Windows.Forms.CheckBox();
            this.chkSCZ = new System.Windows.Forms.CheckBox();
            this.chkMZ3 = new System.Windows.Forms.CheckBox();
            this.chkMZ2 = new System.Windows.Forms.CheckBox();
            this.chkMZ1 = new System.Windows.Forms.CheckBox();
            this.chkOO2 = new System.Windows.Forms.CheckBox();
            this.chkOO1 = new System.Windows.Forms.CheckBox();
            this.chkMC2 = new System.Windows.Forms.CheckBox();
            this.chkMC1 = new System.Windows.Forms.CheckBox();
            this.chkHT2 = new System.Windows.Forms.CheckBox();
            this.chkHT1 = new System.Windows.Forms.CheckBox();
            this.chkCN2 = new System.Windows.Forms.CheckBox();
            this.chkCN1 = new System.Windows.Forms.CheckBox();
            this.chkAR2 = new System.Windows.Forms.CheckBox();
            this.chkAR1 = new System.Windows.Forms.CheckBox();
            this.chkCP2 = new System.Windows.Forms.CheckBox();
            this.chkCP1 = new System.Windows.Forms.CheckBox();
            this.chkEH2 = new System.Windows.Forms.CheckBox();
            this.chkEH1 = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.chkNGPstart = new System.Windows.Forms.CheckBox();
            this.btnSetSplits = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkNGPstart);
            this.groupBox1.Controls.Add(this.chkReset);
            this.groupBox1.Controls.Add(this.chkRunStart);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(10, 10);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(10);
            this.groupBox1.Size = new System.Drawing.Size(455, 50);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "General Options";
            // 
            // chkReset
            // 
            this.chkReset.AutoSize = true;
            this.chkReset.Checked = true;
            this.chkReset.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkReset.Location = new System.Drawing.Point(297, 23);
            this.chkReset.Name = "chkReset";
            this.chkReset.Size = new System.Drawing.Size(79, 17);
            this.chkReset.TabIndex = 2;
            this.chkReset.Text = "Auto Reset";
            this.chkReset.UseVisualStyleBackColor = true;
            // 
            // chkRunStart
            // 
            this.chkRunStart.AutoSize = true;
            this.chkRunStart.Checked = true;
            this.chkRunStart.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRunStart.Dock = System.Windows.Forms.DockStyle.Left;
            this.chkRunStart.Location = new System.Drawing.Point(10, 23);
            this.chkRunStart.Name = "chkRunStart";
            this.chkRunStart.Size = new System.Drawing.Size(134, 17);
            this.chkRunStart.TabIndex = 0;
            this.chkRunStart.Text = "Auto Start (clean save)";
            this.toolTip1.SetToolTip(this.chkRunStart, "Will automatically LiveSplit\'s timer upon\r\nselection of an empy savefile or when " +
        "starting\r\nwith the \"no save\" option.");
            this.chkRunStart.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkDEZ);
            this.groupBox2.Controls.Add(this.chkWFZ);
            this.groupBox2.Controls.Add(this.chkSCZ);
            this.groupBox2.Controls.Add(this.chkMZ3);
            this.groupBox2.Controls.Add(this.chkMZ2);
            this.groupBox2.Controls.Add(this.chkMZ1);
            this.groupBox2.Controls.Add(this.chkOO2);
            this.groupBox2.Controls.Add(this.chkOO1);
            this.groupBox2.Controls.Add(this.chkMC2);
            this.groupBox2.Controls.Add(this.chkMC1);
            this.groupBox2.Controls.Add(this.chkHT2);
            this.groupBox2.Controls.Add(this.chkHT1);
            this.groupBox2.Controls.Add(this.chkCN2);
            this.groupBox2.Controls.Add(this.chkCN1);
            this.groupBox2.Controls.Add(this.chkAR2);
            this.groupBox2.Controls.Add(this.chkAR1);
            this.groupBox2.Controls.Add(this.chkCP2);
            this.groupBox2.Controls.Add(this.chkCP1);
            this.groupBox2.Controls.Add(this.chkEH2);
            this.groupBox2.Controls.Add(this.chkEH1);
            this.groupBox2.Location = new System.Drawing.Point(10, 70);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(10);
            this.groupBox2.Size = new System.Drawing.Size(455, 373);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Autosplitting";
            // 
            // chkDEZ
            // 
            this.chkDEZ.AutoSize = true;
            this.chkDEZ.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkDEZ.Location = new System.Drawing.Point(10, 346);
            this.chkDEZ.Name = "chkDEZ";
            this.chkDEZ.Size = new System.Drawing.Size(435, 17);
            this.chkDEZ.TabIndex = 19;
            this.chkDEZ.Text = "Death Egg Zone";
            this.chkDEZ.UseVisualStyleBackColor = true;
            // 
            // chkWFZ
            // 
            this.chkWFZ.AutoSize = true;
            this.chkWFZ.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkWFZ.Location = new System.Drawing.Point(10, 329);
            this.chkWFZ.Name = "chkWFZ";
            this.chkWFZ.Size = new System.Drawing.Size(435, 17);
            this.chkWFZ.TabIndex = 18;
            this.chkWFZ.Text = "Wing Fortress Zone";
            this.chkWFZ.UseVisualStyleBackColor = true;
            // 
            // chkSCZ
            // 
            this.chkSCZ.AutoSize = true;
            this.chkSCZ.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkSCZ.Location = new System.Drawing.Point(10, 312);
            this.chkSCZ.Name = "chkSCZ";
            this.chkSCZ.Size = new System.Drawing.Size(435, 17);
            this.chkSCZ.TabIndex = 17;
            this.chkSCZ.Text = "Sky Chase Zone";
            this.chkSCZ.UseVisualStyleBackColor = true;
            // 
            // chkMZ3
            // 
            this.chkMZ3.AutoSize = true;
            this.chkMZ3.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkMZ3.Location = new System.Drawing.Point(10, 295);
            this.chkMZ3.Name = "chkMZ3";
            this.chkMZ3.Size = new System.Drawing.Size(435, 17);
            this.chkMZ3.TabIndex = 16;
            this.chkMZ3.Text = "Metropolis Zone - Act 3";
            this.chkMZ3.UseVisualStyleBackColor = true;
            // 
            // chkMZ2
            // 
            this.chkMZ2.AutoSize = true;
            this.chkMZ2.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkMZ2.Location = new System.Drawing.Point(10, 278);
            this.chkMZ2.Name = "chkMZ2";
            this.chkMZ2.Size = new System.Drawing.Size(435, 17);
            this.chkMZ2.TabIndex = 15;
            this.chkMZ2.Text = "Metropolis Zone - Act 2";
            this.chkMZ2.UseVisualStyleBackColor = true;
            // 
            // chkMZ1
            // 
            this.chkMZ1.AutoSize = true;
            this.chkMZ1.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkMZ1.Location = new System.Drawing.Point(10, 261);
            this.chkMZ1.Name = "chkMZ1";
            this.chkMZ1.Size = new System.Drawing.Size(435, 17);
            this.chkMZ1.TabIndex = 14;
            this.chkMZ1.Text = "Metropolis Zone - Act 1";
            this.chkMZ1.UseVisualStyleBackColor = true;
            // 
            // chkOO2
            // 
            this.chkOO2.AutoSize = true;
            this.chkOO2.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkOO2.Location = new System.Drawing.Point(10, 244);
            this.chkOO2.Name = "chkOO2";
            this.chkOO2.Size = new System.Drawing.Size(435, 17);
            this.chkOO2.TabIndex = 13;
            this.chkOO2.Text = "Oil Ocean Zone - Act 2";
            this.chkOO2.UseVisualStyleBackColor = true;
            // 
            // chkOO1
            // 
            this.chkOO1.AutoSize = true;
            this.chkOO1.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkOO1.Location = new System.Drawing.Point(10, 227);
            this.chkOO1.Name = "chkOO1";
            this.chkOO1.Size = new System.Drawing.Size(435, 17);
            this.chkOO1.TabIndex = 12;
            this.chkOO1.Text = "Oil Ocean Zone - Act 1";
            this.chkOO1.UseVisualStyleBackColor = true;
            // 
            // chkMC2
            // 
            this.chkMC2.AutoSize = true;
            this.chkMC2.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkMC2.Location = new System.Drawing.Point(10, 210);
            this.chkMC2.Name = "chkMC2";
            this.chkMC2.Size = new System.Drawing.Size(435, 17);
            this.chkMC2.TabIndex = 11;
            this.chkMC2.Text = "Mystic Cave Zone - Act 2";
            this.chkMC2.UseVisualStyleBackColor = true;
            // 
            // chkMC1
            // 
            this.chkMC1.AutoSize = true;
            this.chkMC1.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkMC1.Location = new System.Drawing.Point(10, 193);
            this.chkMC1.Name = "chkMC1";
            this.chkMC1.Size = new System.Drawing.Size(435, 17);
            this.chkMC1.TabIndex = 10;
            this.chkMC1.Text = "Mystic Cave Zone - Act 1";
            this.chkMC1.UseVisualStyleBackColor = true;
            // 
            // chkHT2
            // 
            this.chkHT2.AutoSize = true;
            this.chkHT2.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkHT2.Location = new System.Drawing.Point(10, 176);
            this.chkHT2.Name = "chkHT2";
            this.chkHT2.Size = new System.Drawing.Size(435, 17);
            this.chkHT2.TabIndex = 9;
            this.chkHT2.Text = "Hill Top Zone - Act 2";
            this.chkHT2.UseVisualStyleBackColor = true;
            // 
            // chkHT1
            // 
            this.chkHT1.AutoSize = true;
            this.chkHT1.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkHT1.Location = new System.Drawing.Point(10, 159);
            this.chkHT1.Name = "chkHT1";
            this.chkHT1.Size = new System.Drawing.Size(435, 17);
            this.chkHT1.TabIndex = 8;
            this.chkHT1.Text = "Hill Top Zone - Act 1";
            this.chkHT1.UseVisualStyleBackColor = true;
            // 
            // chkCN2
            // 
            this.chkCN2.AutoSize = true;
            this.chkCN2.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkCN2.Location = new System.Drawing.Point(10, 142);
            this.chkCN2.Name = "chkCN2";
            this.chkCN2.Size = new System.Drawing.Size(435, 17);
            this.chkCN2.TabIndex = 7;
            this.chkCN2.Text = "Casino Night Zone - Act 2";
            this.chkCN2.UseVisualStyleBackColor = true;
            // 
            // chkCN1
            // 
            this.chkCN1.AutoSize = true;
            this.chkCN1.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkCN1.Location = new System.Drawing.Point(10, 125);
            this.chkCN1.Name = "chkCN1";
            this.chkCN1.Size = new System.Drawing.Size(435, 17);
            this.chkCN1.TabIndex = 6;
            this.chkCN1.Text = "Casino Night Zone - Act 1";
            this.chkCN1.UseVisualStyleBackColor = true;
            // 
            // chkAR2
            // 
            this.chkAR2.AutoSize = true;
            this.chkAR2.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkAR2.Location = new System.Drawing.Point(10, 108);
            this.chkAR2.Name = "chkAR2";
            this.chkAR2.Size = new System.Drawing.Size(435, 17);
            this.chkAR2.TabIndex = 5;
            this.chkAR2.Text = "Aquatic Ruin Zone - Act 2";
            this.chkAR2.UseVisualStyleBackColor = true;
            // 
            // chkAR1
            // 
            this.chkAR1.AutoSize = true;
            this.chkAR1.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkAR1.Location = new System.Drawing.Point(10, 91);
            this.chkAR1.Name = "chkAR1";
            this.chkAR1.Size = new System.Drawing.Size(435, 17);
            this.chkAR1.TabIndex = 4;
            this.chkAR1.Text = "Aquatic Ruin Zone - Act 1";
            this.chkAR1.UseVisualStyleBackColor = true;
            // 
            // chkCP2
            // 
            this.chkCP2.AutoSize = true;
            this.chkCP2.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkCP2.Location = new System.Drawing.Point(10, 74);
            this.chkCP2.Name = "chkCP2";
            this.chkCP2.Size = new System.Drawing.Size(435, 17);
            this.chkCP2.TabIndex = 3;
            this.chkCP2.Text = "Chemical Plant Zone - Act 2";
            this.chkCP2.UseVisualStyleBackColor = true;
            // 
            // chkCP1
            // 
            this.chkCP1.AutoSize = true;
            this.chkCP1.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkCP1.Location = new System.Drawing.Point(10, 57);
            this.chkCP1.Name = "chkCP1";
            this.chkCP1.Size = new System.Drawing.Size(435, 17);
            this.chkCP1.TabIndex = 2;
            this.chkCP1.Text = "Chemical Plant Zone - Act 1";
            this.chkCP1.UseVisualStyleBackColor = true;
            // 
            // chkEH2
            // 
            this.chkEH2.AutoSize = true;
            this.chkEH2.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkEH2.Location = new System.Drawing.Point(10, 40);
            this.chkEH2.Name = "chkEH2";
            this.chkEH2.Size = new System.Drawing.Size(435, 17);
            this.chkEH2.TabIndex = 1;
            this.chkEH2.Text = "Emerald Hill Zone - Act 2";
            this.chkEH2.UseVisualStyleBackColor = true;
            // 
            // chkEH1
            // 
            this.chkEH1.AutoSize = true;
            this.chkEH1.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkEH1.Location = new System.Drawing.Point(10, 23);
            this.chkEH1.Name = "chkEH1";
            this.chkEH1.Size = new System.Drawing.Size(435, 17);
            this.chkEH1.TabIndex = 0;
            this.chkEH1.Text = "Emerald Hill Zone - Act 1";
            this.chkEH1.UseVisualStyleBackColor = true;
            // 
            // chkNGPstart
            // 
            this.chkNGPstart.AutoSize = true;
            this.chkNGPstart.Checked = true;
            this.chkNGPstart.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNGPstart.Location = new System.Drawing.Point(150, 23);
            this.chkNGPstart.Name = "chkNGPstart";
            this.chkNGPstart.Size = new System.Drawing.Size(141, 17);
            this.chkNGPstart.TabIndex = 1;
            this.chkNGPstart.Text = "Auto Start (New Game+)";
            this.toolTip1.SetToolTip(this.chkNGPstart, "Will automatically LiveSplit\'s timer upon\r\nselection of Emerald Hill Zone on a\r\nc" +
        "ompleted savefile.");
            this.chkNGPstart.UseVisualStyleBackColor = true;
            // 
            // btnSetSplits
            // 
            this.btnSetSplits.Location = new System.Drawing.Point(390, 456);
            this.btnSetSplits.Margin = new System.Windows.Forms.Padding(0);
            this.btnSetSplits.Name = "btnSetSplits";
            this.btnSetSplits.Size = new System.Drawing.Size(75, 23);
            this.btnSetSplits.TabIndex = 20;
            this.btnSetSplits.Text = "Set up splits";
            this.btnSetSplits.UseVisualStyleBackColor = true;
            this.btnSetSplits.Click += new System.EventHandler(this.btnSetSplits_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.btnSetSplits);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "Settings";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(475, 500);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkRunStart;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkCP1;
        private System.Windows.Forms.CheckBox chkEH2;
        private System.Windows.Forms.CheckBox chkEH1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox chkDEZ;
        private System.Windows.Forms.CheckBox chkWFZ;
        private System.Windows.Forms.CheckBox chkSCZ;
        private System.Windows.Forms.CheckBox chkMZ3;
        private System.Windows.Forms.CheckBox chkMZ2;
        private System.Windows.Forms.CheckBox chkMZ1;
        private System.Windows.Forms.CheckBox chkOO2;
        private System.Windows.Forms.CheckBox chkOO1;
        private System.Windows.Forms.CheckBox chkMC2;
        private System.Windows.Forms.CheckBox chkMC1;
        private System.Windows.Forms.CheckBox chkHT2;
        private System.Windows.Forms.CheckBox chkHT1;
        private System.Windows.Forms.CheckBox chkCN2;
        private System.Windows.Forms.CheckBox chkCN1;
        private System.Windows.Forms.CheckBox chkAR2;
        private System.Windows.Forms.CheckBox chkAR1;
        private System.Windows.Forms.CheckBox chkCP2;
        private System.Windows.Forms.CheckBox chkReset;
        private System.Windows.Forms.CheckBox chkNGPstart;
        private System.Windows.Forms.Button btnSetSplits;
    }
}
