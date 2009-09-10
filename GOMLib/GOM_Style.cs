using System;

namespace GOMLib
{
	/// <summary>
	/// The drawing style of a drawing operation
	/// </summary>
	public class GOM_Style_Drawing : GOM_Interface_XmlPersistence
	{
		/// <summary>
		/// The constructor of GOM_Style_Drawing
		/// </summary>
		public GOM_Style_Drawing()
		{
			id = null;
			drawingStyle = new System.Drawing.Pen(System.Drawing.Color.Black);
		}
		/// <summary>
		/// Save this drawing style to XML
		/// </summary>
		/// <param name="writer">The XML writer</param>
		public void SaveToXML(System.Xml.XmlWriter writer)
		{
			writer.WriteStartElement(GOM_TAGS.DRAWING_STYLE);
			writer.WriteAttributeString(GOM_TAGS.ID, this.id);
			writer.WriteAttributeString(GOM_TAGS.RED, this.drawingStyle.Color.R.ToString());
			writer.WriteAttributeString(GOM_TAGS.GREEN, this.drawingStyle.Color.G.ToString());
			writer.WriteAttributeString(GOM_TAGS.BLUE, this.drawingStyle.Color.B.ToString());
			writer.WriteAttributeString(GOM_TAGS.WIDTH, this.drawingStyle.Width.ToString());
			switch (this.drawingStyle.DashStyle)
			{
				case System.Drawing.Drawing2D.DashStyle.Solid:
				{
					writer.WriteAttributeString(GOM_TAGS.STYLE, "solid");
					break;
				}
				case System.Drawing.Drawing2D.DashStyle.Dash:
				{
					writer.WriteAttributeString(GOM_TAGS.STYLE, "dash");
					break;
				}
				case System.Drawing.Drawing2D.DashStyle.DashDot:
				{
					writer.WriteAttributeString(GOM_TAGS.STYLE, "dashdot");
					break;
				}
				case System.Drawing.Drawing2D.DashStyle.DashDotDot:
				{
					writer.WriteAttributeString(GOM_TAGS.STYLE, "dashdotdot");
					break;
				}
				case System.Drawing.Drawing2D.DashStyle.Dot:
				{
					writer.WriteAttributeString(GOM_TAGS.STYLE, "dot");
					break;
				}
			}
			writer.WriteEndElement();
		}

