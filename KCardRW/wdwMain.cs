using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Kovalenko.Classes;

namespace KCardRW {
  public partial class wdwMain : Form {
    KCardRWSettings _settings;
    public KCardRWSettings settings {
      get { 
        if (_settings == null) _settings = new KCardRWSettings();
        return _settings;
      } 
    }

    KMVF rdr;
    List<string> messages = new List<string>();

    const string msgNotInitOptions = "Initialization Failure: Check options";
    const string msgChkTrackSwitch = "Write Track";
    const string msgInit = "Initialization...";
    const string msgWaitCard = "Waiting card...";
    const string msgWaitCardN = @"Waiting {0} card...";
    const string msgReadTrack1 = "Read Track 1";
    const string msgReadTrack2 = "Read Track 2";
    const string msgReadTrack3 = "Read Track 3";
    const string msgWriteTrack1 = "Write Track 1";
    const string msgWriteTrack2 = "Write Track 2";
    const string msgWriteTrack3 = "Write Track 3";
    const string msgEjectCard = "Eject card...";
    const string msgResSeparator = " - ";

    public wdwMain() {
      InitializeComponent();

      chk_Tooltip.SetToolTip(chkTrack1, msgChkTrackSwitch);
      chk_Tooltip.SetToolTip(chkTrack2, msgChkTrackSwitch);
      chk_Tooltip.SetToolTip(chkTrack3, msgChkTrackSwitch);

      controlsEnabled(init(true));
    }

    delegate void controlsEnabledCB(bool enable);
    private void controlsEnabled(bool enable) {
      if (chkTrack1.InvokeRequired)
        Invoke(new controlsEnabledCB(controlsEnabled), new object[] { enable });
      else {
        chkTrack1.Enabled = enable;
        chkTrack2.Enabled = enable;
        chkTrack3.Enabled = enable;
        txtTrack1.Enabled = enable;
        txtTrack2.Enabled = enable;
        txtTrack3.Enabled = enable;
        btnRead.Enabled = enable;
        btnWrite.Enabled = enable;
        txtCardsAmount.Enabled = enable;
        chkFrontEject.Enabled = enable;
        chkRearEject.Enabled = enable;
      }
    }

    private void frontEjectToolStripMenuItem_Click(object sender, EventArgs e) {
      controlsEnabled(init(true));
    }

    private void rearEjectToolStripMenuItem_Click(object sender, EventArgs e) {
      controlsEnabled(init(false));
    }

    Boolean init(bool ejFront) {
      if (settings == null) {
        MessageBox.Show(msgNotInitOptions, Application.ProductName);
        wdwOptions frmOptions = new wdwOptions(settings);
        frmOptions.ShowDialog();
        return false;
      }

      txtTrack1.Text = settings.track1;
      chkTrack1.Checked = settings.chkTrack1;
      txtTrack2.Text = settings.track2;
      chkTrack2.Checked = settings.chkTrack2;
      txtTrack3.Text = settings.track3;
      chkTrack3.Checked = settings.chkTrack3;
      txtCardsAmount.Value = (settings.wrAmount <= 0) ? 1 : settings.wrAmount;
      chkFrontEject.Checked = settings.ejFront;
      chkRearEject.Checked = !settings.ejFront;

      if (rdr != null) rdr.close();
      rdr = new KMVF(settings.port, settings.speed);
      string result = "";
      consoleAddLine(msgInit);
      bool ret = rdr.init(ejFront, ref result);
      consoleAppendLine(msgResSeparator + result);
      return ret;
    }

    delegate void consAddLineCB(string txt);
    private void consoleAddLine(string txt) {
      if (console.InvokeRequired)
        Invoke(new consAddLineCB(consoleAddLine), new object[] { txt });
      else {
        messages.Add(txt);
        console.Lines = messages.ToArray();
        console.SelectionStart = console.Text.Length;
        console.ScrollToCaret();
      }
    }

    delegate void consAppendLineCB(string txt);
    private void consoleAppendLine(string txt) {
      if (console.InvokeRequired)
        Invoke(new consAppendLineCB(consoleAppendLine), new object[] { txt });
      else {
        messages[messages.Count - 1] += txt;
        console.Lines = messages.ToArray();
        console.SelectionStart = console.Text.Length;
        console.ScrollToCaret();
      }
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
      Close();
    }

