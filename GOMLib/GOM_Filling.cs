using System;

namespace GOMLib
{
	/// <summary>
	/// A filling operation that fills an ellipse
	/// </summary>
	public class GOM_Filling_Ellipse: GOM_Interface_Filling
	{
		public GOM_Filling_Ellipse(System.Xml.XmlNode node, GOM_ResourceArrays resources)
		{
			m_points = new GOM_Points();
			LoadFromXML(node, resources);
		}
		/// <summary>
		/// The constructor of GOM_Filling_Ellipse
		/// </summary>
		/// <param name="leftTop">The top left point of the bounding box of the ellipse</param>
		/// <param name="righgDown">The right down point of the bounding box of the ellipse</param>
		/// <param name="style">The filling style to fill the ellipse</param>
		public GOM_Filling_Ellipse(GOM_Point leftTop, GOM_Point righgDown, GOM_Style_Filling style)
		{
			m_points = new GOM_Points();
			m_points.Add(leftTop);
			m_points.Add(righgDown);
			m_style = style;
		}
		/// <summary>
		/// Save this filling operation to XML
		/// </summary>
		/// <param name="writer">The XML writer</param>
		public void SaveToXML(System.Xml.XmlWriter writer)
		{
			writer.WriteStartElement(GOM_TAGS.ELLIPSE);
			writer.WriteAttributeString(GOM_TAGS.LEFT_TOP, m_points[0].id);
			writer.WriteAttributeString(GOM_TAGS.RIGHT_DOWN, m_points[1].id);
			writer.WriteAttributeString(GOM_TAGS.STYLE, m_style.id);
			writer.WriteEndElement();
		}
		public void LoadFromXML(System.Xml.XmlNode node, GOM_ResourceArrays resources)
		{
			GOM_Point			leftTop		= null;
			GOM_Point			rightDown	= null;
			GOM_Style_Filling	style;

			style = resources.FillingStyles["default"];

			for (int i = 0; i < node.Attributes.Count; i++)
			{
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.STYLE, true) == 0)
				{
					style = resources.FillingStyles[node.Attributes[i].Value];
				}
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.LEFT_TOP, true) == 0)
				{
					leftTop = resources.Points[node.Attributes[i].Value];
				}
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.RIGHT_DOWN, true) == 0)
				{
					rightDown = resources.Points[node.Attributes[i].Value];
				}
			}

			if ((leftTop == null) || (rightDown == null))
			{
				throw new Exception("Missing points in ellipse filling");
			}

			if (style == null)
			{
				throw new Exception("Missing style in ellipse filling");
			}

			this.m_points.Add(leftTop);
			this.m_points.Add(rightDown);
			this.m_style = style;
		}

		/// <summary>
		/// Fill an ellispe on the canvas
		/// </summary>
		/// <param name="canvas">The canvas in which an ellipse is filled</param>
		public void Fill(System.Drawing.Graphics canvas)
		{
			canvas.FillEllipse(m_style.fillingStyle, m_points[0].x, m_points[0].y, m_points[1].x - m_points[0].x, m_points[1].y - m_points[0].y);
		}
		/// <summary>
		/// Return the bounding box of this drawing operation
		/// </summary>
		public System.Drawing.RectangleF BoundingBox
		{
			get
			{
				return new System.Drawing.RectangleF(m_points[0].x, m_points[0].y, m_points[1].x - m_points[0].x, m_points[1].y - m_points[0].y);
			}
		}
		/// <summary>
		/// The filling style of this filling operation
		/// </summary>
		public GOM_Style_Filling Style
		{
			get
			{
				return m_style;
			}
			set
			{
				m_style = value;
			}
		}
		/// <summary>
		/// The points used to define an ellipse
		/// </summary>
		public GOM_Points Points
		{
			get
			{
				return m_points;
			}
		}
		/// <summary>
		/// The type of this filling operation (Ellipse)
		/// </summary>
		public FillingType Type
		{
			get
			{
				return FillingType.Ellipse;
			}
		}
		/// <summary>The filling style of this filling operation</summary>
		private GOM_Style_Filling	m_style;
		/// <summary>The point used to define an ellipse area</summary>
		private GOM_Points			m_points;
	}
	/// <summary>
	/// A filling operation that fills a rectangle
	/// </summary>
	public class GOM_Filling_Rectangle: GOM_Interface_Filling
	{
		public GOM_Filling_Rectangle(System.Xml.XmlNode node, GOM_ResourceArrays resources)
		{
			m_points = new GOM_Points();
			LoadFromXML(node, resources);
		}
		/// <summary>
		/// The constructor of GOM_Filling_Rectangle
		/// </summary>
		/// <param name="leftTop">The top left corner of the rectangle</param>
		/// <param name="righgDown">The right down corner of the rectangle</param>
		/// <param name="style">The filling style to fill the rectangle</param>
		public GOM_Filling_Rectangle(GOM_Point leftTop, GOM_Point righgDown, GOM_Style_Filling style)
		{
			m_points = new GOM_Points();
			m_points.Add(leftTop);
			m_points.Add(righgDown);
			m_style = style;
		}
		/// <summary>
		/// Save this filling opeartion to XML
		/// </summary>
		/// <param name="writer">The XML writer</param>
		public void SaveToXML(System.Xml.XmlWriter writer)
		{
			writer.WriteStartElement(GOM_TAGS.RECTANGLE);
			writer.WriteAttributeString(GOM_TAGS.LEFT_TOP, m_points[0].id);
			writer.WriteAttributeString(GOM_TAGS.RIGHT_DOWN, m_points[1].id);
			writer.WriteAttributeString(GOM_TAGS.STYLE, m_style.id);
			writer.WriteEndElement();
		}
		public void LoadFromXML(System.Xml.XmlNode node, GOM_ResourceArrays resources)
		{
			GOM_Point				leftTop		= null;
			GOM_Point				rightDown	= null;
			GOM_Style_Filling		style;

			style = resources.FillingStyles["default"];

			for (int i = 0; i < node.Attributes.Count; i++)
			{
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.STYLE, true) == 0)
				{
					style = resources.FillingStyles[node.Attributes[i].Value];
				}
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.LEFT_TOP, true) == 0)
				{
					leftTop = resources.Points[node.Attributes[i].Value];
				}
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.RIGHT_DOWN, true) == 0)
				{
					rightDown = resources.Points[node.Attributes[i].Value];
				}
			}

			if ((leftTop == null) || (rightDown == null))
			{
				throw new Exception("Missing points in rectangle filling");
			}

			if (style == null)
			{
				throw new Exception("Missing style in rectangle filling");
			}

			this.m_points.Add(leftTop);
			this.m_points.Add(rightDown);
			this.m_style = style;
		}

		/// <summary>
		/// Fill a rectangle on the canvas
		/// </summary>
		/// <param name="canvas">The canvas in which a rectangle is filled</param>
		public void Fill(System.Drawing.Graphics canvas)
		{
			canvas.FillRectangle(m_style.fillingStyle, m_points[0].x, m_points[0].y, m_points[1].x - m_points[0].x, m_points[1].y - m_points[0].y);
		}
		/// <summary>
		/// Return the bounding box of this drawing operation
		/// </summary>
		public System.Drawing.RectangleF BoundingBox
		{
			get
			{
				return new System.Drawing.RectangleF(m_points[0].x, m_points[0].y, m_points[1].x - m_points[0].x, m_points[1].y - m_points[0].y);
			}
		}
		/// <summary>
		/// The filling style to fill a rectangle
		/// </summary>
		public GOM_Style_Filling Style
		{
			get
			{
				return m_style;
			}
			set
			{
				m_style = value;
			}
		}
		/// <summary>
		/// The points used to define a rectangle
		/// </summary>
		public GOM_Points Points
		{
			get
			{
				return m_points;
			}
		}
		/// <summary>
		/// The type of this filling operation (Rectangle)
		/// </summary>
		public FillingType Type
		{
			get
			{
				return FillingType.Rectangle;
			}
		}
		/// <summary>The filling style to fill this rectangle</summary>
		private GOM_Style_Filling	m_style;
		/// <summary>The points used to define the rectangle</summary>
		private GOM_Points			m_points;
	}
	/// <summary>
	/// A filling operation that fills a pie
	/// </summary>
	public class GOM_Filling_Pie: GOM_Interface_Filling
	{
		public GOM_Filling_Pie(System.Xml.XmlNode node, GOM_ResourceArrays resources)
		{
			m_points = new GOM_Points();
			LoadFromXML(node, resources);
		}
		/// <summary>
		/// The constructor of GOM_Filling_Pie
		/// </summary>
		/// <param name="leftTop">The top left corner of the bounding box of the ellipse which the pie belongs to</param>
		/// <param name="rightDown">The right down corner of the bounding box of the ellipse which the pie belongs to</param>
		/// <param name="startPoint">The start-point of the pie</param>
		/// <param name="endPoint">The end-point of the pie</param>
		/// <param name="style">The filling style to fill the pie</param>
		public GOM_Filling_Pie(GOM_Point leftTop, GOM_Point rightDown, GOM_Point startPoint, GOM_Point endPoint, GOM_Style_Filling style)
		{
			m_points = new GOM_Points();
			m_points.Add(leftTop);
			m_points.Add(rightDown);
			m_points.Add(startPoint);
			m_points.Add(endPoint);
			m_style = style;
		}
		/// <summary>
		/// Save this filling operetion to XML
		/// </summary>
		/// <param name="writer">The XML writer</param>
		public void SaveToXML(System.Xml.XmlWriter writer)
		{
			writer.WriteStartElement(GOM_TAGS.PIE);
			writer.WriteAttributeString(GOM_TAGS.LEFT_TOP, m_points[0].id);
			writer.WriteAttributeString(GOM_TAGS.RIGHT_DOWN, m_points[1].id);
			writer.WriteAttributeString(GOM_TAGS.START_POINT, m_points[2].id);
			writer.WriteAttributeString(GOM_TAGS.END_POINT, m_points[3].id);
			writer.WriteAttributeString(GOM_TAGS.STYLE, m_style.id);
			writer.WriteEndElement();
		}
		public void LoadFromXML(System.Xml.XmlNode node, GOM_ResourceArrays resources)
		{
			GOM_Point			startPt		= null;
			GOM_Point			endPt		= null;
			GOM_Point			leftTop		= null;
			GOM_Point			rightDown	= null;
			GOM_Style_Filling	style;

			style = resources.FillingStyles["default"];

			for (int i = 0; i < node.Attributes.Count; i++)
			{
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.STYLE, true) == 0)
				{
					style = resources.FillingStyles[node.Attributes[i].Value];
				}
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.START_POINT, true) == 0)
				{
					startPt = resources.Points[node.Attributes[i].Value];
				}
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.END_POINT, true) == 0)
				{
					endPt = resources.Points[node.Attributes[i].Value];
				}
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.LEFT_TOP, true) == 0)
				{
					leftTop = resources.Points[node.Attributes[i].Value];
				}
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.RIGHT_DOWN, true) == 0)
				{
					rightDown = resources.Points[node.Attributes[i].Value];
				}
			}

			if ((startPt == null) || (endPt == null) || (leftTop == null) || (rightDown == null))
			{
				throw new Exception("Missing points in pie filling");
			}

			if (style == null)
			{
				throw new Exception("Missing style in pie filling");
			}

			this.m_points.Add(leftTop);
			this.m_points.Add(rightDown);
			this.m_points.Add(startPt);
			this.m_points.Add(endPt);
			this.m_style = style;
		}

		/// <summary>
		/// Fill a pie on the canvas
		/// </summary>
		/// <param name="canvas">The canvas in which a pie is filled</param>
		public void Fill(System.Drawing.Graphics canvas)
		{
			double	startAngle, endAngle;

			startAngle = System.Math.Atan2(m_points[2].y - (m_points[1].y + m_points[0].y) / 2, m_points[2].x - (m_points[1].x + m_points[0].x) / 2);
			if (startAngle < 0) 
			{
				startAngle += System.Math.PI * 2;
			}
			startAngle = (startAngle / Math.PI) * 180;

			endAngle = System.Math.Atan2(m_points[3].y - (m_points[1].y + m_points[0].y) / 2, m_points[3].x - (m_points[1].x + m_points[0].x) / 2);
			if (endAngle < 0)
			{
				endAngle += System.Math.PI * 2;
			}
			endAngle = (endAngle / Math.PI) * 180;

			endAngle -= startAngle;
			if (endAngle < 0)
			{
				endAngle += 360;
			}

			canvas.FillPie(m_style.fillingStyle, m_points[0].x, m_points[0].y, m_points[1].x - m_points[0].x, m_points[1].y - m_points[0].y, (float)startAngle, (float)endAngle);
		}
		/// <summary>
		/// Return the bounding box of this drawing operation
		/// </summary>
		public System.Drawing.RectangleF BoundingBox
		{
			get
			{
				return new System.Drawing.RectangleF(m_points[0].x, m_points[0].y, m_points[1].x - m_points[0].x, m_points[1].y - m_points[0].y);
			}
		}
		/// <summary>
		/// The filling style to fill the pie
		/// </summary>
		public GOM_Style_Filling Style
		{
			get
			{
				return m_style;
			}
			set
			{
				m_style = value;
			}
		}
		/// <summary>
		/// The points used to define the pie
		/// </summary>
		public GOM_Points Points
		{
			get
			{
				return m_points;
			}
		}
		/// <summary>
		/// The type of the filling operation (Pie)
		/// </summary>
		public FillingType Type
		{
			get
			{
				return FillingType.Pie;
			}
		}
		/// <summary>The filling style to fill the pie</summary>
		private GOM_Style_Filling	m_style;
		/// <summary>List of points used to define the pie</summary>
		private GOM_Points			m_points;
	}
	/// <summary>
	/// A filling operation that fills a polygon
	/// </summary>
	public class GOM_Filling_Polygon: GOM_Interface_Filling
	{
		public GOM_Filling_Polygon(System.Xml.XmlNode node, GOM_ResourceArrays resources)
		{
			LoadFromXML(node, resources);
		}
		/// <summary>
		/// The constructor of GOM_Filling_Polygon
		/// </summary>
		/// <param name="points">The vertices of the polygon</param>
		/// <param name="style">The filling style to fill the polygon</param>
		public GOM_Filling_Polygon(GOM_Points points, GOM_Style_Filling style)
		{
			m_points = points;
			m_style = style;
		}
		/// <summary>
		/// Save the filling operation to XML
		/// </summary>
		/// <param name="writer">The XML writer</param>
		public void SaveToXML(System.Xml.XmlWriter writer)
		{
			writer.WriteStartElement(GOM_TAGS.POLYGON);
			writer.WriteAttributeString(GOM_TAGS.STYLE, m_style.id);
			for (int i = 0; i < m_points.Count; i++)
			{
				writer.WriteStartElement(GOM_TAGS.VERTEX);
				writer.WriteAttributeString(GOM_TAGS.POINT, m_points[i].id);
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
		}
		public void LoadFromXML(System.Xml.XmlNode node, GOM_ResourceArrays resources)
		{
			GOM_Point			pt;
			GOM_Points			rgPoints;
			GOM_Style_Filling	style;

			style = resources.FillingStyles["default"];
			rgPoints = new GOM_Points();

			for (int i = 0; i < node.Attributes.Count; i++)
			{
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.STYLE, true) == 0)
				{
					style = resources.FillingStyles[node.Attributes[i].Value];
				}
			}

			for (int i = 0; i < node.ChildNodes.Count; i++)
			{
				if (System.String.Compare(node.ChildNodes[i].Name, GOM_TAGS.VERTEX, true) == 0)
				{
					for (int j = 0; j < node.ChildNodes[i].Attributes.Count; j++)
					{
						if (System.String.Compare(node.ChildNodes[i].Attributes[j].Name, GOM_TAGS.POINT, true) == 0)
						{
							pt = resources.Points[node.ChildNodes[i].Attributes[j].Value];

							if (pt == null)
							{
								throw new Exception("Missing points in polygon filling");
							}

							rgPoints.Add(pt);
						}
					}
				}
			}

			if (style == null)
			{
				throw new Exception("Missing style in polygon fillin");
			}

			this.m_points = rgPoints;
			this.m_style  = style;
		}

		/// <summary>
		/// Fill a polygon on the canvas
		/// </summary>
		/// <param name="canvas">The canvas on which a polygon is filled</param>
		public void Fill(System.Drawing.Graphics canvas)
		{
			System.Drawing.Point[]	rgPoints;

			rgPoints = new System.Drawing.Point[m_points.Count];
			for (int i = 0; i < m_points.Count; i++)
			{
				rgPoints[i].X = (int)m_points[i].x;
				rgPoints[i].Y = (int)m_points[i].y;
			}

			canvas.FillPolygon(m_style.fillingStyle, rgPoints);
		}
		/// <summary>
		/// Return the bounding box of this drawing operation
		/// </summary>
		public System.Drawing.RectangleF BoundingBox
		{
			get
			{
				float minX, minY, maxX, maxY;

				minX = m_points[0].x;
				minY = m_points[0].y;
				maxX = m_points[0].x;
				maxY = m_points[0].y;

				for (int i = 1; i < m_points.Count; i++)
				{
					minX = System.Math.Min(minX, m_points[i].x);
					minY = System.Math.Min(minY, m_points[i].y);
					maxX = System.Math.Max(maxX, m_points[i].x);
					maxY = System.Math.Max(maxY, m_points[i].y);
				}

				return new System.Drawing.RectangleF(minX, minY, maxX - minX, maxY - minY);
			}
		}
		/// <summary>
		/// The filling style to fill the polygon
		/// </summary>
		public GOM_Style_Filling Style
		{
			get
			{
				return m_style;
			}
			set
			{
				m_style = value;
			}
		}
		/// <summary>
		/// The vertices of the polygon
		/// </summary>
		public GOM_Points Points
		{
			get
			{
				return m_points;
			}
		}
		/// <summary>
		/// The type of this filling operation (Polygon)
		/// </summary>
		public FillingType Type
		{
			get
			{
				return FillingType.Polygon;
			}
		}
		/// <summary>The filling style to fill a polygon</summary>
		private GOM_Style_Filling	m_style;
		/// <summary>List of points which are vertices of the polygon</summary>
		private GOM_Points			m_points;
	}
	/// <summary>
	/// A filling operation that fills a closed path composed of several drawing operations
	/// </summary>
	public class GOM_Filling_FillPath: GOM_Interface_Filling
	{
		public GOM_Filling_FillPath(System.Xml.XmlNode node, GOM_ResourceArrays resources)
		{
			m_points = new GOM_Points();
			LoadFromXML(node, resources);
		}
		/// <summary>
		/// The constructor of GOM_Filling_FillPath
		/// </summary>
		/// <param name="drawings">A list of drawing operation</param>
		/// <param name="style">The filling style to fill the path</param>
		public GOM_Filling_FillPath(GOM_Drawings drawings, GOM_Style_Filling style)
		{
			m_points = new GOM_Points();
			m_style = style;
			m_drawings = drawings;
		}
		/// <summary>
		/// Save the filling operation to XML
		/// </summary>
		/// <param name="writer">The XML writer</param>
		public void SaveToXML(System.Xml.XmlWriter writer)
		{
			writer.WriteStartElement(GOM_TAGS.POLYGON);
			writer.WriteAttributeString(GOM_TAGS.STYLE, m_style.id);
			for (int i = 0; i < m_drawings.Count; i++)
			{
				m_drawings[i].SaveToXML(writer);
			}
			writer.WriteEndElement();
		}
		public void LoadFromXML(System.Xml.XmlNode node, GOM_ResourceArrays resources)
		{
			GOM_Drawings			drawings;
			GOM_Style_Filling		style;

			style = resources.FillingStyles["default"];
			drawings = new GOM_Drawings();

			for (int i = 0; i < node.Attributes.Count; i++)
			{
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.STYLE, true) == 0)
				{
					style = resources.FillingStyles[node.Attributes[i].Value];
				}
			}

			for (int i = 0; i < node.ChildNodes.Count; i++)
			{
				GOM_Utility.LoadDrawingFromXML(node.ChildNodes[i], drawings, resources);
			}

			if (style == null)
			{
				throw new Exception("Missing style in polygon fillin");
			}

			this.m_style = style;
			this.m_drawings = drawings;
		}

		/// <summary>
		/// Fill a closed path composed of several drawing operations
		/// </summary>
		/// <param name="canvas"></param>
		public void Fill(System.Drawing.Graphics canvas)
		{
			System.Drawing.Drawing2D.GraphicsPath path;

			path = new System.Drawing.Drawing2D.GraphicsPath();
			for (int i = 0; i < m_drawings.Count; i++)
			{
				switch (m_drawings[i].Type)
				{
					case DrawingType.Line:
					{
						path.AddLine(m_drawings[i].Points[0].x, m_drawings[i].Points[0].y, m_drawings[i].Points[1].x, m_drawings[i].Points[1].y);
						break;
					}
					case DrawingType.Arc:
					{
						double	startAngle, endAngle;

						startAngle = System.Math.Atan2(m_drawings[i].Points[2].y - (m_drawings[i].Points[1].y + m_drawings[i].Points[0].y) / 2, m_drawings[i].Points[2].x - (m_drawings[i].Points[1].x + m_drawings[i].Points[0].x) / 2);
						if (startAngle < 0) 
						{
							startAngle += System.Math.PI * 2;
						}
						startAngle = (startAngle / Math.PI) * 180;

						endAngle = System.Math.Atan2(m_drawings[i].Points[3].y - (m_drawings[i].Points[1].y + m_drawings[i].Points[0].y) / 2, m_drawings[i].Points[3].x - (m_drawings[i].Points[1].x + m_drawings[i].Points[0].x) / 2);
						if (endAngle < 0)
						{
							endAngle += System.Math.PI * 2;
						}
						endAngle = (endAngle / Math.PI) * 180;

						path.AddArc(m_drawings[i].Points[0].x, m_drawings[i].Points[0].y, m_drawings[i].Points[1].x - m_drawings[i].Points[0].x, m_drawings[i].Points[1].y - m_drawings[i].Points[0].y, (float)startAngle, Math.Abs((float)(endAngle - startAngle)));
						break;
					}
					case DrawingType.Bezier:
					{
						path.AddBezier(m_drawings[i].Points[0].x, m_drawings[i].Points[0].y, m_drawings[i].Points[2].x, m_drawings[i].Points[2].y, m_drawings[i].Points[3].x, m_drawings[i].Points[3].y, m_drawings[i].Points[1].x, m_drawings[i].Points[1].y);
						break;
					}
				}
			}

			canvas.FillPath(m_style.fillingStyle, path);
		}
		/// <summary>
		/// Return the bounding box of this drawing operation
		/// </summary>
		public System.Drawing.RectangleF BoundingBox
		{
			get
			{
				float minX, minY, maxX, maxY;
				System.Drawing.RectangleF	rc;

				rc = m_drawings[0].BoundingBox;
				minX = rc.Left;
				minY = rc.Top;
				maxX = rc.Right;
				maxY = rc.Bottom;

				for (int i = 1; i < m_drawings.Count; i++)
				{
					rc = m_drawings[i].BoundingBox;
					minX = System.Math.Min(minX, rc.Left);
					minY = System.Math.Min(minY, rc.Top);
					maxX = System.Math.Max(maxX, rc.Right);
					maxY = System.Math.Max(maxY, rc.Bottom);
				}

				return new System.Drawing.RectangleF(minX, minY, maxX - minX, maxY - minY);
			}
		}
		/// <summary>
		/// The filling style to fill the path
		/// </summary>
		public GOM_Style_Filling Style
		{
			get
			{
				return m_style;
			}
			set
			{
				m_style = value;
			}
		}
		/// <summary>
		/// Empty list of points
		/// </summary>
		public GOM_Points Points
		{
			get
			{
				return m_points;
			}
		}
		/// <summary>
		/// List of drawing operations
		/// </summary>
		public GOM_Drawings Drawings
		{
			get
			{
				return m_drawings;
			}
		}
		/// <summary>
		/// The type of this filling operation (Path)
		/// </summary>
		public FillingType Type
		{
			get
			{
				return FillingType.Path;
			}
		}
		/// <summary>The filling style to fill the path</summary>
		private GOM_Style_Filling	m_style;
		/// <summary>Empty list of points</summary>
		private GOM_Points			m_points;
		/// <summary>The list of drawing operations</summary>
		private GOM_Drawings		m_drawings;
	}
	/// <summary>
	/// A dynamic list of filling operations
	/// </summary>
	public class GOM_Fillings
	{
		/// <summary>
		/// The constructor of GOM_Fillings
		/// </summary>
		public GOM_Fillings()
		{
			rgFillings = new System.Collections.ArrayList();
		}
		/// <summary>
		/// Return a filling operation by its index
		/// </summary>
		public GOM_Interface_Filling this[int idx]
		{
			get
			{
				return (GOM_Interface_Filling)rgFillings[idx];
			}
			set
			{
				rgFillings[idx] = value;
			}
		}
		/// <summary>
		/// Number of filling operations in the list
		/// </summary>
		public int Count
		{
			get
			{
				return rgFillings.Count;
			}
		}
		/// <summary>
		/// Add a filling operation to the list
		/// </summary>
		/// <param name="filling">The filling operation being added</param>
		public void Add(GOM_Interface_Filling filling)
		{
			rgFillings.Add(filling);
		}
		/// <summary>
		/// Remove a filling operation by its index
		/// </summary>
		/// <param name="idx"></param>
		public void RemoveAt(int idx)
		{
			rgFillings.RemoveAt(idx);
		}
		/// <summary>
		/// Empty the list
		/// </summary>
		public void Clear()
		{
			rgFillings.Clear();
		}
		/// <summary>
		/// The dynamic list of filling operations
		/// </summary>
		private System.Collections.ArrayList rgFillings;
	}	
}