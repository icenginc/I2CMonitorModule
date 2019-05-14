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
    public partial class Form3 : Form2
    {
        public Form3(Form2 example)
        {
            InitializeComponent();
            if (example != null) //we already did Form2
            {
                generate_boards(example.input, dataGridView1);
                generate_addresses(example.addresses, dataGridView2);
                fill_data();
            }
            else //valid
            {
                dataGridView1 = new DataGridView();
                dataGridView2 = new DataGridView();

                dataGridView1.Enabled = false;
                dataGridView2.Enabled = false;
            }

        }

        private void fill_data()
        {
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                int index = InSituMonitoringModule.iface.current_job.tab_page_map[i];
                if (InSituMonitoringModule.iface.current_job.board_names[index] != null)
                    dataGridView1.Rows[i].Cells[1].Value = InSituMonitoringModule.iface.current_job.board_names[index];
            }

            for (int i = 0; i < dataGridView2.Rows.Count -1; i++)
            {
                int low = InSituMonitoringModule.iface.current_job.device_adds[i].Low;
                int high = InSituMonitoringModule.iface.current_job.device_adds[i].High;

                if (low > Int32.MinValue)
                    dataGridView2.Rows[i].Cells[1].Value = InSituMonitoringModule.iface.current_job.device_adds[i].Low;
                if (high < Int32.MaxValue)
                    dataGridView2.Rows[i].Cells[2].Value = InSituMonitoringModule.iface.current_job.device_adds[i].High;
            }
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            update_values(dataGridView1, dataGridView2); //should update and save in Form1
            this.Close();
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
