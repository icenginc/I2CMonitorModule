using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace I2C_Monitor_Module
{
	public partial class InSituMonitoringModule : Form
	{

		private void log_data()
		{
			for (int i = 0; i < 16; i++)
			{
				if (iface.current_job.board_list[i] != null && iface.current_job.board_list[i].Contains(true))
				{
					BackgroundWorker logger = new BackgroundWorker();
					logger.DoWork += Logger_DoWork;
					logger.RunWorkerAsync(i + 1); //send slot number in
				}
			}
		}

		private void Logger_DoWork(object sender, DoWorkEventArgs e)
		{
			string slot = e.Argument.ToString(); //because this is fired off 16 times
			Int32.TryParse(slot, out int slot_num);  //convert to int

			Console.WriteLine("Logging for job " + job);
			var data = iface.current_job.board_log[slot_num - 1]; //copy the data locally - this represent each board's data

			FileInfo file = new FileInfo(iface.current_job.LogFilePath + iface.current_job.LogFileName.Replace(".rlog", "_" + slot + ".rlog"));
			if (file.Exists) //if we have already geneerated teh file with header
			{
				using (StreamWriter writer = File.AppendText(file.FullName))
				{
					string line = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss,");
					for (int i = 0; i < iface.current_job.device_adds.Count; i++)
					{
						device add = iface.current_job.device_adds[i];
						for (int j = 0; j < iface.current_job.Sites; j++)
						{
							line += (new string(data[j].registers[i].Where(ch => (!char.IsLetter(ch))).ToArray()) + ","); //access data of the site, and the addresses in order
						}
					}
					writer.Write(line);
				}
			}
		}
	}

	class log //the 'data structure' that we update label from and also access individual elements
	{
		public log(int size, int dut)
		{
			registers = new string[size];
			this.dut = dut + 1;
		}

		public string[] registers;
		int dut;

		public string Text
		{
			get
			{
				string text = string.Empty;
				text += ("DUT " + dut.ToString());
				foreach (string line in registers)
					text += (line + Environment.NewLine);
				return text;
			}
		}
	}
}