		public void LoadFromXML(System.Xml.XmlNode node, GOM_ResourceArrays resources)
		{
			byte r=0, g=0, b=0;

			for (int i = 0; i < node.Attributes.Count; i++)
			{
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.RED, true) == 0)
				{
					r = byte.Parse(node.Attributes[i].Value);
				}
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.GREEN, true) == 0)
				{
					g = byte.Parse(node.Attributes[i].Value);
				}
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.BLUE, true) == 0)
				{
					b = byte.Parse(node.Attributes[i].Value);
				}
			}

			drawingStyle.Color = System.Drawing.Color.FromArgb(r, g, b);

			for (int i = 0; i < node.Attributes.Count; i++)
			{
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.ID, true) == 0)
				{
					id = node.Attributes[i].Value;
				}
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.STYLE, true) == 0)
				{
					if (System.String.Compare(node.Attributes[i].Value, "solid", true) == 0)
					{
						drawingStyle.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
					}
					if (System.String.Compare(node.Attributes[i].Value, "dash", true) == 0)
					{
						drawingStyle.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
					}
					if (System.String.Compare(node.Attributes[i].Value, "dot", true) == 0)
					{
						drawingStyle.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
					}
					if (System.String.Compare(node.Attributes[i].Value, "dashdot", true) == 0)
					{
						drawingStyle.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
					}
					if (System.String.Compare(node.Attributes[i].Value, "dashdotdot", true) == 0)
					{
						drawingStyle.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot;
					}
				}
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.WIDTH, true) == 0)
				{
					drawingStyle.Width = int.Parse(node.Attributes[i].Value);
				}
			}
		}

		/// <summary>The id of this drawing style</summary>
		public string	id;
		/// <summary>The pen described by this drawing style</summary>
		public System.Drawing.Pen drawingStyle;	
	}



	/// <summary>
	/// The fill style of a filling operation
	/// </summary>
	public class GOM_Style_Filling : GOM_Interface_XmlPersistence
	{
		/// <summary>
		/// The constructor of GOM_Style_Filling
		/// </summary>
		public GOM_Style_Filling()
		{
			id = null;
			fillingStyle = new System.Drawing.SolidBrush(System.Drawing.Color.White);
		}
		/// <summary>
		/// Save this filling style to XML
		/// </summary>
		/// <param name="writer">The XML writer</param>
		public void SaveToXML(System.Xml.XmlWriter writer)
		{
			writer.WriteStartElement(GOM_TAGS.FILLING_STYLE);
			writer.WriteAttributeString(GOM_TAGS.ID, this.id);
			
			if (this.fillingStyle is System.Drawing.SolidBrush)
			{
				writer.WriteAttributeString(GOM_TAGS.RED, ((System.Drawing.SolidBrush)(this.fillingStyle)).Color.R.ToString());
				writer.WriteAttributeString(GOM_TAGS.GREEN, ((System.Drawing.SolidBrush)(this.fillingStyle)).Color.G.ToString());
				writer.WriteAttributeString(GOM_TAGS.BLUE, ((System.Drawing.SolidBrush)(this.fillingStyle)).Color.B.ToString());
				writer.WriteAttributeString(GOM_TAGS.STYLE, "solid");
			}
			else if (this.fillingStyle is System.Drawing.Drawing2D.HatchBrush)
			{
				writer.WriteAttributeString(GOM_TAGS.RED, ((System.Drawing.Drawing2D.HatchBrush)(this.fillingStyle)).ForegroundColor.R.ToString());
				writer.WriteAttributeString(GOM_TAGS.GREEN, ((System.Drawing.Drawing2D.HatchBrush)(this.fillingStyle)).ForegroundColor.G.ToString());
				writer.WriteAttributeString(GOM_TAGS.BLUE, ((System.Drawing.Drawing2D.HatchBrush)(this.fillingStyle)).ForegroundColor.B.ToString());
				switch (((System.Drawing.Drawing2D.HatchBrush)(this.fillingStyle)).HatchStyle)
				{
					case System.Drawing.Drawing2D.HatchStyle.Horizontal:
					{
						writer.WriteAttributeString(GOM_TAGS.STYLE, "horizontal");
						break;
					}
					case System.Drawing.Drawing2D.HatchStyle.Vertical:
					{
						writer.WriteAttributeString(GOM_TAGS.STYLE, "vertical");
						break;
					}
					case System.Drawing.Drawing2D.HatchStyle.Cross:
					{
						writer.WriteAttributeString(GOM_TAGS.STYLE, "cross");
						break;
					}
					case System.Drawing.Drawing2D.HatchStyle.BackwardDiagonal:
					{
						writer.WriteAttributeString(GOM_TAGS.STYLE, "fdiagonal");
						break;
					}
					case System.Drawing.Drawing2D.HatchStyle.ForwardDiagonal:
					{
						writer.WriteAttributeString(GOM_TAGS.STYLE, "bdiagonal");
						break;
					}
					case System.Drawing.Drawing2D.HatchStyle.DiagonalCross:
					{
						writer.WriteAttributeString(GOM_TAGS.STYLE, "diagcross");
						break;
					}
				}
			}

			writer.WriteEndElement();
		}
		public void LoadFromXML(System.Xml.XmlNode node, GOM_ResourceArrays resources)
		{
			byte r=0, g=0, b=0;

			for (int i = 0; i < node.Attributes.Count; i++)
			{
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.RED, true) == 0)
				{
					r = byte.Parse(node.Attributes[i].Value);
				}
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.GREEN, true) == 0)
				{
					g = byte.Parse(node.Attributes[i].Value);
				}
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.BLUE, true) == 0)
				{
					b = byte.Parse(node.Attributes[i].Value);
				}
			}

			System.Drawing.Color color = System.Drawing.Color.FromArgb(r, g, b);

			for (int i = 0; i < node.Attributes.Count; i++)
			{
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.ID, true) == 0)
				{
					id = node.Attributes[i].Value;
				}
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.STYLE, true) == 0)
				{
					if (System.String.Compare(node.Attributes[i].Value, "solid", true) == 0)
					{
						fillingStyle = new System.Drawing.SolidBrush(color);
					}
					if (System.String.Compare(node.Attributes[i].Value, "bdiagonal", true) == 0)
					{
						fillingStyle = new System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.BackwardDiagonal, color, System.Drawing.Color.Transparent);
					}
					if (System.String.Compare(node.Attributes[i].Value, "cross", true) == 0)
					{
						fillingStyle = new System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.Cross, color, System.Drawing.Color.Transparent);
					}
					if (System.String.Compare(node.Attributes[i].Value, "diagcross", true) == 0)
					{
						fillingStyle = new System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.DiagonalCross, color, System.Drawing.Color.Transparent);
					}
					if (System.String.Compare(node.Attributes[i].Value, "fdiagonal", true) == 0)
					{
						fillingStyle = new System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.ForwardDiagonal, color, System.Drawing.Color.Transparent);
					}
					if (System.String.Compare(node.Attributes[i].Value, "horizontal", true) == 0)
					{
						fillingStyle = new System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.Horizontal, color, System.Drawing.Color.Transparent);
					}
					if (System.String.Compare(node.Attributes[i].Value, "vertical", true) == 0)
					{
						fillingStyle = new System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.Vertical, color, System.Drawing.Color.Transparent);
					}
				}
			}
		}

		/// <summary>The id of this filling style</summary>
		public string	id;
		/// <summary>The brush described by this filling style</summary>
		public System.Drawing.Brush fillingStyle;
	}
	/// <summary>
	/// A dynamic list of filling styles
	/// </summary>
	public class GOM_Filling_Styles
	{
		/// <summary>
		/// The constructor of GOM_Filling_Styles
		/// </summary>
		public GOM_Filling_Styles()
		{
			rgStyles = new System.Collections.ArrayList();
		}
		/// <summary>
		/// Return a filling style by its index 
		/// </summary>
		public GOM_Style_Filling this[int idx]
		{
			get
			{
				return (GOM_Style_Filling)rgStyles[idx];
			}
			set
			{
				rgStyles[idx] = value;
			}
		}

		/// <summary>
		/// Return a filling style by its name.
		/// </summary>
		public GOM_Style_Filling this[string name]
		{
			get
			{
				for (int i = 0; i < rgStyles.Count; i++)
				{
					if ( ((GOM_Style_Filling)rgStyles[i]).id.Equals(name) )
					{
						return (GOM_Style_Filling)rgStyles[i];
					}
				}
				return null;
			}
		}
		/// <summary>
		/// Number of filling style in the list
		/// </summary>
		public int Count
		{
			get
			{
				return rgStyles.Count;
			}
		}
		/// <summary>
		/// Add a filling style into the list
		/// </summary>
		/// <param name="style">The filling style being added</param>
		public void Add(GOM_Style_Filling style)
		{
			rgStyles.Add(style);
		}
		public void AddAndReplace(GOM_Style_Filling style)
		{
			Remove(style.id);
			Add(style);
		}

		/// <summary>
		/// Remove a filling style by its index
		/// </summary>
		/// <param name="idx"></param>
		public void RemoveAt(int idx)
		{
			rgStyles.RemoveAt(idx);
		}
		public void Remove(GOM_Style_Filling style)
		{
			rgStyles.Remove(style);
		}
		public GOM_Style_Filling Remove(string name)
		{
			GOM_Style_Filling style = this[name];
			if ( style != null )
			{
				Remove(style);
			}
			return style;
		}
		/// <summary>
		/// Empty the list
		/// </summary>
		public void Clear()
		{
			rgStyles.Clear();
		}
		/// <summary>The list of filling styles</summary>
		private System.Collections.ArrayList rgStyles;
	}	
	/// <summary>
	/// A dynamic list of drawing styles
	/// </summary>
	public class GOM_Drawing_Styles
	{
		/// <summary>
		/// The constructor of GOM_Drawing_Styles
		/// </summary>
		public GOM_Drawing_Styles()
		{
			rgStyles = new System.Collections.ArrayList();
		}
		/// <summary>
		/// Return a drawing style by its index
		/// </summary>
		public GOM_Style_Drawing this[int idx]
		{
			get
			{
				return (GOM_Style_Drawing)rgStyles[idx];
			}
			set
			{
				rgStyles[idx] = value;
			}
		}

		/// <summary>
		/// Return a drawing style by its name.
		/// </summary>
		public GOM_Style_Drawing this[string name]
		{
			get
			{
				for (int i = 0; i < rgStyles.Count; i++)
				{
					if ( ((GOM_Style_Drawing)rgStyles[i]).id.Equals(name) )
					{
						return (GOM_Style_Drawing)rgStyles[i];
					}
				}
				return null;
			}
		}
		/// <summary>
		/// Number of drawing styles in the list
		/// </summary>
		public int Count
		{
			get
			{
				return rgStyles.Count;
			}
		}
		/// <summary>
		/// Add a drawing style into the list
		/// </summary>
		/// <param name="style">The drawing style being added</param>
		public void Add(GOM_Style_Drawing style)
		{
			rgStyles.Add(style);
		}
		public void AddAndReplace(GOM_Style_Drawing style)
		{
			Remove(style.id);
			Add(style);
		}
		/// <summary>
		/// Remove a drawing style by its index
		/// </summary>
		/// <param name="idx"></param>
		public void RemoveAt(int idx)
		{
			rgStyles.RemoveAt(idx);
		}
		public void Remove(GOM_Style_Drawing style)
		{
			rgStyles.Remove(style);
		}
		public GOM_Style_Drawing Remove(string name)
		{
			GOM_Style_Drawing style = this[name];
			if ( style != null )
			{
				Remove(style);
			}
			return style;
		}
		/// <summary>
		/// Empty the list
		/// </summary>
		public void Clear()
		{
			rgStyles.Clear();
		}
		/// <summary>The list of drawing styles</summary>
		private System.Collections.ArrayList rgStyles;
	}	
}