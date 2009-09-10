using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace FloatControlLib
{
	/// <summary>
	/// A tab control of flat style
	/// </summary>
	public class TabControl : System.Windows.Forms.UserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		/// <summary>
		/// The constructor of the tab control
		/// </summary>
		public TabControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
			m_rgTabItems	= new ArrayList();
			SelectionChange = null;
			this.Dock		= DockStyle.Bottom;
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
			// 
			// TabControl
			// 
			this.Name = "TabControl";
			this.Size = new System.Drawing.Size(96, 24);
			this.SizeChanged += new System.EventHandler(this.TabControl_SizeChanged);

		}
		#endregion

		/// <summary>Internal array to store tab items</summary>
		private ArrayList	m_rgTabItems;
		/// <summary>Event fired when the selected tab is changed</summary>
		public event SelectionChangeEvent SelectionChange;
		/// <summary>
		/// Add a tab into the tab control
		/// </summary>
		/// <param name="caption">The caption of the tab</param>
		/// <param name="icon">The icon of the tab</param>
		/// <param name="defaultWidth">The default width of the tab</param>
		/// <param name="tag">Additional user data stored in this tab</param>
		/// <returns>The newly added tab</returns>
		public TabItem AddTab(string caption, Image icon, int defaultWidth, object tag)
		{
			TabItem	item;

			item		= new TabItem(this, caption, icon, defaultWidth);
			item.Tag	= tag;

			m_rgTabItems.Add(item);
			SelectedTab = item;

			this.TabControl_SizeChanged(null, null);

			return item;
		}
		/// <summary>
		/// Get a tab by its index
		/// </summary>
		/// <param name="idx">The index of the tab</param>
		/// <returns>The tab item</returns>
		public TabItem TabItems(int idx)
		{
			return (TabItem)m_rgTabItems[idx];
		}
		/// <summary>
		/// Remove a tab
		/// </summary>
		/// <param name="item">The tab item to be removed</param>
		public void RemoveTab(TabItem item)
		{
			m_rgTabItems.Remove(item);
			item.Dispose();

			if (m_rgTabItems.Count > 0)
			{
				this.SelectedTab = this.TabItems(0);
			}

			this.TabControl_SizeChanged(null, null);
		}

		private void TabControl_SizeChanged(object sender, System.EventArgs e)
		{
			int	iWidth = 0;

			if (this.TabCount > 0)
			{
				for (int i = 0; i < this.TabCount; i++)
				{
					iWidth += this.TabItems(i).DefaultWidth;
				}

				if (iWidth > this.ClientSize.Width)
				{
					iWidth = (iWidth - this.ClientSize.Width) / this.TabCount;
				}
				else
				{
					iWidth = 0;
				}

				for (int i = 0; i < this.TabCount; i++)
				{
					this.TabItems(i).Width = this.TabItems(i).DefaultWidth - iWidth;
				}

				this.Invalidate(true);
			}
		}
		/// <summary>
		/// Remove a tab by its index
		/// </summary>
		/// <param name="idx">The index of the tab to be removed</param>
		public void RemoveTab(int idx)
		{
			RemoveTab((TabItem)m_rgTabItems[idx]);
		}
		/// <summary>
		/// The number of tabs in the control
		/// </summary>
		public int TabCount
		{
			get
			{
				return m_rgTabItems.Count;
			}
		}
		/// <summary>
		/// Get or set current selected tab
		/// </summary>
		public TabItem SelectedTab
		{
			get
			{
				for (int i = 0; i < m_rgTabItems.Count; i++)
				{
					if (((TabItem)m_rgTabItems[i]).Selected)
					{
						return (TabItem)m_rgTabItems[i];
					}
				}
				return null;
			}
			set
			{
				TabItem	item = SelectedTab;

				if (item != null)
				{
					if (!item.Equals(value))
					{
						item.Selected = false;
					}
					else
					{
						return;
					}
				}

				if (value != null)
				{
					value.Selected = true;

					if (SelectionChange != null)
					{
						SelectionChange(value);
					}
				}
			}
		}
	}
	/// <summary>
	/// Event fired when the selected tab is changed
	/// </summary>
	public delegate void SelectionChangeEvent(TabItem selectedItem);
	/// <summary>
	/// The item that contains all information of a tab
	/// </summary>
	public class TabItem
	{
		/// <summary>
		/// The constructor of a tab item
		/// </summary>
		/// <param name="parent">The parent tab control</param>
		/// <param name="caption">The caption of the tab</param>
		/// <param name="icon">The icon of the tab</param>
		/// <param name="defaultWidth">The default width of the tab</param>
		public TabItem(TabControl parent, string caption, Image icon, int defaultWidth)
		{
			m_parent		= parent;
			m_bSelected		= false;
			DefaultWidth	= defaultWidth;

			m_plTab					= new Panel();
			m_plTab.Parent			= parent;
			m_plTab.Dock			= DockStyle.Left;
			m_plTab.BackColor		= Color.FromName("ControlLightLight");
			m_plTab.BorderStyle		= BorderStyle.None;
			m_plTab.Width			= defaultWidth;
			m_plTab.Visible			= true;
			m_plTab.DockPadding.All = 1;
			m_plTab.Click			+=new EventHandler(m_plTab_Click);
			m_plTab.Paint			+=new PaintEventHandler(m_plTab_Paint);

			m_lbTitle			= new Label();
			m_lbTitle.Parent	= m_plTab;
			m_lbTitle.Text		= caption;
			m_lbTitle.Dock		= DockStyle.Left;
			m_lbTitle.BackColor	= Color.FromName("Transparent");
			m_lbTitle.AutoSize	= true;
			m_lbTitle.TextAlign = ContentAlignment.MiddleLeft; 
			m_lbTitle.Visible	= true;
			m_lbTitle.Click		+=new EventHandler(m_plTab_Click);

			m_pbIcon			= new PictureBox();
			m_pbIcon.Parent		= m_plTab;
			m_pbIcon.Image		= icon;
			m_pbIcon.Width		= icon.Width;
			m_pbIcon.Height		= icon.Height;
			m_pbIcon.Dock		= DockStyle.Left;
			m_pbIcon.Visible	= true;
			m_pbIcon.Click		+=new EventHandler(m_plTab_Click);
		}
		/// <summary>
		/// Release all UI resources allocated for the tab
		/// </summary>
		public void Dispose()
		{
			m_lbTitle.Dispose();
			m_pbIcon.Dispose();
			m_plTab.Dispose();
		}
		/// <summary>User data stored in the tab</summary>
		public object Tag;
		/// <summary>The default width of the tab (Not Used)</summary>
		public int DefaultWidth;
		/// <summary>
		/// Get or set the caption of the tab
		/// </summary>
		public string Text
		{
			get
			{
				return m_lbTitle.Text;
			}
			set
			{
				m_lbTitle.Text = value;
			}
		}
		/// <summary>
		/// Get or set the image of the tab
		/// </summary>
		public Image Icon
		{
			get
			{
				return m_pbIcon.Image;
			}
			set
			{
				m_pbIcon.Image = value;
			}
		}
		/// <summary>
		/// Get or set the status of selection of the tab
		/// </summary>
		public bool Selected
		{
			get
			{
				return m_bSelected;
			}
			set
			{
				m_bSelected = value;

				if (m_bSelected)
				{
					m_plTab.BackColor = Color.FromName("Control");
				}
				else
				{
					m_plTab.BackColor = Color.FromName("ControlLightLight");
				}
			}
		}
		/// <summary>
		/// Get or set the width of the tab
		/// </summary>
		public int Width
		{
			get
			{
				return m_plTab.Width;
			}
			set
			{
				m_plTab.Width = value;
			}
		}

		/// <summary>The tab itself</summary>
		private Panel		m_plTab;
		/// <summary>The caption of the tab</summary>
		private Label		m_lbTitle;
		/// <summary>The parent tab control</summary>
		private TabControl	m_parent;
		/// <summary>The icon of the tab</summary>
		private PictureBox	m_pbIcon;
		/// <summary>The flag which indicates whether the tab is selected or not</summary>
		private bool		m_bSelected;
		/// <summary>
		/// Handle the click event of the tab
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void m_plTab_Click(object sender, EventArgs e)
		{
			m_parent.SelectedTab = this;
		}
		/// <summary>
		/// Do some custom drawing on painting
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void m_plTab_Paint(object sender, PaintEventArgs e)
		{
			if (m_bSelected)
			{
				e.Graphics.DrawLine(Pens.Black, 0, 0, this.m_plTab.Width, 0);
				e.Graphics.DrawLine(Pens.Black, 0, this.m_plTab.Height - 1, this.m_plTab.Width, this.m_plTab.Height - 1);
				e.Graphics.DrawLine(Pens.Black, 0, 0, 0, this.m_plTab.Height);
				e.Graphics.DrawLine(Pens.Black, this.m_plTab.Width - 1, 0, this.m_plTab.Width - 1, this.m_plTab.Height);
			}
			else
			{
				e.Graphics.DrawLine(Pens.Gray, 0, 0, this.m_plTab.Width, 0);
				e.Graphics.DrawLine(Pens.Gray, this.m_plTab.Width - 1, 0, this.m_plTab.Width - 1, this.m_plTab.Height);
			}
		}
	}
}