    private void chkTrack1_CheckedChanged(object sender, EventArgs e) {
      settings.chkTrack1 = chkTrack1.Checked;
    }

    private void chkTrack2_CheckedChanged(object sender, EventArgs e) {
      settings.chkTrack2 = chkTrack2.Checked;
    }

    private void chkTrack3_CheckedChanged(object sender, EventArgs e) {
      settings.chkTrack3 = chkTrack3.Checked;
    }

    private void txtTrack1_TextChanged(object sender, EventArgs e) {
      settings.track1 = txtTrack1.Text;
    }

    private void txtTrack2_TextChanged(object sender, EventArgs e) {
      settings.track2 = txtTrack2.Text;
    }

    private void txtTrack3_TextChanged(object sender, EventArgs e) {
      settings.track3 = txtTrack3.Text;
    }

    private void txtCardsAmount_ValueChanged(object sender, EventArgs e) {
      settings.wrAmount = (int)txtCardsAmount.Value;
    }

    private void chkFrontEject_CheckedChanged(object sender, EventArgs e) {
      settings.ejFront = chkFrontEject.Checked;
    }

    private void chkRearEject_CheckedChanged(object sender, EventArgs e) {
      settings.ejFront = !chkRearEject.Checked;
    }

    protected override void OnFormClosed(FormClosedEventArgs e) {
      settings.save();
    }

    private void optToolStripMenuItem_Click(object sender, EventArgs e) {
      wdwOptions frmOptions = new wdwOptions(settings);
      frmOptions.ShowDialog();
    }


    delegate void Track1_SetTextCB(string txt);
    private void Track1_SetText(string txt) {
      if (txtTrack1.InvokeRequired)
        Invoke(new Track1_SetTextCB(Track1_SetText), new object[] { txt });
      else txtTrack1.Text = txt;
    }

    delegate void Track2_SetTextCB(string txt);
    private void Track2_SetText(string txt) {
      if (txtTrack2.InvokeRequired)
        Invoke(new Track2_SetTextCB(Track2_SetText), new object[] { txt });
      else txtTrack2.Text = txt;
    }

    delegate void Track3_SetTextCB(string txt);
    private void Track3_SetText(string txt) {
      if (txtTrack3.InvokeRequired)
        Invoke(new Track3_SetTextCB(Track3_SetText), new object[] { txt });
      else txtTrack3.Text = txt;
    }

    private void readTracks() {
      controlsEnabled(false);

      string result = "";
      consoleAddLine(msgWaitCard);
      rdr.insert(ref result);
      while (!rdr.checkCard(ref result)) ;
      consoleAppendLine(msgResSeparator + result);

      byte[] track1 = null;
      consoleAddLine(msgReadTrack1);
      rdr.readTrackX(1, ref result, ref track1);
      consoleAppendLine(msgResSeparator + result);
      if (track1 != null) Track1_SetText(Encoding.ASCII.GetString(track1));
      else txtTrack1.Text = "";

      byte[] track2 = null;
      consoleAddLine(msgReadTrack2);
      rdr.readTrackX(2, ref result, ref track2);
      consoleAppendLine(msgResSeparator + result);
      if (track2 != null) Track2_SetText(Encoding.ASCII.GetString(track2));
      else txtTrack2.Text = "";

      byte[] track3 = null;
      consoleAddLine(msgReadTrack3);
      rdr.readTrackX(3, ref result, ref track3);
      consoleAppendLine(msgResSeparator + result);
      if (track3 != null) Track3_SetText(Encoding.ASCII.GetString(track3));
      else txtTrack3.Text = "";

      consoleAddLine(msgEjectCard);
      while (!rdr.ejectCard(ref result)) ;
      consoleAppendLine(msgResSeparator + result);

      controlsEnabled(true);
    }

    private void btnRead_Click(object sender, EventArgs e) {
      new Thread(readTracks).Start();
    }

