using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace FloatControlLib
{
	/// <summary>
	/// The floating window used in the float bar control
	/// </summary>
	public class FloatWindow : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel plTitle;
		private System.Windows.Forms.PictureBox pbIcon;
		private System.Windows.Forms.Label lbTitle;
		private System.Windows.Forms.PictureBox pbClose;
		private System.Windows.Forms.PictureBox pbPin;
		private System.Windows.Forms.Timer tmFloat;
		private System.Windows.Forms.Panel plSpacer;
		private System.ComponentModel.IContainer components;
		/// <summary>
		/// The constructor of the floating window
		/// </summary>
		public FloatWindow()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			m_floatItem		= null;
			m_bShowForm		= false;
			m_iShowRatio	= 0;
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
			this.components = new System.ComponentModel.Container();
			this.plTitle = new System.Windows.Forms.Panel();
			this.pbPin = new System.Windows.Forms.PictureBox();
			this.pbClose = new System.Windows.Forms.PictureBox();
			this.lbTitle = new System.Windows.Forms.Label();
			this.pbIcon = new System.Windows.Forms.PictureBox();
			this.tmFloat = new System.Windows.Forms.Timer(this.components);
			this.plSpacer = new System.Windows.Forms.Panel();
			this.plTitle.SuspendLayout();
			this.SuspendLayout();
			// 
			// plTitle
			// 
			this.plTitle.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.plTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.plTitle.Controls.Add(this.pbPin);
			this.plTitle.Controls.Add(this.pbClose);
			this.plTitle.Controls.Add(this.lbTitle);
			this.plTitle.Controls.Add(this.pbIcon);
			this.plTitle.Dock = System.Windows.Forms.DockStyle.Top;
			this.plTitle.DockPadding.All = 2;
			this.plTitle.Location = new System.Drawing.Point(2, 2);
			this.plTitle.Name = "plTitle";
			this.plTitle.Size = new System.Drawing.Size(178, 20);
			this.plTitle.TabIndex = 0;
			// 
			// pbPin
			// 
			this.pbPin.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pbPin.Dock = System.Windows.Forms.DockStyle.Right;
			this.pbPin.Location = new System.Drawing.Point(142, 2);
			this.pbPin.Name = "pbPin";
			this.pbPin.Size = new System.Drawing.Size(16, 14);
			this.pbPin.TabIndex = 3;
			this.pbPin.TabStop = false;
			this.pbPin.Click += new System.EventHandler(this.pbPin_Click);
			this.pbPin.Paint += new System.Windows.Forms.PaintEventHandler(this.pbPin_Paint);
			// 
			// pbClose
			// 
			this.pbClose.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pbClose.Dock = System.Windows.Forms.DockStyle.Right;
			this.pbClose.Location = new System.Drawing.Point(158, 2);
			this.pbClose.Name = "pbClose";
			this.pbClose.Size = new System.Drawing.Size(16, 14);
			this.pbClose.TabIndex = 2;
			this.pbClose.TabStop = false;
			this.pbClose.Click += new System.EventHandler(this.pbClose_Click);
			this.pbClose.Paint += new System.Windows.Forms.PaintEventHandler(this.pbClose_Paint);
			// 
			// lbTitle
			// 
			this.lbTitle.AutoSize = true;
			this.lbTitle.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbTitle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lbTitle.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.lbTitle.Location = new System.Drawing.Point(18, 2);
			this.lbTitle.Name = "lbTitle";
			this.lbTitle.Size = new System.Drawing.Size(26, 17);
			this.lbTitle.TabIndex = 1;
			this.lbTitle.Text = "Title";
			this.lbTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pbIcon
			// 
			this.pbIcon.Dock = System.Windows.Forms.DockStyle.Left;
			this.pbIcon.Location = new System.Drawing.Point(2, 2);
			this.pbIcon.Name = "pbIcon";
			this.pbIcon.Size = new System.Drawing.Size(16, 14);
			this.pbIcon.TabIndex = 0;
			this.pbIcon.TabStop = false;
			// 
			// tmFloat
			// 
			this.tmFloat.Interval = 10;
			this.tmFloat.Tick += new System.EventHandler(this.tmFloat_Tick);
			// 
			// plSpacer
			// 
			this.plSpacer.Dock = System.Windows.Forms.DockStyle.Top;
			this.plSpacer.Location = new System.Drawing.Point(2, 22);
			this.plSpacer.Name = "plSpacer";
			this.plSpacer.Size = new System.Drawing.Size(178, 4);
			this.plSpacer.TabIndex = 4;
			// 
			// FloatWindow
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(184, 448);
			this.Controls.Add(this.plSpacer);
			this.Controls.Add(this.plTitle);
			this.DockPadding.Bottom = 2;
			this.DockPadding.Left = 2;
			this.DockPadding.Right = 4;
			this.DockPadding.Top = 2;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "FloatWindow";
			this.ShowInTaskbar = false;
			this.Text = "FloatWindow";
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.FloatWindow_Paint);
			this.Deactivate += new System.EventHandler(this.FloatWindow_Deactivate);
			this.plTitle.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>The float item to be docked in the window</summary>
		private FloatItem	m_floatItem;
		/// <summary>Determine whether the animation operation should show or hide the form</summary>
		private bool		m_bShowForm;
		/// <summary>The progress of current animation operation</summary>
		private int			m_iShowRatio;
		/// <summary>
		/// The float item to be docked in the window
		/// </summary>
		public FloatItem ActiveItem
		{
			get
			{
				return m_floatItem;
			}
		}
		/// <summary>
		/// Do some custom painting for the pin button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pbPin_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			System.Drawing.Pen	pen = new Pen(System.Drawing.Color.FromName("ActiveCaptionText"));

			e.Graphics.DrawLine(pen, 0, 7, 3, 7);
			e.Graphics.DrawLine(pen, 4, 2, 4, 12);
			e.Graphics.DrawLine(pen, 5, 8, 12, 8);
			e.Graphics.DrawLine(pen, 5, 9, 12, 9);
			e.Graphics.DrawLine(pen, 5, 5, 12, 5);
			e.Graphics.DrawLine(pen, 12, 5, 12, 9);
		}
		/// <summary>
		/// Do some custom painting for the close button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pbClose_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			System.Drawing.Pen	pen = new Pen(System.Drawing.Color.FromName("ActiveCaptionText"));

			e.Graphics.DrawLine(pen, 3, 3, 12, 12);
			e.Graphics.DrawLine(pen, 12, 3, 3, 12);
		}
		/// <summary>
		/// Do some custom painting for the floating window
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FloatWindow_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			if (m_floatItem != null)
			{
				switch (m_floatItem.m_bar.Dock)
				{
					case DockStyle.Top:
					{
						e.Graphics.DrawLine(System.Drawing.Pens.Gray, 0, this.Height - 2, this.Width - 1, this.Height - 2);
						e.Graphics.DrawLine(System.Drawing.Pens.Black, 0, this.Height - 1, this.Width - 1, this.Height - 1);
						break;
					}
					case DockStyle.Bottom:
					{
						e.Graphics.DrawLine(System.Drawing.Pens.Gray, 0, 1, this.Width - 1, 1);
						e.Graphics.DrawLine(System.Drawing.Pens.Black, 0, 0, this.Width - 1, 0);
						break;
					}
					case DockStyle.Left:
					{
						e.Graphics.DrawLine(System.Drawing.Pens.Gray, this.Width - 2, 0, this.Width - 2, this.Height);
						e.Graphics.DrawLine(System.Drawing.Pens.Black, this.Width - 1, 0, this.Width - 1, this.Height);
						break;
					}
					case DockStyle.Right:
					{
						e.Graphics.DrawLine(System.Drawing.Pens.Gray, 1, 0, 1, this.Height);
						e.Graphics.DrawLine(System.Drawing.Pens.Black, 0, 0, 0, this.Height);
						break;
					}
				}
			}
		}
		/// <summary>
		/// Show the floating window
		/// </summary>
		/// <param name="item">The float item to be docked in</param>
		public void AnimateShowFloatWindow(FloatItem item)
		{
			if (m_floatItem != null)
			{
				m_floatItem.m_bar.FireHiding(m_floatItem);
			}

			m_iShowRatio	= 0;
			m_bShowForm		= true;
			m_floatItem		= item;

			this.pbIcon.Image	= item.m_icon;
			this.lbTitle.Text	= item.m_caption;
			this.Owner			= item.m_bar.ParentForm;
			this.Width			= 0;
			this.Height			= 0;

			item.m_control.Parent	= this;
			item.m_control.Visible	= true;
			item.m_control.Dock		= DockStyle.None;
			item.m_control.Enabled	= false;
			item.m_control.BringToFront();
			
			this.tmFloat.Enabled = true;
		}
		/// <summary>
		/// Show the float window step by step
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tmFloat_Tick(object sender, System.EventArgs e)
		{
			if (m_floatItem != null)
			{
				Point		pt = m_floatItem.m_bar.PointToScreen(new Point(0, 0));
				Rectangle	rc = new Rectangle();

				if (this.m_bShowForm)
				{
					m_iShowRatio += 1;
				}
				else
				{
					m_iShowRatio -= 1;
				}

				this.Visible	= true;

				switch (m_floatItem.m_bar.Dock)
				{
					case DockStyle.Top:
					{
						rc.X		= pt.X;
						rc.Y		= pt.Y + m_floatItem.m_bar.Height;
						rc.Height	= m_floatItem.m_floatSize * m_iShowRatio / 10;
						rc.Width	= m_floatItem.m_bar.Width;
						this.Bounds = rc;

						m_floatItem.m_control.Left		= 0;
						m_floatItem.m_control.Top		= this.ClientSize.Height - m_floatItem.m_floatSize - plSpacer.Bottom;
						m_floatItem.m_control.Height	= m_floatItem.m_floatSize - plSpacer.Bottom;
						m_floatItem.m_control.Width		= this.ClientSize.Width;

						break;
					}
					case DockStyle.Bottom:
					{
						rc.X		= pt.X;
						rc.Y		= pt.Y - m_floatItem.m_floatSize * m_iShowRatio / 10;
						rc.Height	= m_floatItem.m_floatSize * m_iShowRatio / 10;
						rc.Width	= m_floatItem.m_bar.Width;
						this.Bounds = rc;

						m_floatItem.m_control.Left		= 0;
						m_floatItem.m_control.Top		= 0;
						m_floatItem.m_control.Height	= m_floatItem.m_floatSize - plSpacer.Bottom;
						m_floatItem.m_control.Width		= this.ClientSize.Width;

						break;
					}
					case DockStyle.Left:
					{
						rc.X		= pt.X + m_floatItem.m_bar.Width;
						rc.Y		= pt.Y;
						rc.Height	= m_floatItem.m_bar.Height;
						rc.Width	= m_floatItem.m_floatSize * m_iShowRatio / 10;
						this.Bounds = rc;

						m_floatItem.m_control.Left		= this.ClientSize.Width - m_floatItem.m_floatSize;
						m_floatItem.m_control.Top		= plSpacer.Bottom;
						m_floatItem.m_control.Height	= this.ClientSize.Height - plSpacer.Bottom;
						m_floatItem.m_control.Width		= m_floatItem.m_floatSize;

						break;
					}
					case DockStyle.Right:
					{
						rc.X		= pt.X - m_floatItem.m_floatSize * m_iShowRatio / 10;
						rc.Y		= pt.Y;
						rc.Height	= m_floatItem.m_bar.Height;
						rc.Width	= m_floatItem.m_floatSize * m_iShowRatio / 10;
						this.Bounds = rc;

						m_floatItem.m_control.Left		= 0;
						m_floatItem.m_control.Top		= plSpacer.Bottom;
						m_floatItem.m_control.Height	= this.ClientSize.Height - plSpacer.Bottom;
						m_floatItem.m_control.Width		= m_floatItem.m_floatSize;

						break;
					}
				}

				this.Refresh();

				if (m_iShowRatio == 11)
				{
					m_floatItem.m_control.Dock		= System.Windows.Forms.DockStyle.Fill;
					m_floatItem.m_control.Enabled	= true;
					tmFloat.Enabled = false;
				}
						
				if (m_iShowRatio == 0)
				{
					tmFloat.Enabled = false;
					this.Visible = false;
					m_floatItem.m_control.Parent	= null;
					m_floatItem.m_control.Enabled	= true;
					m_floatItem = null;
				}
			}
		}
		/// <summary>
		/// Hide the float window when it is deactivated
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FloatWindow_Deactivate(object sender, System.EventArgs e)
		{
			AnimateHideFloatWindow();
		}
		/// <summary>
		/// Hide the float window
		/// </summary>
		public void AnimateHideFloatWindow()
		{
			if (this.Visible)
			{
				if (m_floatItem != null)
				{
					m_floatItem.m_bar.FireHiding(m_floatItem);
					m_floatItem.m_control.Dock = DockStyle.None;
					m_floatItem.m_control.Enabled = false;
				}

				m_iShowRatio	= 10;
				m_bShowForm		= false;
				tmFloat.Enabled	= true;
			}
		}
		/// <summary>
		/// Hide the float window immediately
		/// </summary>
		public void HideFloatWindow()
		{
			if (m_floatItem != null)
			{
				m_floatItem.m_bar.FireHiding(m_floatItem);
			}

			this.Width		= 0;
			this.Height		= 0;
			this.Visible	= false;

			if (m_floatItem != null)
			{
				m_floatItem.m_control.Parent = null;
				m_floatItem = null;
			}
		}
		/// <summary>
		/// Fire the FloatItemDocking when user click on the pin button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pbPin_Click(object sender, System.EventArgs e)
		{
			if (m_floatItem != null)
			{
				m_floatItem.m_bar.FireDocking(m_floatItem);
			}
		}
		/// <summary>
		/// Fire the FloatItemClosing when user click on the close button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pbClose_Click(object sender, System.EventArgs e)
		{
			if (m_floatItem != null)
			{
				m_floatItem.m_bar.FireClosing(m_floatItem);
			}
		}
	}
}
