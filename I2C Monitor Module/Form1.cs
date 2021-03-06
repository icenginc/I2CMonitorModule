﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Forms;
using System.IO;

//Install-Package MSBuild.ILMerge.Task <- use this package in nuget

namespace I2C_Monitor_Module
{
    public partial class InSituMonitoringModule : Form
    {
        Form2 selection; //save this
        public static TotalPhase iface = new TotalPhase(); //one totalphase, redefine the current devices as the selection changes
		public static string config_path = "\\\\BURN-IN_SERVER\\Inspire\\InSitu\\";
		FileInfo config_file;

        public string system { get; set; }
        public string lot { get; set; }
        public string job { get; set; }
		int height = 0;

        bool loop = false; //to stop or esume the measuremetns
        bool delay = false; //if the vector stops, this makes us go into 'slow' mode
        public bool select = false; //this determines if everything was loaded correctly and can continue to operation
        bool run_lock = false;
        bool[] first_log = Enumerable.Repeat<bool>(true, 16).ToArray();

        /// <summary>
        /// these locks are to direct the user to the correct order of steps
        /// </summary>
        public InSituMonitoringModule()
        {
            InitializeComponent();
			set_text();
			load_config();
            tooltips();
            button_scan_Click(this, new EventArgs());
        }

        private void button_scan_Click(object sender, EventArgs e)
        {
			textBox_data.AppendText("SCAN Operation Initiated\n");

			if (select)
                MessageBox.Show("Beagle/Aardvark/Config already selected");
            else
            {
                iface.find_device(); //button to do this, then display devices

                var aardvark_ids = iface.aardvarks.Select(a => a.ID.ToString()).ToArray();
                var beagle_ids = iface.beagles.Select(a => a.ID.ToString()).ToArray();

                comboBox_aardvark.Items.Clear();
                comboBox_beagle.Items.Clear();
                listBox_active.Items.Clear();
				comboBox_config.Items.Clear();

				comboBox_aardvark.ResetText();
				comboBox_beagle.ResetText();
				comboBox_config.ResetText();

                comboBox_aardvark.Items.AddRange(aardvark_ids);
                comboBox_beagle.Items.AddRange(beagle_ids);

                listBox_active.Items.AddRange(aardvark_ids);
                listBox_active.Items.AddRange(beagle_ids);
                listBox_active.SelectionMode = SelectionMode.MultiExtended;

				load_config();
            }
        }

        private void button_select_Click(object sender, EventArgs e)  //start
        {
			textBox_data.AppendText("SELECT Operation Initiated\n");

			select = true;

            this.job = textBox_job.Text;
            this.lot = textBox_lot.Text;
            this.system = textBox_system.Text;

            if (job == "" && lot == "" && system == "" && !checkBox_debug.Checked)
                MessageBox.Show("Job/Lot number and system name not entered!");

            foreach (Aardvark a in iface.aardvarks)
            {
                if (a.ID.ToString() == (string)comboBox_aardvark.SelectedItem)
                {
                    iface.current_aardvark = a;
                    iface.current_aardvark.setup_aardvark();
                }
                else
                    select = false;
            }

            foreach (Beagle b in iface.beagles)
            {
                if (b.ID.ToString() == (string)comboBox_beagle.SelectedItem)
                {
                    iface.current_beagle = b;
                    iface.current_beagle.setup_beagle();
                }
                else
                    select = false;
            } //set the beagle and aardvark if it matches the combobox

            for (int i = 0; i < listBox_active.Items.Count; i++)
            {
                string l = listBox_active.Items[i].ToString();
                if (l == iface.current_aardvark.ID.ToString())
                    listBox_active.SelectedItem = l;
                if (l == iface.current_beagle.ID.ToString())
                    listBox_active.SelectedItem = l;
            } //highlight the text in the listbox

            if (parse_config(config_file)) //wont parse if we don't have beagle/aardvark set up
            {
                if (iface.current_beagle.ID == 0)
                {
                    MessageBox.Show("Error: No Beagle found");
                    select = false;
                }
                if (iface.current_aardvark.ID == 0)
                {
                    MessageBox.Show("Error: No Aardvark found");
                    select = false;
                }
                numericUpDown_values.Enabled = true;
                numericUpDown_values.Maximum = iface.current_job.device_adds.Count - 1;
				numericUpDown_values.Value = numericUpDown_values.Maximum;
			}//if the parse is fine then continue, also check for current beagle and aardvark set
			else
                select = false;

            if (resolve_boards() && select) //select bool means that if the config didn't parse, cant attemp to continue
            {
                selection = new Form2(iface.current_job.board_list, iface.current_job.device_adds);
                selection.ShowDialog();

                for (int i = 0; i < iface.current_job.board_names.Length; i++)
                {
                    int index = iface.current_job.tab_page_map.IndexOf(i);
                    if (index < 0)
                        continue; //if not found (no page exists with that mapping)
                    if (iface.current_job.board_names[i] != null && tabControl_boards.TabPages.Count > 0)
                        tabControl_boards.TabPages[index].Text = iface.current_job.board_names[i];
                }
                iface.current_job.Scanned = true; //change it to true because we have finished scanning, this opens up the DUT info scanning
				numericUpDown_values.Value = 0;
				numericUpDown_values.UpButton();
				create_header(); //do this if the board detect works
            }
            else
                select = false;

            button_select.Visible = false; //hide after starting, should only be able to resume
        }

