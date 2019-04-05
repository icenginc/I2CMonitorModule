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
				List<string> file = new List<string>();
				string line;
				var reader = config.OpenText();
				while ((line = reader.ReadLine()) != null)
				{
					file.Add(line);
				}

				iface.current_job = new job(file);

			}//parse in here
			catch
			{
				return false;
			}
			return true;
		}//parse the ini file here

		private bool resolve_boards()
		{
			if (!select)
			{
				MessageBox.Show("Cannot resolve boards before loading config file!");
				return false;
			}
			//*todo
			//set mux to 0x50-0x5F
			//write to the first register found in teh INI file
			//read it back, if its valid then add to list
			//*
			for (ushort mux_address = 0x50; mux_address < 0x5F; mux_address++)
			{
				bool valid = scan_board(mux_address);
				if (valid)
				{
					tabControl_boards.Enabled = true;
					tabControl_boards.TabPages.Add("Board " + (mux_address - 0x4F));
				}
			}//each iteration is one mux address

			foreach (TabPage page in tabControl_boards.TabPages)
			{
				if (iface.current_job != null)
				{
					for (int i = 0; i < iface.current_job.Biby; i++) //column
					{
						for (int j = 0; j < iface.current_job.Bibx; i++) //row
						{
							//build the DUT thing in here
						}
					}
				} //if the job is loaded, fill the GUI with board dimensions
				else
					MessageBox.Show("Job not loaded in config! Can't draw boards");
			}

			return true; //on success
		}

		private bool scan_board(ushort mux)
		{
			//fake datat for now
			int fake = mux - 0x50;
			if (fake == 0 || fake == 3 || fake == 4)
				return true;
			else
				return false;
		}
	}
}