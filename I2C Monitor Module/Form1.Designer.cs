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
			this.label3 = new System.Windows.Forms.Label();
			this.listBox_active = new System.Windows.Forms.ListBox();
			this.button_i2cread = new System.Windows.Forms.Button();
			this.button_i2cmonitor = new System.Windows.Forms.Button();
			this.button_reset = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// button_scan
			// 
			this.button_scan.Location = new System.Drawing.Point(269, 158);
			this.button_scan.Name = "button_scan";
			this.button_scan.Size = new System.Drawing.Size(75, 23);
			this.button_scan.TabIndex = 0;
			this.button_scan.Text = "Scan";
			this.button_scan.UseVisualStyleBackColor = true;
			this.button_scan.Click += new System.EventHandler(this.button_scan_Click);
			// 
			// button_select
			// 
			this.button_select.Location = new System.Drawing.Point(350, 158);
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
			this.label1.Location = new System.Drawing.Point(283, 50);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(101, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Available Aardvarks";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(286, 96);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(91, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Available Beagles";
			// 
			// comboBox_aardvark
			// 
			this.comboBox_aardvark.FormattingEnabled = true;
			this.comboBox_aardvark.Location = new System.Drawing.Point(286, 67);
			this.comboBox_aardvark.Name = "comboBox_aardvark";
			this.comboBox_aardvark.Size = new System.Drawing.Size(121, 21);
			this.comboBox_aardvark.TabIndex = 4;
			// 
			// comboBox_beagle
			// 
			this.comboBox_beagle.FormattingEnabled = true;
			this.comboBox_beagle.Location = new System.Drawing.Point(286, 113);
			this.comboBox_beagle.Name = "comboBox_beagle";
			this.comboBox_beagle.Size = new System.Drawing.Size(121, 21);
			this.comboBox_beagle.TabIndex = 5;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(9, 12);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(46, 13);
			this.label3.TabIndex = 7;
			this.label3.Text = "Devices";
			// 
			// listBox_active
			// 
			this.listBox_active.FormattingEnabled = true;
			this.listBox_active.Location = new System.Drawing.Point(12, 29);
			this.listBox_active.Name = "listBox_active";
			this.listBox_active.Size = new System.Drawing.Size(120, 95);
			this.listBox_active.TabIndex = 8;
			// 
			// button_i2cread
			// 
			this.button_i2cread.Location = new System.Drawing.Point(269, 220);
			this.button_i2cread.Name = "button_i2cread";
			this.button_i2cread.Size = new System.Drawing.Size(75, 23);
			this.button_i2cread.TabIndex = 9;
			this.button_i2cread.Text = "i2cread";
			this.button_i2cread.UseVisualStyleBackColor = true;
			this.button_i2cread.Click += new System.EventHandler(this.button1_Click);
			// 
			// button_i2cmonitor
			// 
			this.button_i2cmonitor.Location = new System.Drawing.Point(350, 220);
			this.button_i2cmonitor.Name = "button_i2cmonitor";
			this.button_i2cmonitor.Size = new System.Drawing.Size(75, 23);
			this.button_i2cmonitor.TabIndex = 10;
			this.button_i2cmonitor.Text = "i2cmonitor";
			this.button_i2cmonitor.UseVisualStyleBackColor = true;
			this.button_i2cmonitor.Click += new System.EventHandler(this.button_i2cmonitor_Click);
			// 
			// button_reset
			// 
			this.button_reset.Location = new System.Drawing.Point(350, 191);
			this.button_reset.Name = "button_reset";
			this.button_reset.Size = new System.Drawing.Size(75, 23);
			this.button_reset.TabIndex = 11;
			this.button_reset.Text = "buffer reset";
			this.button_reset.UseVisualStyleBackColor = true;
			this.button_reset.Click += new System.EventHandler(this.button_reset_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(437, 261);
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
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ListBox listBox_active;
		private System.Windows.Forms.Button button_i2cread;
		private System.Windows.Forms.Button button_i2cmonitor;
		private System.Windows.Forms.Button button_reset;
	}
}

