namespace I2C_Monitor_Module
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.button_scan = new System.Windows.Forms.Button();
			this.button_select = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.comboBox_aardvark = new System.Windows.Forms.ComboBox();
			this.comboBox_beagle = new System.Windows.Forms.ComboBox();
			this.listBox_active = new System.Windows.Forms.ListBox();
			this.button_i2cread = new System.Windows.Forms.Button();
			this.button_i2cmonitor = new System.Windows.Forms.Button();
			this.button_reset = new System.Windows.Forms.Button();
			this.textBox_data = new System.Windows.Forms.TextBox();
			this.button_stop = new System.Windows.Forms.Button();
			this.comboBox_config = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.tabControl_boards = new System.Windows.Forms.TabControl();
			this.SuspendLayout();
			// 
			// button_scan
			// 
			this.button_scan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button_scan.Location = new System.Drawing.Point(512, 141);
			this.button_scan.Name = "button_scan";
			this.button_scan.Size = new System.Drawing.Size(75, 23);
			this.button_scan.TabIndex = 0;
			this.button_scan.Text = "Scan";
			this.button_scan.UseVisualStyleBackColor = true;
			this.button_scan.Click += new System.EventHandler(this.button_scan_Click);
			// 
			// button_select
			// 
			this.button_select.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button_select.Location = new System.Drawing.Point(512, 170);
			this.button_select.Name = "button_select";
			this.button_select.Size = new System.Drawing.Size(75, 23);
			this.button_select.TabIndex = 1;
			this.button_select.Text = "Select";
			this.button_select.UseVisualStyleBackColor = true;
			this.button_select.Click += new System.EventHandler(this.button_select_Click);
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(300, 59);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(101, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Available Aardvarks";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(430, 59);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(91, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Available Beagles";
			// 
			// comboBox_aardvark
			// 
			this.comboBox_aardvark.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBox_aardvark.FormattingEnabled = true;
			this.comboBox_aardvark.Location = new System.Drawing.Point(303, 76);
			this.comboBox_aardvark.Name = "comboBox_aardvark";
			this.comboBox_aardvark.Size = new System.Drawing.Size(121, 21);
			this.comboBox_aardvark.TabIndex = 4;
			// 
			// comboBox_beagle
			// 
			this.comboBox_beagle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBox_beagle.FormattingEnabled = true;
			this.comboBox_beagle.Location = new System.Drawing.Point(430, 76);
			this.comboBox_beagle.Name = "comboBox_beagle";
			this.comboBox_beagle.Size = new System.Drawing.Size(121, 21);
			this.comboBox_beagle.TabIndex = 5;
			// 
			// listBox_active
			// 
			this.listBox_active.FormattingEnabled = true;
			this.listBox_active.Location = new System.Drawing.Point(12, 143);
			this.listBox_active.Name = "listBox_active";
			this.listBox_active.Size = new System.Drawing.Size(102, 95);
			this.listBox_active.TabIndex = 8;
			// 
			// button_i2cread
			// 
			this.button_i2cread.Location = new System.Drawing.Point(431, 257);
			this.button_i2cread.Name = "button_i2cread";
			this.button_i2cread.Size = new System.Drawing.Size(75, 23);
			this.button_i2cread.TabIndex = 9;
			this.button_i2cread.Text = "i2cread";
			this.button_i2cread.UseVisualStyleBackColor = true;
			this.button_i2cread.Visible = false;
			this.button_i2cread.Click += new System.EventHandler(this.button1_Click);
			// 
			// button_i2cmonitor
			// 
			this.button_i2cmonitor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button_i2cmonitor.Location = new System.Drawing.Point(512, 228);
			this.button_i2cmonitor.Name = "button_i2cmonitor";
			this.button_i2cmonitor.Size = new System.Drawing.Size(75, 23);
			this.button_i2cmonitor.TabIndex = 10;
			this.button_i2cmonitor.Text = "i2cmonitor";
			this.button_i2cmonitor.UseVisualStyleBackColor = true;
			this.button_i2cmonitor.Click += new System.EventHandler(this.button_i2cmonitor_Click);
			// 
			// button_reset
			// 
			this.button_reset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button_reset.Location = new System.Drawing.Point(512, 199);
			this.button_reset.Name = "button_reset";
			this.button_reset.Size = new System.Drawing.Size(75, 23);
			this.button_reset.TabIndex = 11;
			this.button_reset.Text = "buffer reset";
			this.button_reset.UseVisualStyleBackColor = true;
			this.button_reset.Click += new System.EventHandler(this.button_reset_Click);
			// 
			// textBox_data
			// 
			this.textBox_data.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.textBox_data.Location = new System.Drawing.Point(120, 143);
			this.textBox_data.Multiline = true;
			this.textBox_data.Name = "textBox_data";
			this.textBox_data.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox_data.Size = new System.Drawing.Size(380, 139);
			this.textBox_data.TabIndex = 12;
			// 
			// button_stop
			// 
			this.button_stop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button_stop.Location = new System.Drawing.Point(512, 257);
			this.button_stop.Name = "button_stop";
			this.button_stop.Size = new System.Drawing.Size(75, 23);
			this.button_stop.TabIndex = 13;
			this.button_stop.Text = "Stop";
			this.button_stop.UseVisualStyleBackColor = true;
			this.button_stop.Click += new System.EventHandler(this.button_stop_Click);
			// 
			// comboBox_config
			// 
			this.comboBox_config.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBox_config.FormattingEnabled = true;
			this.comboBox_config.Location = new System.Drawing.Point(303, 24);
			this.comboBox_config.Name = "comboBox_config";
			this.comboBox_config.Size = new System.Drawing.Size(248, 21);
			this.comboBox_config.TabIndex = 15;
			this.comboBox_config.SelectedIndexChanged += new System.EventHandler(this.comboBox_config_SelectedIndexChanged);
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(300, 9);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(88, 13);
			this.label4.TabIndex = 14;
			this.label4.Text = "Configuration File";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(9, 126);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(46, 13);
			this.label3.TabIndex = 7;
			this.label3.Text = "Devices";
			// 
			// label5
			// 
			this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(117, 126);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(39, 13);
			this.label5.TabIndex = 17;
			this.label5.Text = "Output";
			// 
			// label6
			// 
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(513, 125);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(45, 13);
			this.label6.TabIndex = 18;
			this.label6.Text = "Controls";
			// 
			// tabControl_boards
			// 
			this.tabControl_boards.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl_boards.Enabled = false;
			this.tabControl_boards.ItemSize = new System.Drawing.Size(60, 20);
			this.tabControl_boards.Location = new System.Drawing.Point(2, 282);
			this.tabControl_boards.Multiline = true;
			this.tabControl_boards.Name = "tabControl_boards";
			this.tabControl_boards.SelectedIndex = 0;
			this.tabControl_boards.Size = new System.Drawing.Size(593, 255);
			this.tabControl_boards.TabIndex = 41;
			this.tabControl_boards.Visible = false;
			this.tabControl_boards.Resize += new System.EventHandler(this.tabControl_boards_Resize);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(598, 287);
			this.Controls.Add(this.tabControl_boards);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.comboBox_config);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.button_stop);
			this.Controls.Add(this.textBox_data);
			this.Controls.Add(this.button_reset);
			this.Controls.Add(this.button_i2cmonitor);
			this.Controls.Add(this.button_i2cread);
			this.Controls.Add(this.listBox_active);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.comboBox_beagle);
			this.Controls.Add(this.comboBox_aardvark);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button_select);
			this.Controls.Add(this.button_scan);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button button_scan;
		private System.Windows.Forms.Button button_select;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox comboBox_aardvark;
		private System.Windows.Forms.ComboBox comboBox_beagle;
		private System.Windows.Forms.ListBox listBox_active;
		private System.Windows.Forms.Button button_i2cread;
		private System.Windows.Forms.Button button_i2cmonitor;
		private System.Windows.Forms.Button button_reset;
		public System.Windows.Forms.TextBox textBox_data;
		private System.Windows.Forms.Button button_stop;
		private System.Windows.Forms.ComboBox comboBox_config;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TabControl tabControl_boards;
	}
}

