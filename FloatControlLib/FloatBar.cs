using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace FloatControlLib
{
	/// <summary>
	/// A float bar that can contains multiple controls in a floating window
	/// </summary>
	public class FloatBar : System.Windows.Forms.UserControl
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		/// <summary>
		/// The constructor of the float bar
		/// </summary>
		public FloatBar()
		{
			m_rgFloatItems	= new ArrayList();
			m_frmFloat		= new FloatWindow();

			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitComponent call
			this.Dock = DockStyle.Left;

			m_frmFloat.Visible	= false;
		}
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if( components != null )
					components.Dispose();
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
			// 
			// FloatBar
			// 
			this.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.DockPadding.Bottom = 2;
			this.DockPadding.Left = 2;
			this.DockPadding.Right = 3;
			this.DockPadding.Top = 2;
			this.Name = "FloatBar";
			this.Size = new System.Drawing.Size(25, 150);
			this.DockChanged += new System.EventHandler(this.FloatBar_DockChanged);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.FloatBar_Paint);

		}
		#endregion

		/// <summary>Internal array to store the float items</summary>
		private ArrayList	m_rgFloatItems;
		/// <summary>The float window used by the float bar</summary>
		private FloatWindow	m_frmFloat;
		/// <summary>Event fired when the pin button of the float window is clicked</summary>
		public event FloatItemDockingEvent	FloatItemDocking;
		/// <summary>Event fired when the close button of the float window is clicked</summary>
		public event FloatItemClosingEvent	FloatItemClosing;
		/// <summary>Event fired when the float window will be hidden</summary>
		public event FloatWindowHidingEvent	FloatWindowHiding;
		/// <summary>
		/// Fire the FloatItemDocking event
		/// </summary>
		/// <param name="item"></param>
		public void FireDocking(FloatItem item)
		{
			bool	bCanceled = false;

			if (FloatItemDocking != null)
			{
				FloatItemDocking(item.m_control, item.m_icon, item.m_caption, ref bCanceled);
			}

			if (!bCanceled)
			{
				m_frmFloat.HideFloatWindow();
				this.RemoveFloatWindow(item);
			}
		}
		/// <summary>
		/// Fire the FloatItemClosing event
		/// </summary>
		/// <param name="item"></param>
		public void FireClosing(FloatItem item)
		{
			bool	bCanceled = false;

			if (FloatItemClosing != null)
			{
				FloatItemClosing(item.m_control, item.m_icon, item.m_caption, ref bCanceled);
			}

			if (!bCanceled)
			{
				m_frmFloat.HideFloatWindow();
				this.RemoveFloatWindow(item);
			}
		}
		/// <summary>
		/// Fire the FloatWindowHiding event
		/// </summary>
		/// <param name="item"></param>
		public void FireHiding(FloatItem item)
		{
			if (FloatWindowHiding != null)
			{
				FloatWindowHiding(item.m_control);
			}
		}
		/// <summary>
		/// Add a control to the float bar
		/// </summary>
		/// <param name="floatControl">The control to be shown</param>
		/// <param name="icon">The icon of the control</param>
		/// <param name="caption">The caption of the control</param>
		/// <param name="floatSize">The size of the float window of this control</param>
		/// <param name="labelSize">The size of label of this control on the float bar</param>
		public FloatItem AddFloatWindow(Control floatControl, Image icon, string caption, int floatSize, int labelSize, bool hideCaptionOnUnselected)
		{
			FloatItem	item;

			item = new FloatItem(this, floatControl, icon, caption, floatSize, labelSize, hideCaptionOnUnselected);
			m_rgFloatItems.Add(item);

			return item;
		}
		/// <summary>
		/// Return a float item by its index
		/// </summary>
		/// <param name="idx">The index of the float item</param>
		/// <returns>The float item</returns>
		public FloatItem FloatWindows(int idx)
		{
			return (FloatItem)m_rgFloatItems[idx];
		}
		/// <summary>
		/// Remove a float item
		/// </summary>
		/// <param name="item">The float item to be removed</param>
		public void RemoveFloatWindow(FloatItem item)
		{
			m_rgFloatItems.Remove(item);
			item.Dispose();
		}
		/// <summary>
		/// Remove a float item by its index
		/// </summary>
		/// <param name="idx">The index of the float item</param>
		public void RemoveFloatWindow(int idx)
		{
			m_rgFloatItems.Remove(m_rgFloatItems[idx]);
		}
		/// <summary>
		/// The number of the float item
		/// </summary>
		public int FloatWindowCount
		{
			get
			{
				return m_rgFloatItems.Count;
			}
		}
		/// <summary>
		/// Do some custom painting for the float bar
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FloatBar_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			e.Graphics.FillRectangle(new SolidBrush(Color.FromName("ControlLightLight")), 0, 0, this.Width, this.Height);

			switch (this.Dock)
			{
				case DockStyle.Left:
				{
					e.Graphics.DrawLine(Pens.Gray, this.Width - 1, 0, this.Width - 1, this.Height - 1);
					break;
				}
				case DockStyle.Right:
				{
					e.Graphics.DrawLine(Pens.Gray, 0, 0, 0, this.Height - 1);
					break;
				}
				case DockStyle.Top:
				{
					e.Graphics.DrawLine(Pens.Gray, 0, this.Height - 1, this.Width - 1, this.Height - 1);
					break;
				}
				case DockStyle.Bottom:
				{
					e.Graphics.DrawLine(Pens.Gray, 0, 0, this.Width - 1, 0);
					break;
				}
			}
		}
		/// <summary>
		/// Change the dock padding style when the dock style of the float bar is changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FloatBar_DockChanged(object sender, System.EventArgs e)
		{
			for (int i = 0; i < this.FloatWindowCount; i++)
			{
				this.FloatWindows(i).PerformLayout();
			}

			switch (this.Dock)
			{
				case DockStyle.Top:
				{
					m_frmFloat.DockPadding.All		= 2;
					m_frmFloat.DockPadding.Bottom	= 4;
					break;
				}
				case DockStyle.Bottom:
				{
					m_frmFloat.DockPadding.All		= 2;
					m_frmFloat.DockPadding.Top		= 4;
					break;
				}
				case DockStyle.Left:
				{
					m_frmFloat.DockPadding.All		= 2;
					m_frmFloat.DockPadding.Right	= 4;
					break;
				}
				case DockStyle.Right:
				{
					m_frmFloat.DockPadding.All		= 2;
					m_frmFloat.DockPadding.Left		= 4;
					break;
				}
			}

			this.Invalidate(true);;
		}
		/// <summary>
		/// Show a float item
		/// </summary>
		/// <param name="item"></param>
		public void AnimateShowFloatWindow(FloatItem item)
		{
			if (m_frmFloat.ActiveItem != null)
			{
				if (m_frmFloat.ActiveItem.Equals(item))
				{
					return;
				}
			}

			RelayoutBySelection(item);

			m_frmFloat.HideFloatWindow();
			m_frmFloat.AnimateShowFloatWindow(item);
		}

		public void RelayoutBySelection(FloatItem selectedItem)
		{
			if ((this.Dock == DockStyle.Left) || (this.Dock == DockStyle.Right))
			{
				selectedItem.m_label.Height = selectedItem.m_labelSize;

				for (int i = 0; i < this.FloatWindowCount; i++)
				{
					if (this.FloatWindows(i).m_hideCaptionOnUnselected)
					{
						if (!this.FloatWindows(i).Equals(selectedItem))
						{
							this.FloatWindows(i).m_label.Height = this.FloatWindows(i).m_icon.Height + 5;
						}
					}
				}
			}
			else if ((this.Dock == DockStyle.Top) || (this.Dock == DockStyle.Bottom))
			{
				selectedItem.m_label.Width = selectedItem.m_labelSize;

				for (int i = 0; i < this.FloatWindowCount; i++)
				{
					if (this.FloatWindows(i).m_hideCaptionOnUnselected)
					{
						if (!this.FloatWindows(i).Equals(selectedItem))
						{
							this.FloatWindows(i).m_label.Width = this.FloatWindows(i).m_icon.Width + 5;
						}
					}
				}
			}
			this.PerformLayout();
		}
	}

	/// <summary>Event fired when the pin button of the float window is clicked</summary>
	public delegate void FloatItemDockingEvent(System.Windows.Forms.Control floatControl, System.Drawing.Image floatIcon, string floatCaption, ref bool bCanceled);
	/// <summary>Event fired when the close button of the float window is clicked</summary>
	public delegate void FloatItemClosingEvent(System.Windows.Forms.Control floatControl, System.Drawing.Image floatIcon, string floatCaption, ref bool bCanceled);
	/// <summary>Event fired when the float window will be hidden</summary>
	public delegate void FloatWindowHidingEvent(System.Windows.Forms.Control floatControl);
	/// <summary>
	/// The float item that contains all information of a control
	/// </summary>
	public class FloatItem
	{
		/// <summary>
		/// Constructor of the float item
		/// </summary>
		/// <param name="parent">The parent float bar</param>
		/// <param name="floatControl">The control to be shown</param>
		/// <param name="icon">The icon of the control</param>
		/// <param name="caption">The caption of the control</param>
		/// <param name="floatSize">The size of the float window of the control</param>
		/// <param name="labelSize">The size of the label of the control on the flaot bar</param>
		public FloatItem(FloatBar parent, Control floatControl, Image icon, string caption, int floatSize, int labelSize, bool hideCaptionOnUnselected)
		{
			m_bar		= parent;
			m_control	= floatControl;
			m_icon		= icon;
			m_caption	= caption;
			m_floatSize	= floatSize;
			m_labelSize	= labelSize;

			m_hideCaptionOnUnselected = hideCaptionOnUnselected;

			m_spacer				= new Panel();
			m_spacer.Parent			= parent;
			m_spacer.BackColor		= parent.BackColor;
			m_spacer.BorderStyle	= System.Windows.Forms.BorderStyle.None;

			m_label					= new Panel();
			m_label.Parent			= parent;
			m_label.BackColor		= System.Drawing.Color.FromName("Control");
			m_label.BorderStyle		= System.Windows.Forms.BorderStyle.FixedSingle;
			m_label.DockPadding.All	= 2;
			m_label.Paint			+=new PaintEventHandler(m_label_Paint);
			m_label.MouseEnter		+=new EventHandler(m_label_MouseEnter);

			m_labelIcon				= new PictureBox();
			m_labelIcon.Parent		= m_label;
			m_labelIcon.Image		= icon;
			m_labelIcon.Width		= icon.Width;
			m_labelIcon.Height		= icon.Height;
			m_labelIcon.MouseEnter	+=new EventHandler(m_label_MouseEnter);

			this.PerformLayout();
			m_bar.RelayoutBySelection(this);
		}
		/// <summary>
		/// Release all UI resources of the float item
		/// </summary>
		public void Dispose()
		{
			m_labelIcon.Image	= null;
			m_control			= null;

			m_label.Dispose();
			m_spacer.Dispose();
			m_labelIcon.Dispose();
		}
		/// <summary>The parent float bar</summary>
		public FloatBar	m_bar;
		/// <summary>The control to be shown</summary>
		public Control	m_control;
		/// <summary>The icon of the control</summary>
		public Image	m_icon;
		/// <summary>The caption of the control</summary>
		public string	m_caption;
		/// <summary>The size of the float window of the control</summary>
		public int		m_floatSize;
		/// <summary>The size of the label of the control on the float bar</summary>
		public int		m_labelSize;
		/// <summary>Whether the caption should be hide when unselected</summary>
		public bool		m_hideCaptionOnUnselected;
		/// <summary>The panel of the float item</summary>
		public Panel		m_label;
		/// <summary>The spacer between float items</summary>
		private Panel		m_spacer;
		/// <summary>The picture box to show the icon</summary>
		private PictureBox	m_labelIcon;
		/// <summary>
		/// Relayout controls on the float item
		/// </summary>
		public void PerformLayout()
		{
			if ((this.m_bar.Dock == DockStyle.Left) || (this.m_bar.Dock == DockStyle.Right))
			{
				m_spacer.Dock		= DockStyle.Top;
				m_spacer.Height		= 2;
				m_label.Dock		= DockStyle.Top;
				m_label.Height		= m_labelSize;
				m_labelIcon.Dock	= DockStyle.Top;
			}
			else if ((this.m_bar.Dock == DockStyle.Top) || (this.m_bar.Dock == DockStyle.Bottom))
			{
				m_spacer.Dock		= DockStyle.Left;
				m_spacer.Width		= 2;
				m_label.Dock		= DockStyle.Left;
				m_label.Width		= m_labelSize;
				m_labelIcon.Dock	= DockStyle.Left;
			}
		}
		/// <summary>
		/// Do some custom painting for the float item
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void m_label_Paint(object sender, PaintEventArgs e)
		{
			if ((this.m_bar.Dock == DockStyle.Left) || (this.m_bar.Dock == DockStyle.Right))
			{
				System.Drawing.Drawing2D.Matrix	orgTrans;

				orgTrans = e.Graphics.Transform;
				e.Graphics.TranslateTransform(this.m_label.Width, this.m_icon.Height);
				e.Graphics.RotateTransform(90);
				e.Graphics.DrawString(this.m_caption, this.m_label.Parent.Font, System.Drawing.Brushes.Black, 2, (this.m_label.Width - this.m_label.Parent.Font.Height) / 2 + 2);
			
				e.Graphics.Transform = orgTrans;
			}
			else if ((this.m_bar.Dock == DockStyle.Top) || (this.m_bar.Dock == DockStyle.Bottom))
			{
				e.Graphics.DrawString(this.m_caption, this.m_label.Parent.Font, Brushes.Black, this.m_icon.Width + 2, (this.m_label.Height - this.m_label.Parent.Font.Height) / 2);
			}	
		}
		/// <summary>
		/// Show the float item when user's mouse enters the area
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void m_label_MouseEnter(object sender, EventArgs e)
		{
			this.m_bar.AnimateShowFloatWindow(this);
		}
	}
}
