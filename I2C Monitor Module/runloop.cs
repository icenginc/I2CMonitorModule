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
            while (!iface.current_job.Scanned)
                System.Threading.Thread.Sleep(100);
			while (iface.current_job.Scanned && loop) //will lock out during initial scan (dont want to populate all DUTs yet)
			{
                iface.current_beagle.buffer.Clear();
                device[] addresses = iface.current_job.device_adds.ToArray(); //put the iface device addresses locally
                for (int i = 0; i < iface.current_job.board_list.Length; i++)
                {
                    ushort mux = (ushort)(i + 0x50);
                    var board_list = iface.current_job.board_list;
                    if (board_list[i] != null && board_list[i].Contains(true))
                    {
                        var tab_page = tabControl_boards.TabPages[i];
                        for (int j = 0; j < iface.current_job.Sites; j++)
                        {                    
                            byte unique = (byte)(iface.current_job.Sites - i);
                            byte[] register_data = new byte[] { (byte)i, unique }; //write the register to pick DUT, and 'unique data'
                            int bytes = iface.current_aardvark.i2c_write(mux, (ushort)register_data.Length, register_data); //set the mux to the DUT
                            
                            //clean above into function?

                            iface.current_beagle.buffer.Clear();
                            var label = tab_page.Controls[j]; //this is the label to update
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                label.Text = "DUT " + (j + 1);
                            }));
                            while (iface.current_beagle.buffer.Count == 0)
                                System.Threading.Thread.Sleep(10); //give a little time for buffer to fill
                            foreach (device address in addresses)
                            {
                                if (iface.current_beagle.buffer.Contains(address.Address))
                                {
                                    ushort value = 0;
                                    int start = iface.current_beagle.buffer.IndexOf(address.Address) + 1; //byte after address
                                    int stop = address.Length + start;//last data byte

                                    for(int k = start; k < stop; k++)//enumerate through data packet
                                        if (iface.current_beagle.buffer.Count > k)                                    
                                            value += (ushort)(((ushort)(iface.current_beagle.buffer[k] & 0xff))<<(8*(stop-k-1)));
                                                                       
                                    string expression = address.Forumla.Replace("ReadValue", value.ToString());
                                    DataTable table = new DataTable();
                                    var result = table.Compute(expression, string.Empty);
                                    this.Invoke(new MethodInvoker(delegate ()
                                    {
                                        label.Text += (Environment.NewLine + address.Name + ": " + result.ToString());
                                    }));
                                    //pick out the data, convert it and put into label
                                }//if we see the address, then read out the data and put it on the label
                            }//enumerate through each address
                        }//enumerate through each label
                    }//filter out invalid slots
                }//enumerate through all possible slots
            }//start the update of all the boards
		}//read loop

		private void Polling_loop_DoWork(object sender, DoWorkEventArgs e)
		{
			while (loop) //loop is set by the start button and ended by the stop button
			{
				var data = iface.current_beagle.snoop_i2c(iface.current_job.device_adds[0].Length); //this adds data into buffer

                if (iface.current_job.Scanned) //if not yet scanned go max speed, also dont add to textbox in initial scan
                {
					/*
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
					*/ //this adds every scan into the textbox
                    System.Threading.Thread.Sleep(10); //delay for slowing down buffer reads
					
					/*
                    if (iface.current_beagle.Buffer_Free == 0)
                    {
                    //    iface.current_beagle.reset_beagle();
                        System.Threading.Thread.Sleep(500);
                    }
                    */ //dont need to slow down for this anymore
                }
			}
		}
	}
}