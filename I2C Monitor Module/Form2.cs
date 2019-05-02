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
		public Form2(bool[][] input)
		{
			InitializeComponent();
			for (int i = 0; i < input.Length; i++)
			{
				bool[] board_map = input[i];

				if (board_map.Contains(true))
					board_list[i] = true;
			}
			generate_boards();
		}
		bool[] board_list = new bool[16];

		private void generate_boards()
		{
			for (int i = 0; i < board_list.Length; i++)
			{
				if (board_list[i])
				{
					dataGridView1.Rows.Add(new object[] { (i + 1).ToString() });
				}//if the board is valid
			}
		}

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}
	}
}
