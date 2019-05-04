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
				System.Threading.Thread.Sleep(100); //stay her euntil scanned
			Stopwatch log_timer = new Stopwatch();
			while (iface.current_job.Scanned && loop) //will lock out during initial scan (dont want to populate all DUTs yet)
			{
                //iface.current_beagle.buffer.Clear();
                for (int i = 0; i < iface.current_job.board_list.Length; i++)
                {
                    if (iface.current_job.board_list[i] != null && iface.current_job.board_list[i].Contains(true))
                    {
                        try
                        {
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                textBox_data.AppendText("Collecting data on " + tabControl_boards.TabPages[i].Text + Environment.NewLine);
                            }));
                        }
                        catch { }

                        collect_data(i);

                        update_labels(i);
                    }
                }
				if (log_timer.ElapsedMilliseconds / 60000 > iface.current_job.LogInterval || !log_timer.IsRunning) //ms to min
				{
					log_timer.Restart();
					log_data(); //do this every interval
				}
			}//start the update of all the boards
		}//read loop

		private void Polling_loop_DoWork(object sender, DoWorkEventArgs e)
		{
			while (loop) //loop is set by the start button and ended by the stop button
			{
				var data = iface.current_beagle.snoop_i2c(iface.current_job.current_adds.Length); //this adds data into buffer

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

        private void collect_data(int i)
        {
            device[] addresses = iface.current_job.device_adds.ToArray(); //put the iface device addresses locally
            int[] read_fails = new int[addresses.Length]; for (int l = 0; l < addresses.Length; l++) read_fails[l] = 0;//set to 0s. this array keeps track of read fails on this board read
            ushort mux = (ushort)(i + 0x50);
            var board_list = iface.current_job.board_list;

            if (board_list[i] != null && board_list[i].Contains(true))
            {
                for (int j = 0; j < iface.current_job.Sites; j++)
                {
                    byte unique = (byte)(iface.current_job.Sites - i);
                    byte[] register_data = new byte[] { (byte)i, unique }; //write the register to pick DUT, and 'unique data'
                    int bytes = iface.current_aardvark.i2c_write(mux, (ushort)register_data.Length, register_data); //set the mux to the DUT

                    //clean above into function?
                    iface.current_beagle.buffer.Clear();

                    log dut = new log(addresses.Length, j); //new log entry for this DUT
                    while (!iface.current_beagle.buffer.Contains(iface.current_job.ReadAddress) && iface.current_beagle.buffer.Count < 10) //if no read is read in
                        System.Threading.Thread.Sleep(5); //give a little time for buffer to fill
                    Stopwatch search_timer = new Stopwatch(); //timer to time out if the address is not found
                    for (int k = 0; k < addresses.Length; k++)
                    {
                        device address = addresses[k];
                        iface.current_job.current_adds = address;

                        if (read_fails[k] > 5) //skip this address if it fails 5 times - that means its proabbly not in the pattern
                            continue;

                        if (!search_timer.IsRunning)
                            search_timer.Restart();
                        

                        if (k == 1)
                            ;

                        if (check_buffer(iface.current_beagle.buffer, address)) //should contain the read command and the proper write command
                        { 
                            ushort value = 0;
                            //int start = iface.current_beagle.buffer.IndexOf(iface.current_job.ReadAddress) + 1; //byte after address
                            int start = iface.current_beagle.buffer.IndexOf(address.Address[0]) + 3; //skip the write commmand and start after read bit
                            int stop = address.Length + start;//last data byte

                            for (int l = start; l < stop; l++)//enumerate through data packet
                                if (iface.current_beagle.buffer.Count > l)
                                    value += (ushort)(((ushort)(iface.current_beagle.buffer[l] & 0xff)) << (8 * (stop - l - 1)));

                            string expression = address.Forumla.Replace("ReadValue", value.ToString()); //use formula to do data
                            DataTable table = new DataTable();
                            var result = table.Compute(expression, string.Empty);

                            dut.registers[k] = (Environment.NewLine + address.Name + ": " + result.ToString());
                            //pick out the data, convert it and put into text dataset
                            search_timer.Reset(); //stop counting, no timeout
                            read_fails[k] = 0; //also reset the counter for timeouts
                        }//if we see the address, then read out the data and put it on the label
                        else
                        {
                            if (search_timer.ElapsedMilliseconds > 1000) //skip if we cant find the address in teh buffer. timeout
                            {
                                read_fails[k]++;
                            } //if we timeout, then go to the next one but keep track of the fail
                            else
                            {
                                System.Threading.Thread.Sleep(5); //give 5 more ms to the buffer
                                k--; //loop again
                            }//if within timeout time, try again
                        }

                    }//enumerate through each address
                    iface.current_job.board_log[i][j] = dut;
                }//enumerate through each site			
            }//filter out invalid slots

        }

		private bool check_buffer(List<ushort> list, device address)
		{
			if (iface.current_job.Extended)
				return (iface.current_beagle.buffer.Contains(address.Address[0])) && iface.current_beagle.buffer.Contains(address.Address[1])
					&& iface.current_beagle.buffer.Contains(iface.current_job.ReadAddress);
			else
				return (iface.current_beagle.buffer.Contains(address.Address[1]) && iface.current_beagle.buffer.Contains(iface.current_job.ReadAddress)); //
		}

        private void update_labels(int i)
        {
            var board_list = iface.current_job.board_list;
            if (board_list[i] != null && board_list[i].Contains(true))
            {
                var tab_page = tabControl_boards.TabPages[i];
                for (int j = 0; j < iface.current_job.Sites; j++)//go through sites
                {
                    var label = tab_page.Controls[j]; //this is the label to update
                    if (board_list[i][j]) //if the DUT is valid
                    {
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            label.Text = iface.current_job.board_log[i][j].Text; //update label
                            }));
                    }//check if dut va lid
                }//iterate through duts
            }//check if board valid
        }
	}
}