		private void set_text()
		{
			this.Text = "InSitu Monitor Module";
			label_version.Text = "Version: " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
			label_version.Location = new Point(this.Width - (label_version.Size.Width + 23), label_version.Location.Y);
		}

        private void tooltips()
        {
            toolTip_scan.SetToolTip(button_scan, "Scan for new Beagle/Aardvark/Config. Use after resetting.");
            toolTip_start.SetToolTip(button_select, "Begin data collection. Select config file and Beagle/Aardvark first.");
			toolTip_reset.SetToolTip(button_reset, "Reset program to start-up state");
            ToolTip job = new ToolTip(); ToolTip lot = new ToolTip(); ToolTip system = new ToolTip();  //define tooltips
            lot.SetToolTip(textBox_lot, "Enter lot number before starting");
            job.SetToolTip(textBox_job, "Enter job number before starting");
            system.SetToolTip(textBox_system, "Enter system name before starting");

			toolTip_scan.AutomaticDelay = 300;
			toolTip_start.AutomaticDelay = 300;
			toolTip_reset.AutomaticDelay = 300;
			lot.AutomaticDelay = 300;
			job.AutomaticDelay = 300;
			system.AutomaticDelay = 300;
		}

        private void button1_Click(object sender, EventArgs e)
        {
            if (iface.current_aardvark.ID != 0 && iface.current_aardvark.Port != 999) //making sure we have set the current aardvark first
            {
                ushort bytes_in = 6;
                ushort bytes_out = 2;
                byte[] data_in = new byte[bytes_in];
                byte[] data_out = new byte[bytes_out];
                data_out = new byte[] { 0x2C, 0x06 }; //measure command to SHT module

                //		Console.WriteLine("Write:" + iface.current_aardvark.i2c_write(bytes_out, data_out));
                System.Threading.Thread.Sleep(500);
                //	Console.WriteLine("Read:" + iface.current_aardvark.i2c_read(bytes_in, out data_in)); //pass data_in by reference

                foreach (byte data in data_in)
                    Console.Write(data + " ");
                Console.WriteLine();

                var cTemp = ((((data_in[0] * 256.0) + data_in[1]) * 175) / 65535.0) - 45;
                var humidity = ((((data_in[3] * 256.0) + data_in[4]) * 100) / 65535.0);
                Console.WriteLine("Temp: " + cTemp + ", Humidity: " + humidity);
            }

            else
                MessageBox.Show("Aardvark device not selected");
        } //for testing with sht31d module - obsolete

        private void button_i2cmonitor_Click(object sender, EventArgs e) //resume button
        {
			textBox_data.AppendText("START Operation Initiated\n");

			if (select)
            {
                loop = true; //continue
                if (!iface.current_job.Scanned)
                    run_here(); //do everything in separate funciton
            }
            else //if valid beagle/aardvark combo not selected
                MessageBox.Show("Aardvark/Beagle/Config not selected!");

            button_i2cmonitor.Visible = false;
            button_stop.Visible = true; //show the stop(pause) again
        }

