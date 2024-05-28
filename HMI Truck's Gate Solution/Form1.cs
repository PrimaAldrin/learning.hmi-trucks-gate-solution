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
        }
    }
}
