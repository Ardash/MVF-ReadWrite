namespace KCardRW {
  partial class wdwMain {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      this.components = new System.ComponentModel.Container();
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.actionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.reInitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.frontEjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.rearEjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripSeparator();
      this.optToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
      this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.chkTrack1 = new System.Windows.Forms.CheckBox();
      this.chkTrack2 = new System.Windows.Forms.CheckBox();
      this.chkTrack3 = new System.Windows.Forms.CheckBox();
      this.txtTrack1 = new System.Windows.Forms.TextBox();
      this.txtTrack2 = new System.Windows.Forms.TextBox();
      this.txtTrack3 = new System.Windows.Forms.TextBox();
      this.chk_Tooltip = new System.Windows.Forms.ToolTip(this.components);
      this.btnRead = new System.Windows.Forms.Button();
      this.btnWrite = new System.Windows.Forms.Button();
      this.lblWrOptions = new System.Windows.Forms.Label();
      this.lblAmount = new System.Windows.Forms.Label();
      this.txtCardsAmount = new System.Windows.Forms.NumericUpDown();
      this.chkFrontEject = new System.Windows.Forms.RadioButton();
      this.chkRearEject = new System.Windows.Forms.RadioButton();
      this.lblEject = new System.Windows.Forms.Label();
      this.console = new System.Windows.Forms.TextBox();
      this.menuStrip1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.txtCardsAmount)).BeginInit();
      this.SuspendLayout();
      // 
      // menuStrip1
      // 
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.actionToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(471, 24);
      this.menuStrip1.TabIndex = 0;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // actionToolStripMenuItem
      // 
      this.actionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reInitToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.optToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
      this.actionToolStripMenuItem.Name = "actionToolStripMenuItem";
      this.actionToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
      this.actionToolStripMenuItem.Text = "Action";
      // 
      // reInitToolStripMenuItem
      // 
      this.reInitToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.frontEjectToolStripMenuItem,
            this.rearEjectToolStripMenuItem});
      this.reInitToolStripMenuItem.Name = "reInitToolStripMenuItem";
      this.reInitToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
      this.reInitToolStripMenuItem.Text = "ReInit";
      // 
      // frontEjectToolStripMenuItem
      // 
      this.frontEjectToolStripMenuItem.Name = "frontEjectToolStripMenuItem";
      this.frontEjectToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
      this.frontEjectToolStripMenuItem.Text = "Front Eject";
      this.frontEjectToolStripMenuItem.Click += new System.EventHandler(this.frontEjectToolStripMenuItem_Click);
      // 
      // rearEjectToolStripMenuItem
      // 
      this.rearEjectToolStripMenuItem.Name = "rearEjectToolStripMenuItem";
      this.rearEjectToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
      this.rearEjectToolStripMenuItem.Text = "Rear Eject";
      this.rearEjectToolStripMenuItem.Click += new System.EventHandler(this.rearEjectToolStripMenuItem_Click);
      // 
      // optionsToolStripMenuItem
      // 
      this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
      this.optionsToolStripMenuItem.Size = new System.Drawing.Size(119, 6);
      // 
      // optToolStripMenuItem
      // 
      this.optToolStripMenuItem.Name = "optToolStripMenuItem";
      this.optToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
      this.optToolStripMenuItem.Text = "Options";
      this.optToolStripMenuItem.Click += new System.EventHandler(this.optToolStripMenuItem_Click);
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(119, 6);
      // 
      // exitToolStripMenuItem
      // 
      this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      this.exitToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
      this.exitToolStripMenuItem.Text = "Exit";
      this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
      // 
      // chkTrack1
      // 
      this.chkTrack1.AutoSize = true;
      this.chkTrack1.Enabled = false;
      this.chkTrack1.Location = new System.Drawing.Point(12, 27);
      this.chkTrack1.Name = "chkTrack1";
      this.chkTrack1.Size = new System.Drawing.Size(60, 17);
      this.chkTrack1.TabIndex = 1;
      this.chkTrack1.Text = "Track1";
      this.chkTrack1.UseVisualStyleBackColor = true;
      this.chkTrack1.CheckedChanged += new System.EventHandler(this.chkTrack1_CheckedChanged);
      // 
      // chkTrack2
      // 
      this.chkTrack2.AutoSize = true;
      this.chkTrack2.Enabled = false;
      this.chkTrack2.Location = new System.Drawing.Point(12, 51);
      this.chkTrack2.Name = "chkTrack2";
      this.chkTrack2.Size = new System.Drawing.Size(60, 17);
      this.chkTrack2.TabIndex = 2;
      this.chkTrack2.Text = "Track2";
      this.chkTrack2.UseVisualStyleBackColor = true;
      this.chkTrack2.CheckedChanged += new System.EventHandler(this.chkTrack2_CheckedChanged);
      // 
      // chkTrack3
      // 
      this.chkTrack3.AutoSize = true;
      this.chkTrack3.Enabled = false;
      this.chkTrack3.Location = new System.Drawing.Point(12, 75);
      this.chkTrack3.Name = "chkTrack3";
      this.chkTrack3.Size = new System.Drawing.Size(60, 17);
      this.chkTrack3.TabIndex = 3;
      this.chkTrack3.Text = "Track3";
      this.chkTrack3.UseVisualStyleBackColor = true;
      this.chkTrack3.CheckedChanged += new System.EventHandler(this.chkTrack3_CheckedChanged);
      // 
      // txtTrack1
      // 
      this.txtTrack1.Enabled = false;
      this.txtTrack1.Location = new System.Drawing.Point(98, 27);
      this.txtTrack1.MaxLength = 76;
      this.txtTrack1.Name = "txtTrack1";
      this.txtTrack1.Size = new System.Drawing.Size(360, 20);
      this.txtTrack1.TabIndex = 4;
      this.txtTrack1.TextChanged += new System.EventHandler(this.txtTrack1_TextChanged);
      // 
      // txtTrack2
      // 
      this.txtTrack2.Enabled = false;
      this.txtTrack2.Location = new System.Drawing.Point(98, 49);
      this.txtTrack2.MaxLength = 37;
      this.txtTrack2.Name = "txtTrack2";
      this.txtTrack2.Size = new System.Drawing.Size(360, 20);
      this.txtTrack2.TabIndex = 5;
      this.txtTrack2.TextChanged += new System.EventHandler(this.txtTrack2_TextChanged);
      // 
      // txtTrack3
      // 
      this.txtTrack3.Enabled = false;
      this.txtTrack3.Location = new System.Drawing.Point(98, 72);
      this.txtTrack3.MaxLength = 104;
      this.txtTrack3.Name = "txtTrack3";
      this.txtTrack3.Size = new System.Drawing.Size(360, 20);
      this.txtTrack3.TabIndex = 6;
      this.txtTrack3.TextChanged += new System.EventHandler(this.txtTrack3_TextChanged);
      // 
      // chk_Tooltip
      // 
      this.chk_Tooltip.ShowAlways = true;
      // 
      // btnRead
      // 
      this.btnRead.Enabled = false;
      this.btnRead.Location = new System.Drawing.Point(13, 99);
      this.btnRead.Name = "btnRead";
      this.btnRead.Size = new System.Drawing.Size(75, 23);
      this.btnRead.TabIndex = 7;
      this.btnRead.Text = "Read";
      this.btnRead.UseVisualStyleBackColor = true;
      this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
      // 
      // btnWrite
      // 
      this.btnWrite.Enabled = false;
      this.btnWrite.Location = new System.Drawing.Point(98, 98);
      this.btnWrite.Name = "btnWrite";
      this.btnWrite.Size = new System.Drawing.Size(75, 23);
      this.btnWrite.TabIndex = 8;
      this.btnWrite.Text = "Write";
      this.btnWrite.UseVisualStyleBackColor = true;
      this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
      // 
      // lblWrOptions
      // 
      this.lblWrOptions.AutoSize = true;
      this.lblWrOptions.Location = new System.Drawing.Point(13, 129);
      this.lblWrOptions.Name = "lblWrOptions";
      this.lblWrOptions.Size = new System.Drawing.Size(74, 13);
      this.lblWrOptions.TabIndex = 9;
      this.lblWrOptions.Text = "Write Options:";
      // 
      // lblAmount
      // 
      this.lblAmount.AutoSize = true;
      this.lblAmount.Location = new System.Drawing.Point(14, 149);
      this.lblAmount.Name = "lblAmount";
      this.lblAmount.Size = new System.Drawing.Size(124, 13);
      this.lblAmount.TabIndex = 10;
      this.lblAmount.Text = "Amount of cards to write:";
      // 
      // txtCardsAmount
      // 
      this.txtCardsAmount.Enabled = false;
      this.txtCardsAmount.Location = new System.Drawing.Point(144, 147);
      this.txtCardsAmount.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
      this.txtCardsAmount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.txtCardsAmount.Name = "txtCardsAmount";
      this.txtCardsAmount.Size = new System.Drawing.Size(74, 20);
      this.txtCardsAmount.TabIndex = 11;
      this.txtCardsAmount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.txtCardsAmount.ValueChanged += new System.EventHandler(this.txtCardsAmount_ValueChanged);
      // 
      // chkFrontEject
      // 
      this.chkFrontEject.AutoSize = true;
      this.chkFrontEject.Checked = true;
      this.chkFrontEject.Enabled = false;
      this.chkFrontEject.Location = new System.Drawing.Point(51, 175);
      this.chkFrontEject.Name = "chkFrontEject";
      this.chkFrontEject.Size = new System.Drawing.Size(49, 17);
      this.chkFrontEject.TabIndex = 12;
      this.chkFrontEject.TabStop = true;
      this.chkFrontEject.Text = "Front";
      this.chkFrontEject.UseVisualStyleBackColor = true;
      this.chkFrontEject.CheckedChanged += new System.EventHandler(this.chkFrontEject_CheckedChanged);
      // 
      // chkRearEject
      // 
      this.chkRearEject.AutoSize = true;
      this.chkRearEject.Enabled = false;
      this.chkRearEject.Location = new System.Drawing.Point(51, 198);
      this.chkRearEject.Name = "chkRearEject";
      this.chkRearEject.Size = new System.Drawing.Size(48, 17);
      this.chkRearEject.TabIndex = 13;
      this.chkRearEject.Text = "Rear";
      this.chkRearEject.UseVisualStyleBackColor = true;
      this.chkRearEject.CheckedChanged += new System.EventHandler(this.chkRearEject_CheckedChanged);
      // 
      // lblEject
      // 
      this.lblEject.AutoSize = true;
      this.lblEject.Location = new System.Drawing.Point(14, 177);
      this.lblEject.Name = "lblEject";
      this.lblEject.Size = new System.Drawing.Size(31, 13);
      this.lblEject.TabIndex = 14;
      this.lblEject.Text = "Eject";
      // 
      // console
      // 
      this.console.Location = new System.Drawing.Point(12, 221);
      this.console.Multiline = true;
      this.console.Name = "console";
      this.console.ReadOnly = true;
      this.console.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.console.Size = new System.Drawing.Size(446, 346);
      this.console.TabIndex = 15;
      // 
      // wdwMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(471, 579);
      this.Controls.Add(this.console);
      this.Controls.Add(this.lblEject);
      this.Controls.Add(this.chkRearEject);
      this.Controls.Add(this.chkFrontEject);
      this.Controls.Add(this.txtCardsAmount);
      this.Controls.Add(this.lblAmount);
      this.Controls.Add(this.lblWrOptions);
      this.Controls.Add(this.btnWrite);
      this.Controls.Add(this.btnRead);
      this.Controls.Add(this.txtTrack3);
      this.Controls.Add(this.txtTrack2);
      this.Controls.Add(this.txtTrack1);
      this.Controls.Add(this.chkTrack3);
      this.Controls.Add(this.chkTrack2);
      this.Controls.Add(this.chkTrack1);
      this.Controls.Add(this.menuStrip1);
      this.MainMenuStrip = this.menuStrip1;
      this.MaximizeBox = false;
      this.MaximumSize = new System.Drawing.Size(479, 613);
      this.MinimumSize = new System.Drawing.Size(479, 613);
      this.Name = "wdwMain";
      this.Text = "Card Read/Write Tool";
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.txtCardsAmount)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem actionToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem reInitToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem frontEjectToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem rearEjectToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator optionsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem optToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    private System.Windows.Forms.CheckBox chkTrack1;
    private System.Windows.Forms.CheckBox chkTrack2;
    private System.Windows.Forms.CheckBox chkTrack3;
    private System.Windows.Forms.TextBox txtTrack1;
    private System.Windows.Forms.TextBox txtTrack2;
    private System.Windows.Forms.TextBox txtTrack3;
    private System.Windows.Forms.ToolTip chk_Tooltip;
    private System.Windows.Forms.Button btnRead;
    private System.Windows.Forms.Button btnWrite;
    private System.Windows.Forms.Label lblWrOptions;
    private System.Windows.Forms.Label lblAmount;
    private System.Windows.Forms.NumericUpDown txtCardsAmount;
    private System.Windows.Forms.RadioButton chkFrontEject;
    private System.Windows.Forms.RadioButton chkRearEject;
    private System.Windows.Forms.Label lblEject;
    private System.Windows.Forms.TextBox console;
  }
}

