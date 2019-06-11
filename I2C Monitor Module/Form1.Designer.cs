namespace I2C_Monitor_Module
{
	partial class InSituMonitoringModule
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
			this.components = new System.ComponentModel.Container();
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
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label7 = new System.Windows.Forms.Label();
			this.textBox_system = new System.Windows.Forms.TextBox();
			this.textBox_lot = new System.Windows.Forms.TextBox();
			this.textBox_job = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.label_range = new System.Windows.Forms.Label();
			this.label_max = new System.Windows.Forms.Label();
			this.label_min = new System.Windows.Forms.Label();
			this.label_avg = new System.Windows.Forms.Label();
			this.checkBox_debug = new System.Windows.Forms.CheckBox();
			this.label12 = new System.Windows.Forms.Label();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.logDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.configDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.userParametersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolTip_scan = new System.Windows.Forms.ToolTip(this.components);
			this.toolTip_start = new System.Windows.Forms.ToolTip(this.components);
			this.numericUpDown_values = new I2C_Monitor_Module.NumericUpDownEx();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_values)).BeginInit();
			this.SuspendLayout();
			// 
			// button_scan
			// 
			this.button_scan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button_scan.Location = new System.Drawing.Point(612, 160);
			this.button_scan.Name = "button_scan";
			this.button_scan.Size = new System.Drawing.Size(75, 30);
			this.button_scan.TabIndex = 0;
			this.button_scan.Text = "Scan";
			this.button_scan.UseVisualStyleBackColor = true;
			this.button_scan.Click += new System.EventHandler(this.button_scan_Click);
			// 
			// button_select
			// 
			this.button_select.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button_select.Location = new System.Drawing.Point(612, 200);
			this.button_select.Name = "button_select";
			this.button_select.Size = new System.Drawing.Size(75, 30);
			this.button_select.TabIndex = 1;
			this.button_select.Text = "Start";
			this.button_select.UseVisualStyleBackColor = true;
			this.button_select.Click += new System.EventHandler(this.button_select_Click);
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(2, 2);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(101, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Available Aardvarks";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(2, 42);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(91, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Available Beagles";
			// 
			// comboBox_aardvark
			// 
			this.comboBox_aardvark.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBox_aardvark.FormattingEnabled = true;
			this.comboBox_aardvark.Location = new System.Drawing.Point(4, 19);
			this.comboBox_aardvark.Name = "comboBox_aardvark";
			this.comboBox_aardvark.Size = new System.Drawing.Size(102, 21);
			this.comboBox_aardvark.TabIndex = 4;
			// 
			// comboBox_beagle
			// 
			this.comboBox_beagle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBox_beagle.FormattingEnabled = true;
			this.comboBox_beagle.Location = new System.Drawing.Point(4, 59);
			this.comboBox_beagle.Name = "comboBox_beagle";
			this.comboBox_beagle.Size = new System.Drawing.Size(102, 21);
			this.comboBox_beagle.TabIndex = 5;
			// 
			// listBox_active
			// 
			this.listBox_active.FormattingEnabled = true;
			this.listBox_active.Location = new System.Drawing.Point(4, 100);
			this.listBox_active.Name = "listBox_active";
			this.listBox_active.Size = new System.Drawing.Size(102, 95);
			this.listBox_active.TabIndex = 8;
			// 
			// button_i2cread
			// 
			this.button_i2cread.Location = new System.Drawing.Point(431, 278);
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
			this.button_i2cmonitor.Location = new System.Drawing.Point(612, 199);
			this.button_i2cmonitor.Name = "button_i2cmonitor";
			this.button_i2cmonitor.Size = new System.Drawing.Size(75, 30);
			this.button_i2cmonitor.TabIndex = 10;
			this.button_i2cmonitor.Text = "Resume";
			this.button_i2cmonitor.UseVisualStyleBackColor = true;
			this.button_i2cmonitor.Visible = false;
			this.button_i2cmonitor.Click += new System.EventHandler(this.button_i2cmonitor_Click);
			// 
			// button_reset
			// 
			this.button_reset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button_reset.Location = new System.Drawing.Point(612, 236);
			this.button_reset.Name = "button_reset";
			this.button_reset.Size = new System.Drawing.Size(75, 30);
			this.button_reset.TabIndex = 11;
			this.button_reset.Text = "Reset";
			this.button_reset.UseVisualStyleBackColor = true;
			this.button_reset.Click += new System.EventHandler(this.button_reset_Click);
			// 
			// textBox_data
			// 
			this.textBox_data.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.textBox_data.Location = new System.Drawing.Point(124, 154);
			this.textBox_data.Multiline = true;
			this.textBox_data.Name = "textBox_data";
			this.textBox_data.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox_data.Size = new System.Drawing.Size(482, 149);
			this.textBox_data.TabIndex = 12;
			// 
			// button_stop
			// 
			this.button_stop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button_stop.Location = new System.Drawing.Point(612, 198);
			this.button_stop.Name = "button_stop";
			this.button_stop.Size = new System.Drawing.Size(75, 30);
			this.button_stop.TabIndex = 13;
			this.button_stop.Text = "Stop";
			this.button_stop.UseVisualStyleBackColor = true;
			this.button_stop.Visible = false;
			this.button_stop.Click += new System.EventHandler(this.button_stop_Click);
			// 
			// comboBox_config
			// 
			this.comboBox_config.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBox_config.FormattingEnabled = true;
			this.comboBox_config.Location = new System.Drawing.Point(438, 106);
			this.comboBox_config.Name = "comboBox_config";
			this.comboBox_config.Size = new System.Drawing.Size(248, 21);
			this.comboBox_config.TabIndex = 15;
			this.comboBox_config.SelectedIndexChanged += new System.EventHandler(this.comboBox_config_SelectedIndexChanged);
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(435, 91);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(88, 13);
			this.label4.TabIndex = 14;
			this.label4.Text = "Configuration File";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(2, 85);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(37, 13);
			this.label3.TabIndex = 7;
			this.label3.Text = "Active";
			// 
			// label5
			// 
			this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(124, 138);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(45, 13);
			this.label5.TabIndex = 17;
			this.label5.Text = "Output";
			// 
			// label6
			// 
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(613, 137);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(53, 13);
			this.label6.TabIndex = 18;
			this.label6.Text = "Controls";
			// 
			// tabControl_boards
			// 
			this.tabControl_boards.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl_boards.Enabled = false;
			this.tabControl_boards.ItemSize = new System.Drawing.Size(60, 20);
			this.tabControl_boards.Location = new System.Drawing.Point(2, 310);
			this.tabControl_boards.Multiline = true;
			this.tabControl_boards.Name = "tabControl_boards";
			this.tabControl_boards.SelectedIndex = 0;
			this.tabControl_boards.Size = new System.Drawing.Size(693, 255);
			this.tabControl_boards.TabIndex = 41;
			this.tabControl_boards.Visible = false;
			this.tabControl_boards.Resize += new System.EventHandler(this.tabControl_boards_Resize);
			// 
			// pictureBox1
			// 
			this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pictureBox1.Image = global::I2C_Monitor_Module.Properties.Resources.ICE_LOGO;
			this.pictureBox1.Location = new System.Drawing.Point(6, 27);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(205, 60);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 42;
			this.pictureBox1.TabStop = false;
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.listBox_active);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.comboBox_aardvark);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.comboBox_beagle);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Location = new System.Drawing.Point(6, 106);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(112, 198);
			this.panel1.TabIndex = 43;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.Location = new System.Drawing.Point(3, 90);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(53, 13);
			this.label7.TabIndex = 44;
			this.label7.Text = "Devices";
			// 
			// textBox_system
			// 
			this.textBox_system.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox_system.Location = new System.Drawing.Point(438, 57);
			this.textBox_system.Name = "textBox_system";
			this.textBox_system.Size = new System.Drawing.Size(58, 20);
			this.textBox_system.TabIndex = 45;
			// 
			// textBox_lot
			// 
			this.textBox_lot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox_lot.Location = new System.Drawing.Point(566, 57);
			this.textBox_lot.Name = "textBox_lot";
			this.textBox_lot.Size = new System.Drawing.Size(120, 20);
			this.textBox_lot.TabIndex = 46;
			// 
			// textBox_job
			// 
			this.textBox_job.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox_job.Location = new System.Drawing.Point(502, 57);
			this.textBox_job.Name = "textBox_job";
			this.textBox_job.Size = new System.Drawing.Size(58, 20);
			this.textBox_job.TabIndex = 47;
			// 
			// label8
			// 
			this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(435, 41);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(44, 13);
			this.label8.TabIndex = 48;
			this.label8.Text = "System:";
			// 
			// label9
			// 
			this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(499, 41);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(34, 13);
			this.label9.TabIndex = 49;
			this.label9.Text = "Job#:";
			// 
			// label10
			// 
			this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(566, 41);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(32, 13);
			this.label10.TabIndex = 50;
			this.label10.Text = "Lot#:";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label11.Location = new System.Drawing.Point(124, 90);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(34, 13);
			this.label11.TabIndex = 51;
			this.label11.Text = "Data";
			// 
			// panel2
			// 
			this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel2.Controls.Add(this.label_range);
			this.panel2.Controls.Add(this.label_max);
			this.panel2.Controls.Add(this.label_min);
			this.panel2.Controls.Add(this.label_avg);
			this.panel2.Location = new System.Drawing.Point(127, 106);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(253, 21);
			this.panel2.TabIndex = 55;
			// 
			// label_range
			// 
			this.label_range.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label_range.AutoSize = true;
			this.label_range.Location = new System.Drawing.Point(179, 4);
			this.label_range.Name = "label_range";
			this.label_range.Size = new System.Drawing.Size(42, 13);
			this.label_range.TabIndex = 58;
			this.label_range.Text = "Range:";
			// 
			// label_max
			// 
			this.label_max.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label_max.AutoSize = true;
			this.label_max.Location = new System.Drawing.Point(119, 4);
			this.label_max.Name = "label_max";
			this.label_max.Size = new System.Drawing.Size(30, 13);
			this.label_max.TabIndex = 57;
			this.label_max.Text = "Max:";
			// 
			// label_min
			// 
			this.label_min.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label_min.AutoSize = true;
			this.label_min.Location = new System.Drawing.Point(60, 4);
			this.label_min.Name = "label_min";
			this.label_min.Size = new System.Drawing.Size(27, 13);
			this.label_min.TabIndex = 56;
			this.label_min.Text = "Min:";
			// 
			// label_avg
			// 
			this.label_avg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label_avg.AutoSize = true;
			this.label_avg.Location = new System.Drawing.Point(1, 4);
			this.label_avg.Name = "label_avg";
			this.label_avg.Size = new System.Drawing.Size(29, 13);
			this.label_avg.TabIndex = 55;
			this.label_avg.Text = "Avg:";
			// 
			// checkBox_debug
			// 
			this.checkBox_debug.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkBox_debug.AutoSize = true;
			this.checkBox_debug.Location = new System.Drawing.Point(622, 274);
			this.checkBox_debug.Name = "checkBox_debug";
			this.checkBox_debug.Size = new System.Drawing.Size(58, 17);
			this.checkBox_debug.TabIndex = 57;
			this.checkBox_debug.Text = "Debug";
			this.checkBox_debug.UseVisualStyleBackColor = true;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label12.Location = new System.Drawing.Point(384, 91);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(23, 13);
			this.label12.TabIndex = 59;
			this.label12.Text = "Adr";
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(698, 24);
			this.menuStrip1.TabIndex = 61;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logDirectoryToolStripMenuItem,
            this.configDirectoryToolStripMenuItem,
            this.userParametersToolStripMenuItem});
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
			this.editToolStripMenuItem.Text = "Edit";
			// 
			// logDirectoryToolStripMenuItem
			// 
			this.logDirectoryToolStripMenuItem.Name = "logDirectoryToolStripMenuItem";
			this.logDirectoryToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
			this.logDirectoryToolStripMenuItem.Text = "Log Directory";
			this.logDirectoryToolStripMenuItem.Click += new System.EventHandler(this.logDirectoryToolStripMenuItem_Click);
			// 
			// configDirectoryToolStripMenuItem
			// 
			this.configDirectoryToolStripMenuItem.Name = "configDirectoryToolStripMenuItem";
			this.configDirectoryToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
			this.configDirectoryToolStripMenuItem.Text = "Config Directory";
			this.configDirectoryToolStripMenuItem.Click += new System.EventHandler(this.configDirectoryToolStripMenuItem_Click);
			// 
			// userParametersToolStripMenuItem
			// 
			this.userParametersToolStripMenuItem.Name = "userParametersToolStripMenuItem";
			this.userParametersToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
			this.userParametersToolStripMenuItem.Text = "User Parameters";
			this.userParametersToolStripMenuItem.Click += new System.EventHandler(this.userParametersToolStripMenuItem_Click);
			// 
			// numericUpDown_values
			// 
			this.numericUpDown_values.Enabled = false;
			this.numericUpDown_values.Location = new System.Drawing.Point(383, 107);
			this.numericUpDown_values.Name = "numericUpDown_values";
			this.numericUpDown_values.Size = new System.Drawing.Size(52, 20);
			this.numericUpDown_values.TabIndex = 60;
			// 
			// InSituMonitoringModule
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(698, 308);
			this.Controls.Add(this.numericUpDown_values);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.checkBox_debug);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.textBox_job);
			this.Controls.Add(this.textBox_lot);
			this.Controls.Add(this.textBox_system);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.pictureBox1);
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
			this.Controls.Add(this.button_select);
			this.Controls.Add(this.button_scan);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "InSituMonitoringModule";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_values)).EndInit();
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
		public System.Windows.Forms.ComboBox comboBox_config;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		public System.Windows.Forms.TabControl tabControl_boards;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox textBox_system;
		private System.Windows.Forms.TextBox textBox_lot;
		private System.Windows.Forms.TextBox textBox_job;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label_range;
        private System.Windows.Forms.Label label_max;
        private System.Windows.Forms.Label label_min;
        private System.Windows.Forms.Label label_avg;
        private System.Windows.Forms.CheckBox checkBox_debug;
        private System.Windows.Forms.Label label12;
        private NumericUpDownEx numericUpDown_values;
		private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userParametersToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip_scan;
        private System.Windows.Forms.ToolTip toolTip_start;
    }
}

