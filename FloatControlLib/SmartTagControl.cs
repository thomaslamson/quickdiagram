using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace FloatControlLib
{
	/// <summary>
	/// Summary description for SmartTagControl.
	/// </summary>
	public class SmartTagControl : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.PictureBox pbTag;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SmartTagControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
			m_tagWindowWidth	= 200;
			m_tagWindowHeight	= 300;
			m_tagControl		= null;
			m_tagWindow			= new SmartTagWindow();
			m_tagWindow.Owner	= this.ParentForm;
			m_tagWindow.HideWindow();
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.pbTag = new System.Windows.Forms.PictureBox();
			this.SuspendLayout();
			// 
			// pbTag
			// 
			this.pbTag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pbTag.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pbTag.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pbTag.Location = new System.Drawing.Point(0, 0);
			this.pbTag.Name = "pbTag";
			this.pbTag.Size = new System.Drawing.Size(18, 18);
			this.pbTag.TabIndex = 0;
			this.pbTag.TabStop = false;
			this.pbTag.Click += new System.EventHandler(this.pbTag_Click);
			// 
			// SmartTagControl
			// 
			this.Controls.Add(this.pbTag);
			this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "SmartTagControl";
			this.Size = new System.Drawing.Size(18, 18);
			this.ResumeLayout(false);

		}
		#endregion

		SmartTagWindow	m_tagWindow;
		Control			m_tagControl;
		int				m_tagWindowWidth;
		int				m_tagWindowHeight;

		private void pbTag_Click(object sender, System.EventArgs e)
		{
			ShowTagWindow();
		}

		public void ShowTagWindow()
		{
			Point	pt = this.PointToScreen(new Point(this.Width, 0));

			m_tagWindow.ShowWindow(pt.X, pt.Y, m_tagWindowWidth, m_tagWindowHeight, m_tagControl);
		}

		public void HideTagWindow()
		{
			m_tagWindow.HideWindow();
		}

		public Image TagIcon
		{
			get
			{
				return pbTag.Image;
			}
			set
			{
				pbTag.Image = value;
			}
		}

		public Control TagControl
		{
			get
			{
				return m_tagControl;
			}
			set
			{
				m_tagControl = value;
			}
		}

		public int TagWindowWidth
		{
			get
			{
				return m_tagWindowWidth;
			}
			set
			{
				m_tagWindowWidth = value;
			}
		}

		public int TagWindowHeight
		{
			get
			{
				return m_tagWindowHeight;
			}
			set
			{
				m_tagWindowHeight = value;
			}
		}
	}
}
