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
			while (iface.current_job != null && !iface.current_job.Scanned)
				System.Threading.Thread.Sleep(100); //stay her euntil scanned
			Stopwatch log_timer = new Stopwatch();
			while (iface.current_job != null && iface.current_job.Scanned) //will lock out during initial scan (dont want to populate all DUTs yet) (also watches for clear)
			{            
                if(!loop)
                {
                    System.Threading.Thread.Sleep(50);
                    continue;
                }
				//iface.current_beagle.buffer.Clear();
				try
				{
					for (int i = 0; i < iface.current_job.board_list.Length; i++)
					{
						if (iface.current_job.board_list[i] != null && iface.current_job.board_list[i].Contains(true))
						{
							try
							{
								this.Invoke(new MethodInvoker(delegate ()
								{
									if (delay)
										textBox_data.AppendText("Detected stop condition, entering slow-scan mode" + Environment.NewLine);
									else if (iface.current_job.board_names[i] == "")
										iface.current_job.board_names[i] = "Board " + (i + 1);
									textBox_data.AppendText("Collecting data on " + iface.current_job.board_names[i] + Environment.NewLine);
								}));
							}
							catch { }

							collect_data(i);
							update_labels(i);
						}
					}
				}
				catch (NullReferenceException)
				{
					Console.WriteLine("Caught null from reset in main board loop, skipping");
					continue; //skip, will go into above continue loop
				}
                calculate_values(); //calculate by adding this data set as well
                if (((log_timer.ElapsedMilliseconds / 60000) > iface.current_job.LogInterval) || !log_timer.IsRunning) //ms to min
				{
					log_timer.Restart();
					log_data(); //do this every interval
				}
			}//start the update of all the boards
		}//read loop

        private void Polling_loop_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (!loop) //loop is set by the start button and ended by the stop button
                {
                    System.Threading.Thread.Sleep(50);
                    continue;
                }

                if (!run_lock)
                    iface.current_beagle.snoop_i2c(iface.current_job.current_adds.Length); //this adds data into buffer

                if (iface.current_job.Scanned) //if not yet scanned go max speed, also dont add to textbox in initial scan
                {
                    if(delay) //only if we have detected the condition where it is turned off
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

        private void calculate_values() //takes all boards
        {
            iface.current_job.board_values = new values(iface.current_job.board_log);
            update_values();
        }

        private void collect_data(int i)
        {
            device[] addresses = iface.current_job.device_adds.ToArray(); //put the iface device addresses locally
            int[] read_fails = new int[addresses.Length]; for (int l = 0; l < addresses.Length; l++) read_fails[l] = 0;//set to 0s. this array keeps track of read fails on this board read
            ushort mux = (ushort)(i + 0x50);
            var board_list = iface.current_job.board_list;

            if (board_list[i] != null && board_list[i].Contains(true))
            {
                
                if(iface.current_job.board_log[i] == null)
                    iface.current_job.board_log[i] = new log[iface.current_job.Sites];
                if (iface.current_job.board_retries[i] == null)
                {
                    iface.current_job.board_retries[i] = new int[iface.current_job.Sites][];
                    for (int z = 0; z < iface.current_job.Sites; z++)
                    {
                        iface.current_job.board_retries[i][z] = new int[addresses.Length]; //new int for each address
                        for (int y = 0; y < addresses.Length; y++)
                            iface.current_job.board_retries[i][z][y] = 0;
                    }
                }
                /*
                var old_board_log = iface.current_job.board_log[i]; //save old one

                if (old_board_log == null)
                    old_board_log = new log[iface.current_job.Sites];
                */
                for (int j = 0; j < iface.current_job.Sites; j++)
                {
                    try
                    {
                        if (!iface.current_job.board_list[i][j])
                            continue; //skip the dut if its not in the initial scan
                        if (!loop)
                            break; //exit out if stopped
                        byte unique = (byte)(j);
                        byte[] register_data = new byte[] { unique }; //write the register to pick DUT, and 'unique data'
                        int bytes = iface.current_aardvark.i2c_write(mux, (ushort)register_data.Length, register_data); //set the mux to the DUT

                        //clean above into function?
                        System.Threading.Thread.Sleep(20);
                        iface.current_beagle.buffer.Clear();

                        iface.current_aardvark.i2c_read(mux, (ushort)register_data.Length, out byte[] data);

                        log dut = new log(addresses.Length, monitor_map(j) - 1); //new log entry for this DUT //mon map maps the text to say the right number
                        log old_dut;
                        if (iface.current_job.board_log[i] != null)
                            old_dut = iface.current_job.board_log[i][j]; //save the old one before changing
                        else
                            old_dut = dut; //if not previous data, just set it to the current

                        while (iface.current_job != null && !iface.current_beagle.buffer.Contains(iface.current_job.ReadAddress) && iface.current_beagle.buffer.Count < 10) //if no read is read in
                            System.Threading.Thread.Sleep(5); //give a little time for buffer to fill
                        Stopwatch search_timer = new Stopwatch(); //timer to time out if the address is not found
                        for (int k = 0; k < addresses.Length; k++)
                        {
                            device address = addresses[k];
							if (iface.current_job != null)
								iface.current_job.current_adds = address;
							else //reset was pressed
								break;

                            if (read_fails[k] > 5 || !loop) //skip this address if it fails 5 times - that means its proabbly not in the pattern
                                continue;

							if (!first_log[i] && address.LogOrder < 1) //if recorded non-log values already, skip now
								continue; 

                            if (!search_timer.IsRunning)
                                search_timer.Restart();

                            if (check_buffer(iface.current_beagle.buffer, address)) //should contain the read command and the proper write command
                            {
                                ushort value = 0;
                                string result = "";
                                int start = find_start(iface.current_beagle.buffer, address.Address);
                                int stop = address.Length + start;//last data byte

                                if (start == -1 || start == 0)
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

                                    float check = 0; //check if its actually all ff's
                                    for (int l = start; l < stop; l++)
                                        check += (ushort)(((ushort)(0xff)) << (8 * (stop - l - 1))); //if we only add ff, what is it?

                                    string expression = address.Forumla.Replace("ReadValue", value.ToString()); //use formula to do data
                                    DataTable table = new DataTable();
                                    result = table.Compute(expression, string.Empty).ToString();

                                    if (check == value)
                                        result = "FFFF";
                                }
                                else
                                    for (int l = start; l < stop; l++)//enumerate through data packet
                                    {
                                        var output = (iface.current_beagle.buffer[l] & 0xff).ToString("X");
                                        if (output.Length == 1)
                                            output = "0" + output;
                                        result += output; //dont bit shift and add up a value, take it literally
                                    } //handle a single digit in here

								string text;
                                if (result != "" && result != "FFFF" && iface.current_job.board_retries[i][j][k] < 6) //if normal
                                {
									text = (address.Name + ": " + result);
                                    iface.current_job.board_retries[i][j][k] = 0;
                                }
                                else if (checkBox_debug.Checked) //debug override
                                {
									text = "";
                                }
                                else if (old_dut != null && iface.current_job.board_retries[i][j][k] < 6)
                                {
									text = old_dut.registers[k]; //use the old one if bad data
                                    iface.current_job.board_retries[i][j][k]++;
                                }
                                else  //if there is no previous data 
                                {
									text = "";
                                    iface.current_job.board_retries[i][j][k]++;
                                }

								if (address.LogOrder < 1) //if its a header item, put it into tooltip
								{
									dut.tooltips[k] = text;
								}
								else
									dut.registers[k] = text; //if its a log item, put it into label

								//pick out the data, convert it and put into text dataset
								search_timer.Reset(); //stop counting, no timeout
                                read_fails[k] = 0; //also reset the counter for timeouts
                            }//if we see the address, then read out the data and put it on the label
                            else
                            {
                                if (search_timer.ElapsedMilliseconds > 50) //skip if we cant find the address in teh buffer. timeout
                                {
                                    if (++read_fails[k] > 5)
                                        Console.WriteLine("Removed " + k + " from dataset");
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
                    }
                    catch (System.NullReferenceException)
                    {
						Console.WriteLine("Exiting out of run loop.. caught null");
						break;
					}
                }//enumerate through each site	
				try
				{
					if (check_board_log_empty(iface.current_job.board_log[i], addresses) && !checkBox_debug.Checked) //board fully empty, and not debug mode
					{
						delay = true; //turn on slow mode even if its just 1 board?
						System.Threading.Thread.Sleep(10000);
					}
					else
						delay = false;
				}
				catch
				{ Console.WriteLine("Board empty check failed, skipping"); }
            }//filter out invalid slots

        }

        private bool check_board_log_empty(log[] board_log, device[] addresses)
        {
            for (int i = 0; i < board_log.Length; i++)
            {
                for (int j = 0; j < addresses.Length; j++)
                    if (board_log[i].Text.Contains(addresses[j].Name))
                        return false; //not empty if at least one register name is in one of the duts
            }
            return true; //if we look through all the duts and theyre all blank, then return true
            //works.. but takes like a minute
        }

		private bool check_buffer(List<ushort> list, device address)
		{           
            bool result = false;
            run_lock = true;
            System.Threading.Thread.Sleep(5);
            ushort[] buffer = new ushort[list.Count*3];

            try
            {
                buffer = list.ToArray();
            }
            catch(System.ArgumentException) //for some reason if buffer size is wrong
            {
                return false;
            }

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
        } //looks in the buffer to find where we start the iterator for the specified address

        private void update_labels(int i)
        {
			try
			{
				var board_list = iface.current_job.board_list;
				var board_pages = iface.current_job.board_pages;

				if (board_list[i] != null && board_list[i].Contains(true))
				{
					if (iface.current_job.tab_page_map.Contains(i)) //look at the tab page map to find the right index to modify
					{
						for (int j = 0; j < iface.current_job.Sites; j++)//go through sites                    
							if (board_list[i][j] && board_pages[i] != null) //if the DUT is valid 
							{
								int index = monitor_map(j) - 1;
								var board_log = iface.current_job.board_log[i][j];
								this.Invoke(new MethodInvoker(delegate ()
								{
									board_pages[i].edit_label_text(board_log.Text, index);
									board_pages[i].edit_tooltip_text(board_log.Tooltip, index);
								}));
							}
					}
				}//check if board valid
			}
			catch (System.NullReferenceException)
			{ Console.WriteLine("Null from reset, skipping label update"); }
        }

        private void update_values()
        {
            //update the min/max/etc values
            var values = iface.current_job.board_values;
            try
            {
                this.Invoke(new MethodInvoker(delegate ()
                {
                    label_min.Text = label_min.Text.Substring(0, label_min.Text.IndexOf(":") + 1)
						+ " " + values.Min[(int)numericUpDown_values.Value]; //strips the numbers
                    label_max.Text = label_max.Text.Substring(0, label_max.Text.IndexOf(":") + 1)
                        + " " + values.Max[(int)numericUpDown_values.Value]; //strips the numbers
                    label_avg.Text = label_avg.Text.Substring(0, label_avg.Text.IndexOf(":") + 1)
                        + " " + values.Avg[(int)numericUpDown_values.Value].ToString("n2"); //strips the numbers
                    label_range.Text = label_range.Text.Substring(0, label_range.Text.IndexOf(":") + 1)
                        + " " + values.Range[(int)numericUpDown_values.Value]; //strips the numbers
                }));
            }
            catch(System.NullReferenceException)
            {
                MessageBox.Show("No boards found!");
                loop = false;
                select = false;
            }
        }
	}

    public class values
    {
        public values(log[][] input)
        {
            for (int i = 0; i < input.Length; i++) //look for valid board
                if (input[i] != null)
                {
                    int length = input[i][0].registers.Length;
                    
                    assign_depth(length); //based on how manly registers we have
                    calculate(input[i]);
                    break;
                }
        } //constr

        float[] min, max, avg, range, num;
        int[] count;

        public float[] Min { get { return min; } }
        public float[] Max { get { return max; } }
        public float[] Avg { get { return avg; } }
        public float[] Range { get { return range; } }

        private void calculate(log[] board)
        {
            if (board != null)
            {
                calculate_min(board);
                calculate_max(board);
                calculate_avg(board);
                calculate_range(board);
            }
        }

        private void assign_depth(int length)
        {
            min = new float[length];
            max = new float[length];
            avg = new float[length];
            range = new float[length];
            num = new float[length];
            count = new int[length];
            min = Enumerable.Repeat<float>(float.MaxValue, length).ToArray();
            max = Enumerable.Repeat<float>(float.MinValue, length).ToArray();
            count = Enumerable.Repeat<int>(0, length).ToArray();
        }

        private void calculate_min(log[] data)
        {
            foreach(log dut in data)
            {
                if (dut != null)
                {
                    for (int i = 0; i < dut.registers.Length; i++)
                    {
                        if (dut.registers[i] != null)
                        {
                            string temp = dut.registers[i];
                            string substring = temp.Substring(temp.IndexOf(":") + 1, temp.Length - temp.IndexOf(":") - 1);
                            if (float.TryParse(substring, out float value))
                                if (value < min[i] && value != 0)
                                    min[i] = value;
                        }
                    }
                }
            }
        }

        private void calculate_max(log[] data)
        {
            foreach (log dut in data)
            {
                if (dut != null)
                {
                    for (int i = 0; i < dut.registers.Length; i++)
                    {
                        if (dut.registers[i] != null)
                        {
                            string temp = dut.registers[i];
                            string substring = temp.Substring(temp.IndexOf(":") + 1, temp.Length - temp.IndexOf(":") - 1);
                            if (float.TryParse(substring, out float value))
                                if (value > max[i])
                                    max[i] = value;
                        }
                    }
                }
            }
        }

        private void calculate_avg(log[] data)
        {
            foreach (log dut in data)
            {
                if (dut != null)
                {
                    for (int i = 0; i < dut.registers.Length; i++)
                    {
                        if (dut.registers[i] != null)
                        {                       
                            string temp = dut.registers[i];
                            string substring = temp.Substring(temp.IndexOf(":") + 1, temp.Length - temp.IndexOf(":") - 1);
                            if (float.TryParse(substring, out float value))
                            {
                                num[i] += value;
                                avg[i] = num[i] / ++count[i];
                            }
                        }
                    }
                }
            }
        }

        private void calculate_range(log[] data)
        {
            foreach (log dut in data)
                if (dut != null)
                    for (int i = 0; i < dut.registers.Length; i++)
                        range[i] = max[i] - min[i];
        }
    }
}