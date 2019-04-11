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
	public partial class InSituMonitoringModule : Form
	{
		

		private void run_here()
		{
			BackgroundWorker polling_loop = new BackgroundWorker(); //this one to poll for data from beagle buffer
			polling_loop.DoWork += Polling_loop_DoWork;
			polling_loop.RunWorkerAsync();

			BackgroundWorker read_loop = new BackgroundWorker(); //this one to analyze polled data and fill GUI
			read_loop.DoWork += Read_loop_DoWork;
			read_loop.RunWorkerAsync();
		}

		private void Read_loop_DoWork(object sender, DoWorkEventArgs e)
		{
			while (iface.current_job.Scanned && loop) //will lock out during initial scan (dont want to populate all DUTs yet)
			{
				var data = iface.current_beagle.Data;

				//analyze data and fill
				foreach(bool[] dut in iface.current_job.board_list)
				{
                    if(dut.Contains(true))//if there is at least one valid dut
                    {
                        //this is where we poll data - after scanning
                    }
                }//go through each board
				System.Threading.Thread.Sleep(500);
			}
		}

		private void Polling_loop_DoWork(object sender, DoWorkEventArgs e)
		{
			while (loop) //loop is set by the start button and ended by the stop button
			{
				var data = iface.current_beagle.snoop_i2c(iface.current_job.device_adds[0].Length); //this adds data into buffer

                if (iface.current_job.Scanned) //if not yet scanned go max speed, also dont add to textbox in initial scan
                {
                    foreach (string line in data)
                    {
                        try
                        {
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                textBox_data.AppendText(line + Environment.NewLine);
                            }));
                        }
                        catch { }//if invoke fails
                    }

                    System.Threading.Thread.Sleep(100); //delay for slowing down buffer reads
                }
				if (iface.current_beagle.Buffer_Free == 0)
				{
					iface.current_beagle.reset_beagle();
					System.Threading.Thread.Sleep(500);
				}
			}
		}
	}
}