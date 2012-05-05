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
    public partial class Compose : Form
    {
        public Compose()
        {
            InitializeComponent();
           

        }
        ~Compose()
        {
            srpModemPort.Close();
           
        }
        private SerialPort srpModemPort = new SerialPort();
        private string port_name = "";

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
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string mobile_number = textBox1.Text;
            string message = textBox2.Text;
           
            try
            {
                if (!srpModemPort.IsOpen)
                    srpModemPort.Open();
                srpModemPort.WriteLine("AT+CMGS=" + mobile_number + "\r" + message + (char)(26));

                MessageBox.Show("Message Send.....");

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            ///MessageBox.Show(message);


        }
    }
}