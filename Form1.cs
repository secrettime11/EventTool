using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.IO.Ports;
using System.Threading;
using AllionTR398Tool;

namespace NewEventTool
{
    public delegate void SetAttenuatorStepValue();
    public partial class Form1 : Form
    {
        [DllImport("kernel32.dll")]
        private extern static uint SetSystemTime(ref SYSTEMTIME lpSystemTime);
        //[System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private SerialPort comport;
        delegate void Display(Byte[] buffer);

        MemoryStream userInput = new MemoryStream();

        private bool isFirst;
        private int count;
        private int LogCount; // for record
        private int f_count_record, e_count_record;
        private int Error_Flag;
        private int f_count, e_count;
        private double total_time;

        // *****attenuator*****
        private int attenuator1;
        private Thread actionTd;
        private int attenuator1StepValue;
        private SetAttenuatorStepValue m_SetAttenuatorStepValue;
        private int Cb3Sel;
        private bool isRun;
        private bool isMultiple;
        private int att1;

        // *****Robot arms*****
        double Pre_X = 0;
        double Pre_Y = 0;
        double Pre_Z = 0;
        int Pre_f = 0;
        int AI_FLAG = 0;
        int Set_Flg = 0;
        string logname;
        private Thread MissionA_Thread;
        bool Scr_Flag;

        //******streamWriter******
        private string FILE_NAME;
        private string FolderTime;
        private string LogTime;
        DateTime TestTime = DateTime.Now;
        //bool record_Flag;
        string rtb2;
        private int neg_count = 0;
        double avtime_record;
        double Correct_Rate_record = 0;
        private double total_time_record;
        private bool AttFlag;


        string ti1;
        string ti2;
        int num;
        DateTime dt1;
        DateTime dt2;
        int NegCount;

        System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
        string rmsg = "";

