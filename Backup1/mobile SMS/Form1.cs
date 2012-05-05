using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace mobile_SMS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private string port_name = "";
        private void button1_Click(object sender, EventArgs e)
        {
            Compose a = new Compose();
            a.A(port_name);
            a.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            Receive b = new Receive();
            b.A(port_name);
            b.Show();
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (string s in System.IO.Ports.SerialPort.GetPortNames())
            {
                comboBox1.Items.Add(s);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            port_name = comboBox1.SelectedItem.ToString();
            button1.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
            comboBox1.Visible = false;
         
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 c = new Form2();
            c.A(port_name);
            c.Show();
            
        }

       
    }
}