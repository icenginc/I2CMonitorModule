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
		bool loop = true;

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

			for (int i = 0; i < listBox_active.Items.Count; i++)
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
			if (iface.current_aardvark.return_id() != 0 && iface.current_aardvark.return_port() != 999) //making sure we have set the current aardvark first
			{
				ushort bytes_in = 6;
				ushort bytes_out = 2;
				byte[] data_in = new byte[bytes_in];
				byte[] data_out = new byte[bytes_out];
				data_out = new byte[] { 0x2C, 0x06 }; //measure command to SHT module

				Console.WriteLine("Write:" + iface.current_aardvark.i2c_write(bytes_out, data_out));
				System.Threading.Thread.Sleep(500);
				Console.WriteLine("Read:" + iface.current_aardvark.i2c_read(bytes_in, out data_in)); //pass data_in by reference

				foreach (byte data in data_in)
					Console.Write(data + " ");
				Console.WriteLine();

				var cTemp = ((((data_in[0] * 256.0) + data_in[1]) * 175) / 65535.0) - 45;
				var humidity = ((((data_in[3] * 256.0) + data_in[4]) * 100) / 65535.0);
				Console.WriteLine("Temp: " + cTemp + ", Humidity: " + humidity);
			}

			else
				MessageBox.Show("Aardvark device not selected");
		} //for testing with sht31d module

		private void button_i2cmonitor_Click(object sender, EventArgs e)
		{
			run_here(); //do everything in separate funciton
		}

		private void button_reset_Click(object sender, EventArgs e)
		{
			iface.current_beagle.reset_beagle();
		}

		private void button_stop_Click(object sender, EventArgs e)
		{
			loop = false;
		}
	}

	

}
