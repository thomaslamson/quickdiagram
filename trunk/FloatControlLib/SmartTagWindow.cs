using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace FloatControlLib
{
	/// <summary>
	/// Summary description for SmartTagWindow.
	/// </summary>
	public class SmartTagWindow : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SmartTagWindow()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			m_tagControl = null;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// SmartTagWindow
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.ClientSize = new System.Drawing.Size(176, 232);
			this.DockPadding.All = 1;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "SmartTagWindow";
			this.ShowInTaskbar = false;
			this.Text = "SmartTagWindow";
			this.TopMost = true;
			this.Deactivate += new System.EventHandler(this.SmartTagWindow_Deactivate);

		}
		#endregion

		Control	m_tagControl;

		private void SmartTagWindow_Deactivate(object sender, System.EventArgs e)
		{
			HideWindow();
		}

		public void ShowWindow(int x, int y, int width, int height, Control tagControl)
		{
			this.Width		= 0;
			this.Height		= 0;
			this.Visible	= true;

			m_tagControl	= tagControl;

			if (tagControl != null)
			{
				tagControl.Parent	= this;
				tagControl.Dock		= DockStyle.Fill;
				tagControl.Visible	= true;
				tagControl.Enabled	= true;
			}
			
			this.Left		= x;
			this.Top		= y;
			this.Width		= width;
			this.Height		= height;

			if (this.Bottom > Screen.PrimaryScreen.Bounds.Bottom)
			{
				this.Top = Screen.PrimaryScreen.Bounds.Bottom - this.Height;
			}

			if (this.Right > Screen.PrimaryScreen.Bounds.Right)
			{
				this.Left = Screen.PrimaryScreen.Bounds.Right - this.Width;
			}
			
			this.PerformLayout();
			this.Activate();
		}

		public void HideWindow()
		{
			this.Width		= 0;
			this.Height		= 0;
			this.Visible	= false;

			if (m_tagControl != null)
			{
				m_tagControl.Parent = null;
				m_tagControl		= null;
			}
		}
	}
}
