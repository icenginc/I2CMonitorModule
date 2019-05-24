using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;


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
                float low = InSituMonitoringModule.iface.current_job.device_adds[i].Low;
                float high = InSituMonitoringModule.iface.current_job.device_adds[i].High;

                if (low > float.MinValue)
                    dataGridView2.Rows[i].Cells[1].Value = InSituMonitoringModule.iface.current_job.device_adds[i].Low;
                if (high < float.MaxValue)
                    dataGridView2.Rows[i].Cells[2].Value = InSituMonitoringModule.iface.current_job.device_adds[i].High;
            }

            textBox_config.Text = InSituMonitoringModule.config_path.Replace("//", "/");
            try
            {
                textBox_log.Text = InSituMonitoringModule.iface.current_job.LogFilePath.Replace("//", "/");
            }
            catch
            {
                Console.WriteLine("Log file path not yet set");
            }
        }

        private void update_paths()
        {
            DirectoryInfo directory = new DirectoryInfo(textBox_log.Text.Replace("/", "//"));
            if (directory.Exists)
                InSituMonitoringModule.iface.current_job.LogFilePath = directory.FullName;

            directory = new DirectoryInfo(textBox_config.Text.Replace("/", "//"));
            if (directory.Exists)
            {
                InSituMonitoringModule.config_path = textBox_config.Text;
            } //figure out how to reflect this into use
           
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            update_values(dataGridView1, dataGridView2); //should update and save in Form1
            update_paths();
            this.Close();
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_config_Click(object sender, EventArgs e)
        {
            if (InSituMonitoringModule.iface.current_job != null)
                MessageBox.Show("Cannot change file path after configuration is loaded");
            else
            {

                try
                {
                    CommonOpenFileDialog dialog = new CommonOpenFileDialog();
                    dialog.IsFolderPicker = true;
                    if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                        textBox_config.Text = dialog.FileName;
                }
                catch
                {
                    MessageBox.Show("Error in file select");
                }
            }
        }

        private void button_log_Click(object sender, EventArgs e)
        {
            if (InSituMonitoringModule.iface.current_job != null)
                MessageBox.Show("Cannot change file path after configuration is loaded");
            else
            {
                try
                {
                    CommonOpenFileDialog dialog = new CommonOpenFileDialog();
                    dialog.IsFolderPicker = true;
                    if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                        textBox_log.Text = dialog.FileName;
                }
                catch
                {
                    MessageBox.Show("Error in file select");
                }
            }
        }
    }
}
