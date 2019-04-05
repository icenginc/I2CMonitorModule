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
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.label20 = new System.Windows.Forms.Label();
			this.label21 = new System.Windows.Forms.Label();
			this.label22 = new System.Windows.Forms.Label();
			this.label23 = new System.Windows.Forms.Label();
			this.label24 = new System.Windows.Forms.Label();
			this.label25 = new System.Windows.Forms.Label();
			this.label26 = new System.Windows.Forms.Label();
			this.tabControl_boards.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// button_scan
			// 
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
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(300, 59);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(101, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Available Aardvarks";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(430, 59);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(91, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Available Beagles";
			// 
			// comboBox_aardvark
			// 
			this.comboBox_aardvark.FormattingEnabled = true;
			this.comboBox_aardvark.Location = new System.Drawing.Point(303, 76);
			this.comboBox_aardvark.Name = "comboBox_aardvark";
			this.comboBox_aardvark.Size = new System.Drawing.Size(121, 21);
			this.comboBox_aardvark.TabIndex = 4;
			// 
			// comboBox_beagle
			// 
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
			this.textBox_data.Location = new System.Drawing.Point(120, 143);
			this.textBox_data.Multiline = true;
			this.textBox_data.Name = "textBox_data";
			this.textBox_data.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox_data.Size = new System.Drawing.Size(380, 139);
			this.textBox_data.TabIndex = 12;
			// 
			// button_stop
			// 
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
			this.comboBox_config.FormattingEnabled = true;
			this.comboBox_config.Location = new System.Drawing.Point(303, 24);
			this.comboBox_config.Name = "comboBox_config";
			this.comboBox_config.Size = new System.Drawing.Size(248, 21);
			this.comboBox_config.TabIndex = 15;
			this.comboBox_config.SelectedIndexChanged += new System.EventHandler(this.comboBox_config_SelectedIndexChanged);
			// 
			// label4
			// 
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
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(117, 126);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(39, 13);
			this.label5.TabIndex = 17;
			this.label5.Text = "Output";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(513, 125);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(45, 13);
			this.label6.TabIndex = 18;
			this.label6.Text = "Controls";
			// 
			// tabControl_boards
			// 
			this.tabControl_boards.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl_boards.Controls.Add(this.tabPage1);
			this.tabControl_boards.Enabled = false;
			this.tabControl_boards.ItemSize = new System.Drawing.Size(60, 20);
			this.tabControl_boards.Location = new System.Drawing.Point(2, 288);
			this.tabControl_boards.Name = "tabControl_boards";
			this.tabControl_boards.SelectedIndex = 0;
			this.tabControl_boards.Size = new System.Drawing.Size(584, 287);
			this.tabControl_boards.TabIndex = 41;
			// 
			// tabPage1
			// 
			this.tabPage1.BackColor = System.Drawing.Color.WhiteSmoke;
			this.tabPage1.Controls.Add(this.tableLayoutPanel1);
			this.tabPage1.Location = new System.Drawing.Point(4, 24);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(576, 259);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "DUTs";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.ColumnCount = 5;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel1.Controls.Add(this.label7, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.label8, 3, 0);
			this.tableLayoutPanel1.Controls.Add(this.label9, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.label10, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.label11, 4, 0);
			this.tableLayoutPanel1.Controls.Add(this.label12, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.label13, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.label14, 2, 1);
			this.tableLayoutPanel1.Controls.Add(this.label15, 3, 1);
			this.tableLayoutPanel1.Controls.Add(this.label16, 4, 1);
			this.tableLayoutPanel1.Controls.Add(this.label17, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.label18, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.label19, 2, 2);
			this.tableLayoutPanel1.Controls.Add(this.label20, 3, 2);
			this.tableLayoutPanel1.Controls.Add(this.label21, 4, 2);
			this.tableLayoutPanel1.Controls.Add(this.label22, 4, 3);
			this.tableLayoutPanel1.Controls.Add(this.label23, 3, 3);
			this.tableLayoutPanel1.Controls.Add(this.label24, 2, 3);
			this.tableLayoutPanel1.Controls.Add(this.label25, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.label26, 1, 3);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 4;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(554, 259);
			this.tableLayoutPanel1.TabIndex = 41;
			// 
			// label7
			// 
			this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label7.BackColor = System.Drawing.SystemColors.ControlLight;
			this.label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label7.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.label7.Location = new System.Drawing.Point(3, 0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(104, 64);
			this.label7.TabIndex = 40;
			this.label7.Text = "DUT1";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label8
			// 
			this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label8.BackColor = System.Drawing.SystemColors.ControlLight;
			this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.label8.Location = new System.Drawing.Point(333, 0);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(104, 64);
			this.label8.TabIndex = 43;
			this.label8.Text = "DUT4";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label9
			// 
			this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label9.BackColor = System.Drawing.SystemColors.ControlLight;
			this.label9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.label9.Location = new System.Drawing.Point(223, 0);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(104, 64);
			this.label9.TabIndex = 42;
			this.label9.Text = "DUT3";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label10
			// 
			this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label10.BackColor = System.Drawing.SystemColors.ControlLight;
			this.label10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.label10.Location = new System.Drawing.Point(113, 0);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(104, 64);
			this.label10.TabIndex = 41;
			this.label10.Text = "DUT2";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label11
			// 
			this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label11.BackColor = System.Drawing.SystemColors.ControlLight;
			this.label11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.label11.Location = new System.Drawing.Point(443, 0);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(108, 64);
			this.label11.TabIndex = 44;
			this.label11.Text = "DUT5";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label12
			// 
			this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label12.BackColor = System.Drawing.SystemColors.ControlLight;
			this.label12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label12.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.label12.Location = new System.Drawing.Point(3, 64);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(104, 64);
			this.label12.TabIndex = 45;
			this.label12.Text = "DUT6";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label13
			// 
			this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label13.BackColor = System.Drawing.SystemColors.ControlLight;
			this.label13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.label13.Location = new System.Drawing.Point(113, 64);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(104, 64);
			this.label13.TabIndex = 46;
			this.label13.Text = "DUT7";
			this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label14
			// 
			this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label14.BackColor = System.Drawing.SystemColors.ControlLight;
			this.label14.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.label14.Location = new System.Drawing.Point(223, 64);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(104, 64);
			this.label14.TabIndex = 47;
			this.label14.Text = "DUT8";
			this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label15
			// 
			this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label15.BackColor = System.Drawing.SystemColors.ControlLight;
			this.label15.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.label15.Location = new System.Drawing.Point(333, 64);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(104, 64);
			this.label15.TabIndex = 48;
			this.label15.Text = "DUT9";
			this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label16
			// 
			this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label16.BackColor = System.Drawing.SystemColors.ControlLight;
			this.label16.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.label16.Location = new System.Drawing.Point(443, 64);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(108, 64);
			this.label16.TabIndex = 49;
			this.label16.Text = "DUT10";
			this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label17
			// 
			this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label17.BackColor = System.Drawing.SystemColors.ControlLight;
			this.label17.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.label17.Location = new System.Drawing.Point(3, 128);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(104, 64);
			this.label17.TabIndex = 50;
			this.label17.Text = "DUT11";
			this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label18
			// 
			this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label18.BackColor = System.Drawing.SystemColors.ControlLight;
			this.label18.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.label18.Location = new System.Drawing.Point(113, 128);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(104, 64);
			this.label18.TabIndex = 51;
			this.label18.Text = "DUT12";
			this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label19
			// 
			this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label19.BackColor = System.Drawing.SystemColors.ControlLight;
			this.label19.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label19.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.label19.Location = new System.Drawing.Point(223, 128);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(104, 64);
			this.label19.TabIndex = 52;
			this.label19.Text = "DUT13";
			this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label20
			// 
			this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label20.BackColor = System.Drawing.SystemColors.ControlLight;
			this.label20.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label20.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.label20.Location = new System.Drawing.Point(333, 128);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(104, 64);
			this.label20.TabIndex = 53;
			this.label20.Text = "DUT14";
			this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label21
			// 
			this.label21.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label21.BackColor = System.Drawing.SystemColors.ControlLight;
			this.label21.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label21.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.label21.Location = new System.Drawing.Point(443, 128);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(108, 64);
			this.label21.TabIndex = 54;
			this.label21.Text = "DUT15";
			this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label22
			// 
			this.label22.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label22.BackColor = System.Drawing.SystemColors.ControlLight;
			this.label22.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label22.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.label22.Location = new System.Drawing.Point(443, 192);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(108, 67);
			this.label22.TabIndex = 59;
			this.label22.Text = "DUT20";
			this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label23
			// 
			this.label23.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label23.BackColor = System.Drawing.SystemColors.ControlLight;
			this.label23.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label23.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.label23.Location = new System.Drawing.Point(333, 192);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(104, 67);
			this.label23.TabIndex = 58;
			this.label23.Text = "DUT19";
			this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label24
			// 
			this.label24.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label24.BackColor = System.Drawing.SystemColors.ControlLight;
			this.label24.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label24.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.label24.Location = new System.Drawing.Point(223, 192);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(104, 67);
			this.label24.TabIndex = 57;
			this.label24.Text = "DUT18";
			this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label25
			// 
			this.label25.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label25.BackColor = System.Drawing.SystemColors.ControlLight;
			this.label25.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label25.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.label25.Location = new System.Drawing.Point(3, 192);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(104, 67);
			this.label25.TabIndex = 55;
			this.label25.Text = "DUT16";
			this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label26
			// 
			this.label26.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label26.BackColor = System.Drawing.SystemColors.ControlLight;
			this.label26.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label26.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.label26.Location = new System.Drawing.Point(113, 192);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(104, 67);
			this.label26.TabIndex = 56;
			this.label26.Text = "DUT17";
			this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(598, 575);
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
			this.tabControl_boards.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
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
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.Label label26;
	}
}

