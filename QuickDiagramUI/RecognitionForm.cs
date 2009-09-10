using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace QuickDiagramUI
{
	/// <summary>
	/// Summary description for RecognitionForm.
	/// </summary>
	public class RecognitionForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView lvRecognition;
		private System.Windows.Forms.ImageList imgsRecognition;
		private System.ComponentModel.IContainer components;

		public RecognitionForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			RecognitionResultSelected = null;
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
			this.lvRecognition = new System.Windows.Forms.ListView();
			this.imgsRecognition = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			// 
			// lvRecognition
			// 
			this.lvRecognition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lvRecognition.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvRecognition.LargeImageList = this.imgsRecognition;
			this.lvRecognition.Location = new System.Drawing.Point(0, 0);
			this.lvRecognition.Name = "lvRecognition";
			this.lvRecognition.Size = new System.Drawing.Size(192, 288);
			this.lvRecognition.TabIndex = 0;
			this.lvRecognition.Click += new System.EventHandler(this.lvRecognition_Click);
			// 
			// imgsRecognition
			// 
			this.imgsRecognition.ImageSize = new System.Drawing.Size(33, 33);
			this.imgsRecognition.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// RecognitionForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(192, 288);
			this.Controls.Add(this.lvRecognition);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "RecognitionForm";
			this.Text = "RecognitionForm";
			this.ResumeLayout(false);

		}
		#endregion

		public event RecognitionResultSelectedEvent RecognitionResultSelected;

		public int  GetRecognitionCount()
		{
			return lvRecognition.Items.Count;
		}

		public void ClearRecognitionResult()
		{
			lvRecognition.Items.Clear();
			imgsRecognition.Images.Clear();
		}

		public void BeginUpdateRecognitionResult()
		{
			lvRecognition.BeginUpdate();
		}

		public void EndUpdateRecognitionResult()
		{
			lvRecognition.EndUpdate();
		}

		public void AddRecognitionResult(TemplatePack template)
		{
			int insertIndex = -1;
			for(int i=0; i<lvRecognition.Items.Count; i++)
			{
				if ( ((TemplatePack)lvRecognition.Items[i].Tag).similarity < template.similarity )
				{
					insertIndex = i;
					break;
				}
			}
			if ( insertIndex < 0 && lvRecognition.Items.Count < 50 )
			{
				insertIndex = lvRecognition.Items.Count;
			}

			if ( insertIndex >= 0 )
			{
				ListViewItem	item;

				imgsRecognition.Images.Add(template.bitmap);

				item = new ListViewItem();
				item.Text = template.template.id;
				item.Tag = template;
				item.ImageIndex = imgsRecognition.Images.Count - 1;

				lvRecognition.Items.Insert(insertIndex, item);

				if ( lvRecognition.Items.Count > 50 )
				{
					lvRecognition.Items.RemoveAt(lvRecognition.Items.Count - 1);
				}
			}

		}

		private void lvRecognition_Click(object sender, System.EventArgs e)
		{
			if ((lvRecognition.SelectedItems.Count == 1) && (RecognitionResultSelected != null))
			{
				RecognitionResultSelected((TemplatePack)lvRecognition.SelectedItems[0].Tag);
			}
		}
	}

	public delegate void RecognitionResultSelectedEvent(TemplatePack template);

	public class TemplatePack
	{
		public TemplatePack()
		{
			bitmap		= null;
			template	= null;
			primitive	= null;
		}

		public System.Drawing.Bitmap		bitmap;
		public System.Drawing.RectangleF	rect;
		public GOMLib.GOM_Template			template;
		public GOMLib.GOM_Object_Primitive	primitive;
		public float						rotation;
		public float						similarity;
	}
}