        private void button_reset_Click(object sender, EventArgs e)
        {
			textBox_data.AppendText("RESET Operation Initiated\n"); 
			if(iface.current_aardvark.Port != 999)
				AardvarkApi.aa_close(iface.current_aardvark.Port);
			/*
			if (iface.current_beagle.Port != 999)
			{
				BeagleApi.bg_capture_stop(iface.current_beagle.Port);
				BeagleApi.bg_close(iface.current_beagle.Port);
			}
			*/ //dont need this
			System.Diagnostics.Process.Start(Application.ExecutablePath);
			this.Close();
			//reset these - OLD WAY BELOW, NEW WAY ABOVE WORKS BETTER
			/*
			if (select) //only if the scan completed successfully
				this.Size = new Size(this.Size.Width, this.Size.Height - height); //resize to hide
			loop = false; //to stop or esume the measuremetns
			select = false; //this determines if everything was loaded correctly and can continue to operation
			run_lock = false;
			first_log = Enumerable.Repeat<bool>(true, 16).ToArray();
			for(ushort i = 0x50; i <= 0x5f; i++)
				iface.current_aardvark.i2c_write(i, (ushort)1, new byte[] { 128 }); //set the mux to the off (all boards)

			System.Threading.Thread.Sleep(750); //wait to allow loops to exit out correctly

			while (tabControl_boards.TabCount > 0) //release resources
				tabControl_boards.TabPages[tabControl_boards.TabCount - 1].Dispose();
			tabControl_boards.TabPages.Clear(); //remove 

			BeagleApi.bg_capture_stop(iface.current_beagle.Port);
			AardvarkApi.aa_i2c_free_bus(iface.current_aardvark.Port);
			AardvarkApi.aa_close(iface.current_aardvark.Port);
			BeagleApi.bg_close(iface.current_beagle.Port);
			iface.current_aardvark = new Aardvark(); //placeholder
			iface.current_beagle = new Beagle(); //placeholder
			iface.current_job = null; //clear it out			
			iface = new TotalPhase();

			label_avg.Text = "Avg: ";
			label_max.Text = "Max: ";
			label_min.Text = "Min: ";
			label_range.Text = "Range: ";
			textBox_data.ResetText();
			textBox_job.ResetText();
			textBox_lot.ResetText();
			textBox_system.ResetText();

			comboBox_aardvark.Items.Clear();
            comboBox_beagle.Items.Clear();
			comboBox_config.Items.Clear();
            listBox_active.Items.Clear();
			comboBox_aardvark.ResetText();
			comboBox_beagle.ResetText();
			comboBox_config.ResetText();
			
			button_stop.Visible = false;
			button_i2cmonitor.Visible = false; //resume
			button_select.Visible = true; //start
			checkBox_debug.Checked = false;
			numericUpDown_values.Value = 0;
			numericUpDown_values.ResetText();
			/*
			if (select)
			{
				iface.current_beagle.reset_beagle();
			}
			else //if valid beagle/aardvark combo not selected
				MessageBox.Show("Aardvark/Beagle/Config not selected!");
				*/
			//log_data();
			
        }

        private void button_stop_Click(object sender, EventArgs e)
        {
            loop = false; //pause
			for (ushort i = 0x50; i <= 0x5f; i++)
				iface.current_aardvark.i2c_write(i, (ushort)1, new byte[] { 128 }); //set the mux to the off (all boards)
			button_stop.Visible = false;
            button_i2cmonitor.Visible = true; //show the continue button
            button_reset.Visible = true;
        }

        private void comboBox_config_SelectedIndexChanged(object sender, EventArgs e)
        {
            config_file = new FileInfo(config_path + comboBox_config.Text);
            //find the file
        }

        private void tabControl_boards_Resize(object sender, EventArgs e)
        {
            resize_pages();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loop = false;
            Environment.Exit(0);
        }

        private void logDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string log_directory;
            if (select)
                log_directory = (iface.current_job.LogFilePath);
            else
            {
                log_directory = "C://InSituMonitorModule//ReadLogs//";
                MessageBox.Show("No config file loaded, opening default path");                
            }
            log_directory = "/C start " + log_directory;

            ProcessStartInfo info = new ProcessStartInfo();
            info.CreateNoWindow = true;
            info.UseShellExecute = false;
            info.FileName = "CMD.exe";
            info.Arguments = log_directory;

            Process.Start(info);
        }

        private void configDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string config_directory = "/C start " + config_path;

            ProcessStartInfo info = new ProcessStartInfo();
            info.CreateNoWindow = true;
            info.UseShellExecute = false;
            info.FileName = "CMD.exe";
            info.Arguments = config_directory;

