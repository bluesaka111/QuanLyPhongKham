/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/27/2015
 * Time: 2:36 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Dialog
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public bool runFromApp = false;
		
		public MainForm()
		{
			InitializeComponent();
		}
		
		private void configButtons()
		{
			bt1_panel.Visible = false;
			bt2_panel.Visible = true;
			bt3_panel.Visible = false;
			
			bt2_bt.Text = "OK";
			bt2_bt.DialogResult = DialogResult.OK;
			bt2_bt.Visible = true;
			
			bt1_bt.Visible = false;
			bt3_bt.Visible = false;
			
			panel4.Width = 120;
		}
		
		private void configButtons(MessageBoxButtons mbb)
		{
			switch(mbb)
			{
				case MessageBoxButtons.OK:
				{
					bt1_panel.Visible = false;
					bt2_panel.Visible = true;
					bt3_panel.Visible = false;
						
					bt2_bt.Text = "OK";
					bt2_bt.DialogResult = DialogResult.OK;
					bt2_bt.Visible = true;
					
					bt1_bt.Visible = false;
					bt3_bt.Visible = false;
                    panel4.Width = 120;
					break;
				}
				case MessageBoxButtons.OKCancel:
				{
					bt1_panel.Visible = true;
					bt2_panel.Visible = true;
					bt3_panel.Visible = false;
						
					bt1_bt.Text = "Cancel";
					bt1_bt.DialogResult = DialogResult.Cancel;
					bt1_bt.Visible = true;
					
					bt2_bt.Text = "OK";
					bt2_bt.DialogResult = DialogResult.OK;
					bt2_bt.Visible = true;
					
					bt3_bt.Visible = false;
                    panel4.Width = 240;
					break;
				}
				case MessageBoxButtons.YesNo:
				{
					bt1_panel.Visible = true;
					bt2_panel.Visible = true;
					bt3_panel.Visible = false;
						
					bt1_bt.Text = "No";
					bt1_bt.DialogResult = DialogResult.No;
					bt1_bt.Visible = true;
					
					bt2_bt.Text = "Yes";
					bt2_bt.DialogResult = DialogResult.Yes;
					bt2_bt.Visible = true;
					
					bt3_bt.Visible = false;
                    panel4.Width = 240;
					break;
				}
				case MessageBoxButtons.RetryCancel:
				{
					bt1_panel.Visible = true;
					bt2_panel.Visible = true;
					bt3_panel.Visible = false;
						
					bt1_bt.Text = "Cancel";
					bt1_bt.DialogResult = DialogResult.Cancel;
					bt1_bt.Visible = true;
					
					bt2_bt.Text = "Retry";
					bt2_bt.DialogResult = DialogResult.Retry;
					bt2_bt.Visible = true;
					
					bt3_bt.Visible = false;
                    panel4.Width = 240;
					break;
				}
				case MessageBoxButtons.YesNoCancel:
				{
					bt1_panel.Visible = true;
					bt2_panel.Visible = true;
					bt3_panel.Visible = true;
						
					bt1_bt.Text = "No";
					bt1_bt.DialogResult = DialogResult.No;
					bt1_bt.Visible = true;
					
					bt2_bt.Text = "Cancel";
					bt2_bt.DialogResult = DialogResult.Cancel;
					bt2_bt.Visible = true;
					
					bt3_bt.Text = "Yes";
					bt3_bt.DialogResult = DialogResult.Yes;
					bt3_bt.Visible = true;
                    panel4.Width = 360;
					break;
				}
				case MessageBoxButtons.AbortRetryIgnore:
				{
					bt1_panel.Visible = true;
					bt2_panel.Visible = true;
					bt3_panel.Visible = true;
						
					bt1_bt.Text = "Abort";
					bt1_bt.DialogResult = DialogResult.Abort;
					bt1_bt.Visible = true;
					
					bt2_bt.Text = "Retry";
					bt2_bt.DialogResult = DialogResult.Retry;
					bt2_bt.Visible = true;
					
					bt3_bt.Text = "Ignore";
					bt3_bt.DialogResult = DialogResult.Ignore;
					bt3_bt.Visible = true;
                    panel4.Width = 360;
					break;
				}
			}
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			if(!runFromApp)
			{
				this.Close();
			}
			else
			{
				title_lb.Text = setTitle;
				content_txt.Text = setContain.Trim();
			}
		}
		
		private DialogResult temp = new DialogResult();
		
		private string setTitle = null;
		private string setContain = null;
		
		#region ShowDialog replace
		
		public static DialogResult DShowDialog(MessageBoxButtons mbb, string title, string contain)
		{
			Dialog.MainForm dia = new Dialog.MainForm();
			dia.configButtons(mbb);
			dia.setTitle = title;
			dia.setContain = contain;
			dia.runFromApp = true;
			dia.ShowDialog();
			return dia.temp;
		}
		
		public static DialogResult DShowDialog(string title, string contain)
		{
			Dialog.MainForm dia = new Dialog.MainForm();
			dia.configButtons();
			dia.setTitle = title;
			dia.setContain = contain;
			dia.runFromApp = true;
			dia.ShowDialog();
			return dia.temp;
		}
		
		public static DialogResult DShowDialog(MessageBoxButtons mbb, string contain)
		{
			Dialog.MainForm dia = new Dialog.MainForm();
			dia.configButtons(mbb);
			dia.setTitle = String.Empty;
			dia.setContain = contain;
			dia.runFromApp = true;
			dia.ShowDialog();
			return dia.temp;
		}
		
		public static DialogResult DShowDialog(string contain)
		{
			Dialog.MainForm dia = new Dialog.MainForm();
			dia.configButtons();
			dia.setTitle = String.Empty;
			dia.setContain = contain;
			dia.runFromApp = true;
			dia.ShowDialog();
			return dia.temp;
		}
		
		public static DialogResult DShowDialog()
		{
			Dialog.MainForm dia = new Dialog.MainForm();
			dia.configButtons();
			dia.content_txt.Visible = false;
			dia.runFromApp = true;
			dia.ShowDialog();
			return dia.temp;
		}

		#endregion
		
		#region Show replace
	
		public static DialogResult DShow(MessageBoxButtons mbb, string title, string contain)
		{
			Dialog.MainForm dia = new Dialog.MainForm();
			dia.configButtons(mbb);
			dia.setTitle = title;
			dia.setContain = contain;
			dia.runFromApp = true;
			dia.Show();
			return dia.temp;
		}
		
		public static DialogResult DShow(string title, string contain)
		{
			Dialog.MainForm dia = new Dialog.MainForm();
			dia.configButtons();
			dia.setTitle = title;
			dia.setContain = contain;
			dia.runFromApp = true;
			dia.Show();
			return dia.temp;
		}
		
		public static DialogResult DShow(MessageBoxButtons mbb, string contain)
		{
			Dialog.MainForm dia = new Dialog.MainForm();
			dia.configButtons(mbb);
			dia.setTitle = String.Empty;
			dia.setContain = contain;
			dia.runFromApp = true;
			dia.Show();
			return dia.temp;
		}
		
		public static DialogResult DShow(string contain)
		{
			Dialog.MainForm dia = new Dialog.MainForm();
			dia.configButtons();
			dia.setTitle = String.Empty;
			dia.setContain = contain;
			dia.runFromApp = true;
			dia.Show();
			return dia.temp;
		}
		
		public static DialogResult DShow()
		{
			Dialog.MainForm dia = new Dialog.MainForm();
			dia.configButtons();
			dia.content_txt.Visible = false;
			dia.runFromApp = true;
			dia.Show();
			return dia.temp;
		}

		#endregion
		
		private void Bt_btClick(object sender, EventArgs e)
		{
			this.temp = (sender as Button).DialogResult;
		}
	}
}
