using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace I2C_Monitor_Module
{
    public partial class InSituMonitoringModule : Form
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
					if(!line.StartsWith("; ")) //only read the line if its not commented out
						file.Add(line);
				}

				iface.current_job = new job(file);

			}//parse in here
			catch
			{
				MessageBox.Show("Error: Failed to parse config file");
				return false;
			}
			return true;
		}//parse the ini file here

		private bool resolve_boards()
		{
			if (!select)
			{
				MessageBox.Show("Error: Scan for boards failed!");
				return false;
			}
			iface.current_job.Scanned = false; //not yet scanned
			//*todo
			//set mux to 0x50-0x5F
			//write to the first register found in teh INI file
			//read it back, if its valid then add to list
			//*

            button_i2cmonitor_Click(this, new EventArgs());
            Cursor.Current = Cursors.WaitCursor;
			for (ushort mux_address = 0x50; mux_address <= 0x5F; mux_address++)
			{
				var valid = scan_board(mux_address);
                if (valid.Contains(true))
                {
                    iface.current_job.board_list[mux_address - 0x50] = valid;
                    tabControl_boards.Enabled = true;
                    tabControl_boards.Visible = true;
                    TabPage page = new TabPage("Board " + (mux_address - 0x4f));
                    tabControl_boards.TabPages.Add(page);
                    iface.current_job.tab_page_map.Add(mux_address - 0x50); //save what index in the tab control, so we can update it later.
                } //build each page, this happens once
            }//each iteration is one mux address

			Console.WriteLine("Buildt pages - populating now..");
			
			if (iface.current_job != null)
			{
				foreach (TabPage page in tabControl_boards.TabPages) //now build after scanning
					build_page(page); //if the job is loaded, fill the GUI with board dimensions

				this.Size = new Size(this.Size.Width, this.Size.Height + tabControl_boards.Size.Height); //resize to show
				tabControl_boards.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right; //make it resizable dynamically
			}
			else
			{
				MessageBox.Show("Job not loaded in config! Can't draw boards");
				return false;
			}
            Cursor.Current = Cursors.Default;
			return true; //on success
		}

        private int monitor_map(int dut)
        {
            return iface.current_job.Monitor_Map[dut];
        }

		private void build_page(TabPage page)
		{
			var x = page.Size.Width - 10;
			var y = page.Size.Height;
			var width = x / iface.current_job.Bibx;
			var height = y / iface.current_job.Biby;

			for (int i = 0; i < iface.current_job.Biby; i++) //column
			{
				for (int j = 0; j < iface.current_job.Bibx; j++) //row
				{
					//build the DUT thing in here
					Label label = new Label();
					label.Text = "DUT " + iface.current_job.Monitor_Map[((i * iface.current_job.Bibx) + j)]; //use monnitor map from file to name
					label.TextAlign = ContentAlignment.TopCenter;
					label.Location = new Point(j * width, (i * height) + 1); //calculated height based on loop position
					label.BorderStyle = BorderStyle.Fixed3D;
					label.Size = new Size(width - 3, height - 2);

					page.Controls.Add(label); //add the label

					//label.Anchor = ((AnchorStyles.Top | AnchorStyles.Left));// | AnchorStyles.Right | AnchorStyles.Left);
					//label.AutoSize = true;
					label.Dock = DockStyle.None;
				}
			}

			myLabel prototype = new myLabel(); //this draws the golden box using a rotate label, filled in
			prototype.RotateAngle = 90;
			prototype.BackColor = System.Drawing.Color.Gold;
			prototype.Size = new Size(10, y - 20);
			//prototype.Anchor = AnchorStyles.Right;

			page.Controls.Add(prototype);
			prototype.Left = x - 5;
			prototype.Top = 10;
		}

		private void resize_pages()
		{
			foreach (TabPage page in tabControl_boards.TabPages)
			{
				page.Size = new Size(tabControl_boards.Width, tabControl_boards.Height);//(page.Controls[0].Height + 2) * iface.current_job.Biby);
				var x = page.Size.Width - 10;
				var y = page.Size.Height;
				var width = x / iface.current_job.Bibx;
				var height = y / iface.current_job.Biby;	

				for (int i = 0; i < iface.current_job.Biby; i++) //column
				{
					for (int j = 0; j < iface.current_job.Bibx; j++) //row
					{
						Label label = (Label)page.Controls[(i * iface.current_job.Bibx)+j];
						label.Location = new Point(j * width, (i * height) + 1); //calculated height based on loop position
						label.Size = new Size(width - 3, height - 2);
					}
				}

				myLabel prototype = (myLabel)page.Controls[page.Controls.Count-1];
				prototype.Size = new Size(10, y - 20);
				prototype.Left = x - 5;
				prototype.Top = 10;

				//page.Size = new Size(tabControl_boards.Width, (page.Controls[0].Height+2)*iface.current_job.Biby);
			}
			//tabControl_boards.Size = new Size(tabControl_boards.Width, tabControl_boards.TabPages[0].Height + 28);
		}

		private bool[] scan_board(ushort mux) //scans each mux address for responses on DUTs, then builds them up
		{
			textBox_data.AppendText("Scanning board at mux address " + mux.ToString("X") + "..." + Environment.NewLine);
			//listen for the DUTs here
			bool[] valid = Enumerable.Repeat<bool>(false, 40).ToArray(); //board sites
            
            //BackgroundWorker tab_populate = new BackgroundWorker();
            //tab_populate.DoWork += Tab_populate_DoWork;
            //tab_populate.RunWorkerAsync(mux); //used this so no infinite loop
            device[] addresses = iface.current_job.device_adds.ToArray(); //put the iface device addresses locally
            //ushort mux = (ushort)e.Argument;
            for (byte i = 0; i < iface.current_job.Sites; i++)
            {
                byte unique = (byte)(i);// + (1<<7));
                byte[] register_data = new byte[] { unique }; //write the register to pick DUT, and 'unique data'
                int bytes = iface.current_aardvark.i2c_write(mux, (ushort)register_data.Length, register_data); //set the mux to the DUT
                iface.current_aardvark.i2c_read(mux, (ushort)register_data.Length, out byte[] data);
                iface.current_beagle.buffer.Clear();

                
                if (!data.Contains(unique) || bytes < register_data.Length)
                    continue; //skip the DUT if it doesn't read back the unique data

                while(iface.current_beagle.buffer.Count < 10)
                    System.Threading.Thread.Sleep(10); //give a little time for buffer to fill
                
                foreach (device address in addresses)
                {
                    textBox_data.AppendText("Looking for address " + iface.current_job.ReadAddress.ToString("X") + " on board " + (mux - 0x50) + "...");
                    if (iface.current_beagle.buffer.Contains(iface.current_job.ReadAddress)) //just look for a readback
                    {
                        textBox_data.AppendText(" Found\n");
                        valid[i] = true;
                        break; //dont need to look for the rest if one is in there
                    }//if we get even 1 DUTs address data back, then the board is there with a DUT
                    else
                        textBox_data.AppendText(Environment.NewLine);
                }//check for each address in the data

            }
            return valid;
		} //do this once for each board to build the board

        private void create_header()
        {
            string filename = iface.current_job.LogFileName.Replace("\"", string.Empty);
            string datetime = DateTime.Now.ToString("MM.dd.yy-HH.mm.ss");
            string log_path = iface.current_job.LogFilePath + system + "_" + lot + "_" + job + "\\";
            if (system != "")
                filename = filename.Replace("System", system);
            if (lot != "")
                filename = filename.Replace("LotNumber", lot);
            if (job != "")
                filename = filename.Replace("JobNumber", job);
            filename = filename.Replace("DateTime", datetime);
            iface.current_job.LogFileName = filename;
            iface.current_job.LogFilePath = log_path;

            DirectoryInfo path = new DirectoryInfo(log_path);
            if (!path.Exists)
                path.Create();//if the directory is not there, then create it

            for (int i = 0; i < 16; i++)
                if (iface.current_job.board_list[i] != null && iface.current_job.board_list[i].Contains(true))
                {
                    BackgroundWorker header_worker = new BackgroundWorker();
                    header_worker.DoWork += Header_worker_DoWork;
                    header_worker.RunWorkerAsync(i); //create 16 files
                }
        }

        private void Header_worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Console.WriteLine("Writing header for job");

            string slot = e.Argument.ToString();            
            string filename = iface.current_job.LogFileName.Replace(".rlog", "_" + slot + ".rlog");

            using (StreamWriter writer = File.AppendText(iface.current_job.LogFilePath + filename))
            {
                /*
                foreach (string content in iface.current_job.file_contents)
                    writer.WriteLine(content);
                */
                string line ="Timestamp,";
                for (int i = 0; i < iface.current_job.device_adds.Count; i++)
                {
                    
                    device add = iface.current_job.device_adds[i];
                    for (int j = 0; j < iface.current_job.Sites; j++)
                        line += ("DUT " + (j + 1) + " - " + add.Name + ',');
                    if (add.LogOrder == 0) //in the case of header item, write it into file
                        writer.WriteLine(line);
                    
                }
                writer.WriteLine(line); //these should be all the headers
            }
        }

        private void Tab_populate_DoWork(object sender, DoWorkEventArgs e)
		{
			
		} //moved here temporarily, moved back
	}
    	
	class myLabel : System.Windows.Forms.Label //resused this code from ultracomm project (roundabout but works)
	{
		public int RotateAngle { get; set; }  // to rotate text
		public string NewText { get; set; }   // to draw text
               
		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			int mx = this.Size.Width / 2;
			int my = this.Size.Height / 2;

			SizeF size = e.Graphics.MeasureString(Text, Font);

			Brush b = new SolidBrush(this.ForeColor);
			e.Graphics.TranslateTransform(this.Width / 2, this.Height / 2);
			e.Graphics.RotateTransform(this.RotateAngle);
			e.Graphics.DrawString(this.NewText, this.Font, b, mx - (int)size.Width / 2, my - (int)size.Height / 2);
			base.OnPaint(e);
		}
	}
}