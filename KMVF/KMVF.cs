using System;
using System.IO.Ports;
using System.Linq;
using System.Threading;

namespace Kovalenko.Classes {
  public class KMVF {
    #region Properties
    public enum kmvf_speed {
      _1200,
      _2400,
      _4800,
      _9600,
      _19200,
    }

    int _baud;
    public kmvf_speed baudRate {
      get {
        switch (_baud) {
          case 1200:  return kmvf_speed._1200;
          case 2400:  return kmvf_speed._2400;
          case 4800:  return kmvf_speed._4800;
          case 9600:  return kmvf_speed._9600;
          case 19200:
          default:    return kmvf_speed._19200;
        }
      }

      set { 
        switch (value) {
          case kmvf_speed._1200: _baud = 1200;  break;
          case kmvf_speed._2400: _baud = 2400;  break;
          case kmvf_speed._4800: _baud = 4800;  break;
          case kmvf_speed._9600: _baud = 9600;  break;
          case kmvf_speed._19200:
          default:               _baud = 19200; break;
        }
      }
    }

    public string portNum { get;  set; }
    
    #endregion

    #region Variables
    SerialPort hPort;
    #endregion
    
    #region Constants
    // Default values
    const string portN = "COM1";
    const kmvf_speed baudR = kmvf_speed._19200;
    const Parity parity = Parity.None;
    const int dataBits = 8;
    const StopBits stopBits = StopBits.One;

    // Errors
    const string err_ACK =    "DLE ACK Error. Recv: {0:x2} {1:x2}";
    const string err_NAK =    "DLE NAK Received";
    const string err_Format = "Received Packet Format Error";
    const string err_Cmd =    "Req Cmd != Resp Cmd";
    const string err_Fatal =  "Program error";
    const string p_res =      "Positive ";
    const string n_res =      "Negative ";
    const string u_res =      "Unknown Result";

    // Response lengths
    const int start_seq_l = 2;
    const int stop_seq_l = 3;
    const int start_stop_seq_l = start_seq_l + stop_seq_l;
    const int cmd_fixed_part_l = 3;
    const int resp_fixed_part_l = 5;
    const int ack_nak_l = 2;
    const int enq_l = 2;

    const int mag_data_tr1 = 76;
    const int mag_data_tr2 = 37;
    const int mag_data_tr3 = 104;

    const int common_res_l = start_stop_seq_l + resp_fixed_part_l + 0;
    const int res_tr1_l = start_stop_seq_l + resp_fixed_part_l + mag_data_tr1;
    const int res_tr2_l = start_stop_seq_l + resp_fixed_part_l + mag_data_tr2;
    const int res_tr3_l = start_stop_seq_l + resp_fixed_part_l + mag_data_tr3;
    #endregion Constants

    #region Constructor
    public KMVF() { serialOpen(portN, baudR); }
    public KMVF (string port, kmvf_speed speed) { serialOpen(port, speed); }
    #endregion Constructor

    #region Public Functions
    public void close() { serialClose(); }
    
    // Initialization Functions
    //
    //  frontEject 
    //    true  - eject card through Take-out position
    //    false - eject card through Rear Eject position
    //
    //  result - result description
    //
    //  RC: 
    //      true  - success
    //      false - 
    public Boolean init(ref string result) {
      return init(true, ref result);
    }

    public Boolean init(bool frontEject, ref string result) {
      return processPacket(
        frontEject ? RCC.InitEject : RCC.InitReject, ref result);
    }

    // 'Insert Card' Command
    //
    //  result - result description
    //
    //  RC: 
    //      true  - success
    //      false - 
    public Boolean insert(ref string result) {
      return processPacket(RCC.WaitCard, ref result);
    }

    // 'Check card in reader' function
    //
    //  result - result description
    //
    //  RC: 
    //      true  - Card in reader
    //      false - 
    public Boolean checkCard(ref string result) {
      byte[] resp = null;
      if (!processPacket(RCC.CheckStat, ref resp, ref result)) return false;
      if (resp == null) { result = err_Fatal; return false; }
      return BytesToUShort(Take(resp, 5, 2)) == p_e02;
    }

    // 'Reject Card' Command - Eject card through Rear Eject Position
    //
    //  result - result description
    //
    //  RC: 
    //      true  - No card in reader
    //      false - 
    public Boolean rejectCard(ref string result) {
      byte[] resp = null;
      if (!processPacket(RCC.RejectCard, ref resp, ref result)) return false;
      if (resp == null) { result = err_Fatal; return false; }
      return BytesToUShort(Take(resp, 5, 2)) == p_e00;
    }

