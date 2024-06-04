using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMI_Truck_s_Gate_Solution{
    public partial class Monitor : Form{
        public Monitor(){
            InitializeComponent();
        }

        private void Monitor_Load(object sender, EventArgs e){
            String[] portList = System.IO.Ports.SerialPort.GetPortNames(); // Getting the available ports
            foreach(String portName in portList){
                comboBox1.Items.Add(portName);
            }

            // Fix error empty port
            button1.Enabled = false;
            button2.Enabled = false;

            // fix error sending data while the port is closed
            button3.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e){
            try{
                serialPort1.PortName = comboBox1.Text;
                serialPort1.NewLine = "\r\n";
                serialPort1.Open();
                Monitor.ActiveForm.Text = serialPort1.PortName + " is Connected";
            }
            catch(Exception ex){
                Monitor.ActiveForm.Text = "Error: " + ex.Message.ToString();
                // Fix error when clicking the connect button twice
                button1.Enabled = true;

                button2.Enabled = false; // enable the dosconnect button when its connected
                button3.Enabled = false; // you can send data when its connected
            }

            if (serialPort1.IsOpen){
                button1.Enabled = false;

                button2.Enabled = true;
                button3.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e){
            serialPort1.Close();
            Monitor.ActiveForm.Text = "Serial Communication";

            button1.Enabled = true; // You can connect again after it is disconnected
            button3.Enabled = false; // You cant send data when you are disconnected
        }

        private void button3_Click(object sender, EventArgs e){
            serialPort1.WriteLine(textBox1.Text);
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e){
            Tampilkan(serialPort1.ReadExisting());
        }

        private delegate void TampilkanDelegate(object item);

        private void Tampilkan(object item){
            if (InvokeRequired){
                listBox1.Invoke(new TampilkanDelegate(Tampilkan), item);
            }
            else{
                listBox1.Items.Add(item);
            }
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e){
            button1.Enabled = true;
        }
    }
}
