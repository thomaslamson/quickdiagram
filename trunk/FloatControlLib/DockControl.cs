using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace FloatControlLib
{
	/// <summary>
	/// A control that allows multiple windows docking in it
	/// </summary>
	public class DockControl : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.Panel plTitle;
		private System.Windows.Forms.PictureBox pbPin;
		private System.Windows.Forms.PictureBox pbClose;
		private System.Windows.Forms.Label lbTitle;
		private System.Windows.Forms.PictureBox pbIcon;
		private System.Windows.Forms.Panel plSpacer;
		private System.Windows.Forms.Timer tmFireDock;
		private FloatControlLib.TabControl tabDock;
		private System.ComponentModel.IContainer components;
		/// <summary>
		/// Constructor of the dock control
		/// </summary>
		public DockControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

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
			this.components = new System.ComponentModel.Container();
			this.plTitle = new System.Windows.Forms.Panel();
			this.pbPin = new System.Windows.Forms.PictureBox();
			this.pbClose = new System.Windows.Forms.PictureBox();
			this.lbTitle = new System.Windows.Forms.Label();
			this.pbIcon = new System.Windows.Forms.PictureBox();
			this.plSpacer = new System.Windows.Forms.Panel();
			this.tmFireDock = new System.Windows.Forms.Timer(this.components);
			this.tabDock = new FloatControlLib.TabControl();
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
			this.plTitle.Size = new System.Drawing.Size(140, 20);
			this.plTitle.TabIndex = 1;
			// 
			// pbPin
			// 
			this.pbPin.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pbPin.Dock = System.Windows.Forms.DockStyle.Right;
			this.pbPin.Location = new System.Drawing.Point(104, 2);
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
			this.pbClose.Location = new System.Drawing.Point(120, 2);
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
			this.lbTitle.Size = new System.Drawing.Size(0, 17);
			this.lbTitle.TabIndex = 1;
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
			// plSpacer
			// 
			this.plSpacer.Dock = System.Windows.Forms.DockStyle.Top;
			this.plSpacer.Location = new System.Drawing.Point(2, 22);
			this.plSpacer.Name = "plSpacer";
			this.plSpacer.Size = new System.Drawing.Size(140, 4);
			this.plSpacer.TabIndex = 3;
			// 
			// tmFireDock
			// 
			this.tmFireDock.Tick += new System.EventHandler(this.FireDock_Tick);
			// 
			// tabDock
			// 
			this.tabDock.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.tabDock.Location = new System.Drawing.Point(2, 274);
			this.tabDock.Name = "tabDock";
			this.tabDock.SelectedTab = null;
			this.tabDock.Size = new System.Drawing.Size(140, 20);
			this.tabDock.TabIndex = 4;
			this.tabDock.SelectionChange += new FloatControlLib.SelectionChangeEvent(this.tabDock_SelectionChange);
			// 
			// DockControl
			// 
			this.Controls.Add(this.tabDock);
			this.Controls.Add(this.plSpacer);
			this.Controls.Add(this.plTitle);
			this.DockPadding.All = 2;
			this.Name = "DockControl";
			this.Size = new System.Drawing.Size(144, 296);
			this.plTitle.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>Event fired when the pin button is clicked</summary>
		public event DockItemFloatingEvent DockItemFloating;
		/// <summary>Event fired when the close button is clicked</summary>
		public event DockItemClosingEvent DockItemClosing;
		/// <summary>
		/// HACK!!!!
		/// Because if a window is put directly into the control, it will not be displayed.
		/// A timer is used to put the window into the control for several times so that 
		/// the window can be displayed correctly. I think it should be a bug in the .Net
		/// framework. This variable is used to control the number of times to put the window.
		/// </summary>
		private int m_iFireDock;
		/// <summary>
		/// Fire the DockItemFloating event
		/// </summary>
		/// <param name="item"></param>
		public void FireFloating(DockItem item)
		{
			bool bCanceled = false;

			if (DockItemFloating != null)
			{
				DockItemFloating(item.m_control, item.m_icon, item.m_caption, ref bCanceled);
			}

			if (!bCanceled)
			{
				RemoveDockWindow(item);
			}
		}
		/// <summary>
		/// Fire the DockItemClosing event
		/// </summary>
		/// <param name="item"></param>
		public void FireClosing(DockItem item)
		{
			bool bCanceled = false;

			if (DockItemClosing != null)
			{
				DockItemClosing(item.m_control, item.m_icon, item.m_caption, ref bCanceled);
			}

			if (!bCanceled)
			{
				RemoveDockWindow(item);
			}
		}
		/// <summary>
		/// Do custom painting for the close button
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
		/// Do some custom painting for the pin button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pbPin_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			System.Drawing.Pen	pen = new Pen(System.Drawing.Color.FromName("ActiveCaptionText"));

			e.Graphics.DrawLine(pen, 7, 13, 7, 10);
			e.Graphics.DrawLine(pen, 2, 9, 12, 9);
			e.Graphics.DrawLine(pen, 8, 9, 8, 1);
			e.Graphics.DrawLine(pen, 9, 9, 9, 1);
			e.Graphics.DrawLine(pen, 5, 9, 5, 1);
			e.Graphics.DrawLine(pen, 5, 1, 9, 1);
		}
		/// <summary>
		/// Add a docked window into the dock control
		/// </summary>
		/// <param name="dockControl">The window to be docked</param>
		/// <param name="icon">The icon of the window</param>
		/// <param name="caption">The caption of the window</param>
		/// <param name="defaultWidth">The default width of the label</param>
		public void AddDockWindow(Control dockControl, Image icon, string caption, int defaultWidth)
		{
			DockItem	item;

			item	= new DockItem(dockControl, icon, caption);

			tabDock.AddTab(caption, icon, defaultWidth, item);
			ShowDockWindow(item);

			m_iFireDock = 0;
			tmFireDock.Enabled = true;
		}
		/// <summary>
		/// Number of the docked window in the dock control
		/// </summary>
		public int DockWindowCount
		{
			get
			{
				return tabDock.TabCount;
			}
		}
		/// <summary>
		/// Remove a docked window
		/// </summary>
		/// <param name="item">The dock item to be removed</param>
		public void RemoveDockWindow(DockItem item)
		{
			for (int i = 0; i < tabDock.TabCount; i++)
			{
				if (tabDock.TabItems(i).Tag.Equals(item))
				{
					tabDock.RemoveTab(i);
					item.m_control.Parent = null;
				}
			
			}	
		}
		/// <summary>
		/// Remove a docked window by its index
		/// </summary>
		/// <param name="idx">The index of the window</param>
		public void RemoveDockWindow(int idx)
		{
			RemoveDockWindow((DockItem)tabDock.TabItems(idx).Tag);
		}
		/// <summary>
		/// Get a dock item by its index
		/// </summary>
		/// <param name="idx">The index of the dock item</param>
		/// <returns>The dock item</returns>
		public DockItem DockWindows(int idx)
		{
			return (DockItem)tabDock.TabItems(idx).Tag;
		}
		/// <summary>
		/// Display a docked window
		/// </summary>
		/// <param name="item"></param>
		private void ShowDockWindow(DockItem item)
		{
			for (int i = 0; i < tabDock.TabCount; i++)
			{
				if (!tabDock.TabItems(i).Tag.Equals(item))
				{
					((DockItem)tabDock.TabItems(i).Tag).m_control.Parent = null;
				}
			}

			this.pbIcon.Image	= item.m_icon;
			this.lbTitle.Text	= item.m_caption;

			item.m_control.Parent = this;
			item.m_control.Dock = DockStyle.Fill;
			item.m_control.Visible = true;
			item.m_control.Enabled = true;
			item.m_control.BringToFront();
		}
		/// <summary>
		/// Fire the DockItemFloating event when the pin button is clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pbPin_Click(object sender, System.EventArgs e)
		{
			if (tabDock.SelectedTab != null)
			{
				FireFloating((DockItem)tabDock.SelectedTab.Tag);
			}
		}
		/// <summary>
		/// Fire the DockItemClosing event when the close button is clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pbClose_Click(object sender, System.EventArgs e)
		{
			if (tabDock.SelectedTab != null)
			{
				FireClosing((DockItem)tabDock.SelectedTab.Tag);
			}
		}
		/// <summary>
		/// HACK!!!!
		/// Because if a window is put directly into the control, it will not be displayed.
		/// A timer is used to put the window into the control for several times so that 
		/// the window can be displayed correctly. I think it should be a bug in the .Net
		/// framework. This is the timer proc
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FireDock_Tick(object sender, System.EventArgs e)
		{
			m_iFireDock++;

			if (tabDock.SelectedTab != null)
			{
				ShowDockWindow((DockItem)this.tabDock.SelectedTab.Tag);
			}

			if (m_iFireDock > 5)
			{
				tmFireDock.Enabled = false;
			}
		}
		/// <summary>
		/// Change the docked window to display when the selection of the tab control is changed
		/// </summary>
		/// <param name="selectedItem"></param>
		private void tabDock_SelectionChange(FloatControlLib.TabItem selectedItem)
		{
			if (tabDock.SelectedTab != null)
			{
				ShowDockWindow((DockItem)tabDock.SelectedTab.Tag);
			}
			else
			{
				this.pbIcon.Image	= null;
				this.lbTitle.Text	= "";
			}		
		}
	}
	/// <summary>
	/// The dock item to store all information of a docked window
	/// </summary>
	public class DockItem
	{
		/// <summary>
		/// Constructor of a dock item
		/// </summary>
		/// <param name="dockControl">The window to be docked</param>
		/// <param name="icon">The icon of the window</param>
		/// <param name="caption">The caption of the window</param>
		public DockItem(Control dockControl, Image icon, string caption)
		{
			m_control	= dockControl;
			m_icon		= icon;
			m_caption	= caption;
		}
		/// <summary>The window to be docked</summary>
		public Control	m_control;
		/// <summary>The icon of the window</summary>
		public Image	m_icon;
		/// <summary>The caption of the window</summary>
		public string	m_caption;
	}
	
	/// <summary>Event fired when the pin button is clicked</summary>
	public delegate void DockItemFloatingEvent(System.Windows.Forms.Control dockControl, System.Drawing.Image dockIcon, string dockCaption, ref bool bCanceled);
	/// <summary>Event fired when the close button is clicked</summary>
	public delegate void DockItemClosingEvent(System.Windows.Forms.Control dockControl, System.Drawing.Image dockIcon, string dockCaption, ref bool bCanceled);
}
