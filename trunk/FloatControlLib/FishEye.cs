using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace FloatControlLib
{
	/// <summary>
	/// Summary description for FishEye.
	/// </summary>
	public class FishEye : System.Windows.Forms.UserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private bool	bExpanded;
		private int		iTagWidth;
		private int		iTagHeight;
		private int		iLabelHeight;
		private int		iLabelWidth;
		private int		iImageHeight;
		private int		iSelectedIndex;
		private string	szCaption;

		private System.Drawing.Image			imgIcon;
		private System.Collections.ArrayList	rgFishEyeItems;

		public event FishEyeItemSelectedEvent	FishEyeItemSelected;

		public FishEye()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
			iSelectedIndex	= -1;
			iLabelWidth		= 33;
			iLabelHeight	= 20;
			iImageHeight	= 33;
			iTagWidth		= this.Width;
			iTagHeight		= this.Height;
			rgFishEyeItems	= new ArrayList();
			imgIcon			= null;
			bExpanded		= false;

			FishEyeItemSelected	= null;
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
			// FishEye
			// 
			this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "FishEye";
			this.Size = new System.Drawing.Size(18, 18);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.FishEye_Paint);
			this.MouseEnter += new System.EventHandler(this.FishEye_MouseEnter);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FishEye_MouseMove);
			this.MouseLeave += new System.EventHandler(this.FishEye_MouseLeave);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FishEye_MouseDown);

		}
		#endregion
	
		public Image TagIcon
		{
			get
			{
				return imgIcon;
			}
			set
			{
				imgIcon = value;
			}
		}

		public string Caption
		{
			get
			{
				return szCaption;
			}
			set
			{
				szCaption = value;
			}
		}

		public int SelectedIndex
		{
			get
			{
				return iSelectedIndex;
			}
			set
			{
				iSelectedIndex = value;
			}
		}

		public FishEyeItem SelectedItem
		{
			get
			{
				if ((0 <= iSelectedIndex) && (iSelectedIndex < rgFishEyeItems.Count))
				{
					return (FishEyeItem)rgFishEyeItems[iSelectedIndex];
				}
				else
				{
					return null;
				}
			}
		}

		public int LabelHeight
		{
			get
			{
				return iLabelHeight;
			}
			set
			{
				iLabelHeight = value;
			}
		}

		public int ImageHeight
		{
			get
			{
				return iImageHeight;
			}
			set
			{
				iImageHeight = value;
			}
		}

		public int LabelWidth
		{
			get
			{
				return iLabelWidth;
			}
			set
			{
				iLabelWidth = value;
			}
		}

		public int ItemCount
		{
			get
			{
				return rgFishEyeItems.Count;
			}
		}

		public void AddFishEyeItem(string Caption, System.Drawing.Image Picture, object UserData)
		{
			FishEyeItem	item;

			item			= new FishEyeItem();
			item.Caption	= Caption;
			item.Picture	= Picture;
			item.UserData	= UserData;

			rgFishEyeItems.Add(item);
			this.Invalidate();
		}

		public void ClearFishEyeItems()
		{
			rgFishEyeItems.Clear();
			this.Invalidate();
		}

		private void FishEye_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			if (bExpanded)
			{
				int				y = 0;
				RectangleF		rc = new Rectangle();
				FishEyeItem		item;
				Brush			brLight = new SolidBrush(Color.FromArgb(245, 245, 245));

				e.Graphics.DrawRectangle(System.Drawing.Pens.DarkGray, 0, 0, this.Width - 1, this.Height - 1);
				e.Graphics.DrawRectangle(System.Drawing.Pens.DarkGray, 0, 0, iTagWidth, this.Height - 1);
				e.Graphics.DrawImage(imgIcon, 1, 1, System.Math.Min(iTagWidth, imgIcon.Width), System.Math.Min(iTagHeight, imgIcon.Height));

				System.Drawing.Drawing2D.Matrix	orgTrans;

				orgTrans = e.Graphics.Transform;
				e.Graphics.TranslateTransform(iTagWidth, System.Math.Min(iTagHeight, imgIcon.Height));
				e.Graphics.RotateTransform(90);
				e.Graphics.DrawString(szCaption, this.Font, System.Drawing.Brushes.Black, 2, (iTagWidth - this.Font.Height) / 2);
			
				e.Graphics.Transform = orgTrans;


				for (int i = 0; i < rgFishEyeItems.Count; i++)
				{
					item = (FishEyeItem)rgFishEyeItems[i];

					if (i == iSelectedIndex)
					{
						float	fRatio	= System.Math.Min(((float)iLabelWidth / (float)item.Picture.Width), ((float)iImageHeight / (float)item.Picture.Height));
						int		iWidth	= (int)(item.Picture.Width * fRatio);
						int		iHeight	= (int)(item.Picture.Height * fRatio);

						e.Graphics.DrawImage(item.Picture, iTagWidth + 5 + (iLabelWidth - iWidth) / 2, y + (iImageHeight - iHeight) / 2, iWidth, iHeight);
						e.Graphics.DrawRectangle(System.Drawing.Pens.Black, iTagWidth, y, iLabelWidth + 10, iImageHeight - 1);

						y += iImageHeight;
					}
					else
					{
						if (i > 0)
						{
							rc.Y	= y;
						}
						else
						{
							rc.Y	= y + 1;
						}
						if (i < rgFishEyeItems.Count - 1)
						{
							rc.Height	= iLabelHeight;
						}
						else
						{
							rc.Height	= iLabelHeight - 1;
						}
						rc.X		= iTagWidth + 1;
						rc.Width	= iLabelWidth + 8;
						e.Graphics.FillRectangle(brLight, rc);

						rc.X		= iTagWidth + 5;
						rc.Y		= y;
						rc.Width	= iLabelWidth;
						rc.Height	= iLabelHeight;
						e.Graphics.DrawString(item.Caption, this.Font, System.Drawing.Brushes.Black, rc);

						y += iLabelHeight;
					}
				}
			}
			else if (imgIcon != null)
			{
				int x = (this.Width - imgIcon.Width) / 2;
				int y = (this.Height - imgIcon.Height) / 2;
				e.Graphics.DrawRectangle(System.Drawing.Pens.Black, x - 1, y - 1, imgIcon.Width + 1, imgIcon.Height + 1);
				e.Graphics.DrawImage(imgIcon, x, y, imgIcon.Width, imgIcon.Height);
			}
		}

		private void FishEye_MouseEnter(object sender, System.EventArgs e)
		{
			if (rgFishEyeItems.Count > 0)
			{
				bExpanded		= true;
				iSelectedIndex	= 0;
				iTagWidth		= this.Width;
				iTagHeight		= this.Height;
				this.Width		= iTagWidth + iLabelWidth + 11;
				this.Height		= iImageHeight + (rgFishEyeItems.Count - 1) * iLabelHeight;
				this.Invalidate();
			}
		}

		private void FishEye_MouseLeave(object sender, System.EventArgs e)
		{
			if (bExpanded)
			{
				bExpanded	= false;
				this.Width	= iTagWidth;
				this.Height	= iTagHeight;
				this.Invalidate();
			}
		}

		private void FishEye_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			int y	= 0;
			int idx = -1;

			for (int i = 0; i < rgFishEyeItems.Count; i++)
			{
				if (e.X > iTagWidth)
				{
					if (i == iSelectedIndex)
					{
						if ((y < e.Y) && (e.Y < y + iImageHeight))
						{
							idx = i;
							break;
						}
						y += iImageHeight;
					}
					else
					{
						if ((y < e.Y) && (e.Y < y + iLabelHeight))
						{
							idx = i;
							break;
						}
						y += iLabelHeight;
					}
				}
			}

			if ((idx != -1) && (idx != iSelectedIndex))
			{
				iSelectedIndex = idx;
				this.Invalidate();
			}
		}

		private void FishEye_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (bExpanded)
			{
				bExpanded = false;
				this.Width	= iTagWidth;
				this.Height	= iTagHeight;
				this.Invalidate();

				if ((iSelectedIndex != -1) && (FishEyeItemSelected != null))
				{
					FishEyeItemSelected((FishEyeItem)rgFishEyeItems[iSelectedIndex]);
				}
			}
		}
	}

	public delegate void FishEyeItemSelectedEvent(FishEyeItem item);

	public class FishEyeItem
	{
		public FishEyeItem()
		{
			Caption = "";
			Picture	= null;
		}

		public string				Caption;
		public object				UserData;
		public System.Drawing.Image	Picture;
	}
}
