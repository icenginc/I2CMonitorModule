using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace I2C_Monitor_Module
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();

			TotalPhase iface = new TotalPhase(); //each totalphase is a new pair, create more if more devices
			iface.find_device(); //button to do this, then display devices

			//select devices to use
			//maybe some config file to define the devices? so we can make sure it is the right pair

			//read test
			
		}
	}

	

}
