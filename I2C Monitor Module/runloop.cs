using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace I2C_Monitor_Module
{
	public partial class Form1 : Form
	{
		private void load_config()
		{
			try
			{
				DirectoryInfo config_directory = new DirectoryInfo(config_path);

				var files = config_directory.GetFiles().ToList();

				foreach (FileInfo file in files)
				{
					if (file.Name.EndsWith(".ini"))
						comboBox_config.Items.Add(file);
				}
			}
			catch
			{
				Console.WriteLine("Close the document first!");
			}
		}//this happens upon loading the program

		private bool parse_config(FileInfo config)
		{
			try
			{

			}//parse in here
			catch
			{
				return false;
			}
			return true;
		}//parse the ini file here

		private void run_here()
		{
			BackgroundWorker polling_loop = new BackgroundWorker();
			polling_loop.DoWork += Polling_loop_DoWork;
			polling_loop.RunWorkerAsync();
		}

		private void Polling_loop_DoWork(object sender, DoWorkEventArgs e)
		{
			while (loop)
			{
				var data = iface.current_beagle.snoop_i2c(2);
				foreach (string line in data)
					textBox_data.Text += (line + Environment.NewLine);
				textBox_data.AppendText("\n\n");
				textBox_data.Select(textBox_data.Text.Length, 0);
				//var data = iface.current_beagle.return_data();
				System.Threading.Thread.Sleep(333);
				if (iface.current_beagle.return_buffer() == 0)
					iface.current_beagle.reset_beagle();
			}
		}
	}
}