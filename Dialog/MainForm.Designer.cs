/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/27/2015
 * Time: 2:36 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Dialog
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Panel bt3_panel;
		private System.Windows.Forms.Button bt3_bt;
		private System.Windows.Forms.Panel bt2_panel;
		private System.Windows.Forms.Button bt2_bt;
		private System.Windows.Forms.Panel bt1_panel;
		private System.Windows.Forms.Button bt1_bt;
		private System.Windows.Forms.TextBox content_txt;
		private System.Windows.Forms.Panel panel8;
		private System.Windows.Forms.Panel panel9;
		private System.Windows.Forms.Label title_lb;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.content_txt = new System.Windows.Forms.TextBox();
            this.panel9 = new System.Windows.Forms.Panel();
            this.title_lb = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.bt1_panel = new System.Windows.Forms.Panel();
            this.bt1_bt = new System.Windows.Forms.Button();
            this.bt2_panel = new System.Windows.Forms.Panel();
            this.bt2_bt = new System.Windows.Forms.Button();
            this.bt3_panel = new System.Windows.Forms.Panel();
            this.bt3_bt = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.bt1_panel.SuspendLayout();
            this.bt2_panel.SuspendLayout();
            this.bt3_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Highlight;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel8);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(1);
            this.panel1.Size = new System.Drawing.Size(744, 339);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(28)))));
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panel3.Controls.Add(this.content_txt);
            this.panel3.Controls.Add(this.panel9);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.Location = new System.Drawing.Point(1, 37);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(10);
            this.panel3.Size = new System.Drawing.Size(740, 272);
            this.panel3.TabIndex = 1;
            // 
            // content_txt
            // 
            this.content_txt.AcceptsReturn = true;
            this.content_txt.AcceptsTab = true;
            this.content_txt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(28)))));
            this.content_txt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.content_txt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.content_txt.ForeColor = System.Drawing.Color.White;
            this.content_txt.Location = new System.Drawing.Point(10, 55);
            this.content_txt.Multiline = true;
            this.content_txt.Name = "content_txt";
            this.content_txt.ReadOnly = true;
            this.content_txt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.content_txt.Size = new System.Drawing.Size(720, 207);
            this.content_txt.TabIndex = 0;
            this.content_txt.TabStop = false;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.title_lb);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(168)))));
            this.panel9.Location = new System.Drawing.Point(10, 10);
            this.panel9.Margin = new System.Windows.Forms.Padding(0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(720, 45);
            this.panel9.TabIndex = 1;
            // 
            // title_lb
            // 
            this.title_lb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.title_lb.Location = new System.Drawing.Point(0, 0);
            this.title_lb.Name = "title_lb";
            this.title_lb.Size = new System.Drawing.Size(720, 45);
            this.title_lb.TabIndex = 0;
            this.title_lb.Text = "label2";
            this.title_lb.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(68)))));
            this.panel8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(168)))));
            this.panel8.Location = new System.Drawing.Point(1, 309);
            this.panel8.Name = "panel8";
            this.panel8.Padding = new System.Windows.Forms.Padding(5);
            this.panel8.Size = new System.Drawing.Size(740, 27);
            this.panel8.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(1, 1);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(740, 36);
            this.panel2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(68)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(168)))));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.label1.Size = new System.Drawing.Size(380, 36);
            this.label1.TabIndex = 0;
            this.label1.Text = "Application Dialog";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(28)))));
            this.panel4.Controls.Add(this.bt1_panel);
            this.panel4.Controls.Add(this.bt2_panel);
            this.panel4.Controls.Add(this.bt3_panel);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(380, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(360, 36);
            this.panel4.TabIndex = 1;
            // 
            // bt1_panel
            // 
            this.bt1_panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.bt1_panel.Controls.Add(this.bt1_bt);
            this.bt1_panel.Dock = System.Windows.Forms.DockStyle.Right;
            this.bt1_panel.Location = new System.Drawing.Point(0, 0);
            this.bt1_panel.Name = "bt1_panel";
            this.bt1_panel.Padding = new System.Windows.Forms.Padding(5);
            this.bt1_panel.Size = new System.Drawing.Size(120, 36);
            this.bt1_panel.TabIndex = 2;
            // 
            // bt1_bt
            // 
            this.bt1_bt.BackColor = System.Drawing.Color.White;
            this.bt1_bt.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.bt1_bt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bt1_bt.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.bt1_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bt1_bt.Location = new System.Drawing.Point(5, 5);
            this.bt1_bt.Name = "bt1_bt";
            this.bt1_bt.Size = new System.Drawing.Size(110, 26);
            this.bt1_bt.TabIndex = 0;
            this.bt1_bt.TabStop = false;
            this.bt1_bt.Text = "Abort";
            this.bt1_bt.UseVisualStyleBackColor = false;
            this.bt1_bt.Click += new System.EventHandler(this.Bt_btClick);
            // 
            // bt2_panel
            // 
            this.bt2_panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.bt2_panel.Controls.Add(this.bt2_bt);
            this.bt2_panel.Dock = System.Windows.Forms.DockStyle.Right;
            this.bt2_panel.Location = new System.Drawing.Point(120, 0);
            this.bt2_panel.Name = "bt2_panel";
            this.bt2_panel.Padding = new System.Windows.Forms.Padding(5);
            this.bt2_panel.Size = new System.Drawing.Size(120, 36);
            this.bt2_panel.TabIndex = 1;
            // 
            // bt2_bt
            // 
            this.bt2_bt.BackColor = System.Drawing.Color.White;
            this.bt2_bt.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bt2_bt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bt2_bt.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.bt2_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bt2_bt.Location = new System.Drawing.Point(5, 5);
            this.bt2_bt.Name = "bt2_bt";
            this.bt2_bt.Size = new System.Drawing.Size(110, 26);
            this.bt2_bt.TabIndex = 0;
            this.bt2_bt.TabStop = false;
            this.bt2_bt.Text = "Cancel";
            this.bt2_bt.UseVisualStyleBackColor = false;
            this.bt2_bt.Click += new System.EventHandler(this.Bt_btClick);
            // 
            // bt3_panel
            // 
            this.bt3_panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.bt3_panel.Controls.Add(this.bt3_bt);
            this.bt3_panel.Dock = System.Windows.Forms.DockStyle.Right;
            this.bt3_panel.Location = new System.Drawing.Point(240, 0);
            this.bt3_panel.Name = "bt3_panel";
            this.bt3_panel.Padding = new System.Windows.Forms.Padding(5);
            this.bt3_panel.Size = new System.Drawing.Size(120, 36);
            this.bt3_panel.TabIndex = 0;
            // 
            // bt3_bt
            // 
            this.bt3_bt.BackColor = System.Drawing.Color.White;
            this.bt3_bt.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bt3_bt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bt3_bt.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.bt3_bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bt3_bt.Location = new System.Drawing.Point(5, 5);
            this.bt3_bt.Name = "bt3_bt";
            this.bt3_bt.Size = new System.Drawing.Size(110, 26);
            this.bt3_bt.TabIndex = 0;
            this.bt3_bt.TabStop = false;
            this.bt3_bt.Text = "OK";
            this.bt3_bt.UseVisualStyleBackColor = false;
            this.bt3_bt.Click += new System.EventHandler(this.Bt_btClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(744, 339);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Dialog";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.bt1_panel.ResumeLayout(false);
            this.bt2_panel.ResumeLayout(false);
            this.bt3_panel.ResumeLayout(false);
            this.ResumeLayout(false);

		}
    }
}