    // 'Eject Card' Command - Eject card through Take-out position
    //
    //  result - result description
    //
    //  RC: 
    //      true  - No card in reader
    //      false - 
    public Boolean ejectCard(ref string result) {
      byte[] resp = null;
      if (!processPacket(RCC.EjectCard, ref resp, ref result)) return false;
      if (resp == null) { result = err_Fatal; return false; }
      return BytesToUShort(Take(resp, 5, 2)) == p_e00;
    }

    // 'Write Track' function
    //
    //  X - track number (1, 2, 3)
    //  result - result description
    //  track - Track to write
    //
    //  RC: 
    //      true  - success
    //      false - 
    public Boolean writeTrackX(int X, ref string result, byte[] track) {
      if (track == null) return false;
      RCC cmd = 0;
      switch (X) {
        case 1: cmd = RCC.writeTrack1; break;
        case 2: cmd = RCC.writeTrack2; break;
        case 3: cmd = RCC.writeTrack3; break;
        default: result = err_Fatal; return false;
      }
      return processPacket(cmd, track, ref result);
    }

    // 'Read Track' function
    //
    //  X - track number (1, 2, 3)
    //  result - result description
    //  track - Read track
    //
    //  RC: 
    //      true  - success
    //      false - 
    public Boolean readTrackX(int X, ref string result, ref byte[] track) {
      RCC cmd = 0;
      switch (X) {
        case 1: cmd = RCC.readTrack1; break;
        case 2: cmd = RCC.readTrack2; break;
        case 3: cmd = RCC.readTrack3; break;
        default: result = err_Fatal; return false;
      }

      byte[] resp = null;
      if (!processPacket(cmd, ref resp, ref result)) return false;
      if (resp == null) { result = err_Fatal; return false; }

      return getTextFromResp(resp, ref track, ref result);
    }
    #endregion Public Functions

    #region Internal Functions
    byte[] Take(byte[] bytes, int start, int count) {
      return bytes.Skip(start).Take(count).ToArray();
    }

    ushort BytesToUShort(byte[] bytes) {
      return (ushort)(bytes[0] * 0x100 + bytes[1]);
    }

    void serialClose() {
      if ((hPort != null) && (hPort.IsOpen)) hPort.Close();
    }

    void serialOpen(string port, kmvf_speed speed) {
      baudRate = speed;
      portNum = port;

      hPort = new SerialPort(portNum, _baud, parity, dataBits, stopBits);
      hPort.DtrEnable = true;
      hPort.Open();
    }

    enum RCC { // Reader Control Commands (RCC)
      InitEject =  0x3030,
      InitReject = 0x3031,

      CheckStat =  0x3130,

      EjectCard =  0x3330,
      RejectCard = 0x3331,

      readTrack1 = 0x3631,
      readTrack2 = 0x3632,
      readTrack3 = 0x3633,

      writeTrack1 = 0x3731,
      writeTrack2 = 0x3732,
      writeTrack3 = 0x3733,

      WaitCard = 0x3A30,
    }

    enum TCC { // Transmission Control Codes (TCC)
      STX = 0x02,
      ETX = 0x03,
      EOT = 0x04,
      ENQ = 0x05,
      ACK = 0x06,
      DLE = 0x10,
      NAK = 0x15,
    }

    Boolean processPacket(RCC cmd, byte[] data, ref string result) {
      byte[] resp = null;
      return processPacket(cmd, data, ref resp, ref result);
    }

    Boolean processPacket(RCC cmd, ref byte[] response, ref string result) {
      return processPacket(cmd, null, ref response, ref result);
    }

    Boolean processPacket(RCC cmd, ref string result) { 
      byte[] resp = null;
      return processPacket(cmd, null, ref resp, ref result);
    }

