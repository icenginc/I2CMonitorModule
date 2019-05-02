namespace I2C_Monitor_Module
{
	partial class Form2
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.baord = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.bib_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridView2 = new System.Windows.Forms.DataGridView();
			this.address = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.hi = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.lo = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.button_ok = new System.Windows.Forms.Button();
			this.button_cancel = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToDeleteRows = false;
			dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.baord,
            this.bib_num});
			dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle14;
			this.dataGridView1.Location = new System.Drawing.Point(18, 13);
			this.dataGridView1.Name = "dataGridView1";
			dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle15;
			this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.dataGridView1.Size = new System.Drawing.Size(145, 150);
			this.dataGridView1.TabIndex = 0;
			// 
			// baord
			// 
			this.baord.HeaderText = "Board";
			this.baord.Name = "baord";
			this.baord.ReadOnly = true;
			this.baord.Width = 42;
			// 
			// bib_num
			// 
			this.bib_num.HeaderText = "BiB#";
			this.bib_num.Name = "bib_num";
			this.bib_num.Width = 60;
			// 
			// dataGridView2
			// 
			this.dataGridView2.AllowUserToDeleteRows = false;
			dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle16;
			this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.address,
            this.hi,
            this.lo});
			dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridView2.DefaultCellStyle = dataGridViewCellStyle17;
			this.dataGridView2.Location = new System.Drawing.Point(190, 13);
			this.dataGridView2.Name = "dataGridView2";
			dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridView2.RowHeadersDefaultCellStyle = dataGridViewCellStyle18;
			this.dataGridView2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.dataGridView2.Size = new System.Drawing.Size(193, 150);
			this.dataGridView2.TabIndex = 1;
			// 
			// address
			// 
			this.address.HeaderText = "Address";
			this.address.Name = "address";
			this.address.ReadOnly = true;
			this.address.Width = 50;
			// 
			// hi
			// 
			this.hi.HeaderText = "Hi";
			this.hi.Name = "hi";
			this.hi.Width = 50;
			// 
			// lo
			// 
			this.lo.HeaderText = "Lo";
			this.lo.Name = "lo";
			this.lo.Width = 50;
			// 
			// button_ok
			// 
			this.button_ok.Location = new System.Drawing.Point(207, 183);
			this.button_ok.Name = "button_ok";
			this.button_ok.Size = new System.Drawing.Size(85, 25);
			this.button_ok.TabIndex = 3;
			this.button_ok.Text = "OK";
			this.button_ok.UseVisualStyleBackColor = true;
			this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
			// 
			// button_cancel
			// 
			this.button_cancel.Location = new System.Drawing.Point(298, 183);
			this.button_cancel.Name = "button_cancel";
			this.button_cancel.Size = new System.Drawing.Size(85, 25);
			this.button_cancel.TabIndex = 4;
			this.button_cancel.Text = "Cancel";
			this.button_cancel.UseVisualStyleBackColor = true;
			this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
			// 
			// Form2
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(407, 220);
			this.Controls.Add(this.button_cancel);
			this.Controls.Add(this.button_ok);
			this.Controls.Add(this.dataGridView2);
			this.Controls.Add(this.dataGridView1);
			this.Name = "Form2";
			this.Text = "Form2";
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.DataGridView dataGridView2;
		private System.Windows.Forms.DataGridViewTextBoxColumn address;
		private System.Windows.Forms.DataGridViewTextBoxColumn hi;
		private System.Windows.Forms.DataGridViewTextBoxColumn lo;
		private System.Windows.Forms.DataGridViewTextBoxColumn baord;
		private System.Windows.Forms.DataGridViewTextBoxColumn bib_num;
		private System.Windows.Forms.Button button_ok;
		private System.Windows.Forms.Button button_cancel;
	}
}