        //**********窗體縮放************
        int igFormWidth = new int();  //窗口寬度
        int igFormHeight = new int(); //窗口高度
        float fgWidthScaling = new float(); //寬度縮放比例
        float fgHeightScaling = new float(); //高度縮放比例
        public Form1()
        {
            InitializeComponent();
            igFormWidth = this.Width;
            igFormHeight = this.Height;
            InitConTag(this);
            isRun = false; // Attenuator flag
            Set_Flg = 1; // Robot flag
            Scr_Flag = true; // Script flag
            string[] baud = { "9600", "14400", "19200", "38400", "57600", "115200" };
            string[] myPorts = SerialPort.GetPortNames();
            try
            {
                if (myPorts[0] != null)
                {
                    comboBox1.Items.AddRange(myPorts);
                    comboBox1.SelectedIndex = 0;
                }
            }
            catch
            {
                MessageBox.Show("Please Input ComPort!!");
            }
            comboBox2.DataSource = baud;
            comboBox2.SelectedIndex = 5;
            GetNetworkTime();
            count = 0;
            num = 1;
            f_count = 0;
            e_count = 0;

            //個別紀錄用
            LogCount = 0;
            f_count_record = 0;
            e_count_record = 0;
            AttFlag = false;
            Error_Flag = 0;
            isFirst = true;
            //Setup events to listen on keypress
            richTextBox2.TextChanged += richtextBox2_TextChanged;
            richTextBox2.KeyDown += richTextBox2_KeyDown;
            richTextBox2.KeyPress += richTextBox2_KeyPress;
            richTextBox2.KeyUp += richTextBox2_KeyUp;


            //Setup value of Attenuator at begining
            detectAttenuator();
            comboBox3.SelectedIndex = 0;
            Cb3Sel = 0;
            m_SetAttenuatorStepValue = new SetAttenuatorStepValue(setStepValue);
            if ((attenuator1 == 0))
            {
                MessageBox.Show("There's no any attenuator be detected.", "Warning(Attenuation)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public struct SYSTEMTIME
        {
            public ushort wYear;
            public ushort wMonth;
            public ushort wDayOfWeek;
            public ushort wDay;
            public ushort wHour;
            public ushort wMinute;
            public ushort wSecond;
            public ushort wMilliseconds;
        }
        static string GetNetworkTime()
        {
            try
            {
                //default Windows time server
                const string ntpServer = "time.windows.com";
                //const string ntpServer = "tntp1.aliyun.com";

                // NTP message size - 16 bytes of the digest (RFC 2030)
                var ntpData = new byte[48];

                //Setting the Leap Indicator, Version Number and Mode values
                ntpData[0] = 0x1B; //LI = 0 (no warning), VN = 3 (IPv4 only), Mode = 3 (Client Mode)

                var addresses = Dns.GetHostEntry(ntpServer).AddressList;

                //The UDP port number assigned to NTP is 123
                var ipEndPoint = new IPEndPoint(addresses[0], 123);
                //NTP uses UDP 
                var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                socket.Connect(ipEndPoint);

                //Stops code hang if NTP is blocked
                socket.ReceiveTimeout = 3000;

                socket.Send(ntpData);
                socket.Receive(ntpData);
                socket.Close();

                //Offset to get to the "Transmit Timestamp" field (time at which the reply 
                //departed the server for the client, in 64-bit timestamp format."
                const byte serverReplyTime = 40;

                //Get the seconds part
                ulong intPart = BitConverter.ToUInt32(ntpData, serverReplyTime);

                //Get the seconds fraction
                ulong fractPart = BitConverter.ToUInt32(ntpData, serverReplyTime + 4);

                //Convert From big-endian to little-endian
                intPart = SwapEndianness(intPart);
                fractPart = SwapEndianness(fractPart);

                var milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);



                //**UTC** time
                System.DateTime oUTC = (new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddMilliseconds((long)milliseconds).AddHours(8);

                return oUTC.ToString("hh:mm:ssfff");
            }
            catch
            {
                return "fail";
            }

        }
        public void run_script()
        {
            try
            {
                Thread.Sleep(3000);
                isFirst = true;
                int loop = Int32.Parse(textBox5.Text.ToString());

                for (int i = 1; i <= loop; i++)
                {
                    TcpClient tcpclnt = new TcpClient();
                    tcpclnt.Connect("192.168.0.1", 30002);
                    //使用 Ethernet socket, 以 port 30002 與UR連接

                    byte[] ba;      //關閉程式開頭定義時, 此行要開

                    ASCIIEncoding asen = new ASCIIEncoding();
                    NetworkStream stream = tcpclnt.GetStream();
                    StreamReader file = new StreamReader(textBox1.Text.ToString());
                    string line1 = file.ReadToEnd();
                    ba = asen.GetBytes(line1 + "\n");
                    Console.Write(line1 + "\n");
                    Console.Write("--------------------" + "\n");
                    stream.Write(ba, 0, ba.Length);
                    
                    tcpclnt.Close();
                    file.Dispose();
                    Thread.Sleep(1300);
                }
                string avg_record = $"{att1}dB 準確率(%): " + Correct_Rate_record.ToString("0.##%") + $"   平均時間(s): " + avtime_record.ToString("f5") + "s";

                File.AppendAllText($"./Data/{FILE_NAME}/Average_Result/{FolderTime}.txt", avg_record + "\r\n");
                
                AttFlag = true;
                
                if (Cb3Sel != 2 && LogCount == loop && AttFlag == true)
                {
                    LogCount = 0; // 紀錄初始化
                    f_count_record = 0;
                    e_count_record = 0;
                    total_time_record = 0;
                    neg_count = 0;
                    Correct_Rate_record = 0;
                    isMultiple = true;
                }
            }
            catch (Exception)
            {
                string avg_record = $"{att1}dB 準確率(%): " + Correct_Rate_record.ToString("0.##%") + $"   平均時間(s): " + avtime_record.ToString("f5") + "s";

                File.AppendAllText($"./Data/{FILE_NAME}/Average_Result/{FolderTime}.txt", avg_record + "\r\n");
            }
        }

        private void detectAttenuator()
        {
            attenuator1 = 0;

            label3.Text = "Attenuator(NA)";
            label4.Text = "Attenuator(NA)";
            label5.Text = "Attenuator(NA)";
            label6.Text = "Attenuator(NA)";

            Attenuator.fnLDA_SetTestMode(false);
            int device = Attenuator.fnLDA_GetNumDevices();

            for (int i = 0; i < device; i++)
            {
                Attenuator.fnLDA_InitDevice(i + 1);
                int serialNumber = Attenuator.fnLDA_GetSerialNumber(i + 1);

                switch (i)
                {
                    case 0:
                        attenuator1 = i + 1;
                        label3.Text = "Attenuator (" + attenuator1 + ")";
                        break;
                    case 1:
                        attenuator1 = i + 1;
                        label4.Text = "Attenuator (" + attenuator1 + ")";
                        break;
                    case 2:
                        attenuator1 = i + 1;
                        label5.Text = "Attenuator (" + attenuator1 + ")";
                        break;
                    case 3:
                        attenuator1 = i + 1;
                        label6.Text = "Attenuator (" + attenuator1 + ")";
                        break;
                }

                Attenuator.fnLDA_CloseDevice(i + 1);
            }
        }
        //Attenuator
        private void setAttenuatorValue(int deviceId, double attenuatorValue)
        {
            Attenuator.fnLDA_SetTestMode(false);
            Attenuator.fnLDA_InitDevice(deviceId);

            double value = attenuatorValue / 0.25;
            Attenuator.fnLDA_SetAttenuation(deviceId, (int)value);
            //MessageBox.Show(Attenuator.fnLDA_GetAttenuation(deviceId).ToString());
            Attenuator.fnLDA_CloseDevice(deviceId);
        }
        void setStepValue()
        {
            attenuator1StepValue = Convert.ToInt32(textBox3.Text.Trim());
        }
        static uint SwapEndianness(ulong x)
        {
            return (uint)(((x & 0x000000ff) << 24) +
                           ((x & 0x0000ff00) << 8) +
                           ((x & 0x00ff0000) >> 8) +
                           ((x & 0xff000000) >> 24));
        }
        private void richTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }
        private void richTextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "g" || e.KeyCode.ToString() == "G")
            {

                if (num > 1)
                {
                    Error_Flag = 1;
                    //Console.Write("Error_Flag: " + Error_Flag + "\n");
                }
                else
                {
                    num++;
                    Error_Flag = 0;
                }
            }
            Console.Write("Error_Flag: " + Error_Flag + "\n");
        }
        private void richtextBox2_TextChanged(object sender, EventArgs e)
        {
            //ti2 = GetNetworkTime();
            if (isFirst)
            {
                //time3 = DateTime.UtcNow.Ticks;
                dt2 = DateTime.Now;
                ti2 = DateTime.Now.TimeOfDay.ToString();
                Console.WriteLine("show DateTime.Now.TimeOfDay： " + ti2);
                isFirst = false;

                count = count + 1;
                LogCount = LogCount + 1;
                //stopwatch.Stop();
                //Console.Write("stopwatch:" + stopwatch.ElapsedTicks.ToString() + "\n");

                //Console.WriteLine("Ticks={0:N0}", stopwatch.ElapsedTicks + "\n");
                //Console.WriteLine("Freq={0:N0}", Stopwatch.Frequency + "\n");
                //decimal ns = 1000000000M / Stopwatch.Frequency;
                //Console.WriteLine("1 Tick = {0:N4}ns", ns);

                //decimal ms = stopwatch.ElapsedTicks / Stopwatch.Frequency;
                //Console.WriteLine("Tick = {0:N10}ns", ms);

                //DateTime _starttime = DateTime.UtcNow;
                //Stopwatch _stopwatch = Stopwatch.StartNew();
                //DateTime highresDT = _starttime.AddTicks(_stopwatch.Elapsed.Ticks);

                //Console.Write("1"+_starttime.TimeOfDay.ToString() + "\n");
                //Console.Write("1" + _stopwatch.ElapsedTicks.ToString() + "\n");
                //Console.Write("1" + highresDT.TimeOfDay.ToString() + "\n");


            }

            //Console.Write("TextChanged: " + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "\n");
        }
        private void richTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            FILE_NAME = TestTime.ToString("yyyyMMdd");
            FolderTime = TestTime.ToString("HH mm ss.fffff");

