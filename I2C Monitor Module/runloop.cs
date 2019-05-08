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
                                if (iface.current_job.board_names[i] == "")
                                    iface.current_job.board_names[i] = "Board " + (i + 1);
                                textBox_data.AppendText("Collecting data on " + iface.current_job.board_names[i] + Environment.NewLine);
                            }));
                        }
                        catch { }

                        collect_data(i);

                        update_labels(i);
                    }
                }
                if (log_timer.ElapsedMilliseconds / 60000 > iface.current_job.LogInterval || !log_timer.IsRunning || log_timer.ElapsedMilliseconds > 10000) //ms to min
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
                if(!run_lock)
				    iface.current_beagle.snoop_i2c(iface.current_job.current_adds.Length); //this adds data into buffer

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
					//System.Threading.Thread.Sleep(1); //delay for slowing down buffer reads

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
                var old_board_log = iface.current_job.board_log[i]; //save old one
                iface.current_job.board_log[i] = new log[iface.current_job.Sites];

                if (old_board_log == null)
                    old_board_log = new log[iface.current_job.Sites];

                for (int j = 0; j < iface.current_job.Sites; j++)
                {
                    if (!iface.current_job.board_list[i][j])
                        continue; //skip the dut if its not in the initial scan
                    byte unique = (byte)(j);
                    byte[] register_data = new byte[] { unique }; //write the register to pick DUT, and 'unique data'
                    int bytes = iface.current_aardvark.i2c_write(mux, (ushort)register_data.Length, register_data); //set the mux to the DUT

                    //clean above into function?
                    System.Threading.Thread.Sleep(20);
                    iface.current_beagle.buffer.Clear();

                    iface.current_aardvark.i2c_read(mux, (ushort)register_data.Length, out byte[] data);

                    log dut = new log(addresses.Length, monitor_map(j)-1); //new log entry for this DUT //mon map maps the text to say the right number
                    log old_dut = old_board_log[j]; //save the old one before changing

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
                    
                        if (check_buffer(iface.current_beagle.buffer, address)) //should contain the read command and the proper write command
                        { 
                            ushort value = 0;
                            string result = "";
                            //int start = iface.current_beagle.buffer.IndexOf(iface.current_job.ReadAddress) + 1; //byte after address
                            //int start = iface.current_beagle.buffer.FindIndex(a => ((a & 0xff) == address.Address[0])); //skip the write commmand and start after read bit
                            //start += (1 + (1+Convert.ToInt16(iface.current_job.Extended))); //skip the query command (+1), and go to the start of data bits (+1 for add, +1 if extended add)
                            int start = find_start(iface.current_beagle.buffer, address.Address);
                            int stop = address.Length + start;//last data byte

                            if(start == -1 || start == 0)
                            {
                                k--;
                                System.Threading.Thread.Sleep(5);
                                continue; 
                            } //if the start sequence is at the end (incomplete trasnaction) wait for a bit then go again

                            if (address.Forumla != "(ReadValue)" && address.Forumla != "ReadValue")
                            {
                                for (int l = start; l < stop; l++)//enumerate through data packet
                                    if (iface.current_beagle.buffer.Count > l)
                                        value += (ushort)(((ushort)(iface.current_beagle.buffer[l] & 0xff)) << (8 * (stop - l - 1)));

                                string expression = address.Forumla.Replace("ReadValue", value.ToString()); //use formula to do data
                                DataTable table = new DataTable();
                                result = table.Compute(expression, string.Empty).ToString();
                            }
                            else
                                for (int l = start; l < stop; l++)//enumerate through data packet
                                {
                                    var output = (iface.current_beagle.buffer[l] & 0xff).ToString("X");
                                    if (output.Length == 1)
                                        output = "0" + output;
                                    result += output; //dont bit shift and add up a value, take it literally
                                } //handle a single digit in here

                            

                            if (result != "" && result != "FFFF")
                                dut.registers[k] = (address.Name + ": " + result);
                            else if (old_dut != null)
                                dut.registers[k] = old_dut.registers[k]; //use the old one if bad data
                            else
                                dut.registers[k] = "";
                            //pick out the data, convert it and put into text dataset
                            search_timer.Reset(); //stop counting, no timeout
                            read_fails[k] = 0; //also reset the counter for timeouts
                        }//if we see the address, then read out the data and put it on the label
                        else
                        {
                            if (search_timer.ElapsedMilliseconds > 250) //skip if we cant find the address in teh buffer. timeout
                            {
                                read_fails[k]++;
                                Console.WriteLine("Timeout");
                                dut.registers[k] = "";
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
            /*
			if (iface.current_job.Extended)
				return (iface.current_beagle.buffer.Contains(address.Address[0])) && iface.current_beagle.buffer.Contains(address.Address[1])
					&& iface.current_beagle.buffer.Contains(iface.current_job.ReadAddress);
			else
				return (iface.current_beagle.buffer.Contains(address.Address[1]) && iface.current_beagle.buffer.Contains(iface.current_job.ReadAddress)); //
             //old way without bit mask

            */
            bool result = false;
            run_lock = true;
            System.Threading.Thread.Sleep(7);

            ushort[] buffer = new ushort[list.Count];
            buffer = list.ToArray();

            if (iface.current_job.Extended)
                result = (buffer.Any(sh => ((sh & 0xff) == address.Address[0])) && buffer.Any(sh => ((sh & 0xff) == address.Address[1]))
                    && buffer.Any(sh => ((sh & 0xff) == iface.current_job.ReadAddress)));
            else
                result = (buffer.Any(sh => ((sh & 0xff) == address.Address[0])) && buffer.Any(sh => ((sh & 0xff) == iface.current_job.ReadAddress)));

            run_lock = false;
            return result;
                
        }

        private int find_start(List<ushort> buffer, ushort[] registers)
        {
            if (registers.Length == 1) //not extended address
            {
                int start = buffer.FindIndex(a => ((a & 0xff) == registers[0])); //skip the write commmand and start after read bit
                start += (1 + (1 + Convert.ToInt16(iface.current_job.Extended))); //skip the query command (+1), and go to the start of data bits (+1 for add, +1 if extended add)
                return start;
            }
            else if (registers.Length == 2) //extended
            {
                for (int i = 0; i < buffer.Count - 1; i++)
                {
                    if ((buffer[i] & 0xff) == registers[0] && (buffer[i + 1] & 0xff) == registers[1])
                    {
                        int start = i;
                        start += (1 + (1 + Convert.ToInt16(iface.current_job.Extended)));
                        if (start > (buffer.Count - 2))
                            return -1;
                        return start;
                    }               
                }
            }
            else
            {
                MessageBox.Show("Invalid register length in config file.. data collection won't work");
                return -1; //address doesn't match.. this case will only happen if the config is wrong
            }

            return 0; //not found, try again
        }

        private void update_labels(int i)
        {
            var board_list = iface.current_job.board_list;

            if (board_list[i] != null && board_list[i].Contains(true))
            {
                int index;
                if (iface.current_job.tab_page_map.Contains(i)) //look at the tab page map to find the right index to modify
                {
                    index = iface.current_job.tab_page_map.IndexOf(i);

                    var tab_page = tabControl_boards.TabPages[index];
                    for (int j = 0; j < iface.current_job.Sites; j++)//go through sites
                    {
                        var label = tab_page.Controls[monitor_map(j)-1]; //this is the label to update
                        if (board_list[i][j]) //if the DUT is valid
                        {
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                label.Text = iface.current_job.board_log[i][j].Text; //update label
                            }));
                        }//check if dut va lid
                    }//iterate through duts
                }
            }//check if board valid
        }
	}
}