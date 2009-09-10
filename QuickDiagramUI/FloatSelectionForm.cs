using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace QuickDiagramUI
{
	/// <summary>
	/// Summary description for FloatSelectionForm.
	/// </summary>
	public class FloatSelectionForm : System.Windows.Forms.Form
	{
		private System.ComponentModel.IContainer components = null;

		public FloatSelectionForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.Left		= 0;
			this.Top		= 0;
			this.Width		= 0;
			this.Height		= 0;
			this.m_caption	= "";
			this.m_stencil	= null;

			this.HideOnDeactivated	= false;
			this.TemplateSelected	= null;
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
			// FloatSelectionForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(292, 271);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "FloatSelectionForm";
			this.Opacity = 0.75;
			this.ShowInTaskbar = false;
			this.Text = "FloatSelectionForm";
			this.TopMost = true;
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FloatSelectionForm_MouseDown);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.FloatSelectionForm_Paint);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FloatSelectionForm_MouseMove);
			this.Deactivate += new System.EventHandler(this.FloatSelectionForm_Deactivate);

		}
		#endregion

		public bool		HideOnDeactivated;

		private Stencil	m_stencil;
		private string	m_caption;
		private int		m_MouseX, m_MouseY;

		public event TemplateSelectedEvent	TemplateSelected;

		public void AnimateShowForm(string caption, int x, int y, Stencil stencil)
		{
			if (stencil.rgImages.Count > 0)
			{
				this.Left		= x;
				this.Top		= y;
				this.Width		= 204;
				this.Height		= ((int)Math.Ceiling((float)(stencil.rgImages.Count / 5.0))) * 40 + 3 + this.Font.Height;
				this.m_caption	= caption;
				this.m_stencil	= stencil;
				this.m_MouseX	= 0;
				this.m_MouseY	= 0;

				if (this.Bottom > Screen.PrimaryScreen.Bounds.Bottom)
				{
					this.Top = Screen.PrimaryScreen.Bounds.Bottom - this.Height;
				}

				if (this.Right > Screen.PrimaryScreen.Bounds.Right)
				{
					this.Left = Screen.PrimaryScreen.Bounds.Right - this.Width;
				}

				this.Refresh();

				if (this.HideOnDeactivated)
				{
					this.Activate();
				}
			}
			else
			{
				HideForm();
			}
		}

		public void HideForm()
		{
			this.Width		= 0;
			this.Height		= 0;
			this.Left		= 0;
			this.Top		= 0;
			this.m_caption	= "";
		}

		private void FloatSelectionForm_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			e.Graphics.DrawRectangle(Pens.DarkSlateGray, 0, 0, this.Width - 1, this.Height - 1);
			e.Graphics.FillRectangle(Brushes.DarkSlateGray, 0, 0, this.Width, this.Font.Height);
			e.Graphics.DrawString(this.m_caption, this.Font, Brushes.White, 2, 0);

			if (m_stencil != null)
			{
				int x, y;

				for (int i = 0; i < m_stencil.rgImages.Count; i++)
				{
					x = (i % 5) * 40 + 3;
					y = (i / 5) * 40 + this.Font.Height + 2;
					e.Graphics.DrawImage((Image)m_stencil.rgImages[i], x + 2, y + 2);

					if ((x < m_MouseX) && (m_MouseX < x + 40) && (y < m_MouseY) && (m_MouseY < y + 40))
					{
						e.Graphics.DrawRectangle(Pens.Red, x, y, 36, 36);
					}
				}
			}
		}

		private void FloatSelectionForm_Deactivate(object sender, System.EventArgs e)
		{
			if (this.HideOnDeactivated)
			{
				HideForm();
			}
		}

		private void FloatSelectionForm_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			m_MouseX = e.X;
			m_MouseY = e.Y;

			this.Invalidate();
		}

		private void FloatSelectionForm_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			int x, y;

			if (m_stencil != null)
			{
				for (int i = 0; i < m_stencil.rgImages.Count; i++)
				{
					x = (i % 5) * 40 + 3;
					y = (i / 5) * 40 + this.Font.Height + 2;

					if ((x < m_MouseX) && (m_MouseX < x + 40) && (y < m_MouseY) && (m_MouseY < y + 40))
					{
						if (TemplateSelected != null)
						{
							TemplateSelected(m_stencil.rgTemplates[i]);
						}
						break;
					}
				}
			}
		}
	}
}