    private void writeTracks() {
      controlsEnabled(false);
      string res = "";

      for (int i = 0; i < txtCardsAmount.Value; i++) {
        consoleAddLine(String.Format(msgWaitCardN, i + 1));
        rdr.insert(ref res);
        while (!rdr.checkCard(ref res)) ;
        consoleAppendLine(msgResSeparator + res);

        if (chkTrack1.Checked) {
          consoleAddLine(msgWriteTrack1);
          rdr.writeTrackX(1, ref res, Encoding.ASCII.GetBytes(txtTrack1.Text));
          consoleAppendLine(msgResSeparator + res);
        }

        if (chkTrack2.Checked) {
          consoleAddLine(msgWriteTrack2);
          rdr.writeTrackX(2, ref res, Encoding.ASCII.GetBytes(txtTrack2.Text));
          consoleAppendLine(msgResSeparator + res);
        }

        if (chkTrack3.Checked) {
          consoleAddLine(msgWriteTrack3);
          rdr.writeTrackX(3, ref res, Encoding.ASCII.GetBytes(txtTrack3.Text));
          consoleAppendLine(msgResSeparator + res);
        }

        consoleAddLine(msgEjectCard);
        while (chkFrontEject.Checked ? !rdr.ejectCard(ref res) :
                                       !rdr.rejectCard(ref res)) ;
        consoleAppendLine(msgResSeparator + res);
      }

      controlsEnabled(true);
    }
    private void btnWrite_Click(object sender, EventArgs e) {
      new Thread(writeTracks).Start();
    }
  }

  public class KCardRWSettings {
    string ext = ".cfg";
    public KCardRWSettings() { read(); }

    struct setts {
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
      public string port;
      public KMVF.kmvf_speed speed;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 76 + 1)]
      public string track1;
      public bool chkTrack1;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 37 + 1)]
      public string track2;
      public bool chkTrack2;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 104 + 1)]
      public string track3;
      public bool chkTrack3;
      public int wrAmount;
      public bool ejFront;
    }

    setts settings;
    public string port {
      get { return settings.port; }
      set { settings.port = value; }
    }

    public KMVF.kmvf_speed speed {
      get { return settings.speed; }
      set { settings.speed = value; }
    }

    public string track1 {
      get { return settings.track1; }
      set { settings.track1 = value; }
    }

    public bool chkTrack1 {
      get { return settings.chkTrack1; }
      set { settings.chkTrack1 = value; }
    }

    public string track2 {
      get { return settings.track2; }
      set { settings.track2 = value; }
    }

    public bool chkTrack2 {
      get { return settings.chkTrack2; }
      set { settings.chkTrack2 = value; }
    }

    public string track3 {
      get { return settings.track3; }
      set { settings.track3 = value; }
    }

    public bool chkTrack3 {
      get { return settings.chkTrack3; }
      set { settings.chkTrack3 = value; }
    }
    
    public int wrAmount {
      get { return settings.wrAmount; }
      set { settings.wrAmount = value; }
    }
    public bool ejFront {
      get { return settings.ejFront; }
      set { settings.ejFront = value; }
    }

    public int SizeOf() { return Marshal.SizeOf(settings); }

    public byte[] getBytes() {
      int size = Marshal.SizeOf(settings);
      byte[] arr = new byte[size];
      IntPtr ptr = Marshal.AllocHGlobal(size);

      Marshal.StructureToPtr(settings, ptr, true);
      Marshal.Copy(ptr, arr, 0, size);
      Marshal.FreeHGlobal(ptr);

      return arr;
    }

    public void fromBytes(byte[] arr) {
      settings = new setts();
      int size = Marshal.SizeOf(settings);
      IntPtr ptr = Marshal.AllocHGlobal(size);
      Marshal.Copy(arr, 0, ptr, size);
      settings = (setts)Marshal.PtrToStructure(ptr, settings.GetType());
      Marshal.FreeHGlobal(ptr);
    }

    public void save() {
      FileStream fs;
      try {
        fs = new FileStream(Application.StartupPath + "\\" +
                            Application.ProductName + ext,
                            FileMode.Create,
                            FileAccess.Write);
        fs.Write(getBytes(), 0, SizeOf());
        fs.Close();
      } catch {}
    }

    public void read() { 
      try {
        int len = SizeOf();
        byte[] b = new byte[len];
        FileStream fs = new FileStream(Application.StartupPath + "\\" +
                                       Application.ProductName + ext,
                                       FileMode.Open,
                                       FileAccess.Read);
        fs.Read(b, 0, len);
        fs.Close();
        fromBytes(b);
      } catch {
        settings.port = "COM1";
        settings.speed = KMVF.kmvf_speed._19200;
      }
    }
  }
}
