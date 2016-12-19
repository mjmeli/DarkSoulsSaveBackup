namespace DS2SaveBackup
{
    partial class DS2SaveBackup
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DS2SaveBackup));
            this.headline = new System.Windows.Forms.Label();
            this.tbSaveLoc = new System.Windows.Forms.TextBox();
            this.saveLocBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lbSaveLoc = new System.Windows.Forms.Label();
            this.lbBackLoc = new System.Windows.Forms.Label();
            this.tbBULoc = new System.Windows.Forms.TextBox();
            this.bSaveLoc = new System.Windows.Forms.Button();
            this.bBULoc = new System.Windows.Forms.Button();
            this.saveList = new System.Windows.Forms.ListBox();
            this.saveFolBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.buFolBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.lbBUNum = new System.Windows.Forms.Label();
            this.bEmptyBU = new System.Windows.Forms.Button();
            this.lbBUFreq = new System.Windows.Forms.Label();
            this.udFreq = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.bActivate = new System.Windows.Forms.Button();
            this.bDeactivate = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lbStatus = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.bOpenBU = new System.Windows.Forms.Button();
            this.bReplace = new System.Windows.Forms.Button();
            this.lbdelFreq = new System.Windows.Forms.Label();
            this.udDel = new System.Windows.Forms.NumericUpDown();
            this.lbDel1 = new System.Windows.Forms.Label();
            this.lbDel2 = new System.Windows.Forms.Label();
            this.cbDel = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.bBUNow = new System.Windows.Forms.Button();
            this.btReset = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.saveLocBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udFreq)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udDel)).BeginInit();
            this.SuspendLayout();
            // 
            // headline
            // 
            this.headline.AutoSize = true;
            this.headline.Location = new System.Drawing.Point(13, 13);
            this.headline.Name = "headline";
            this.headline.Size = new System.Drawing.Size(501, 13);
            this.headline.TabIndex = 0;
            this.headline.Text = "Welcome to the Dark Souls II Save Backup Program. Please configure as desired. Cl" +
    "ick Activate to start!";
            // 
            // tbSaveLoc
            // 
            this.tbSaveLoc.Location = new System.Drawing.Point(116, 41);
            this.tbSaveLoc.Name = "tbSaveLoc";
            this.tbSaveLoc.ReadOnly = true;
            this.tbSaveLoc.Size = new System.Drawing.Size(410, 20);
            this.tbSaveLoc.TabIndex = 1;
            // 
            // lbSaveLoc
            // 
            this.lbSaveLoc.AutoSize = true;
            this.lbSaveLoc.Location = new System.Drawing.Point(13, 44);
            this.lbSaveLoc.Name = "lbSaveLoc";
            this.lbSaveLoc.Size = new System.Drawing.Size(80, 13);
            this.lbSaveLoc.TabIndex = 2;
            this.lbSaveLoc.Text = "Save Directory:";
            // 
            // lbBackLoc
            // 
            this.lbBackLoc.AutoSize = true;
            this.lbBackLoc.Location = new System.Drawing.Point(12, 141);
            this.lbBackLoc.Name = "lbBackLoc";
            this.lbBackLoc.Size = new System.Drawing.Size(92, 13);
            this.lbBackLoc.TabIndex = 3;
            this.lbBackLoc.Text = "Backup Directory:";
            // 
            // tbBULoc
            // 
            this.tbBULoc.Location = new System.Drawing.Point(115, 138);
            this.tbBULoc.Name = "tbBULoc";
            this.tbBULoc.ReadOnly = true;
            this.tbBULoc.Size = new System.Drawing.Size(410, 20);
            this.tbBULoc.TabIndex = 4;
            // 
            // bSaveLoc
            // 
            this.bSaveLoc.Location = new System.Drawing.Point(532, 39);
            this.bSaveLoc.Name = "bSaveLoc";
            this.bSaveLoc.Size = new System.Drawing.Size(75, 23);
            this.bSaveLoc.TabIndex = 5;
            this.bSaveLoc.Text = "Change";
            this.toolTip1.SetToolTip(this.bSaveLoc, "Change the location of your save directory.");
            this.bSaveLoc.UseVisualStyleBackColor = true;
            this.bSaveLoc.Click += new System.EventHandler(this.bSaveLoc_Click);
            // 
            // bBULoc
            // 
            this.bBULoc.Location = new System.Drawing.Point(531, 136);
            this.bBULoc.Name = "bBULoc";
            this.bBULoc.Size = new System.Drawing.Size(75, 23);
            this.bBULoc.TabIndex = 6;
            this.bBULoc.Text = "Change";
            this.toolTip1.SetToolTip(this.bBULoc, "Change the location of your backup directory. This must differ from the save dire" +
        "ctory.");
            this.bBULoc.UseVisualStyleBackColor = true;
            this.bBULoc.Click += new System.EventHandler(this.bBULoc_Click);
            // 
            // saveList
            // 
            this.saveList.FormattingEnabled = true;
            this.saveList.Location = new System.Drawing.Point(116, 68);
            this.saveList.Name = "saveList";
            this.saveList.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.saveList.Size = new System.Drawing.Size(410, 56);
            this.saveList.TabIndex = 7;
            this.toolTip1.SetToolTip(this.saveList, "List of save files detected in the save directory. All of these will be backed up" +
        ".");
            // 
            // lbBUNum
            // 
            this.lbBUNum.AutoSize = true;
            this.lbBUNum.Location = new System.Drawing.Point(113, 171);
            this.lbBUNum.Name = "lbBUNum";
            this.lbBUNum.Size = new System.Drawing.Size(195, 13);
            this.lbBUNum.TabIndex = 8;
            this.lbBUNum.Text = "No backup files exist in this location yet.";
            // 
            // bEmptyBU
            // 
            this.bEmptyBU.Location = new System.Drawing.Point(530, 195);
            this.bEmptyBU.Name = "bEmptyBU";
            this.bEmptyBU.Size = new System.Drawing.Size(75, 23);
            this.bEmptyBU.TabIndex = 9;
            this.bEmptyBU.Text = "Empty";
            this.toolTip1.SetToolTip(this.bEmptyBU, "Empty the backup directory. This will delete ALL files.");
            this.bEmptyBU.UseVisualStyleBackColor = true;
            this.bEmptyBU.Click += new System.EventHandler(this.bEmptyBU_Click);
            // 
            // lbBUFreq
            // 
            this.lbBUFreq.AutoSize = true;
            this.lbBUFreq.Location = new System.Drawing.Point(12, 200);
            this.lbBUFreq.Name = "lbBUFreq";
            this.lbBUFreq.Size = new System.Drawing.Size(100, 13);
            this.lbBUFreq.TabIndex = 10;
            this.lbBUFreq.Text = "Backup Frequency:";
            // 
            // udFreq
            // 
            this.udFreq.Location = new System.Drawing.Point(116, 197);
            this.udFreq.Name = "udFreq";
            this.udFreq.Size = new System.Drawing.Size(85, 20);
            this.udFreq.TabIndex = 12;
            this.udFreq.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.udFreq.ValueChanged += new System.EventHandler(this.UpDowns_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(208, 200);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "minutes";
            // 
            // bActivate
            // 
            this.bActivate.Location = new System.Drawing.Point(114, 292);
            this.bActivate.Name = "bActivate";
            this.bActivate.Size = new System.Drawing.Size(178, 64);
            this.bActivate.TabIndex = 14;
            this.bActivate.Text = "Activate";
            this.toolTip1.SetToolTip(this.bActivate, "Start backing up.");
            this.bActivate.UseVisualStyleBackColor = true;
            this.bActivate.Click += new System.EventHandler(this.bActivate_Click);
            // 
            // bDeactivate
            // 
            this.bDeactivate.Enabled = false;
            this.bDeactivate.Location = new System.Drawing.Point(347, 292);
            this.bDeactivate.Name = "bDeactivate";
            this.bDeactivate.Size = new System.Drawing.Size(178, 64);
            this.bDeactivate.TabIndex = 15;
            this.bDeactivate.Text = "Deactivate";
            this.toolTip1.SetToolTip(this.bDeactivate, "Stop backing up.");
            this.bDeactivate.UseVisualStyleBackColor = true;
            this.bDeactivate.Click += new System.EventHandler(this.bDeactivate_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(114, 367);
            this.progressBar1.MarqueeAnimationSpeed = 50;
            this.progressBar1.Maximum = 0;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(410, 23);
            this.progressBar1.Step = 50;
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 16;
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.Location = new System.Drawing.Point(111, 402);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(101, 13);
            this.lbStatus.TabIndex = 17;
            this.lbStatus.Text = "Currently not active.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Enabled = false;
            this.label3.Location = new System.Drawing.Point(560, 470);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "meli v1.1";
            // 
            // bOpenBU
            // 
            this.bOpenBU.Location = new System.Drawing.Point(530, 166);
            this.bOpenBU.Name = "bOpenBU";
            this.bOpenBU.Size = new System.Drawing.Size(75, 23);
            this.bOpenBU.TabIndex = 19;
            this.bOpenBU.Text = "Open";
            this.toolTip1.SetToolTip(this.bOpenBU, "Open the backup directory.");
            this.bOpenBU.UseVisualStyleBackColor = true;
            this.bOpenBU.Click += new System.EventHandler(this.bOpenBU_Click);
            // 
            // bReplace
            // 
            this.bReplace.Location = new System.Drawing.Point(112, 462);
            this.bReplace.Name = "bReplace";
            this.bReplace.Size = new System.Drawing.Size(412, 28);
            this.bReplace.TabIndex = 20;
            this.bReplace.Text = "Restore";
            this.toolTip1.SetToolTip(this.bReplace, "Replaces your save files with the latest backup files for each one.");
            this.bReplace.UseVisualStyleBackColor = true;
            this.bReplace.Click += new System.EventHandler(this.bReplace_Click);
            // 
            // lbdelFreq
            // 
            this.lbdelFreq.AutoSize = true;
            this.lbdelFreq.Location = new System.Drawing.Point(12, 232);
            this.lbdelFreq.Name = "lbdelFreq";
            this.lbdelFreq.Size = new System.Drawing.Size(94, 13);
            this.lbdelFreq.TabIndex = 21;
            this.lbdelFreq.Text = "Delete Frequency:";
            // 
            // udDel
            // 
            this.udDel.Enabled = false;
            this.udDel.Location = new System.Drawing.Point(166, 254);
            this.udDel.Name = "udDel";
            this.udDel.Size = new System.Drawing.Size(85, 20);
            this.udDel.TabIndex = 22;
            this.udDel.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.udDel.ValueChanged += new System.EventHandler(this.UpDowns_ValueChanged);
            // 
            // lbDel1
            // 
            this.lbDel1.AutoSize = true;
            this.lbDel1.Enabled = false;
            this.lbDel1.Location = new System.Drawing.Point(113, 256);
            this.lbDel1.Name = "lbDel1";
            this.lbDel1.Size = new System.Drawing.Size(53, 13);
            this.lbDel1.TabIndex = 23;
            this.lbDel1.Text = "Keep the ";
            // 
            // lbDel2
            // 
            this.lbDel2.AutoSize = true;
            this.lbDel2.Enabled = false;
            this.lbDel2.Location = new System.Drawing.Point(257, 256);
            this.lbDel2.Name = "lbDel2";
            this.lbDel2.Size = new System.Drawing.Size(164, 13);
            this.lbDel2.TabIndex = 24;
            this.lbDel2.Text = "most recent backups of each file.";
            // 
            // cbDel
            // 
            this.cbDel.AutoSize = true;
            this.cbDel.Checked = true;
            this.cbDel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDel.Location = new System.Drawing.Point(116, 232);
            this.cbDel.Name = "cbDel";
            this.cbDel.Size = new System.Drawing.Size(195, 17);
            this.cbDel.TabIndex = 25;
            this.cbDel.Text = "Do not automatically delete any files";
            this.cbDel.UseVisualStyleBackColor = true;
            this.cbDel.CheckedChanged += new System.EventHandler(this.cbDel_Checked);
            // 
            // bBUNow
            // 
            this.bBUNow.Location = new System.Drawing.Point(112, 426);
            this.bBUNow.Name = "bBUNow";
            this.bBUNow.Size = new System.Drawing.Size(412, 28);
            this.bBUNow.TabIndex = 26;
            this.bBUNow.Text = "Backup Now";
            this.toolTip1.SetToolTip(this.bBUNow, "Instantly creates a copy of your save files in the backup folder, independent of " +
        "the automatic process.");
            this.bBUNow.UseVisualStyleBackColor = true;
            this.bBUNow.Click += new System.EventHandler(this.bBUNow_Click);
            // 
            // btReset
            // 
            this.btReset.Location = new System.Drawing.Point(532, 68);
            this.btReset.Name = "btReset";
            this.btReset.Size = new System.Drawing.Size(75, 23);
            this.btReset.TabIndex = 27;
            this.btReset.Text = "Reset";
            this.toolTip1.SetToolTip(this.btReset, "Reset the save directory to default.");
            this.btReset.UseVisualStyleBackColor = true;
            this.btReset.Click += new System.EventHandler(this.btReset_OnClick);
            // 
            // DS2SaveBackup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 499);
            this.Controls.Add(this.btReset);
            this.Controls.Add(this.bBUNow);
            this.Controls.Add(this.cbDel);
            this.Controls.Add(this.lbDel2);
            this.Controls.Add(this.lbDel1);
            this.Controls.Add(this.udDel);
            this.Controls.Add(this.lbdelFreq);
            this.Controls.Add(this.bReplace);
            this.Controls.Add(this.bOpenBU);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbStatus);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.bDeactivate);
            this.Controls.Add(this.bActivate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.udFreq);
            this.Controls.Add(this.lbBUFreq);
            this.Controls.Add(this.bEmptyBU);
            this.Controls.Add(this.lbBUNum);
            this.Controls.Add(this.saveList);
            this.Controls.Add(this.bBULoc);
            this.Controls.Add(this.bSaveLoc);
            this.Controls.Add(this.tbBULoc);
            this.Controls.Add(this.lbBackLoc);
            this.Controls.Add(this.lbSaveLoc);
            this.Controls.Add(this.tbSaveLoc);
            this.Controls.Add(this.headline);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DS2SaveBackup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dark Souls II Save Backup";
            this.Shown += new System.EventHandler(this.DS2SB_OnShow);
            ((System.ComponentModel.ISupportInitialize)(this.saveLocBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udFreq)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udDel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label headline;
        private System.Windows.Forms.TextBox tbSaveLoc;
        private System.Windows.Forms.BindingSource saveLocBindingSource;
        private System.Windows.Forms.Label lbSaveLoc;
        private System.Windows.Forms.Label lbBackLoc;
        private System.Windows.Forms.TextBox tbBULoc;
        private System.Windows.Forms.Button bSaveLoc;
        private System.Windows.Forms.Button bBULoc;
        private System.Windows.Forms.ListBox saveList;
        private System.Windows.Forms.FolderBrowserDialog saveFolBrowser;
        private System.Windows.Forms.FolderBrowserDialog buFolBrowser;
        private System.Windows.Forms.Label lbBUNum;
        private System.Windows.Forms.Button bEmptyBU;
        private System.Windows.Forms.Label lbBUFreq;
        private System.Windows.Forms.NumericUpDown udFreq;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bActivate;
        private System.Windows.Forms.Button bDeactivate;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button bOpenBU;
        private System.Windows.Forms.Button bReplace;
        private System.Windows.Forms.Label lbdelFreq;
        private System.Windows.Forms.NumericUpDown udDel;
        private System.Windows.Forms.Label lbDel1;
        private System.Windows.Forms.Label lbDel2;
        private System.Windows.Forms.CheckBox cbDel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button bBUNow;
        private System.Windows.Forms.Button btReset;

    }
}