    Boolean processPacket(RCC cmd, byte[] data, 
                          ref byte[] resp, ref string result) {
      // Send Command
      if (!sendCmd(cmd, data)) { result = err_Fatal; return false; }

      // Wait ACK/NAK
      if (!waitAckNak(ref result)) return false;

      // DLE ENQ - Response Request
      sendEnq();

      int respL = 0;
      switch (cmd) {
        case RCC.readTrack1: respL = res_tr1_l; break;
        case RCC.readTrack2: respL = res_tr2_l; break;
        case RCC.readTrack3: respL = res_tr3_l; break;
        default: respL = common_res_l; break;
      }
      resp = new byte[respL];
      int offset = 0;

      // Response waiting timeout - 30 sec (5.02 sec minimum)
      var timerTh = new Thread(() => { Thread.Sleep(30 * 1000); });
      timerTh.Start();
      while (offset < respL && !checkPktEnd(resp, offset) && timerTh.IsAlive) 
        if (hPort.BytesToRead > 0)
          offset += hPort.Read(resp, offset, respL - offset);
      timerTh.Abort();

      Array.Resize(ref resp, offset);
      if (!checkResponse(resp, cmd, ref result)) return false;
      if (!checkPkt(resp, ref result)) return false;
      
      return true;
    }

    // Send DLE ENQ - Response Request
    void sendEnq() {
      byte[] b = new byte[] {(byte)TCC.DLE, (byte)TCC.ENQ};
      hPort.Write(b, 0, enq_l);
    }

    // Send Command (DLE STX 'C' cmd prm DLE ETX BCC)
    Boolean sendCmd(RCC cmd, byte[] prm) {
      var len = start_stop_seq_l + // DLE STX ... DLE ETX BCC (5 bytes)
                cmd_fixed_part_l + // 'C' XX (XX - Command) (3 bytes)
                ((prm != null) ? prm.Length : 0);
      byte[] b = new byte[len];
      b[0] = (byte)TCC.DLE;
      b[1] = (byte)TCC.STX;
      b[2] = 0x43;  // "C"
      b[3] = (byte)((UInt16)cmd / 0x100);
      b[4] = (byte)((UInt16)cmd % 0x100); ;
      ///////////
      if (prm != null) {
        int i, j;
        for (i = 0, j = 5; i < prm.Length; i++, j++) {
          // if DLE character is used in prm, double DLE
          if (prm[i] == (byte)TCC.DLE) {
            len++;
            Array.Resize(ref b, len);
            b[j++] = prm[i];
          }
          b[j] = prm[i];
        }
      }
      ///////////
      b[b.Length - 3] = (byte)TCC.DLE;
      b[b.Length - 2] = (byte)TCC.ETX;
      Boolean res = false;
      b[b.Length - 1] = bcc(b, ref res);
      if (res) hPort.Write(b, 0, b.Length);
      return res;
    }

    // Get Response data from Respose
    // (DLE STX 'P/N' cmd res text DLE ETX BCC
    //  cmd - 2 bytes, res - 2 bytes)
    Boolean getTextFromResp(byte[] resp, ref byte[] text, ref string result) {
      int textLen = resp.Length - start_stop_seq_l - resp_fixed_part_l;
      if (textLen <= 0) { result = err_Fatal; return false; }

      byte[] Text = Take(resp, start_seq_l + resp_fixed_part_l, textLen);
      Array.Resize(ref text, textLen);
      Boolean flDLE = false;
      int i = 0;
      foreach (byte b in Text) {
        // if DLE not doubled - error
        if (b != (byte)TCC.DLE && flDLE) { 
          result = err_Fatal; 
          return false; 
        }

        // Skip first DLE in pair
        if (b == (byte)TCC.DLE) flDLE = !flDLE; 
        if (flDLE) { Array.Resize(ref text, --textLen); continue; }

        text[i++] = b;
      }
      
      // Last DLE byte not paired - error
      if (flDLE) { result = err_Fatal; return false; }

      return true;
    }

    // BCC Vertical Parity
    // Command DLE STX Text DLE ETX BCC
    // BCC calculated for Text and ETX byte
    // * All DLE bytes in Text must be doubled. 
    //   BCC must be calculated only for one of DLE pair
    byte bcc(byte[] buf, ref Boolean result) {
      result = true;

      if (buf.Length - start_stop_seq_l <= 0) { result = false; return 0; }
      byte[] Text = Take(buf, 2, buf.Length - start_stop_seq_l);

      byte tmpB = 0;
      Boolean flDLE = false;
      foreach (byte b in Text) {
        // if DLE not doubled - error
        if (b != (byte)TCC.DLE && flDLE) { result = false; return 0; }
        
        // Skip first DLE in pair
        if (b == (byte)TCC.DLE) flDLE = !flDLE; if (flDLE) continue;
        
        // BCC calculate
        tmpB ^= b;
      }

      // Last DLE byte not paired - error
      if (flDLE) { result = false; return 0; }

      // plus ETX byte
      tmpB ^= (byte)TCC.ETX;
      return tmpB;
    }