            Process.Start(info);
        }

        private void userParametersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 user_config = new Form3(selection);
            user_config.ShowDialog();
            if (iface.current_job != null) //only works if loaded
            {
                for (int i = 0; i < iface.current_job.board_names.Length; i++)
                {
                    int index = iface.current_job.tab_page_map.IndexOf(i);
                    if (index < 0)
                        continue; //if not found (no page exists with that mapping)
					if (iface.current_job.board_names[i] != null && tabControl_boards.TabPages.Count > 0)
					{
						string text = tabControl_boards.TabPages[index].Text;
						string name = iface.current_job.board_names[i];
						int underscore = text.LastIndexOf('_');
						if (underscore > 0) //already has an underscore
							if (char.IsDigit(text[underscore + 1])) //next letter is a number (means board)
								name = name.Substring(0, name.Length - underscore); //cut off the underscore and number
						tabControl_boards.TabPages[index].Text = name;
					}
                }
            }
        }
	}

	public class NumericUpDownEx : NumericUpDown
    {
        public NumericUpDownEx()
        {
        }

		public override void UpButton()
		{
			var iface = InSituMonitoringModule.iface;
			if (iface != null && iface.current_job != null && iface.current_job.Scanned)
			{
				int count = iface.current_job.device_adds.Count;
				int loop_counter = 0;
				decimal original = this.Value;
				do
				{
					if (iface.current_job.device_adds[(int)this.Value].LogOrder > 0)
						break; //accept it if it works

					if (this.Value >= (count - 1))
					{
						this.Value = 0; //rotate back from the end to now
						loop_counter++;
						if (loop_counter > 1)
						{
							this.Value = original; //just skip if it doesnt find a valid one once scanning all values
							break;
						}
					} //if reach end, loop back

					base.UpButton(); //increment value
				} while (this.Value < count);
			}
		}

		public override void DownButton()
		{
			var iface = InSituMonitoringModule.iface;
			if (iface != null && iface.current_job != null && iface.current_job.Scanned)
			{
				int count = iface.current_job.device_adds.Count;
				int loop_counter = 0;
				decimal original = this.Value;
				do
				{
					if (iface.current_job.device_adds[(int)this.Value].LogOrder > 0)
						break; //accept it if it works

					if (this.Value < 1)
					{
						this.Value = count; //rotate back from the end to now
						loop_counter++;
						if (loop_counter > 1)
						{
							this.Value = original; //just skip if it doesnt find a valid one once scanning all values
							break;
						}
					} //if reach end, loop back

					base.DownButton(); //increment value
				} while (this.Value < count);
			}
		}
		/*
				protected override void OnValueChanged(EventArgs e)
				{
					var iface = InSituMonitoringModule.iface;
					if (iface != null && iface.current_job != null && iface.current_job.Scanned)
					{
						int count = iface.current_job.device_adds.Count;
						int loop_counter = 0;
						for (int i = (int)this.Value; i < count; i++)
						{
							if (iface.current_job.device_adds[i].LogOrder > 0)
							{
								this.Value = i; //accept it if its a log item
								base.OnValueChanged(e);
								break;
							}	


						}
					}
					else
						base.OnValueChanged(e);
				}
			*/
		protected override void UpdateEditText()
        {
			var iface = InSituMonitoringModule.iface;
			// Append the register name
			if (iface != null && iface.current_job != null && iface.current_job.Scanned)
                this.Text = iface.current_job.device_adds[(int)this.Value].Name;
            else
                this.Text = this.Value.ToString();
        }
    }

    public class colorLabel : Label
    {
        public colorLabel() { }

        public string newText { get; set; }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (this.newText != "" && InSituMonitoringModule.iface.current_job != null) //if stop condition dont keep going
            {
                var split = this.newText.Split('\n');
                for (int i = 0; i < InSituMonitoringModule.iface.current_job.device_adds.Count + 1; i++) //-2 for the first "DUT#" and last blank
                {
                    SolidBrush color = new SolidBrush(Color.Black); //one for each register
                    if (i == 0)
                    {
                        e.Graphics.DrawString(split[i], this.Font, color, 1, 13 * i);
                    } //dont color if dut label
                    else
                    { 
                        var address = InSituMonitoringModule.iface.current_job.device_adds[i - 1];

                        if (i < split.Length) //if calculated value, then we sh ould color
                        {
                            string temp = split[i];
                            string substring = temp.Substring(temp.IndexOf(":") + 1, temp.Length - temp.IndexOf(":") - 1);
                            if (float.TryParse(substring, out float value))
                            {
                                if (value > address.High)
                                    color = new SolidBrush(Color.Red);
                                else if (value < address.Low)
                                    color = new SolidBrush(Color.Blue);

                            }
                            this.TextAlign = ContentAlignment.TopLeft;
                            e.Graphics.DrawString(split[i], this.Font, color, 1, 13 * i);
                        }
                    } //register entries
                }
            }
        }
    }    
}
