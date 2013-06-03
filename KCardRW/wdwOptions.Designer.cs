namespace KCardRW {
  partial class wdwOptions {
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
      this.lblPort = new System.Windows.Forms.Label();
      this.lstPorts = new System.Windows.Forms.ComboBox();
      this.lblSpeed = new System.Windows.Forms.Label();
      this.lstSpeed = new System.Windows.Forms.ComboBox();
      this.grOptions = new System.Windows.Forms.GroupBox();
      this.btnOk = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.grOptions.SuspendLayout();
      this.SuspendLayout();
      // 
      // lblPort
      // 
      this.lblPort.AutoSize = true;
      this.lblPort.Location = new System.Drawing.Point(6, 23);
      this.lblPort.Name = "lblPort";
      this.lblPort.Size = new System.Drawing.Size(26, 13);
      this.lblPort.TabIndex = 0;
      this.lblPort.Text = "Port";
      // 
      // lstPorts
      // 
      this.lstPorts.FormattingEnabled = true;
      this.lstPorts.Location = new System.Drawing.Point(71, 20);
      this.lstPorts.Name = "lstPorts";
      this.lstPorts.Size = new System.Drawing.Size(121, 21);
      this.lstPorts.TabIndex = 1;
      // 
      // lblSpeed
      // 
      this.lblSpeed.AutoSize = true;
      this.lblSpeed.Location = new System.Drawing.Point(6, 59);
      this.lblSpeed.Name = "lblSpeed";
      this.lblSpeed.Size = new System.Drawing.Size(38, 13);
      this.lblSpeed.TabIndex = 2;
      this.lblSpeed.Text = "Speed";
      // 
      // lstSpeed
      // 
      this.lstSpeed.FormattingEnabled = true;
      this.lstSpeed.Items.AddRange(new object[] {
            "1200",
            "2400",
            "4800",
            "9600",
            "19200"});
      this.lstSpeed.Location = new System.Drawing.Point(71, 56);
      this.lstSpeed.Name = "lstSpeed";
      this.lstSpeed.Size = new System.Drawing.Size(121, 21);
      this.lstSpeed.TabIndex = 3;
      // 
      // grOptions
      // 
      this.grOptions.Controls.Add(this.lstSpeed);
      this.grOptions.Controls.Add(this.lblSpeed);
      this.grOptions.Controls.Add(this.lblPort);
      this.grOptions.Controls.Add(this.lstPorts);
      this.grOptions.Location = new System.Drawing.Point(13, 13);
      this.grOptions.Name = "grOptions";
      this.grOptions.Size = new System.Drawing.Size(211, 94);
      this.grOptions.TabIndex = 4;
      this.grOptions.TabStop = false;
      this.grOptions.Text = "Port options";
      // 
      // btnOk
      // 
      this.btnOk.Location = new System.Drawing.Point(64, 126);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(75, 23);
      this.btnOk.TabIndex = 5;
      this.btnOk.Text = "OK";
      this.btnOk.UseVisualStyleBackColor = true;
      this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(145, 126);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 6;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // wdwOptions
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(237, 159);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOk);
      this.Controls.Add(this.grOptions);
      this.MaximizeBox = false;
      this.Name = "wdwOptions";
      this.Text = "Options";
      this.Load += new System.EventHandler(this.wdwOptions_Load);
      this.grOptions.ResumeLayout(false);
      this.grOptions.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label lblPort;
    private System.Windows.Forms.ComboBox lstPorts;
    private System.Windows.Forms.Label lblSpeed;
    private System.Windows.Forms.ComboBox lstSpeed;
    private System.Windows.Forms.GroupBox grOptions;
    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.Button btnCancel;
  }
}