using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace QuickDiagramUI
{
	/// <summary>
	/// Summary description for CatalogForm.
	/// </summary>
	public class CatalogForm : System.Windows.Forms.Form
	{
		private ProgressForm formProgress = null;
		private System.Windows.Forms.TreeView tvCatalog;
		private System.Windows.Forms.ImageList imgsCatalog;
		private System.ComponentModel.IContainer components;

		public CatalogForm()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(CatalogForm));
			this.tvCatalog = new System.Windows.Forms.TreeView();
			this.imgsCatalog = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			// 
			// tvCatalog
			// 
			this.tvCatalog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvCatalog.ImageList = this.imgsCatalog;
			this.tvCatalog.Location = new System.Drawing.Point(0, 0);
			this.tvCatalog.Name = "tvCatalog";
			this.tvCatalog.Size = new System.Drawing.Size(192, 336);
			this.tvCatalog.TabIndex = 0;
			this.tvCatalog.DoubleClick += new System.EventHandler(this.tvCatalog_DoubleClick);
			this.tvCatalog.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tvCatalog_MouseMove);
			// 
			// imgsCatalog
			// 
			this.imgsCatalog.ImageSize = new System.Drawing.Size(16, 16);
			this.imgsCatalog.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgsCatalog.ImageStream")));
			this.imgsCatalog.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// CatalogForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(192, 336);
			this.Controls.Add(this.tvCatalog);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "CatalogForm";
			this.Text = "CatalogForm";
			this.Load += new System.EventHandler(this.CatalogForm_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private FloatSelectionForm	m_floatSelect;

		public event StencilSelectedEvent	StencilSelected;
		public event TemplateSelectedEvent	TemplateSelected;

		public bool HideOnDeactivated
		{
			get
			{
				return m_floatSelect.HideOnDeactivated;
			}
			set
			{
				m_floatSelect.HideOnDeactivated = value;
			}
		}

		public void HideFloatSelectionForm()
		{
			m_floatSelect.HideForm();
		}

		public TemplatePack GetTemplateByID(string id)
		{
			TemplatePack	template;

			for (int i = 0; i < tvCatalog.Nodes.Count; i++)
			{
				template = InternalGetTemplateByID(tvCatalog.Nodes[i], id);
				if (template != null)
				{
					return template;
				}
			}

			return null;
		}

		private TemplatePack InternalGetTemplateByID(TreeNode node, string id)
		{
			Stencil			stencil;
			TemplatePack	template;

			stencil = (Stencil)node.Tag;
			template = stencil.GetTemplateByID(id);

			if (template != null)
			{
				return template;
			}

			for (int i = 0; i < node.Nodes.Count; i++)
			{
				template = InternalGetTemplateByID(node.Nodes[i], id);
				if (template != null)
				{
					return template;
				}
			}

			return null;
		}

		public void LoadTemplates(string path)
		{
			tvCatalog.Nodes.Clear();
			formProgress = new ProgressForm();
			formProgress.Show();
			LoadTemplateTree(new System.IO.DirectoryInfo(path), tvCatalog.Nodes, 0);
			formProgress.Close();
			formProgress = null;
			tvCatalog.ExpandAll();
		}

		private void LoadTemplateTree(System.IO.DirectoryInfo path, TreeNodeCollection col, int depth)
		{
			System.IO.DirectoryInfo[]	rgPaths;
			Stencil		stencil;
			TreeNode	node;

			stencil		= new Stencil();
			node		= new TreeNode(path.Name);
			node.Tag	= stencil;
			rgPaths		= path.GetDirectories();

			col.Add(node);
			try
			{
				stencil.LoadFromPath(path.FullName);
			}
			catch(ReadingTemplatesAbortException)
			{}

			for (int i = 0; i < rgPaths.Length; i++)
			{
				LoadTemplateTree(rgPaths[i], node.Nodes, depth+1);
				if ( depth == 0 && formProgress!=null )
				{
					formProgress.progressBar.Value = ((i+1)*100)/rgPaths.Length;
				}
			}
		}

		private void tvCatalog_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			TreeNode	node = tvCatalog.GetNodeAt(e.X, e.Y);
			Point		pt;

			if (node != null)
			{
				if ((!node.Equals(tvCatalog.Tag)) || (m_floatSelect.Height == 0))
				{
					tvCatalog.Tag = node;

					pt = new Point(node.Bounds.Right, node.Bounds.Top);
					pt = tvCatalog.PointToScreen(pt);

					m_floatSelect.AnimateShowForm(node.Text, pt.X, pt.Y, (Stencil)node.Tag);
				}
			}
			else
			{
				m_floatSelect.HideForm();
				tvCatalog.Tag = null;
			}
		}

		private void tvCatalog_DoubleClick(object sender, System.EventArgs e)
		{
			if (tvCatalog.SelectedNode != null)
			{
				if (StencilSelected != null)
				{
					StencilSelected((Stencil)tvCatalog.SelectedNode.Tag, tvCatalog.SelectedNode.Text);
				}
			}
		}

		private void m_floatSelect_TemplateSelected(GOMLib.GOM_Template template)
		{
			if (this.TemplateSelected != null)
			{
				this.TemplateSelected(template);
			}
		}

		private void CatalogForm_Load(object sender, System.EventArgs e)
		{
			m_floatSelect			= new FloatSelectionForm();
			m_floatSelect.Owner		= this.ParentForm;
			m_floatSelect.Visible	= true;
			m_floatSelect.Width		= 0;
			m_floatSelect.Height	= 0;
			m_floatSelect.Left		= 0;
			m_floatSelect.Top		= 0;

			m_floatSelect.TemplateSelected +=new TemplateSelectedEvent(m_floatSelect_TemplateSelected);
		}
	}

	public delegate void StencilSelectedEvent(Stencil selectedStencil, string caption);

	public class Stencil
	{
		public Stencil()
		{
			StencilName = "";
			rgObjects	= new GOMLib.GOM_Objects();
			rgTemplates = new GOMLib.GOM_Templates();
			rgImages	= new ArrayList();
		}

		public void LoadFromPath(string pathName)
		{
			System.IO.DirectoryInfo path;
			System.IO.FileInfo[]	rgFiles;

			path	= new System.IO.DirectoryInfo(pathName);
			rgFiles = path.GetFiles();

			for (int i = 0; i < rgFiles.Length; i++)
			{
				try
				{
					LoadFromFile(rgFiles[i].FullName);
				}
				catch(Exception ex)
				{
					string msg = "Template ["+rgFiles[i].FullName+"] cannot been read.\r\n";
					msg += "The reason is \""+ex.Message+"\"\r\n";
					DialogResult result = MessageBox.Show(msg, "Reading Templates",
						MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Warning,
						MessageBoxDefaultButton.Button3);
					switch(result)
					{
						case DialogResult.Abort:
							throw new ReadingTemplatesAbortException();
						case DialogResult.Retry:
							i--;
							break;
					}
				}
			}
		}

		public void LoadFromFile(string fileName)
		{
			System.Drawing.Graphics		canvas;
			System.Drawing.Bitmap		bitmap;
			System.Drawing.RectangleF	rc;
			GOMLib.GOM_Template			template;
			GOMLib.GOM_Object_Primitive	primitive;

			template	= GOMLib.GOM_Template.LoadFromFile(fileName);
			primitive	= new GOMLib.GOM_Object_Primitive();

			primitive.InitializeFromTemplate(template);
			bitmap		= new Bitmap(33, 33, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

			rc = primitive.BoundingBox;

			if (rc.Width > rc.Height)
			{
				primitive.width		= 32;
				primitive.height	= 32 * rc.Height / rc.Width;

				rc = primitive.BoundingBox;

				primitive.xOffset	= -rc.Left;
				primitive.yOffset	= -rc.Top + (rc.Width - rc.Height) / 2;
			}
			else
			{
				primitive.height	= 32;
				primitive.width		= 32 * rc.Width / rc.Height;

				rc = primitive.BoundingBox;

				primitive.xOffset	= -rc.Left + (rc.Height - rc.Width) / 2;
				primitive.yOffset	= -rc.Top;
			}

			canvas = Graphics.FromImage(bitmap);
			canvas.FillRectangle(System.Drawing.Brushes.White, 0, 0, 33, 33);
            primitive.Draw(canvas, false);

			rgImages.Add(bitmap);
			rgTemplates.Add(template);
			rgObjects.Add(primitive);
		}

		public TemplatePack GetTemplateByID(string id)
		{
			for (int i = 0; i < rgTemplates.Count; i++)
			{
				if (rgTemplates[i].id == id)
				{
					TemplatePack	template;

					template = new TemplatePack();
					template.primitive	= (GOMLib.GOM_Object_Primitive)rgObjects[i];
					template.template	= rgTemplates[i];
					template.bitmap		= (System.Drawing.Bitmap)rgImages[i];

					return template;
				}
			}
			return null;
		}

		public string				StencilName;
		public ArrayList			rgImages;
		public GOMLib.GOM_Objects	rgObjects;
		public GOMLib.GOM_Templates	rgTemplates;
	}

	class ReadingTemplatesAbortException : Exception
	{
	}

}
