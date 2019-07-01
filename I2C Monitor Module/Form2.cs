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
        public Form2()
        { }

		public Form2(bool[][] input, List<device> addresses)
		{
			InitializeComponent();
			this.Text = "Enter Parameters";
            this.input = input;
            this.addresses = addresses;

			generate_boards(input, dataGridView1);
			generate_addresses(addresses, dataGridView2);

			if ((dataGridView1.Rows.Count * dataGridView1.Rows[0].Height) > dataGridView1.Height)
				dataGridView1.Width += 17; //make the control wider to fit the scrollbar
			if ((dataGridView2.Rows.Count * dataGridView2.Rows[0].Height) > dataGridView2.Height)
				dataGridView2.Width += 17; //make the control wider to fit the scrollbar
		}

		bool[] board_list = new bool[16];
        public bool[][] input;
        public List<device> addresses;

        protected void generate_boards(bool[][] board_list, DataGridView grid)
		{
			for (int i = 0; i < board_list.Length; i++)
			{
				bool[] board_map = board_list[i]; //break down to current board

				if (board_map != null && board_map.Contains(true)) //if it contains a true then add that board
				{
					grid.Rows.Add(new object[] { (i + 1).ToString() });
				}//if the board is valid
			}
		}

		protected void generate_addresses(List<device> addresses, DataGridView grid)
		{
			foreach (device s in addresses)
				if(s.LogOrder > 0) //cleans up user parameter entry display //makes register low/hi save into the wrong register, but it is read back with the same logic so it ends up matching
					grid.Rows.Add(new object[] { s.Name + " (" + s.Address[0].ToString("X") + s.Address[1].ToString("X")+")" });
		}

		private void button_ok_Click(object sender, EventArgs e)
		{
            update_values(dataGridView1, dataGridView2);
            this.Close();
        }

        public void update_values(DataGridView grid1, DataGridView grid2)
        {
            for (int i = 0; i < grid1.Rows.Count - 1; i++)
            {
                int index = InSituMonitoringModule.iface.current_job.tab_page_map[i];

                if (grid1.Rows[i].Cells[1].Value != null && grid1.Rows[i].Cells[1].Value.ToString() != ("Board" + (index + 1)))  //if valid
                {
                    string name = grid1.Rows[i].Cells[1].Value.ToString();
                    InSituMonitoringModule.iface.current_job.board_names[index] = (index + 1) + "_" + name.ToUpper();
                }
                else if (InSituMonitoringModule.iface.current_job.board_names[index] == null) //if empty (unset)
                {
                    string name = grid1.Rows[i].Cells[0].Value.ToString();
                    InSituMonitoringModule.iface.current_job.board_names[index] = ("Board" + (index + 1));
                }
            }

            for (int i = 0; i < grid2.Rows.Count - 1; i++)
                try
                {
                    if (grid2.Rows[i].Cells[1].Value != null)  //if valid
                    {
                        string low = grid2.Rows[i].Cells[1].Value.ToString();
                        InSituMonitoringModule.iface.current_job.device_adds[i].Low = float.Parse(low);
                    }
                    else if(InSituMonitoringModule.iface.current_job.device_adds[i].Low > float.MinValue) //if blank - check if filled already
                        InSituMonitoringModule.iface.current_job.device_adds[i].Low = float.MinValue; //if blank

                    if (grid2.Rows[i].Cells[2].Value != null)
                    {
                        string high = grid2.Rows[i].Cells[2].Value.ToString();
                        InSituMonitoringModule.iface.current_job.device_adds[i].High = float.Parse(high);
                    }
                    else if (InSituMonitoringModule.iface.current_job.device_adds[i].High < float.MaxValue) //if blank - check if filld already
                        InSituMonitoringModule.iface.current_job.device_adds[i].High = float.MaxValue; //if blank

                }
                catch
                {
                    MessageBox.Show("Invalid value entered for hi/lo on address for " + InSituMonitoringModule.iface.current_job.device_adds[i].Name);
                    InSituMonitoringModule.iface.current_job.device_adds[i].Low = float.MinValue;
                    InSituMonitoringModule.iface.current_job.device_adds[i].High = float.MaxValue;
                }
        }

		private void button_cancel_Click(object sender, EventArgs e)
		{
			this.Close();
			MessageBox.Show("Using default values");
			for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
			{
				int index = InSituMonitoringModule.iface.current_job.tab_page_map[i];
				InSituMonitoringModule.iface.current_job.board_names[index] = ("Board" + (index + 1));
			}
			for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
			{
				InSituMonitoringModule.iface.current_job.device_adds[i].Low = float.MinValue;
				InSituMonitoringModule.iface.current_job.device_adds[i].High = float.MaxValue;
			}
		}
	}
}
