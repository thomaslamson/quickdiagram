using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace QuickDiagramUI
{
	/// <summary>
	/// Summary description for StencilForm.
	/// </summary>
	public class StencilForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView lvStencil;
		private System.Windows.Forms.ImageList imgsStencil;
		private System.ComponentModel.IContainer components;

		public StencilForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
			this.lvStencil = new System.Windows.Forms.ListView();
			this.imgsStencil = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			// 
			// lvStencil
			// 
			this.lvStencil.Activation = System.Windows.Forms.ItemActivation.OneClick;
			this.lvStencil.BackColor = System.Drawing.Color.White;
			this.lvStencil.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lvStencil.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvStencil.LargeImageList = this.imgsStencil;
			this.lvStencil.Location = new System.Drawing.Point(0, 0);
			this.lvStencil.MultiSelect = false;
			this.lvStencil.Name = "lvStencil";
			this.lvStencil.Size = new System.Drawing.Size(216, 312);
			this.lvStencil.TabIndex = 0;
			this.lvStencil.ItemActivate += new System.EventHandler(this.lvStencil_ItemActivate);
			// 
			// imgsStencil
			// 
			this.imgsStencil.ImageSize = new System.Drawing.Size(33, 33);
			this.imgsStencil.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// StencilForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(216, 312);
			this.Controls.Add(this.lvStencil);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "StencilForm";
			this.Text = "StencilForm";
			this.ResumeLayout(false);

		}
		#endregion

		private Stencil	m_stencil;

		public event TemplateSelectedEvent TemplateSelected;

		public void InitializeStencil(Stencil stencil)
		{
			ListViewItem item;

			lvStencil.Items.Clear();
			imgsStencil.Images.Clear();

			for (int i = 0; i < stencil.rgImages.Count; i++)
			{
				imgsStencil.Images.Add((Image)stencil.rgImages[i]);

				item			= new ListViewItem();
				item.Text		= stencil.rgTemplates[i].id;
				item.Tag		= stencil.rgTemplates[i];
				item.ImageIndex	= i;

				lvStencil.Items.Add(item);
			}

			m_stencil = stencil;
		}

		private void lvStencil_ItemActivate(object sender, System.EventArgs e)
		{
			if ((TemplateSelected != null) && (lvStencil.SelectedItems.Count == 1))
			{
				TemplateSelected((GOMLib.GOM_Template)lvStencil.SelectedItems[0].Tag);
			}
		}
	}

	public delegate void TemplateSelectedEvent(GOMLib.GOM_Template template);
}
