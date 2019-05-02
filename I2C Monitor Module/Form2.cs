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

namespace I2C_Monitor_Module
{
	public partial class Form2 : Form
	{
		public Form2(bool[][] input, List<device> addresses)
		{
			InitializeComponent();
			this.Name = "Enter Parameters";
			generate_boards(input);
			generate_addresses(addresses);

			if ((dataGridView1.Rows.Count * dataGridView1.Rows[0].Height) > dataGridView1.Height)
				dataGridView1.Width += 17; //make the control wider to fit the scrollbar
			if ((dataGridView2.Rows.Count * dataGridView2.Rows[0].Height) > dataGridView2.Height)
				dataGridView2.Width += 17; //make the control wider to fit the scrollbar
		}

		bool[] board_list = new bool[16];

		private void generate_boards(bool[][] board_list)
		{
			for (int i = 0; i < board_list.Length; i++)
			{
				bool[] board_map = board_list[i]; //break down to current board

				if (board_map.Contains(true)) //if it contains a true then add that board
				{
					dataGridView1.Rows.Add(new object[] { (i + 1).ToString() });
				}//if the board is valid
			}
		}

		private void generate_addresses(List<device> addresses)
		{
			foreach (device s in addresses)
				dataGridView2.Rows.Add(new object[] { s.Name + " (" + s.Address[0].ToString("X") + s.Address[1].ToString("X")+")" });
		}

		private void button_ok_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
			{

				if (dataGridView1.Rows[i].Cells[1].Value != null)
				{
					string name = dataGridView1.Rows[i].Cells[1].Value.ToString();
					InSituMonitoringModule.iface.current_job.board_names[i] = name.ToUpper();
				}
				else
					InSituMonitoringModule.iface.current_job.board_names[i] = ("Board " + (i + 1));
			}

			for (int i = 0; i < dataGridView2.Rows.Count -1; i++)
				try
				{
					if (dataGridView2.Rows[i].Cells[1].Value != null)
					{
						string low = dataGridView2.Rows[i].Cells[1].Value.ToString();
						InSituMonitoringModule.iface.current_job.device_adds[i].Low = Int32.Parse(low);
					}
					else
						InSituMonitoringModule.iface.current_job.device_adds[i].Low = Int32.MinValue; //if blank

					if (dataGridView2.Rows[i].Cells[2].Value != null)
					{
						string high = dataGridView2.Rows[i].Cells[2].Value.ToString();
						InSituMonitoringModule.iface.current_job.device_adds[i].High = Int32.Parse(high);
					}
					else
						InSituMonitoringModule.iface.current_job.device_adds[i].High = Int32.MaxValue; //if blank

				}
				catch
				{
					MessageBox.Show("Invalid value entered for hi/lo on address for " + InSituMonitoringModule.iface.current_job.device_adds[i].Name);
					InSituMonitoringModule.iface.current_job.device_adds[i].Low = Int32.MinValue;
					InSituMonitoringModule.iface.current_job.device_adds[i].High = Int32.MaxValue;
				}

			this.Close();
		}

		private void button_cancel_Click(object sender, EventArgs e)
		{
			this.Close();
			MessageBox.Show("Using default values");
			for (int i = 0; i < dataGridView1.Rows.Count; i++)
				InSituMonitoringModule.iface.current_job.board_names[i] = ("Board " + (i + 1));
			for (int i = 0; i < dataGridView2.Rows.Count; i++)
			{
				InSituMonitoringModule.iface.current_job.device_adds[i].Low = Int32.MinValue;
				InSituMonitoringModule.iface.current_job.device_adds[i].High = Int32.MaxValue;
			}
		}
	}
}