            double Correct_Rate = 0;

            if (Error_Flag == 1)
            {
                e_count++;
                e_count_record++; //記錄用
                Console.WriteLine("e_count： " + e_count);
            }
            string pos = att1 + "dB";
            string rtb1 = (dt2.ToString("HH:mm:ss.fffff")) + " (" + f_count.ToString() + ")" + "\r\n";
            if (f_count != 0)
            {
                richTextBox2.AppendText(" " + (dt2.ToString("HH:mm:ss.fffff")) + " (" + f_count.ToString() + ")" + "\n");
            }

            isFirst = true;

            label10.Text = (count - NegCount - e_count).ToString();

            System.IO.File.WriteAllText("keylog.txt", richTextBox2.Text.Replace("\n", Environment.NewLine));
            System.IO.File.WriteAllText("T_keylog.txt", richTextBox1.Text.Replace("\n", Environment.NewLine));

            TimeSpan ts = dt2 - dt1;

            string rtb3 = (ts.TotalSeconds.ToString("f5")) + " (" + LogCount.ToString() + ")  " + att1 + "dB" + "\r\n";
            if (f_count != 0)
            {
                richTextBox3.AppendText((ts.TotalSeconds.ToString("f5")) + " (" + LogCount.ToString() + ")  " + att1 + "dB" + "\n");    //單位S
            }

