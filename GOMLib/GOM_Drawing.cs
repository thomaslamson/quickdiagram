using System;

namespace GOMLib
{
	/// <summary>
	/// The line drawing operation
	/// </summary>
	public class GOM_Drawing_Line: GOM_Interface_Drawing
	{
		public GOM_Drawing_Line( System.Xml.XmlNode node, GOM_ResourceArrays resources)
		{
			m_points = new GOM_Points();
			LoadFromXML(node, resources);
		}
		/// <summary>
		/// The constructor of GOM_Drawing_Line
		/// </summary>
		/// <param name="startPoint">The start-point of the line</param>
		/// <param name="endPoint">The end-point of the line</param>
		/// <param name="style">The drawing style of line</param>
		public GOM_Drawing_Line(GOM_Point startPoint, GOM_Point endPoint, GOM_Style_Drawing style)
		{
			m_points = new GOM_Points();
			m_points.Add(startPoint);
			m_points.Add(endPoint);
			m_style = style;
		}
		/// <summary>
		/// Save this line drawing operation to XML
		/// </summary>
		/// <param name="writer">The XML writer</param>
		public void SaveToXML(System.Xml.XmlWriter writer)
		{
			writer.WriteStartElement(GOM_TAGS.LINE);
			writer.WriteAttributeString(GOM_TAGS.START_POINT, m_points[0].id);
			writer.WriteAttributeString(GOM_TAGS.END_POINT, m_points[1].id);
			writer.WriteAttributeString(GOM_TAGS.STYLE, m_style.id);
			writer.WriteEndElement();
		}
		public void LoadFromXML(System.Xml.XmlNode node, GOM_ResourceArrays resources)
		{
			GOM_Point			startPt	= null;
			GOM_Point			endPt	= null;
			GOM_Style_Drawing	style;

			style = resources.DrawingStyles["default"];

			for (int i = 0; i < node.Attributes.Count; i++)
			{
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.STYLE, true) == 0)
				{
					style = resources.DrawingStyles[node.Attributes[i].Value];
				}
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.START_POINT, true) == 0)
				{
					startPt = resources.Points[node.Attributes[i].Value];
				}
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.END_POINT, true) == 0)
				{
					endPt = resources.Points[node.Attributes[i].Value];
				}
			}

			if ((startPt == null) || (endPt == null))
			{
				throw new Exception("Missing points in line drawing");
			}

			if (style == null)
			{
				throw new Exception("Missing style in line drawing");
			}

			this.m_points.Add(startPt);
			this.m_points.Add(endPt);
			m_style = style;
		}

		/// <summary>
		/// Draw a line on the canvas
		/// </summary>
		/// <param name="canvas">The canvas on which the line is drawn</param>
		public void Draw(System.Drawing.Graphics canvas)
		{
			canvas.DrawLine(m_style.drawingStyle, m_points[0].x, m_points[0].y, m_points[1].x, m_points[1].y);
		}
		/// <summary>
		/// Return the bounding box of this drawing operation
		/// </summary>
		public System.Drawing.RectangleF BoundingBox
		{
			get
			{
				float	x, y;
				float	width, height;

				x	= System.Math.Min(m_points[0].x, m_points[1].x);
				y	= System.Math.Min(m_points[0].y, m_points[1].y);

				width	= System.Math.Abs(m_points[0].x - m_points[1].x);
				height	= System.Math.Abs(m_points[0].y - m_points[1].y);

				return new System.Drawing.RectangleF(x, y, width, height);
			}
		}
		/// <summary>
		/// The drawing style of this line
		/// </summary>
		public GOM_Style_Drawing Style
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
		/// The start and end-point of this line
		/// </summary>
		public GOM_Points Points
		{
			get
			{
				return m_points;
			}
		}
		/// <summary>
		/// The type of this drawing operation (Line)
		/// </summary>
		public DrawingType	Type
		{
			get
			{
				return DrawingType.Line;
			}
		}
		/// <summary>
		/// The drawing style of this line
		/// </summary>
		private GOM_Style_Drawing	m_style;
		/// <summary>
		/// The start and end-point of this line
		/// </summary>
		private GOM_Points			m_points;
	}
	/// <summary>
	/// The arc drawing operation
	/// </summary>
	/// <remarks>If the start-point and end-point refer to the same point, the arc will be an ellipse.</remarks>
	public class GOM_Drawing_Arc: GOM_Interface_Drawing
	{
		/// <summary>
		/// The contructor of GOM_Drawing_Arc. It loads its data from xml node.
		/// </summary>
		/// <param name="node"></param>
		/// <param name="resources"></param>
		public GOM_Drawing_Arc(System.Xml.XmlNode node, GOM_ResourceArrays resources)
		{
			m_points = new GOM_Points();
			LoadFromXML(node, resources );
		}
		/// <summary>
		/// The constructor of GOM_Drawing_Arc
		/// </summary>
		/// <param name="leftTop">The left top corner of the bounding box of the ellipse which the arc belongs to</param>
		/// <param name="rightDown">The right down corner of the bounding box of the ellipse which the arc belongs to</param>
		/// <param name="startPoint">The start-point of the arc</param>
		/// <param name="endPoint">The end-point of the arc</param>
		/// <param name="style">The drawing style of the arc</param>
		public GOM_Drawing_Arc(GOM_Point leftTop, GOM_Point rightDown, GOM_Point startPoint, GOM_Point endPoint, GOM_Style_Drawing style, float rotateAngle)
		{
			m_points = new GOM_Points();
			m_points.Add(leftTop);
			m_points.Add(rightDown);
			m_points.Add(startPoint);
			m_points.Add(endPoint);
			m_style = style;
			m_rotateAngle = rotateAngle;
		}
		/// <summary>
		/// Save the arc drawing operation to XML
		/// </summary>
		/// <param name="writer">The XML writer</param>
		public void SaveToXML(System.Xml.XmlWriter writer)
		{
			writer.WriteStartElement(GOM_TAGS.ARC);
			writer.WriteAttributeString(GOM_TAGS.LEFT_TOP, m_points[0].id);
			writer.WriteAttributeString(GOM_TAGS.RIGHT_DOWN, m_points[1].id);
			writer.WriteAttributeString(GOM_TAGS.START_POINT, m_points[2].id);
			writer.WriteAttributeString(GOM_TAGS.END_POINT, m_points[3].id);
			writer.WriteAttributeString(GOM_TAGS.STYLE, m_style.id);
			writer.WriteAttributeString(GOM_TAGS.ROTATION, m_rotateAngle.ToString());
			writer.WriteEndElement();
		}
		public void LoadFromXML(System.Xml.XmlNode node, GOM_ResourceArrays resources)
		{
			GOM_Point			startPt		= null;
			GOM_Point			endPt		= null;
			GOM_Point			leftTop		= null;
			GOM_Point			rightDown	= null;
			GOM_Style_Drawing	style;

			m_rotateAngle = 0F;
			style = resources.DrawingStyles["default"];

			for (int i = 0; i < node.Attributes.Count; i++)
			{
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.STYLE, true) == 0)
				{
					style = resources.DrawingStyles[node.Attributes[i].Value];
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
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.ROTATION, true) == 0)
				{
					m_rotateAngle = float.Parse(node.Attributes[i].Value);
				}
			}

			if ((startPt == null) || (endPt == null) || (leftTop == null) || (rightDown == null))
			{
				throw new Exception("Missing points in arc drawing");
			}

			if (style == null)
			{
				throw new Exception("Missing style in arc drawing");
			}

			m_points.Add(leftTop);
			m_points.Add(rightDown);
			m_points.Add(startPt);
			m_points.Add(endPt);
			m_style = style;
		}

		/// <summary>
		/// Draw an arc on the canvas
		/// </summary>
		/// <param name="canvas">The canvas on which the arc is drawn</param>
		public void Draw(System.Drawing.Graphics canvas)
		{
			System.Drawing.Drawing2D.Matrix	matrix;
			double	startAngle, endAngle;

			matrix = canvas.Transform;

			canvas.TranslateTransform((m_points[0].x + m_points[1].x) / 2, (m_points[0].y + m_points[1].y) / 2);
			canvas.RotateTransform((float)(m_rotateAngle / System.Math.PI * 180));

			if (m_points[2].Equals(m_points[3]))
			{
				canvas.DrawEllipse(m_style.drawingStyle, -(m_points[1].x - m_points[0].x) / 2, -(m_points[1].y - m_points[0].y) / 2, m_points[1].x - m_points[0].x, m_points[1].y - m_points[0].y);
			}
			else
			{
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

				canvas.DrawArc(m_style.drawingStyle, -(m_points[1].x - m_points[0].x) / 2, -(m_points[1].y - m_points[0].y) / 2, m_points[1].x - m_points[0].x, m_points[1].y - m_points[0].y, (float)startAngle, (float)endAngle);
			}

			canvas.Transform = matrix;
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
		/// The drawing style of the arc
		/// </summary>
		public GOM_Style_Drawing Style
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
		/// The points used to define the arc
		/// </summary>
		public GOM_Points Points
		{
			get
			{
				return m_points;
			}
		}
		/// <summary>
		/// The type of the drawing operation (Arc)
		/// </summary>
		public DrawingType	Type
		{
			get
			{
				return DrawingType.Arc;
			}
		}

		public float RotateAngle
		{
			get
			{
				return m_rotateAngle;
			}
			set
			{
				m_rotateAngle = value;
			}
		}
		/// <summary>The drawing style of the arc</summary>
		private GOM_Style_Drawing	m_style;
		/// <summary>List of points used to define the arc</summary>
		private GOM_Points			m_points;
		private float				m_rotateAngle;
	}
	/// <summary>
	/// The bezier drawing operation
	/// </summary>
	public class GOM_Drawing_Bezier: GOM_Interface_Drawing
	{
		public GOM_Drawing_Bezier(System.Xml.XmlNode node, GOM_ResourceArrays resources)
		{
			LoadFromXML(node, resources);
		}
		/// <summary>
		/// The constructor of GOM_Drawing_Bezier
		/// </summary>
		/// <param name="points">The points used to define the bezier curve</param>
		/// <param name="style">The drawing style of the bezier curve</param>
		public GOM_Drawing_Bezier(GOM_Points points, GOM_Style_Drawing style)
		{
			m_style = style;
			m_points = points;
		}
		/// <summary>
		/// Save the bezier drawing operation to XML
		/// </summary>
		/// <param name="writer">The XML writer</param>
		public void SaveToXML(System.Xml.XmlWriter writer)
		{
			writer.WriteStartElement(GOM_TAGS.BEZIER);
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
			GOM_Style_Drawing	style;

			style = resources.DrawingStyles["default"];
			rgPoints = new GOM_Points();

			for (int i = 0; i < node.Attributes.Count; i++)
			{
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.STYLE, true) == 0)
				{
					style = resources.DrawingStyles[node.Attributes[i].Value];
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
								throw new Exception("Missing points in bezier drawing");
							}

							rgPoints.Add(pt);
						}
					}
				}
			}

			if (rgPoints.Count < 4)
			{
				throw new Exception("Missing points in bezier drawing");
			}

			if (((rgPoints.Count - 4) % 3) != 0)
			{
				throw new Exception("Incorrect number of points in bezier drawing");
			}

			if (style == null)
			{
				throw new Exception("Missing style in bezier drawing");
			}

			this.m_points = rgPoints;
			this.m_style = style;
		}

		/// <summary>
		/// Draw a bezier curve on the canvas
		/// </summary>
		/// <param name="canvas">The canvas on which the bezier curve is drawn</param>
		public void Draw(System.Drawing.Graphics canvas)
		{
			System.Drawing.Point[]	rgPoints;

			rgPoints = new System.Drawing.Point[m_points.Count];
			for (int i = 0; i < m_points.Count; i++)
			{
				rgPoints[i].X = (int)m_points[i].x;
				rgPoints[i].Y = (int)m_points[i].y;
			}
			
			canvas.DrawBeziers(m_style.drawingStyle, rgPoints);
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
		/// The drawing style of the bezier curve
		/// </summary>
		public GOM_Style_Drawing Style
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
		/// The points used to define the bezier curve
		/// </summary>
		public GOM_Points Points
		{
			get
			{
				return m_points;
			}
		}
		/// <summary>
		/// The type of the drawing operation (Bezier)
		/// </summary>
		public DrawingType	Type
		{
			get
			{
				return DrawingType.Bezier;
			}
		}
		/// <summary>The drawing style of the bezier curve</summary>
		private GOM_Style_Drawing	m_style;
		/// <summary>The list of points used to define the bezier curve</summary>
		private GOM_Points			m_points;
	}
	/// <summary>
	/// A dynamic list of drawing operation
	/// </summary>
	public class GOM_Drawings
	{
		/// <summary>
		/// The constructor of GOM_Drawings
		/// </summary>
		public GOM_Drawings()
		{
			rgDrawings = new System.Collections.ArrayList();
		}
		/// <summary>
		/// Return a drawing operation by its index
		/// </summary>
		public GOM_Interface_Drawing this[int idx]
		{
			get
			{
				return (GOM_Interface_Drawing)rgDrawings[idx];
			}
			set
			{
				rgDrawings[idx] = value;
			}
		}
		/// <summary>
		/// Number of drawing operations in the list
		/// </summary>
		public int Count
		{
			get
			{
				return rgDrawings.Count;
			}
		}
		/// <summary>
		/// Add a drawing operation into the list
		/// </summary>
		/// <param name="drawing">The drawing operation being added</param>
		public void Add(GOM_Interface_Drawing drawing)
		{
			rgDrawings.Add(drawing);
		}
		/// <summary>
		/// Remove a drawing operation by its index
		/// </summary>
		/// <param name="idx"></param>
		public void RemoveAt(int idx)
		{
			rgDrawings.RemoveAt(idx);
		}
		/// <summary>
		/// Empty the list
		/// </summary>
		public void Clear()
		{
			rgDrawings.Clear();
		}
		/// <summary>The dynamic list of drawing operations</summary>
		private System.Collections.ArrayList rgDrawings;
	}	
}