    Boolean checkPktEnd(byte[] b, int len) {
      // If len < minimal packet length - there isn't end
      if (len < start_stop_seq_l) return false;
      // If last bytes looks like end
      if ((b[len - 3] == (byte)TCC.DLE) && (b[len - 2] == (byte)TCC.ETX)) {
        int i = len - 4;
        while (true) {
          if (i < start_seq_l) return true;

          // Is last(?) byte in text part - DLE?
          if (b[i] == (byte)TCC.DLE) {
            // Is it paired? 
            //  - Yes, check prev DLE pair 
            //  - No, there were not end
            if (b[i - 1] == (byte)TCC.DLE) i -= 2; else return false;
          } else return true;
        }
      }
      return false;
    }

    Boolean checkPkt(byte[] b, ref string result) {
      Boolean res = false;
      byte tmpBcc = bcc(b, ref res);
      if ((b[0] != (byte)TCC.DLE) || (b[b.Length - 3] != (byte)TCC.DLE)
       || (b[1] != (byte)TCC.STX) || (b[b.Length - 2] != (byte)TCC.ETX)
       || (b[b.Length - 1] != tmpBcc) || !res) {
        result = err_Format;
        return false;
      }
      return true;
    }

    Boolean waitAckNak(ref string s) {
      int offset = 0;
      byte[] r = new byte[ack_nak_l];

      // ACK/NAK waiting timeout - 30 sec (5.02 sec minimum)
      var timerThread = new Thread(() => { Thread.Sleep(30* 1000); });
      timerThread.Start();
      while (offset < ack_nak_l && timerThread.IsAlive)
        if (hPort.BytesToRead > 0)
          offset += hPort.Read(r, offset, ack_nak_l - offset);
      timerThread.Abort();

      // We're waiting for DLE ACK / DLE NAK pair
      // Is first byte DLE?
      if (r[0] != (byte)TCC.DLE) {
        s = String.Format(err_ACK, r[0], r[1]);
        return false;
      }

      // Get result ACK/NAK from second byte
      switch ((TCC)r[1]) {
        case TCC.ACK: break;
        case TCC.NAK: s = err_NAK; return false;
        default: s = String.Format(err_ACK, r[0], r[1]); return false;
      }
      return true;
    }

    Boolean checkResponse(byte[] buf, RCC cmd, ref string s) {
      // Check negative/positive
      switch (buf[2]) {
        case 0x50: // Positive
          s = p_res;
          addPosErr(ref s, BytesToUShort(Take(buf, 5, 2)));
          break;
        case 0x4E: // Negative
          s = n_res;
          addNegErr(ref s, BytesToUShort(Take(buf, 5, 2)));
          break;
        default:   // Undefined
          s = u_res; 
          return false; 
      };

      // Check command
      if (BytesToUShort(Take(buf, 3, 2)) != (ushort)cmd) {
        s += err_Cmd;
        return false;
      }

      return true;
    }

    // Status Table of Positive Response
    const UInt16 p_e00 = 0x3030;
    const string p_e00_s = "No card in reader";
    const UInt16 p_e01 = 0x3031;
    const string p_e01_s = "A card is in the Takeout Position";
    const UInt16 p_e02 = 0x3032;
    const string p_e02_s = "A card is in the reader";
    const UInt16 p_e04 = 0x3034;
    const string p_e04_s = "A card is in Read Start Position of the MM Sensor";
    const UInt16 p_e10 = 0x3130;
    const string p_e10_s = "IC Contact is pressed to the ICC";
    const UInt16 p_e11 = 0x3131;
    const string p_e11_s = "ICC is in the Activation Status";
    const UInt16 p_e20 = 0x3230;
    const string p_e20_s = "Transmission to the ICC is completed. (with/ without Receiving Data, with SW1 + SW2)";
    const UInt16 p_e21 = 0x3231;
    const string p_e21_s = "Continuous receiving Status from the ICC. (with Receiving Data, without SW1 + SW2)";
    const UInt16 p_e22 = 0x3232;
    const string p_e22_s = "Continuous sending Status to the ICC. (without Receiving Data, without SW1 + SW2)";
    const UInt16 p_e23 = 0x3233;
    const string p_e23_s = "Ends the Completion of ICC Transmission by forcedly interruption";
    const UInt16 p_e30 = 0x3330;
    const string p_e30_s = "In Down loading";
    const UInt16 p_e31 = 0x3331;
    const string p_e31_s = "Normal Completion of Down loading, Status of Initial Reset Waiting";
    const string p_eU_s = "Unknown Error";

