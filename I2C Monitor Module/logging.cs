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
					logger.RunWorkerAsync(i); //send slot number in
				}
			}
		}

		private void Logger_DoWork(object sender, DoWorkEventArgs e)
		{
			string slot = e.Argument.ToString(); //because this is fired off 16 times
			Int32.TryParse(slot, out int slot_num);  //convert to int

            if (first_log[slot_num])
                System.Threading.Thread.Sleep(1000); //let data build up then go if its the first time

            Console.WriteLine("Logging for job " + job);
			var data = iface.current_job.board_log[slot_num]; //copy the data locally - this represent each board's data
			var name = iface.current_job.board_names[slot_num];
			name = "_" + name;
			FileInfo file = new FileInfo(iface.current_job.LogFilePath + iface.current_job.LogFileName.Replace(".rlog", name + "_" + ".rlog.csv"));
            string line = string.Empty;

            if (file.Exists && first_log[slot_num]) //if header is done, first log
            {

                string[] lines = File.ReadLines(file.FullName).ToArray();
                File.WriteAllText(file.FullName, string.Empty); //erase the headers
                using (StreamWriter writer = File.AppendText(file.FullName))
                {
                    for (int i = 0; i < iface.current_job.device_adds.Count; i++)
                    {

                        device add = iface.current_job.device_adds[i];
                        line = (lines[i] + Environment.NewLine + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss,")); //add the header line, and date on next line
                        for (int j = 0; j < iface.current_job.Sites; j++)
                        {
                            if (data[j] == null)
                                line += ",";
                            else
                            {
								string temp;
								if (data[j].registers[i] != null)
									temp = data[j].registers[i];
								else if (data[j].tooltips[i] != null)
									temp = data[j].tooltips[i];
								else
								{
									line += ",";
									continue;
								} //if BOTH data sources are not valid, then put blank
                                string line_item = temp.Substring(temp.IndexOf(":") + 1, temp.Length - temp.IndexOf(":") - 1);
                                line += (line_item + ","); //access data of the site, and the addresses in order
                            }
                        } //then add the relevant data
                        writer.WriteLine(line);
                    }
                }
            } //the first time around, we have to insert data in between the header

            else if (file.Exists && !delay) //if we have already geneerated teh file with header, subsequent log // dont go if hold condition
            {
                using (StreamWriter writer = File.AppendText(file.FullName))
                {
                    line += DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss,");
                    for (int i = 0; i < iface.current_job.device_adds.Count; i++)
                    {
                        device add = iface.current_job.device_adds[i];
                        if (add.LogOrder > 0)
                        {
                            for (int j = 0; j < iface.current_job.Sites; j++)
                            {
                                if (data[j] == null) // no data 
                                    line += ",";
                                else
                                {
                                    try
                                    {
                                        string temp = data[j].registers[i];
                                        string line_item = temp.Substring(temp.IndexOf(":") + 1, temp.Length - temp.IndexOf(":") - 1);
                                        if (iface.current_job.board_retries[slot_num][j][i] > 5)
                                            line_item = "0"; //if it has failed, then record 0's
                                        line += (line_item + ","); //access data of the site, and the addresses in order //access data of the site, and the addresses in order
                                    }
                                    catch
                                    { Console.WriteLine("Logging failed. trying again next interval"); }
                                }
                            }
                        } //dont do the ones that only should happen once
                    }
                    writer.WriteLine(line);
                }
            }
            first_log[slot_num] = false; //mark that we have done the first go around
		}
	}

	public class log //the 'data structure' that we update label from and also access individual elements
	{
		public log(int size, int dut)
		{
			registers = new string[size];
			tooltips = new string[size];
			this.dut = dut + 1;
		}

		public string[] registers;
		public string[] tooltips;
		int dut;

		public string Text
		{
			get
			{
				string text = string.Empty;
				text += ("S" + dut.ToString() + Environment.NewLine);
				foreach (string line in registers)
					if(line != null)
						text += (line + Environment.NewLine);
				return text;
			}
		}

		public string Tooltip
		{
			get
			{
				string text = string.Empty;
				foreach (string line in tooltips)
					if (line != null)
						text += (line + Environment.NewLine);
				return text;
			}
		}
	}
}
