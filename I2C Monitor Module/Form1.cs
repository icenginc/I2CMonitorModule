using System;
using System.Collections.Generic;
using System.Collections;
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
        public static TotalPhase iface = new TotalPhase(); //one totalphase, redefine the current devices as the selection changes
        string config_path = "C://InSituMonitorModule//";
        FileInfo config_file;

        public string system { get; set; }
        public string lot { get; set; }
        public string job { get; set; }

        bool loop = false; //to stop or esume the measuremetns
        bool select = false; //this determines if everything was loaded correctly and can continue to operation
        bool run_lock = false;
        bool[] first_log = Enumerable.Repeat<bool>(true, 16).ToArray();

        /// <summary>
        /// these locks are to direct the user to the correct order of steps
        /// </summary>
        public InSituMonitoringModule()
        {
            InitializeComponent();
            this.Text = "InSitu Monitor Module";
            load_config();
            button_scan_Click(this, new EventArgs());
        }

        private void button_scan_Click(object sender, EventArgs e)
        {
            iface.find_device(); //button to do this, then display devices

            var aardvark_ids = iface.aardvarks.Select(a => a.ID.ToString()).ToArray();
            var beagle_ids = iface.beagles.Select(a => a.ID.ToString()).ToArray();

            comboBox_aardvark.Items.AddRange(aardvark_ids);
            comboBox_beagle.Items.AddRange(beagle_ids);

            listBox_active.Items.AddRange(aardvark_ids);
            listBox_active.Items.AddRange(beagle_ids);
            listBox_active.SelectionMode = SelectionMode.MultiExtended;
        }

        private void button_select_Click(object sender, EventArgs e)
        {
            select = true;

            this.job = textBox_job.Text;
            this.lot = textBox_lot.Text;
            this.system = textBox_system.Text;

            foreach (Aardvark a in iface.aardvarks)
            {
                if (a.ID.ToString() == (string)comboBox_aardvark.SelectedItem)
                    iface.current_aardvark = a;
                else
                    select = false;
            }

            foreach (Beagle b in iface.beagles)
            {
                if (b.ID.ToString() == (string)comboBox_beagle.SelectedItem)
                    iface.current_beagle = b;
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
            }//if the parse is fine then continue, also check for current beagle and aardvark set
            else
                select = false;

            if (resolve_boards() && select) //select bool means that if the config didn't parse, cant attemp to continue
            {
                Form2 selection = new Form2(iface.current_job.board_list, iface.current_job.device_adds);
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
                create_header(); //do this if the board detect works
            }
            else
                select = false;

            button_select.Visible = false; //hide after starting, should only be able to resume
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
            loop = false;
            /*
			if (select)
			{
				iface.current_beagle.reset_beagle();
			}
			else //if valid beagle/aardvark combo not selected
				MessageBox.Show("Aardvark/Beagle/Config not selected!");
				*/
            log_data();
        }

        private void button_stop_Click(object sender, EventArgs e)
        {
            loop = false; //pause
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
    }

    public class NumericUpDownEx : NumericUpDown
    {
        public NumericUpDownEx()
        {
        }

        protected override void UpdateEditText()
        {
            // Append the register name
            if (InSituMonitoringModule.iface != null && InSituMonitoringModule.iface.current_job != null && InSituMonitoringModule.iface.current_job.Scanned)
                this.Text = InSituMonitoringModule.iface.current_job.device_adds[(int)this.Value].Name;
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
            if (this.newText != "")
            {
                var split = this.newText.Split('\n');
                for (int i = 0; i < InSituMonitoringModule.iface.current_job.device_adds.Count + 1; i++) //-2 for the first "DUT#" and last blank
                {
                    SolidBrush color = new SolidBrush(Color.Black); //one for each register
                    if (i == 0)
                    {
                        e.Graphics.DrawString(split[i], this.Font, color, 1, 13 * i);
                    }
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
                                    color = new SolidBrush(Color.SkyBlue);

                            }
                            this.TextAlign = ContentAlignment.TopLeft;
                            e.Graphics.DrawString(split[i], this.Font, color, 1, 13 * i);
                        }
                    }
                }
            }
        }
    }    
}
