using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;

namespace mobile_SMS
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        ~Form2()
        {
          
            srpModemPort.Close();
        }
        public static bool _Continue = false;
        public static bool _ContSMS = false;
        private bool _Wait = false;
        public static bool _ReadPort = false;
        private int flag_thread = 0;
        private int flag_return = 0;
        private string port_name = "";
        private SerialPort srpModemPort = new SerialPort();
        private int line = 0;
        /// public event SendingEventHandler Sending;
       /// public event DataReceivedEventHandler DataReceived; 
       /// 
        public delegate void SetText(string text);
        
        public void SetTextBoxValue(string textValue)
        {

            if (this.textBox1.InvokeRequired)
            {

                SetText del = new SetText(this.SetTextBoxValue);

                this.textBox1.Invoke(del, new object[] { textValue });

            }

            else
            {
                line++;
                if (line > 10)
                {
                    line = 1;
                    this.textBox1.Text = "";
                }
                this.textBox1.Text += "\n" + textValue + "\r\n";
                

            }

        }

        public void A(string a)
        {
            port_name = a;
            srpModemPort = new SerialPort();
            srpModemPort.PortName = port_name;
            srpModemPort.BaudRate = 115200;
            srpModemPort.Parity = Parity.None;
            srpModemPort.DataBits = 8;
            srpModemPort.StopBits = StopBits.One;
            srpModemPort.Handshake = Handshake.RequestToSend;
            srpModemPort.DtrEnable = true;
            srpModemPort.RtsEnable = true;
            srpModemPort.NewLine = System.Environment.NewLine;
            srpModemPort.WriteTimeout = 2000;
           /// srpModemPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.srpModemPort_DataReceived);
        
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            flag_thread = 0;
            flag_return = 0;
            if (!srpModemPort.IsOpen)
                srpModemPort.Open();
           
            Thread t = new Thread(new ThreadStart(ReadPort));
            
            textBox1.ReadOnly = true;

            t.Start();
          

         /////   textBox1.ReadOnly = true;

        }

        private void ReadPort()
        {
            string SerialIn = "";
            byte[] RXBuffer = new byte[srpModemPort.ReadBufferSize + 1];
            string SMSMessage = null;
            int Strpos = 0;
            string TmpStr = null;
            while (srpModemPort.IsOpen == true)
            {
                if (flag_thread == 1)
                {
                    flag_return = 1;
                    while ( flag_thread == 1 )
                    { 
                    
                    }
                }
                flag_return = 0;
                if ((srpModemPort.BytesToRead != 0) & (srpModemPort.IsOpen == true))
                {
                    while (srpModemPort.BytesToRead != 0)
                    {
                        //srpModemPort.Read(RXBuffer, 0, srpModemPort.ReadBufferSize);
                        SerialIn += srpModemPort.ReadLine();
                        
                        //SerialIn += System.Text.Encoding.ASCII.GetString( RXBuffer );

                        
                        /*
                        if (SerialIn.Contains(">") == true)
                        {
                            _ContSMS = true;
                        }
                        if (SerialIn.Contains("+CMGS:") == true)
                        {
                            _Continue = true;
                           // if (Sending != null)
                            ///    Sending(true);
                            _Wait = false;
                           /// SerialIn = string.Empty;
                            ////RXBuffer = new byte[srpModemPort.ReadBufferSize + 1];
                        }
                         */
                    }

                    if (SerialIn.Length > 0)
                    {

                        //MessageBox.Show(SerialIn);
                        //textBox1.Text += "\n" + SerialIn;
                        SetTextBoxValue(SerialIn);
                        SerialIn = "";
                    }
                    //if (DataReceived != null)
                    ////  DataReceived(SerialIn);
                    ////SerialIn = string.Empty;
                    RXBuffer = new byte[srpModemPort.ReadBufferSize + 1];
                }
            }
        }    





        private void srpModemPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            textBox1.Text = "COM Event!!!";

            textBox1.Text += srpModemPort.ReadExisting();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            flag_thread = 1;
            while(flag_return == 0)
            { 
            
            }
            string command = textBox2.Text;
            ////command.Replace('\\',' ');
            
            
           

            int i,n,j=0;
            n=command.Length;

            string new_s = "";

            new_s = command;

            if (n >= 8)
            {
                new_s = "";

                for (i = 0; i < 8; i++)
                    new_s += command[i].ToString();
                if (new_s.Equals("AT+CMGS="))
                {
                    for (i = 8; i < n; i++)
                    {       if (command[i].Equals('r') != true)
                            new_s += command[i].ToString();
                        else
                     {    new_s +="\r";
                         break; 
                    }
                    }



                    for (i = i + 1; i < n;i++ )
                        new_s += command[i].ToString();

                    srpModemPort.WriteLine(new_s + (char)26);
                    j = 1;
                }
                else
                    new_s = command;

            }
           

            if(j==0)
            srpModemPort.WriteLine(new_s + "\n");  
            //srpModemPort.WriteLine(new_s + (char)(26));

            flag_thread = 0;



        }


    }
}