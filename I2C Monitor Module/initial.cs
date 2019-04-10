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
					file.Add(line);
				}

				iface.current_job = new job(file);

			}//parse in here
			catch
			{
				return false;
			}
			return true;
		}//parse the ini file here

		private bool resolve_boards()
		{
			if (!select)
			{
				MessageBox.Show("Cannot resolve boards before loading config file!");
				return false;
			}
			//*todo
			//set mux to 0x50-0x5F
			//write to the first register found in teh INI file
			//read it back, if its valid then add to list
			//*
			for (ushort mux_address = 0x50; mux_address < 0x5F; mux_address++)
			{
				bool valid = scan_board(mux_address);
				if (valid)
				{
					tabControl_boards.Enabled = true;
					tabControl_boards.Visible = true;
					TabPage page = new TabPage("Board " + (mux_address - 0x4F));
					tabControl_boards.TabPages.Add(page);
				}
			}//each iteration is one mux address

			Console.WriteLine("Buildt pages - populating now..");
			/*
			BackgroundWorker tab_populate = new BackgroundWorker();
			tab_populate.DoWork += Tab_populate_DoWork;
			tab_populate.RunWorkerAsync();
			*/
			if (iface.current_job != null)
			{
				foreach (TabPage page in tabControl_boards.TabPages)
					build_page(page); //if the job is loaded, fill the GUI with board dimensions

				this.Size = new Size(this.Size.Width, this.Size.Height + tabControl_boards.Size.Height); //resize to show
				tabControl_boards.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right; //make it resizable dynamically
			}
			else
			{
				MessageBox.Show("Job not loaded in config! Can't draw boards");
				return false;
			}
			return true; //on success
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

		private void Tab_populate_DoWork(object sender, DoWorkEventArgs e)
		{
		} //moved here temporarily, moved back

		private bool scan_board(ushort mux) //scans each mux address for responses on DUTs, then builds them up
		{
			//fake datat for now
			int fake = mux - 0x50;
			if (fake == 0 || fake == 3 || fake == 4)
				return true;
			else
				return false;
		}
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