            int loop = Convert.ToInt32(textBox5.Text.Trim());

            #region Count Error Results
            if (ts.TotalSeconds <= 0)
            {
                NegCount++;
                neg_count++; // 記錄用
            }
            if (ts.TotalSeconds > 0 && Error_Flag == 0)
            {
                total_time += Convert.ToDouble(ts.TotalSeconds.ToString("f5"));
                total_time_record += Convert.ToDouble(ts.TotalSeconds.ToString("f5"));
            }
            #endregion
            Error_Flag = 0;


            System.IO.File.WriteAllText("Result.txt", richTextBox3.Text.Replace("\n", Environment.NewLine));

            double avtime = total_time / (count - NegCount - e_count);
            avtime_record = total_time_record / (LogCount - neg_count - e_count_record);
            //Console.WriteLine("total_time： " + total_time);
            //Console.WriteLine("avtime： " + avtime);
            //Console.WriteLine($"Attenuator : {att1}");
            //Console.WriteLine($"negCount : {NegCount}");
            num = 1;

            Correct_Rate = (double)(count - NegCount - e_count) / count;
            Correct_Rate_record = (double)(LogCount - neg_count - e_count_record) / LogCount;
            //Console.WriteLine("Correct_Rate： " + Correct_Rate);
            string avg = Correct_Rate.ToString("0.##%") + "   " + avtime.ToString("f5");
            File.WriteAllText("Average_Result.txt", avg);
            textBox4.Text = att1.ToString();
            label12.Text = (NegCount + e_count).ToString();
            