    void addPosErr(ref string s, UInt16 err) {
      switch (err) {
        case p_e00: s += p_e00_s; break;
        case p_e01: s += p_e01_s; break;
        case p_e02: s += p_e02_s; break;
        case p_e04: s += p_e04_s; break;
        case p_e10: s += p_e10_s; break;
        case p_e11: s += p_e11_s; break;
        case p_e20: s += p_e20_s; break;
        case p_e21: s += p_e21_s; break;
        case p_e22: s += p_e22_s; break;
        case p_e23: s += p_e23_s; break;
        case p_e30: s += p_e30_s; break;
        case p_e31: s += p_e31_s; break;
        default:    s += p_eU_s;  break;
      }
    }

    // Status Table of Negative Response
    const UInt16 n_e00 = 0x3030;
    const string n_e00_s = "Undefined Command Receipt";
    const UInt16 n_e01 = 0x3031;
    const string n_e01_s = "Command Sequence Error";
    const UInt16 n_e02 = 0x3032;
    const string n_e02_s = "Command Data Error";
    const UInt16 n_e03 = 0x3033;
    const string n_e03_s = "Write Track setting Error";

    const UInt16 n_e10 = 0x3130;
    const string n_e10_s = "Card Jam";
    const UInt16 n_e11 = 0x3131;
    const string n_e11_s = "Shutter Abnormality";
    const UInt16 n_e12 = 0x3132;
    const string n_e12_s = "Sensor Abnormality";
    const UInt16 n_e13 = 0x3133;
    const string n_e13_s = "Motor Abnormality";
    const UInt16 n_e14 = 0x3134;
    const string n_e14_s = "Card Drawn Out";
    const UInt16 n_e15 = 0x3135;
    const string n_e15_s = "Card Jam in Re-intake";
    const UInt16 n_e16 = 0x3136;
    const string n_e16_s = "Card Jam at the Rear-end";
    const UInt16 n_e17 = 0x3137;
    const string n_e17_s = "75 bpi encoder abnormality";
    const UInt16 n_e18 = 0x3138;
    const string n_e18_s = "Power Down Detection";
    const UInt16 n_e19 = 0x3139;
    const string n_e19_s = "Waiting Initial Reset";

    const UInt16 n_e20 = 0x3230;
    const string n_e20_s = "Too Long Card";
    const UInt16 n_e21 = 0x3231;
    const string n_e21_s = "Too Short Card";

    const UInt16 n_e32 = 0x3332;
    const string n_e32_s = "Card Position Change";
    const UInt16 n_e33 = 0x3333;
    const string n_e33_s = "Memory Information Abnormality";

    const UInt16 n_e40 = 0x3430;
    const string n_e40_s = "Read Error ( SS error )";
    const UInt16 n_e41 = 0x3431;
    const string n_e41_s = "Read Error ( ES error )";
    const UInt16 n_e42 = 0x3432;
    const string n_e42_s = "Read Error ( VRC error )";
    const UInt16 n_e43 = 0x3433;
    const string n_e43_s = "Read Error ( LRC error )";
    const UInt16 n_e44 = 0x3434;
    const string n_e44_s = "Read Error ( No Encode )";
    const UInt16 n_e45 = 0x3435;
    const string n_e45_s = "Read Error ( No Data )";
    const UInt16 n_e46 = 0x3436;
    const string n_e46_s = "Read Error (Jitter Error)";
    const UInt16 n_e49 = 0x3439;
    const string n_e49_s = "Read Track setting Error";
    const UInt16 n_e50 = 0x3530;
    const string n_e50_s = "Write Error ( SS error )";
    const UInt16 n_e51 = 0x3531;
    const string n_e51_s = "Write Error ( ES error )";
    const UInt16 n_e52 = 0x3532;
    const string n_e52_s = "Write Error ( VRC error )";
    const UInt16 n_e53 = 0x3533;
    const string n_e53_s = "Write Error ( LRC error )";
    const UInt16 n_e54 = 0x3534;
    const string n_e54_s = "Write Error ( No Encode )";
    const UInt16 n_e55 = 0x3535;
    const string n_e55_s = "Write Error (Data discordance)";
    const UInt16 n_e56 = 0x3536;
    const string n_e56_s = "Write Error ( Jitter error )";

