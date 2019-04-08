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

			Console.WriteLine("Buildt pages - populating now..");
			/*
			BackgroundWorker tab_populate = new BackgroundWorker();
			tab_populate.DoWork += Tab_populate_DoWork;
			tab_populate.RunWorkerAsync();
			*/
			foreach (TabPage page in tabControl_boards.TabPages)
			{
				if (iface.current_job != null)
				{
					var x = tabControl_boards.Size.Width - 2;
					var y = tabControl_boards.Size.Height - 2;
					var width = x / iface.current_job.Bibx;
					var height = y / iface.current_job.Biby;

					for (int i = 0; i < iface.current_job.Biby; i++) //column
					{
						for (int j = 0; j < iface.current_job.Bibx; j++) //row
						{
							//build the DUT thing in here
							Button button = new Button();
							button.Text = "DUT " + iface.current_job.Monitor_Map[((i * iface.current_job.Bibx) + j)]; //use monnitor map from file to name
							button.Location = new Point(j * width, i * height); //calculated height based on loop position
							button.Size = new Size(width - 3, height - 3);
							button.Show();
							page.Controls.Add(button); //add a button
						}
					}
				} //if the job is loaded, fill the GUI with board dimensions
				else
					MessageBox.Show("Job not loaded in config! Can't draw boards");
			}

			return true; //on success
		}

		private void Tab_populate_DoWork(object sender, DoWorkEventArgs e)
		{
		} //moved here temporarily, moved back

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