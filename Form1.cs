using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace seriova_komunikce
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        { // init
            disconnect.Enabled = false;
            connect.Enabled = true;
            delete.Enabled = false;
        }
        private void connect_Click(object sender, EventArgs e)
        {// zkontroluje zda je připojen správný port a připojí ho
            try
            {
                serialPort.PortName = portName.Text;
                serialPort.Open();
                connect.Enabled = false;
                portName.Enabled = false;
                disconnect.Enabled = true;
                timer1.Enabled = true;
            }
            catch
            {
                MessageBox.Show("Zadal jste špatný název portu");
            }
        }
        private void disconnect_Click(object sender, EventArgs e)
        {// odpojí port
            serialPort.Close();
            connect.Enabled = true;
            portName.Enabled = true;
            disconnect.Enabled = false;
            timer1.Enabled = false;
        }
        private void delete_Click(object sender, EventArgs e)
        {// vymaže zobrazené hodnoty 
            hodnoty.Clear();
        }
        private void hodnoty_TextChanged(object sender, EventArgs e)
        {// pokud je nějaká hodnota zobrazena tak zapne tl. pro vymazání
            if (hodnoty.TextLength > 0)
                delete.Enabled = true;
            else
                delete.Enabled = false;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {// vyzvedne hodnoty ze sériového portu a zobrazí je                 
               /* string s = serialPort.ReadExisting();
                hodnoty.Text += s;      */      
        }

        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string s = serialPort.ReadExisting();
            hodnoty.Text += s;
        }
    }
}
