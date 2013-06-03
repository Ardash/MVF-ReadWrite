using System;
using System.IO.Ports;
using System.Windows.Forms;
using Kovalenko.Classes;

namespace KCardRW {
  public partial class wdwOptions : Form {
    KCardRWSettings settings_;

    public wdwOptions(KCardRWSettings settings) {
      InitializeComponent();
      settings_ = settings;
    }

    private void wdwOptions_Load(object sender, EventArgs e) {
      lstPorts.Items.Clear();
      string [] ports = SerialPort.GetPortNames(); Array.Sort(ports);
      foreach (string s in ports) lstPorts.Items.Add(s);
      if (lstPorts.Items.Count > 0) lstPorts.SelectedIndex = 0;

      if ((int)settings_.speed > lstSpeed.Items.Count)
        lstSpeed.SelectedIndex =  4;
      else lstSpeed.SelectedIndex = (int)settings_.speed;

      int i = lstPorts.Items.IndexOf(settings_.port);
      lstPorts.SelectedIndex = (i >= 0) ? i : 0;
    }

    private void btnOk_Click(object sender, EventArgs e) {
      settings_.port = lstPorts.SelectedItem.ToString();
      settings_.speed = (KMVF.kmvf_speed)lstSpeed.SelectedIndex;
      settings_.save();
      
      Close();
    }

    private void btnCancel_Click(object sender, EventArgs e) { Close(); }
  }
}
