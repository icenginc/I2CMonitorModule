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

				job new_job = new job(file);

			}//parse in here
			catch
			{
				return false;
			}
			return true;
		}//parse the ini file here
	}
}