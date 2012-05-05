using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace mobile_SMS
{
    public partial class Receive : Form
    {
        public Receive()
        {
            InitializeComponent();
        }
        ~Receive()
        {
           //// srpModemPort.Close();
        }
        ///private SerialPort srpModemPort = new SerialPort();
        private string port_name = "";

        public void A(string a)
        {

            port_name = a;
            serialPort1.PortName = port_name;
            serialPort1.Open();
            /*
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
            srpModemPort.ReadTimeout = 3000;
             */
        }

      
        private void Receive_Load(object sender, EventArgs e)
        {

            
            /*
            
            string Message = "";
            
           try {
            

            if (!srpModemPort.IsOpen)
                srpModemPort.Open();

            Message = srpModemPort.ReadExisting();
            label1.Text = Message;
         }
         catch (Exception exc)
         {
             Message = "Error receiving the answer. Reason:" + exc.Message;
             label1.Text = Message;
         }
             */
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            label1.Text = " Yes ";
            label1.Text+=serialPort1.ReadExisting();

        }

        private void serialPort1_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            label1.Text = e.ToString();
        }

        private void serialPort1_PinChanged(object sender, SerialPinChangedEventArgs e)
        {
            label1.Text = e.ToString();
        }

   
    }
}