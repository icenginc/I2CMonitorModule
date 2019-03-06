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
		TotalPhase iface = new TotalPhase(); //one totalphase, redefine the current devices as the selection changes

		public Form1()
		{
			InitializeComponent();

			button_scan_Click(this, new EventArgs());
			

			

			//select devices to use
			//maybe some config file to define the devices? so we can make sure it is the right pair

			//read test
			
		}

		private void button_scan_Click(object sender, EventArgs e)
		{
			iface.find_device(); //button to do this, then display devices

			var aardvark_ids = iface.aardvarks.Select(a => a.return_id().ToString()).ToArray();
			var beagle_ids = iface.beagles.Select(a => a.return_id().ToString()).ToArray();

			comboBox_aardvark.Items.AddRange(aardvark_ids);
			comboBox_beagle.Items.AddRange(beagle_ids);

			listBox_active.Items.AddRange(aardvark_ids);
			listBox_active.Items.AddRange(beagle_ids);
			listBox_active.SelectionMode = SelectionMode.MultiExtended;
		}

		private void button_select_Click(object sender, EventArgs e)
		{
			foreach (Aardvark a in iface.aardvarks)
			{
				if (a.return_id().ToString() == (string)comboBox_aardvark.SelectedItem)
					iface.current_aardvark = a;
			}

			foreach (Beagle b in iface.beagles)
			{
				if (b.return_id().ToString() == (string)comboBox_beagle.SelectedItem)
					iface.current_beagle = b;
			} //set the beagle and aardvark if it matches the combobox

			for(int i = 0; i < listBox_active.Items.Count; i++)
			{
				string l = listBox_active.Items[i].ToString();
				if (l == iface.current_aardvark.return_id().ToString())
					listBox_active.SelectedItem = l;
				if (l == iface.current_beagle.return_id().ToString())
					listBox_active.SelectedItem = l;
			} //highlight the text in the listbox


		}

		private void button1_Click(object sender, EventArgs e)
		{
			iface.current_aardvark.i2c_read();
		}
	}

	

}