    const UInt16 n_e60 = 0x3630;
    const string n_e60_s = "Card Taken Out When Re-intake";
    const UInt16 n_e61 = 0x3631;
    const string n_e61_s = "Insertion monitoring Time is up";
    const UInt16 n_e62 = 0x3632;
    const string n_e62_s = "Take-out monitoring Time is up";
    const UInt16 n_e63 = 0x3633;
    const string n_e63_s = "Re-intake monitoring Time is up";
    const UInt16 n_e64 = 0x3634;
    const string n_e64_s = "Card was held at takeout position during initial reset";

    const UInt16 n_e70 = 0x3730;
    const string n_e70_s = "FW Imperfection";
    const UInt16 n_e71 = 0x3731;
    const string n_e71_s = "Initial CMD waiting after FW loading completion";

    const UInt16 n_e80 = 0x3830;
    const string n_e80_s = "Receiving from ICC is Impossibility";
    const UInt16 n_e81 = 0x3831;
    const string n_e81_s = "ICC Solenoid Abnormality";
    const UInt16 n_e82 = 0x3832;
    const string n_e82_s = "ICC Activation Abnormality";
    const UInt16 n_e84 = 0x3834;
    const string n_e84_s = "ICC Communication Abnormality";
    const UInt16 n_e85 = 0x3835;
    const string n_e85_s = "ICC Compulsory Abort Reception";
    const UInt16 n_e86 = 0x3836;
    const string n_e86_s = "ICC Reception Data Abnormality";
    const UInt16 n_e87 = 0x3837;
    const string n_e87_s = "Unsupported ICC";
    const UInt16 n_e88 = 0x3838;
    const string n_e88_s = "ICC movement in press";

    const string n_eU_s = "Unknown Error";

    void addNegErr(ref string s, UInt16 err) {
      switch (err) {
        case n_e00: s += n_e00_s; break;
        case n_e01: s += n_e01_s; break;
        case n_e02: s += n_e02_s; break;
        case n_e03: s += n_e03_s; break;

        case n_e10: s += n_e10_s; break;
        case n_e11: s += n_e11_s; break;
        case n_e12: s += n_e12_s; break;
        case n_e13: s += n_e13_s; break;
        case n_e14: s += n_e14_s; break;
        case n_e15: s += n_e15_s; break;
        case n_e16: s += n_e16_s; break;
        case n_e17: s += n_e17_s; break;
        case n_e18: s += n_e18_s; break;
        case n_e19: s += n_e19_s; break;

        case n_e20: s += n_e20_s; break;
        case n_e21: s += n_e21_s; break;

        case n_e32: s += n_e32_s; break;
        case n_e33: s += n_e33_s; break;

        case n_e40: s += n_e40_s; break;
        case n_e41: s += n_e41_s; break;
        case n_e42: s += n_e42_s; break;
        case n_e43: s += n_e43_s; break;
        case n_e44: s += n_e44_s; break;
        case n_e45: s += n_e45_s; break;
        case n_e46: s += n_e46_s; break;
        case n_e49: s += n_e49_s; break;
        case n_e50: s += n_e50_s; break;
        case n_e51: s += n_e51_s; break;
        case n_e52: s += n_e52_s; break;
        case n_e53: s += n_e53_s; break;
        case n_e54: s += n_e54_s; break;
        case n_e55: s += n_e55_s; break;
        case n_e56: s += n_e56_s; break;

        case n_e60: s += n_e60_s; break;
        case n_e61: s += n_e61_s; break;
        case n_e62: s += n_e62_s; break;
        case n_e63: s += n_e63_s; break;
        case n_e64: s += n_e64_s; break;

        case n_e70: s += n_e70_s; break;
        case n_e71: s += n_e71_s; break;

        case n_e80: s += n_e80_s; break;
        case n_e81: s += n_e81_s; break;
        case n_e82: s += n_e82_s; break;
        case n_e84: s += n_e84_s; break;
        case n_e85: s += n_e85_s; break;
        case n_e86: s += n_e86_s; break;
        case n_e87: s += n_e87_s; break;
        case n_e88: s += n_e88_s; break;

        default:    s += n_eU_s;  break;
      }
    }
    #endregion Internal Functions

  }
}