            //自動記錄資料
            if (!Directory.Exists($"./Data/{FILE_NAME}/{att1}dB/{FolderTime}"))
            {
                Directory.CreateDirectory($"./Data/{FILE_NAME}/{att1}dB/{FolderTime}");
            }
            if (!Directory.Exists($"./Data/{FILE_NAME}/Average_Result"))
            {
                Directory.CreateDirectory($"./Data/{FILE_NAME}/Average_Result");
            }
            if (f_count != 0)
            {
                File.AppendAllText($"./Data/{FILE_NAME}/{pos}/{FolderTime}/keylog.txt", rtb1);
                File.AppendAllText($"./Data/{FILE_NAME}/{pos}/{FolderTime}/Result.txt", rtb3);
            }
            if ((neg_count + e_count_record) >= 5)
            {
                Scr_Flag = false;
                if (actionTd != null && actionTd.IsAlive)
                {
                    actionTd.Abort();
                    actionTd.Join();
                }

                if (MissionA_Thread != null && MissionA_Thread.IsAlive)
                {
                    MissionA_Thread.Abort();
                    MissionA_Thread.Join();
                }
                if (comport.IsOpen)
                {
                    comport.Close();
                    button1.Enabled = true;
                }

                string avg_record = $"{att1}dB 準確率(%): " + Correct_Rate_record.ToString("0.##%") + $"   平均時間(s): " + avtime_record.ToString("f5") + "s";

                File.AppendAllText($"./Data/{FILE_NAME}/Average_Result/{FolderTime}.txt", avg_record + "\r\n");

                MessageBox.Show("Detective five error state.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void RichTextBox2_TextChanged_1(object sender, EventArgs e)
        {
            if (isFirst)
            {
                dt2 = DateTime.Now;
                ti2 = DateTime.Now.TimeOfDay.ToString();
                Console.WriteLine("show DateTime.Now.TimeOfDay： " + ti2);
                isFirst = false;

                count = count + 1;
                LogCount = LogCount + 1;
            }
            //Console.Write("TextChanged: " + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "\n");
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (MissionA_Thread != null && MissionA_Thread.IsAlive)
            {
                MissionA_Thread.Abort();
                MissionA_Thread.Join();
            }
        }

        private void Label9_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                int baudrate = int.Parse(comboBox2.Text.ToString());
                comport = new SerialPort(comboBox1.Text, baudrate, Parity.None, 8, StopBits.One);
                comport.Encoding = Encoding.ASCII;
                comport.DataReceived += new SerialDataReceivedEventHandler(comport_DataReceived);
                if (!comport.IsOpen)
                {
                    comport.Open();
                    comport.Write("1");
                    button1.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // *********** Attenuation **********
            isRun = true;
            Cb3Sel = comboBox3.SelectedIndex;
            actionTd = new Thread(startiPerfAction);
            //actionTd.IsBackground = true; 
            actionTd.Start();
        }
        private void startiPerfAction()
        {
            setStepValue();
            isRun = true;
            att1 = Convert.ToInt32(textBox2.Text.Trim());
            int loop = Convert.ToInt32(textBox5.Text.Trim());
            while (isRun)
            {
                if (isMultiple == true)
                {
                    if (Cb3Sel == 0)
                    {
                        att1 = att1 + attenuator1StepValue;
                        MissionA_Thread.Abort();
                        MissionA_Thread.Join();
                        AttFlag = false;

                        if (CheckNET("192.168.0.1", 30002, 3) == true)
                        {
                            if (Set_Flg == 1)
                            {
                                if (textBox5 != null)
                                {
                                    //logname = DateTime.Now.ToString("yyyyMMddHHmmss");
                                    MissionA_Thread = new Thread(new ThreadStart(run_script));
                                    MissionA_Thread.IsBackground = true;
                                    MissionA_Thread.Start();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Please Loading Setting Value", "Message!!");
                            }
                        }
                        
                    }
                    else if (Cb3Sel == 1)
                    {
                        att1 = att1 - attenuator1StepValue;
                        MissionA_Thread.Abort();
                        MissionA_Thread.Join();
                        AttFlag = false;
                        if (CheckNET("192.168.0.1", 30002, 3) == true)
                        {
                            if (Set_Flg == 1)
                            {
                                if (textBox5 != null)
                                {
                                    //logname = DateTime.Now.ToString("yyyyMMddHHmmss");
                                    MissionA_Thread = new Thread(new ThreadStart(run_script));
                                    MissionA_Thread.IsBackground = true;
                                    MissionA_Thread.Start();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Please Loading Setting Value", "Message!!");
                            }
                        }
                    }
                    if (att1 > 63)
                    {
                        att1 = 63;
                    }

                    else if (att1 < 0)
                    {
                        att1 = 0;
                    }
                    
                    isMultiple = false;
                }
                if (attenuator1 != 0)
                {
                    setAttenuatorValue(attenuator1, att1);
                }
                //Console.WriteLine($"Attenuator : {att1}");
                //Console.WriteLine($"negCount : {NegCount}");
            }

            if (isFirst)
            {
                Invoke(m_SetAttenuatorStepValue);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (comport.IsOpen)
                {
                    comport.Close();
                    button1.Enabled = true;
                }
                comport.Dispose();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void comport_DataReceived(Object sender, SerialDataReceivedEventArgs e)
        {
            try
            {

                int B2R = comport.BytesToRead;
                Byte[] buffer = new Byte[B2R];
                Int32 length = (sender as SerialPort).Read(buffer, 0, B2R);
                //comport.DiscardInBuffer();
                Array.Resize(ref buffer, length);
                Display d = new Display(DisplayText);
                this.Invoke(d, new Object[] { buffer });


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "(*.script)|*.script";
            openFileDialog1.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            //Pre_X = 0;
            //Pre_Y = 0;
            //Pre_Z = 0;
            //Pre_f = 0;
            //AI_FLAG = 0;

            //if (CheckNET("192.168.0.1", 30002, 3) == true && CheckNET("127.0.0.1", 8787, 3) == true)
            if (CheckNET("192.168.0.1", 30002, 3) == true)
            {
                if (Set_Flg == 1)
                {
                    if (textBox5 != null)
                    {
                        logname = DateTime.Now.ToString("yyyyMMddHHmmss");
                        MissionA_Thread = new Thread(new ThreadStart(run_script));
                        MissionA_Thread.IsBackground = true;   
                        MissionA_Thread.Start();
                    }
                }
                else
                {
                    MessageBox.Show("Please Loading Setting Value", "Message!!");
                }
            }
            else
            {
                MessageBox.Show("Internet Error!!!", "Message!!");
            }
        }
        private void DisplayText(Byte[] buffer)
        {

            try
            {
                richTextBox2.ScrollBars = RichTextBoxScrollBars.Vertical;
                richTextBox1.ScrollBars = RichTextBoxScrollBars.Vertical;
                //Encoding encoding = Encoding.GetEncoding(1252);
                //string s = BitConverter.ToString(buffer).Replace("-", " ");
                //string message = System.Text.Encoding.ASCII.GetString(buffer);
                string newst = Encoding.ASCII.GetString(buffer);

                if (newst.Contains("\n"))
                {
                    dt1 = DateTime.Now;
                    ti1 = DateTime.Now.TimeOfDay.ToString();
                    Console.WriteLine("show Time： " + ti1);
                    Console.WriteLine("1:" + newst);
                    rmsg = dt1.ToString("HH:mm:ss.fffff") + " " + rmsg + newst;
                    Console.WriteLine("2:" + rmsg);

                    richTextBox1.Text += rmsg;
                    rtb2 = rmsg; //儲存rmsg用來紀錄
                    richTextBox1.SelectionStart = richTextBox1.Text.Length;
                    richTextBox3.SelectionStart = richTextBox3.Text.Length;
                    richTextBox1.ScrollToCaret();
                    richTextBox3.ScrollToCaret();
                    rmsg = "";
                    f_count = f_count + 1;
                    f_count_record = f_count_record + 1;
                    Console.WriteLine("f_count:" + f_count);
                    return;
                }
                rmsg = rmsg + newst;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //string st5 = st4.Replace("[0m", "");
            //string st6= st5.Replace("[0m", "");
            //string st7 = st6.Replace("[32m", "");
            //textBox1.Text += Encoding.Default.GetString(buffer).Replace("[0m", " ") + "\n";


            //textBox1.Text += String.Format("{0}{1}", BitConverter.ToString(buffer), Environment.NewLine).Replace("-", " ");

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            File.WriteAllText($"./Data/{FILE_NAME}/T_keylog_{FolderTime}.txt", richTextBox1.Text.Replace("\n", Environment.NewLine));

            //關閉執行緒

            if (actionTd != null && actionTd.IsAlive)
            {
                actionTd.Abort();
                actionTd.Join();
            }
            if (MissionA_Thread != null && MissionA_Thread.IsAlive)
            {
                MissionA_Thread.Abort();
                MissionA_Thread.Join();
            }
        }
        
        private void RestBtn_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
        static bool CheckNET(string IPStr, int Port, int Timeout)
        {
            bool success = false;
            try
            {
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                success = socket.BeginConnect(IPAddress.Parse(IPStr), Port, null, null).AsyncWaitHandle.WaitOne(Timeout, true);
                byte[] getbuffer = new byte[100];
                socket.Receive(getbuffer);
                Thread.Sleep(50);
                byte[] bmsg = Encoding.UTF8.GetBytes("out");
                socket.Send(bmsg);
                socket.Close();
            }
            catch { }
            return success;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //SetWindowPos(this.Handle, HWND_TOPMOST, 100, 100, 300, 300, TOPMOST_FLAGS);
            /*record_Flag = true;*/ // 記錄用旗子
            //AllocConsole();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (igFormWidth == 0 || igFormHeight == 0) return;
            fgWidthScaling = (float)this.Width / (float)igFormWidth;
            fgHeightScaling = (float)this.Height / (float)igFormHeight;
            ResizeCon(fgWidthScaling, fgHeightScaling, this);
        }
        private void InitConTag(Control cons)
        {
            foreach (Control con in cons.Controls) //遍歷控件集
            {
                con.Tag = con.Left + "," + con.Top + "," + con.Width + "," + con.Height + "," + con.Font.Size;
                if (con.Controls.Count > 0) //處理子控件
                {
                    InitConTag(con);
                }
            }
        }
        private void ResizeCon(float widthScaling, float heightScaling, Control cons)
        {
            float fTmp = new float();

            foreach (Control con in cons.Controls) //遍歷控件集
            {
                string[] conTag = con.Tag.ToString().Split(new char[] {','});
                fTmp = Convert.ToSingle(conTag[0]) * widthScaling;
                con.Left = (int)fTmp;
                fTmp = Convert.ToSingle(conTag[1]) * heightScaling;
                con.Top = (int)fTmp;
                fTmp = Convert.ToSingle(conTag[2]) * widthScaling;
                con.Width = (int)fTmp;
                fTmp = Convert.ToSingle(conTag[3]) * heightScaling;
                con.Height = (int)fTmp;
                fTmp = Convert.ToSingle(conTag[4]) * widthScaling * heightScaling;
                con.Font = new Font("", fTmp);
                if (con.Controls.Count > 0) //處理子控件
                {
                    ResizeCon(widthScaling, heightScaling, con);
                }
            }
        }
        private void Form1_Deactivate(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            Show();
            Activate();
        }

        public static void Send(Socket socket, byte[] buffer, int offset, int size, int timeout)
        {
            int startTickCount = Environment.TickCount;
            int sent = 0;  // how many bytes is already sent
            do
            {
                if (Environment.TickCount > startTickCount + timeout)
                    throw new Exception("Timeout.");
                try
                {
                    sent += socket.Send(buffer, offset + sent, size - sent, SocketFlags.None);
                }
                catch (SocketException ex)
                {
                    if (ex.SocketErrorCode == SocketError.WouldBlock ||
                        ex.SocketErrorCode == SocketError.IOPending ||
                        ex.SocketErrorCode == SocketError.NoBufferSpaceAvailable)
                    {
                        // socket buffer is probably full, wait and try again
                        Thread.Sleep(30);
                    }
                    else
                        throw ex;  // any serious error occurr
                }
            } while (sent < size);
        }
    }
}
