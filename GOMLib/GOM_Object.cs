using System;

namespace GOMLib
{
	/// <summary>
	/// The primitive graphic object
	/// </summary>
	public class GOM_Object_Primitive: GOM_Interface_Graphic_Object
	{
		/// <summary>
		/// The constructor of GOM_Object_Primitive
		/// </summary>
		public GOM_Object_Primitive()
		{
			m_id		= Guid.NewGuid().ToString("D");
			m_xOffset	= 0;
			m_yOffset	= 0;
			m_rotation	= 0;
			m_template	= null;

			GOM_Constraint_Set	constraintSet;

			m_boundingBox = new System.Drawing.RectangleF(0, 0, 0, 0);

			m_Top_Connector = new GOM_Point();
			m_Top_Connector.id = GOM_Special_Point_Name.TOP_CONNECTOR;
			m_Top_Connector.Connectable = true;

			m_Bottom_Connector = new GOM_Point();
			m_Bottom_Connector.id = GOM_Special_Point_Name.BOTTOM_CONNECTOR;
			m_Bottom_Connector.Connectable = true;

			m_Left_Connector = new GOM_Point();
			m_Left_Connector.id = GOM_Special_Point_Name.LEFT_CONNECTOR;
			m_Left_Connector.Connectable = true;

			m_Right_Connector = new GOM_Point();
			m_Right_Connector.id = GOM_Special_Point_Name.RIGHT_CONNECTOR;
			m_Right_Connector.Connectable = true;

			m_SE_Resize_Point			= new GOM_Point();
			m_SE_Resize_Point.id		= GOM_Special_Point_Name.SE_RESIZING_POINT;
			constraintSet				= new GOM_Constraint_Set();
			constraintSet.EditingMode	= "default";
			constraintSet.Add(new GOM_Resizing_SE_Constraint(this, m_SE_Resize_Point));
			m_SE_Resize_Point.Constraints.Add(constraintSet);

			m_NW_Resize_Point			= new GOM_Point();
			m_NW_Resize_Point.id		= GOM_Special_Point_Name.NW_RESIZING_POINT;
			constraintSet				= new GOM_Constraint_Set();
			constraintSet.EditingMode	= "default";
			constraintSet.Add(new GOM_Resizing_NW_Constraint(this, m_NW_Resize_Point));
			m_NW_Resize_Point.Constraints.Add(constraintSet);

			m_SW_Resize_Point			= new GOM_Point();
			m_SW_Resize_Point.id		= GOM_Special_Point_Name.SW_RESIZING_POINT;
			constraintSet				= new GOM_Constraint_Set();
			constraintSet.EditingMode	= "default";
			constraintSet.Add(new GOM_Resizing_SW_Constraint(this, m_SW_Resize_Point));
			m_SW_Resize_Point.Constraints.Add(constraintSet);

			m_NE_Resize_Point			= new GOM_Point();
			m_NE_Resize_Point.id		= GOM_Special_Point_Name.NE_RESIZING_POINT;
			constraintSet				= new GOM_Constraint_Set();
			constraintSet.EditingMode	= "default";
			constraintSet.Add(new GOM_Resizing_NE_Constraint(this, m_NE_Resize_Point));
			m_NE_Resize_Point.Constraints.Add(constraintSet);

			m_Rotation_Point			= new GOM_Point();
			m_Rotation_Point.id			= GOM_Special_Point_Name.ROTATION_POINT;
			constraintSet				= new GOM_Constraint_Set();
			constraintSet.EditingMode	= "default";
			constraintSet.Add(new GOM_Rotation_Constraint(this, m_Rotation_Point));
			m_Rotation_Point.Constraints.Add(constraintSet);

			innerText		= "";
			font			= new System.Drawing.Font("Tahoma", 10);
			fontColor		= System.Drawing.Color.Black;
			extScale		= false;
			extRotate		= false;
			extConnect		= false;
			keepAspectRatio = false;

			rgPoints		= new GOM_Points();
			rgDrawings		= new GOM_Drawings();
			rgFillings		= new GOM_Fillings();
			rgEditingModes	= new GOM_Strings();
			rgDrawingStyles = new GOM_Drawing_Styles();
			rgFillingStyles = new GOM_Filling_Styles();
		}
		/// <summary>
		/// Create a copy of current graphic object
		/// </summary>
		/// <returns></returns>
		public GOM_Interface_Graphic_Object Clone()
		{
			GOM_Object_Primitive	obj;

			obj				= new GOM_Object_Primitive();
			obj.m_id		= this.m_id;
			obj.m_xOffset	= this.m_xOffset;
			obj.m_yOffset	= this.m_yOffset;
			obj.m_rotation	= this.m_rotation;
			obj.m_template	= this.m_template;
			obj.innerText	= this.innerText;
			obj.fontColor	= this.fontColor;
			obj.font		= (System.Drawing.Font)this.font.Clone();

			obj.InitializeFromTemplate(this.m_template);

			for (int i = 0; i < obj.rgPoints.Count; i++)
			{
				GOM_Point	pt;

				pt = this.Points(obj.rgPoints[i].id);

				obj.rgPoints[i].x = pt.x;
				obj.rgPoints[i].y = pt.y;
			}

			for (int i = 0; i < obj.rgDrawingStyles.Count; i++)
			{
				obj.rgDrawingStyles[i].drawingStyle = (System.Drawing.Pen)this.DrawingStyles(obj.rgDrawingStyles[i].id).drawingStyle.Clone();
			}

			for (int i = 0; i < obj.rgFillingStyles.Count; i++)
			{
				obj.rgFillingStyles[i].fillingStyle = (System.Drawing.Brush)this.FillingStyles(obj.rgFillingStyles[i].id).fillingStyle.Clone();
			}

			return obj;
		}
		/// <summary>
		/// Get a movable point according to the given coordinates
		/// </summary>
		/// <param name="x">The x coordinate</param>
		/// <param name="y">The y coordinate</param>
		/// <returns>Return the movable point if found, otherwise null is returned</returns>
		public GOM_Point GetMovablePointAt(int x, int y)
		{
			System.Drawing.RectangleF	rc;
			System.Drawing.PointF		pt;

			rc = this.BoundingBox;
			pt = PointToObject(new System.Drawing.PointF(x, y));

			for (int i = 0; i < this.rgPoints.Count; i++)
			{
				if (rgPoints[i].Controllable)
				{
					if (GOM_Default_Values.IsMouseOnPoint(rgPoints[i].x, rgPoints[i].y, pt.X, pt.Y))
					{
						return rgPoints[i];
					}
				}
			}

			if (this.extScale)
			{
				this.m_SE_Resize_Point.x = rc.Right + 3;
				this.m_SE_Resize_Point.y = rc.Bottom + 3;
				if (GOM_Default_Values.IsMouseOnPoint(m_SE_Resize_Point.x, m_SE_Resize_Point.y, pt.X, pt.Y))
				{
					return m_SE_Resize_Point;
				}

				this.m_NW_Resize_Point.x = rc.Left - 3;
				this.m_NW_Resize_Point.y = rc.Top - 3;
				if (GOM_Default_Values.IsMouseOnPoint(m_NW_Resize_Point.x, m_NW_Resize_Point.y, pt.X, pt.Y))
				{
					return m_NW_Resize_Point;
				}

				this.m_SW_Resize_Point.x = rc.Left - 3;
				this.m_SW_Resize_Point.y = rc.Bottom + 3;
				if (GOM_Default_Values.IsMouseOnPoint(m_SW_Resize_Point.x, m_SW_Resize_Point.y, pt.X, pt.Y))
				{
					return m_SW_Resize_Point;
				}

				this.m_NE_Resize_Point.x = rc.Right + 3;
				this.m_NE_Resize_Point.y = rc.Top - 3;
				if (GOM_Default_Values.IsMouseOnPoint(m_NE_Resize_Point.x, m_NE_Resize_Point.y, pt.X, pt.Y))
				{
					return m_NE_Resize_Point;
				}
			}

			if (this.extRotate)
			{
				this.m_Rotation_Point.x = (rc.Left + rc.Right) / 2;
				this.m_Rotation_Point.y = rc.Top - 27;
				if (GOM_Default_Values.IsMouseOnPoint(m_Rotation_Point.x, m_Rotation_Point.y, pt.X, pt.Y))
				{
					return m_Rotation_Point;
				}
			}

			return null;
		}
		/// <summary>
		/// Get a connectable point according to the given coordinates
		/// </summary>
		/// <param name="x">The x coordinate</param>
		/// <param name="y">The y coordinate</param>
		/// <returns>Return the movable point if found, otherwise null is returned</returns>
		public GOM_Point GetConnectablePointAt(int x, int y)
		{
			System.Drawing.RectangleF	rc;
			System.Drawing.PointF		pt;

			rc = this.BoundingBox;
			pt = PointToObject(new System.Drawing.PointF(x, y));

			for (int i = 0; i < this.rgPoints.Count; i++)
			{
				if (rgPoints[i].Connectable)
				{
					if (GOM_Default_Values.IsMouseOnPoint(rgPoints[i].x, rgPoints[i].y, pt.X, pt.Y))
					{
						return rgPoints[i];
					}
				}
			}

			if (this.extConnect)
			{
				this.m_Top_Connector.x = (rc.Left + rc.Right) / 2;
				this.m_Top_Connector.y = rc.Top - 3;
				if (GOM_Default_Values.IsMouseOnPoint(m_Top_Connector.x, m_Top_Connector.y, pt.X, pt.Y))
				{
					return m_Top_Connector;
				}

				this.m_Bottom_Connector.x = (rc.Left + rc.Right) / 2;
				this.m_Bottom_Connector.y = rc.Bottom + 3;
				if (GOM_Default_Values.IsMouseOnPoint(m_Bottom_Connector.x, m_Bottom_Connector.y, pt.X, pt.Y))
				{
					return m_Bottom_Connector;
				}

				this.m_Left_Connector.x = rc.Left - 3;
				this.m_Left_Connector.y = (rc.Top + rc.Bottom) / 2;
				if (GOM_Default_Values.IsMouseOnPoint(m_Left_Connector.x, m_Left_Connector.y, pt.X, pt.Y))
				{
					return m_Left_Connector;
				}

				this.m_Right_Connector.x = rc.Right + 3;
				this.m_Right_Connector.y = (rc.Top + rc.Bottom) / 2;
				if (GOM_Default_Values.IsMouseOnPoint(m_Right_Connector.x, m_Right_Connector.y, pt.X, pt.Y))
				{
					return m_Right_Connector;
				}
			}

			return null;
		}
		/// <summary>
		/// Judge whether a point is inside the graphic object
		/// </summary>
		/// <param name="x">The x coordinate of the object</param>
		/// <param name="y">The y coordinate of the object</param>
		/// <returns>Return true is the point is inside the graphic object. Otherwise, false is returned</returns>
		public bool IsPointInObject(int x, int y)
		{
			System.Drawing.RectangleF	rc;
			System.Drawing.PointF		pt;

			rc = this.BoundingBox;
			pt = PointToObject(new System.Drawing.PointF(x, y));
			
			if ((rc.Left <= pt.X) && (pt.X <= rc.Right) && (rc.Top <= pt.Y) && (pt.Y <= rc.Bottom))
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// Convert a point in gloabl coordinate into a point in local coordinate
		/// </summary>
		/// <param name="pt">The point in gloabl coordinate</param>
		/// <returns>The point in local coordinate</returns>
		public System.Drawing.PointF PointToObject(System.Drawing.PointF pt)
		{
			System.Drawing.Drawing2D.Matrix	matrix;
			System.Drawing.RectangleF		rc;
			System.Drawing.PointF[]			rgPts;

			rc = this.BoundingBox;

			rgPts = new System.Drawing.PointF[1];
			rgPts[0].X = pt.X;
			rgPts[0].Y = pt.Y;

			matrix = new System.Drawing.Drawing2D.Matrix();
			matrix.Translate(-this.xOffset, -this.yOffset);
			matrix.TransformPoints(rgPts);

			matrix.Reset();
			matrix.Translate(-(rc.Left + rc.Right) / 2, -(rc.Top + rc.Bottom) / 2);
			matrix.TransformPoints(rgPts);

			matrix.Reset();
			matrix.Rotate(-this.rotation);
			matrix.TransformPoints(rgPts);

			matrix.Reset();
			matrix.Translate((rc.Left + rc.Right) / 2, (rc.Top + rc.Bottom) / 2);
			matrix.TransformPoints(rgPts);

			return rgPts[0];
		}
		/// <summary>
		/// Convert a point in local coordinate into a point in global coordinate
		/// </summary>
		/// <param name="pt">The point in local coordinate</param>
		/// <returns>The point in global coordinate</returns>
		public System.Drawing.PointF PointToCanvas(System.Drawing.PointF pt)
		{
			System.Drawing.Drawing2D.Matrix	matrix;
			System.Drawing.RectangleF		rc;
			System.Drawing.PointF[]			rgPts;

			rc = this.BoundingBox;

			rgPts = new System.Drawing.PointF[1];
			rgPts[0].X = pt.X;
			rgPts[0].Y = pt.Y;

			matrix = new System.Drawing.Drawing2D.Matrix();
			matrix.Translate(-(rc.Left + rc.Right) / 2, -(rc.Top + rc.Bottom) / 2);
			matrix.TransformPoints(rgPts);

			matrix.Reset();
			matrix.Rotate(this.rotation);
			matrix.TransformPoints(rgPts);

			matrix.Reset();
			matrix.Translate(this.xOffset, this.yOffset);
			matrix.TransformPoints(rgPts);

			matrix.Reset();
			matrix.Translate((rc.Left + rc.Right) / 2, (rc.Top + rc.Bottom) / 2);
			matrix.TransformPoints(rgPts);

			return rgPts[0];
		}
		/// <summary>
		/// Return the bounding region of the graphic object in global coordinate
		/// </summary>
		public System.Drawing.Region BoundingRegion
		{
			get
			{
				System.Drawing.Drawing2D.Matrix	matrix;
				System.Drawing.RectangleF	rc;
				System.Drawing.Region		rgn;

				rc = this.BoundingBox;
				rgn = new System.Drawing.Region(rc);

				matrix = new System.Drawing.Drawing2D.Matrix();
				matrix.Translate(-(rc.Left + rc.Right) / 2, -(rc.Top + rc.Bottom) / 2);
				rgn.Transform(matrix);

				matrix.Reset();
				matrix.Rotate(this.rotation);
				rgn.Transform(matrix);

				matrix.Reset();
				matrix.Translate(this.xOffset, this.yOffset);
				rgn.Transform(matrix);

				matrix.Reset();
				matrix.Translate((rc.Left + rc.Right) / 2, (rc.Top + rc.Bottom) / 2);
				rgn.Transform(matrix);

				return rgn;
			}
		}
		/// <summary>
		/// The bounding box of this graphic object
		/// </summary>
		public System.Drawing.RectangleF BoundingBox
		{
			get
			{
				return m_boundingBox;
			}
		}

		public void CalculateBoundingBox()
		{
			System.Drawing.RectangleF rc;
			float minX, minY, maxX, maxY;

			minX = 0;
			minY = 0;
			maxX = 0;
			maxY = 0;

			if (rgPoints.Count > 0)
			{
				minX = rgPoints[0].x;
				minY = rgPoints[0].y;
				maxX = rgPoints[0].x;
				maxY = rgPoints[0].y;

				for (int i = 1; i < rgPoints.Count; i++)
				{
					minX = System.Math.Min(minX, rgPoints[i].x);
					minY = System.Math.Min(minY, rgPoints[i].y);
					maxX = System.Math.Max(maxX, rgPoints[i].x);
					maxY = System.Math.Max(maxY, rgPoints[i].y);
				}

				for (int i = 0; i < rgDrawings.Count; i++)
				{
					rc = rgDrawings[i].BoundingBox;
					minX = System.Math.Min(minX, rc.Left);
					minY = System.Math.Min(minY, rc.Top);
					maxX = System.Math.Max(maxX, rc.Right);
					maxY = System.Math.Max(maxY, rc.Bottom);
				}

				for (int i = 0; i < rgFillings.Count; i++)
				{
					rc = rgFillings[i].BoundingBox;
					minX = System.Math.Min(minX, rc.Left);
					minY = System.Math.Min(minY, rc.Top);
					maxX = System.Math.Max(maxX, rc.Right);
					maxY = System.Math.Max(maxY, rc.Bottom);
				}
			}

			m_boundingBox = new System.Drawing.RectangleF(minX, minY, maxX - minX, maxY - minY);
		}
		/// <summary>
		/// ID of this primitive object
		/// </summary>
		public string id
		{
			get
			{
				return m_id;
			}
			set
			{
				m_id = value;
			}
		}
		public bool KeepAspectRatio
		{
			get
			{
				return this.keepAspectRatio;
			}
			set
			{
				this.keepAspectRatio = value;
			}
		}
		/// <summary>
		/// Type of this primitive object (Primitive)
		/// </summary>
		public GOMLib.GraphicObjectType type
		{
			get
			{
				return GraphicObjectType.Primitive;
			}
		}
		/// <summary>
		/// X offset of this primitive object
		/// </summary>
		public float xOffset
		{
			get
			{
				return m_xOffset;
			}
			set
			{
				m_xOffset = value;
			}
		}
		/// <summary>
		/// Y offset of this primitive object
		/// </summary>
		public float yOffset
		{
			get
			{
				return m_yOffset;
			}
			set
			{
				m_yOffset = value;
			}
		}
		/// <summary>
		/// Synchronize the position of external connectors
		/// </summary>
		private void SynchronizeConnectorPosition()
		{
			System.Drawing.RectangleF rc = this.BoundingBox;

			this.m_Top_Connector.x = (rc.Left + rc.Right) / 2;
			this.m_Top_Connector.y = rc.Top - 3;

			this.m_Bottom_Connector.x = (rc.Left + rc.Right) / 2;
			this.m_Bottom_Connector.y = rc.Bottom + 3;

			this.m_Left_Connector.x = rc.Left - 3;
			this.m_Left_Connector.y = (rc.Top + rc.Bottom) / 2;

			this.m_Right_Connector.x = rc.Right + 3;
			this.m_Right_Connector.y = (rc.Top + rc.Bottom) / 2;
		}
		/// <summary>
		/// Width of this primitive object
		/// </summary>
		public float width
		{
			get
			{
				return this.BoundingBox.Width;
			}
			set
			{
				System.Drawing.RectangleF rc = this.BoundingBox;
				float scale = value / rc.Width;

				for (int i = 0; i < rgPoints.Count; i++)
				{
					rgPoints[i].x = (rgPoints[i].x - rc.Left) * scale + rc.Left;
				}

				SynchronizeConnectorPosition();
				CalculateBoundingBox();
			}
		}
		/// <summary>
		/// Height of this primitive object
		/// </summary>
		public float height
		{
			get
			{
				return this.BoundingBox.Height;
			}
			set
			{
				System.Drawing.RectangleF rc = this.BoundingBox;
				float scale = value / rc.Height;

				for (int i = 0; i < rgPoints.Count; i++)
				{
					rgPoints[i].y = (rgPoints[i].y - rc.Top) * scale + rc.Top;
				}

				SynchronizeConnectorPosition();
				CalculateBoundingBox();
			}
		}
		/// <summary>
		/// Angel of rotation of this primitive object
		/// </summary>
		public float rotation
		{
			get
			{
				return m_rotation;
			}
			set
			{
				m_rotation = value;
			}
		}
		public GOM_Template template
		{
			get
			{
				return m_template;
			}
		}
		/// <summary>
		/// Load a primitive object from a string
		/// </summary>
		/// <param name="strXml">The string that contains the definition of the primitive object</param>
		/// <param name="rgTemplates">The list of known templates</param>
		/// <returns>If successful, a primitve object is returned. Otherwise, an exception will be thrown out</returns>
		static public GOM_Object_Primitive LoadFromString(string strXml, GOM_Templates rgTemplates)
		{
			System.Xml.XmlDocument	doc;

			doc = new System.Xml.XmlDocument();
			doc.LoadXml(strXml);

			return LoadFromXml(doc.DocumentElement, rgTemplates);
		}
		/// <summary>
		/// Load a primitive object from a file
		/// </summary>
		/// <param name="fileName">The name of file that contains the definition of the primitive object</param>
		/// <param name="rgTemplates">The list of known templates</param>
		/// <returns>If successful, a primitve object is returned. Otherwise, an exception will be thrown out</returns>
		static public GOM_Object_Primitive LoadFromFile(string fileName, GOM_Templates rgTemplates)
		{
			System.Xml.XmlDocument	doc;

			doc = new System.Xml.XmlDocument();
			doc.Load(fileName);

			return LoadFromXml(doc.DocumentElement, rgTemplates);
		}
		/// <summary>
		/// Load a primitive object from a node of XML tree
		/// </summary>
		/// <param name="node">The node which is the root of the XML tree</param>
		/// <param name="rgTemplates">The list of known templates</param>
		/// <returns>If successful, a primitve object is returned. Otherwise, an exception will be thrown out</returns>
		static public GOM_Object_Primitive LoadFromXml(System.Xml.XmlNode node, GOM_Templates rgTemplates)
		{
			GOM_Object_Primitive	primitive = null;

			if (System.String.Compare(node.Name, GOM_TAGS.GRAPHIC_OBJECT) != 0)
			{
				throw new Exception("Invalid input. Not a graphic object node!");
			}

			primitive = new GOM_Object_Primitive();
			//Load properties of the graphic object
			for (int i = 0; i < node.Attributes.Count; i++)
			{
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.ID, true) == 0)
				{
					primitive.id = node.Attributes[i].Value;
				}
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.TYPE, true) == 0)
				{
					if (!node.Attributes[i].Value.Equals("primitive"))
					{
						throw new Exception("Invalid input. Not a primitive object node!");
					}
				}
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.X_OFFSET, true) == 0)
				{
					primitive.m_xOffset = float.Parse(node.Attributes[i].Value);
				}
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.Y_OFFSET, true) == 0)
				{
					primitive.m_yOffset = float.Parse(node.Attributes[i].Value);
				}
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.ROTATION, true) == 0)
				{
					primitive.m_rotation = float.Parse(node.Attributes[i].Value);
				}
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.TEMPLATE, true) == 0)
				{
					for (int j = 0; j < rgTemplates.Count; j++)
					{
						if (rgTemplates[j].id.Equals(node.Attributes[i].Value))
						{
							primitive.m_template = rgTemplates[j];
						}
					}

					if (primitive.m_template == null)
					{
						throw new Exception("Unknown template");
					}
				}
			}

			if (primitive.m_template == null)
			{
				throw new Exception("Can not find template of the graphic object");
			}
			//Initialize the graphic object according to the template
			primitive.InitializeFromTemplate(primitive.m_template);
			//Update status of the graphic object
			for (int i = 0; i < node.ChildNodes.Count; i++)
			{
				if (System.String.Compare(node.ChildNodes[i].Name, GOM_TAGS.POINTS, true) == 0)
				{
					primitive.UpdatePoints(node.ChildNodes[i]);
				}
				if (System.String.Compare(node.ChildNodes[i].Name, GOM_TAGS.STYLES, true) == 0)
				{
					primitive.UpdateStyles(node.ChildNodes[i]);
				}
				if (System.String.Compare(node.ChildNodes[i].Name, GOM_TAGS.TEXT, true) == 0)
				{
					primitive.LoadText(node.ChildNodes[i]);
				}
			}

			return primitive;
		}
		/// <summary>
		/// Update coordinates of points of the graphic object
		/// </summary>
		/// <param name="node">The node of the list of points</param>
		public void UpdatePoints(System.Xml.XmlNode node)
		{
			GOM_Point	pt;

			for (int i = 0; i < node.ChildNodes.Count; i++)
			{
				if (System.String.Compare(node.ChildNodes[i].Name, GOM_TAGS.POINT, true) == 0)
				{
					pt = null;

					for (int j = 0; j < node.ChildNodes[i].Attributes.Count; j++)
					{
						if (System.String.Compare(node.ChildNodes[i].Attributes[j].Name, GOM_TAGS.ID, true) == 0)
						{
							pt = Points(node.ChildNodes[i].Attributes[j].Value);
						}
					}

					if (pt != null)
					{
						for (int j = 0; j < node.ChildNodes[i].Attributes.Count; j++)
						{
							if (System.String.Compare(node.ChildNodes[i].Attributes[j].Name, GOM_TAGS.X, true) == 0)
							{
								pt.x = float.Parse(node.ChildNodes[i].Attributes[j].Value);
							}
							if (System.String.Compare(node.ChildNodes[i].Attributes[j].Name, GOM_TAGS.Y, true) == 0)
							{
								pt.y = float.Parse(node.ChildNodes[i].Attributes[j].Value);
							}
						}
					}
				}
			}
		}
		/// <summary>
		/// Update drawing or filling styles of the graphic object
		/// </summary>
		/// <param name="node">The node of the list of styles</param>
		public void UpdateStyles(System.Xml.XmlNode node)
		{
			for (int i = 0; i < node.ChildNodes.Count; i++)
			{
				if (System.String.Compare(node.ChildNodes[i].Name, GOM_TAGS.DRAWING_STYLE, true) == 0)
				{
					GOM_Style_Drawing	style;
					byte r, g, b;

					r = 0;
					g = 0;
					b = 0;

					style = null;

					for (int j = 0; j < node.ChildNodes[i].Attributes.Count; j++)
					{
						if (System.String.Compare(node.ChildNodes[i].Attributes[j].Name, GOM_TAGS.ID, true) == 0)
						{
							style = DrawingStyles(node.ChildNodes[i].Attributes[j].Value);
						}
					}

					if (style != null)
					{

						for (int j = 0; j < node.ChildNodes[i].Attributes.Count; j++)
						{
							if (System.String.Compare(node.ChildNodes[i].Attributes[j].Name, GOM_TAGS.RED, true) == 0)
							{
								r = byte.Parse(node.ChildNodes[i].Attributes[j].Value);
							}
							if (System.String.Compare(node.ChildNodes[i].Attributes[j].Name, GOM_TAGS.GREEN, true) == 0)
							{
								g = byte.Parse(node.ChildNodes[i].Attributes[j].Value);
							}
							if (System.String.Compare(node.ChildNodes[i].Attributes[j].Name, GOM_TAGS.BLUE, true) == 0)
							{
								b = byte.Parse(node.ChildNodes[i].Attributes[j].Value);
							}
						}

						style.drawingStyle.Color = System.Drawing.Color.FromArgb(r, g, b);

						for (int j = 0; j < node.ChildNodes[i].Attributes.Count; j++)
						{
							if (System.String.Compare(node.ChildNodes[i].Attributes[j].Name, GOM_TAGS.STYLE, true) == 0)
							{
								if (System.String.Compare(node.ChildNodes[i].Attributes[j].Value, "solid", true) == 0)
								{
									style.drawingStyle.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
								}
								if (System.String.Compare(node.ChildNodes[i].Attributes[j].Value, "dash", true) == 0)
								{
									style.drawingStyle.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
								}
								if (System.String.Compare(node.ChildNodes[i].Attributes[j].Value, "dot", true) == 0)
								{
									style.drawingStyle.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
								}
								if (System.String.Compare(node.ChildNodes[i].Attributes[j].Value, "dashdot", true) == 0)
								{
									style.drawingStyle.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
								}
								if (System.String.Compare(node.ChildNodes[i].Attributes[j].Value, "dashdotdot", true) == 0)
								{
									style.drawingStyle.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot;
								}
							}
							if (System.String.Compare(node.ChildNodes[i].Attributes[j].Name, GOM_TAGS.WIDTH, true) == 0)
							{
								style.drawingStyle.Width = int.Parse(node.ChildNodes[i].Attributes[j].Value);
							}
						}
					}
				}

				if (System.String.Compare(node.ChildNodes[i].Name, GOM_TAGS.FILLING_STYLE, true) == 0)
				{
					GOM_Style_Filling	style;
					byte r, g, b;

					r = 0;
					g = 0;
					b = 0;

					style = null;

					for (int j = 0; j < node.ChildNodes[i].Attributes.Count; j++)
					{
						if (System.String.Compare(node.ChildNodes[i].Attributes[j].Name, GOM_TAGS.ID, true) == 0)
						{
							style = FillingStyles(node.ChildNodes[i].Attributes[j].Value);
						}
					}
					
					if (style != null)
					{
						for (int j = 0; j < node.ChildNodes[i].Attributes.Count; j++)
						{
							if (System.String.Compare(node.ChildNodes[i].Attributes[j].Name, GOM_TAGS.RED, true) == 0)
							{
								r = byte.Parse(node.ChildNodes[i].Attributes[j].Value);
							}
							if (System.String.Compare(node.ChildNodes[i].Attributes[j].Name, GOM_TAGS.GREEN, true) == 0)
							{
								g = byte.Parse(node.ChildNodes[i].Attributes[j].Value);
							}
							if (System.String.Compare(node.ChildNodes[i].Attributes[j].Name, GOM_TAGS.BLUE, true) == 0)
							{
								b = byte.Parse(node.ChildNodes[i].Attributes[j].Value);
							}
						}

						for (int j = 0; j < node.ChildNodes[i].Attributes.Count; j++)
						{
							if (System.String.Compare(node.ChildNodes[i].Attributes[j].Name, GOM_TAGS.STYLE, true) == 0)
							{
								if (System.String.Compare(node.ChildNodes[i].Attributes[j].Value, "solid", true) == 0)
								{
									style.fillingStyle = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(r, g, b));
								}
								if (System.String.Compare(node.ChildNodes[i].Attributes[j].Value, "bdiagonal", true) == 0)
								{
									style.fillingStyle = new System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.BackwardDiagonal, System.Drawing.Color.FromArgb(r, g, b), System.Drawing.Color.Transparent);
								}
								if (System.String.Compare(node.ChildNodes[i].Attributes[j].Value, "cross", true) == 0)
								{
									style.fillingStyle = new System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.Cross, System.Drawing.Color.FromArgb(r, g, b), System.Drawing.Color.Transparent);
								}
								if (System.String.Compare(node.ChildNodes[i].Attributes[j].Value, "diagcross", true) == 0)
								{
									style.fillingStyle = new System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.DiagonalCross, System.Drawing.Color.FromArgb(r, g, b), System.Drawing.Color.Transparent);
								}
								if (System.String.Compare(node.ChildNodes[i].Attributes[j].Value, "fdiagonal", true) == 0)
								{
									style.fillingStyle = new System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.ForwardDiagonal, System.Drawing.Color.FromArgb(r, g, b), System.Drawing.Color.Transparent);
								}
								if (System.String.Compare(node.ChildNodes[i].Attributes[j].Value, "horizontal", true) == 0)
								{
									style.fillingStyle = new System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.Horizontal, System.Drawing.Color.FromArgb(r, g, b), System.Drawing.Color.Transparent);
								}
								if (System.String.Compare(node.ChildNodes[i].Attributes[j].Value, "vertical", true) == 0)
								{
									style.fillingStyle = new System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.Vertical, System.Drawing.Color.FromArgb(r, g, b), System.Drawing.Color.Transparent);
								}
							}
						}
					}
				}
			}
		}
		/// <summary>
		/// Load text embedded in the graphic object
		/// </summary>
		/// <param name="node">The node of the text</param>
		public void LoadText(System.Xml.XmlNode node)
		{
			for (int i = 0; i < node.ChildNodes.Count; i++)
			{
				if (System.String.Compare(node.ChildNodes[i].Name, GOM_TAGS.TEXT, true) == 0)
				{
					this.innerText = node.InnerText;
				}

				if (System.String.Compare(node.ChildNodes[i].Name, GOM_TAGS.FONT, true) == 0)
				{
					byte	r, g, b;
					int		fontSize = 10;
					string	fontFamily = "Tahoma";
					System.Drawing.FontStyle fontStyle;

					r = 0;
					g = 0;
					b = 0;

					for (int j = 0; j < node.ChildNodes[i].Attributes.Count; j++)
					{
						if (System.String.Compare(node.ChildNodes[i].Attributes[j].Name, GOM_TAGS.RED, true) == 0)
						{
							r = byte.Parse(node.ChildNodes[i].Attributes[j].Value);
						}
						if (System.String.Compare(node.ChildNodes[i].Attributes[j].Name, GOM_TAGS.GREEN, true) == 0)
						{
							g = byte.Parse(node.ChildNodes[i].Attributes[j].Value);
						}
						if (System.String.Compare(node.ChildNodes[i].Attributes[j].Name, GOM_TAGS.BLUE, true) == 0)
						{
							b = byte.Parse(node.ChildNodes[i].Attributes[j].Value);
						}
					}

					this.fontColor = System.Drawing.Color.FromArgb(r, g, b);
					fontStyle = System.Drawing.FontStyle.Regular;

					for (int j = 0; j < node.ChildNodes[i].Attributes.Count; j++)
					{
						if (System.String.Compare(node.ChildNodes[i].Attributes[j].Name, GOM_TAGS.FAMILY, true) == 0)
						{
							fontFamily = node.ChildNodes[i].Attributes[j].Value;
						}
						if (System.String.Compare(node.ChildNodes[i].Attributes[j].Name, GOM_TAGS.SIZE, true) == 0)
						{
							fontSize = int.Parse(node.ChildNodes[i].Attributes[j].Value);
						}
						if (System.String.Compare(node.ChildNodes[i].Attributes[j].Name, GOM_TAGS.BOLD, true) == 0)
						{
							if (System.String.Compare(node.ChildNodes[i].Attributes[j].Value, GOM_TAGS.TRUE, true) == 0)
							{
								fontStyle |= System.Drawing.FontStyle.Bold;
							}
						}
						if (System.String.Compare(node.ChildNodes[i].Attributes[j].Name, GOM_TAGS.ITALIC, true) == 0)
						{
							if (System.String.Compare(node.ChildNodes[i].Attributes[j].Value, GOM_TAGS.TRUE, true) == 0)
							{
								fontStyle |= System.Drawing.FontStyle.Italic;
							}
						}
						if (System.String.Compare(node.ChildNodes[i].Attributes[j].Name, GOM_TAGS.STRIKEOUT, true) == 0)
						{
							if (System.String.Compare(node.ChildNodes[i].Attributes[j].Value, GOM_TAGS.TRUE, true) == 0)
							{
								fontStyle |= System.Drawing.FontStyle.Strikeout;
							}
						}
						if (System.String.Compare(node.ChildNodes[i].Attributes[j].Name, GOM_TAGS.UNDERLINE, true) == 0)
						{
							if (System.String.Compare(node.ChildNodes[i].Attributes[j].Value, GOM_TAGS.TRUE, true) == 0)
							{
								fontStyle |= System.Drawing.FontStyle.Underline;
							}
						}
					}

					this.font = new System.Drawing.Font(fontFamily, fontSize, fontStyle);
				}
			}
		}
		/// <summary>
		/// Initialize the graphic object according to a given template
		/// </summary>
		/// <param name="template"></param>
		public void InitializeFromTemplate(GOM_Template template)
		{
			//Initialize property
			this.m_template			= template;
			this.extConnect			= template.extConnect;
			this.extRotate			= template.extRotate;
			this.extScale			= template.extScale;
			this.keepAspectRatio	= template.keepAspectRatio;
			//Clear up existing data
			this.rgPoints.Clear();
			this.rgFillings.Clear();
			this.rgDrawings.Clear();
			this.rgEditingModes.Clear();
			this.rgDrawingStyles.Clear();
			this.rgFillingStyles.Clear();
			//Initialize editing mode
			for (int i = 0; i < template.rgEditingModes.Count; i++)
			{
				this.rgEditingModes.Add(template.rgEditingModes[i]);
			}
			//Initialize drawing styles
			for (int i = 0; i < template.rgDrawingStyles.Count; i++)
			{
				GOM_Style_Drawing	style;

				style = new GOM_Style_Drawing();
				style.id = template.rgDrawingStyles[i].id;
				style.drawingStyle = (System.Drawing.Pen)template.rgDrawingStyles[i].drawingStyle.Clone();

				this.rgDrawingStyles.Add(style);
			}
			//Initialize filling styles
			for (int i = 0; i < template.rgFillingStyles.Count; i++)
			{
				GOM_Style_Filling	style;

				style = new GOM_Style_Filling();
				style.id = template.rgFillingStyles[i].id;
				style.fillingStyle = (System.Drawing.Brush)template.rgFillingStyles[i].fillingStyle.Clone();

				this.rgFillingStyles.Add(style);
			}
			//Initialize basic properties of points
			for (int i = 0; i < template.rgPoints.Count; i++)
			{
				GOM_Point	pt;

				pt = new GOM_Point();
				pt.id = template.rgPoints[i].id;
				pt.x = template.rgPoints[i].x;
				pt.y = template.rgPoints[i].y;
				pt.Connectable = template.rgPoints[i].Connectable;
				pt.Controllable = template.rgPoints[i].Controllable;

				this.rgPoints.Add(pt);
			}
			//Initialize drawing operations
			for (int i = 0; i < template.rgDrawings.Count; i++)
			{
				rgDrawings.Add(CloneDrawing(template.rgDrawings[i]));
			}
			//Initialize filling operations
			for (int i = 0; i < template.rgFillings.Count; i++)
			{
				rgFillings.Add(CloneFilling(template.rgFillings[i]));
			}
			//Initialize constraints of points
			for (int i = 0; i < template.rgPoints.Count; i++)
			{
				for (int j = 0; j < template.rgPoints[i].Constraints.Count; j++)
				{
					GOM_Constraint_Set	constraintSet;

					constraintSet = new GOM_Constraint_Set();

					for (int k = 0; k < template.rgPoints[i].Constraints[j].Count; k++)
					{
						constraintSet.Add(CloneConstraint(template.rgPoints[i].Constraints[j][k]));
					}

					rgPoints[i].Constraints.Add(constraintSet);
				}
			}

			CalculateBoundingBox();
		}
		/// <summary>
		/// Clone a constraint
		/// </summary>
		/// <param name="constraint">The original constraint</param>
		/// <returns>If successful, a constraint is returned. Otherwise, null is returned.</returns>
		private GOM_Interface_Constraint CloneConstraint(GOM_Interface_Constraint constraint)
		{
			if (constraint is GOM_Assignment_Constraint)
			{
				return new GOM_Assignment_Constraint(CloneValue(constraint.values[0]), CloneValue(constraint.values[1]));
			}

			return null;
		}
		/// <summary>
		/// Clone a value node
		/// </summary>
		/// <param name="valueNode">The original value node</param>
		/// <returns>If successful, a value node is returned. Otherwise, null is returned.</returns>
		private GOM_Interface_Value CloneValue(GOM_Interface_Value valueNode)
		{
			if (valueNode is GOM_Num_Value)
			{
				GOM_Num_Value	val;

				val = new GOM_Num_Value();
				val.Value = valueNode.Value;

				return valueNode;
			}

			if (valueNode is GOM_Point_Value)
			{
				return new GOM_Point_Value(Points(((GOM_Point_Value)valueNode).point.id), valueNode.Type);
			}

			if (valueNode is GOM_Unary_Value)
			{
				return new GOM_Unary_Value(CloneValue(valueNode.values[0]), valueNode.Type);
			}

			if (valueNode is GOM_Binary_Value)
			{
				return new GOM_Binary_Value(CloneValue(valueNode.values[0]), CloneValue(valueNode.values[1]), valueNode.Type);
			}

			return null;
		}
		/// <summary>
		/// Clone a drawing operation
		/// </summary>
		/// <param name="drawing">The original drawing operation</param>
		/// <returns>If successful, a drawing operation is returned. Otherwise, null is returned.</returns>
		private GOM_Interface_Drawing CloneDrawing(GOM_Interface_Drawing drawing)
		{
			if (drawing is GOM_Drawing_Line)
			{
				return new GOM_Drawing_Line(Points(drawing.Points[0].id), Points(drawing.Points[1].id), DrawingStyles(drawing.Style.id));
			}

			if (drawing is GOM_Drawing_Arc)
			{
				return new GOM_Drawing_Arc(Points(drawing.Points[0].id), Points(drawing.Points[1].id), Points(drawing.Points[2].id), Points(drawing.Points[3].id), DrawingStyles(drawing.Style.id), 0);
			}

			if (drawing is GOM_Drawing_Bezier)
			{
				GOM_Points	rgPts;

				rgPts = new GOM_Points();

				for (int i = 0; i < drawing.Points.Count; i++)
				{
					rgPts.Add(Points(drawing.Points[i].id));
				}

				return new GOM_Drawing_Bezier(rgPts, DrawingStyles(drawing.Style.id));
			}

			return null;
		}
		/// <summary>
		/// Clone a filling operation
		/// </summary>
		/// <param name="filling">The original filling operation</param>
		/// <returns>If successful, a drawing operation is returned. Otherwise, null is returned.</returns>
		private GOM_Interface_Filling CloneFilling(GOM_Interface_Filling filling)
		{
			if (filling is GOM_Filling_Ellipse)
			{
				return new GOM_Filling_Ellipse(Points(filling.Points[0].id), Points(filling.Points[1].id), FillingStyles(filling.Style.id));
			}

			if (filling is GOM_Filling_FillPath)
			{
				GOM_Drawings	drawings;

				drawings = new GOM_Drawings();

				for (int i = 0; i < ((GOM_Filling_FillPath)filling).Drawings.Count; i++)
				{
					drawings.Add(CloneDrawing(((GOM_Filling_FillPath)filling).Drawings[i]));
				}

				return new GOM_Filling_FillPath(drawings, FillingStyles(filling.Style.id));
			}

			if (filling is GOM_Filling_Pie)
			{
				return new GOM_Filling_Pie(Points(filling.Points[0].id), Points(filling.Points[1].id), Points(filling.Points[2].id), Points(filling.Points[3].id), FillingStyles(filling.Style.id));
			}

			if (filling is GOM_Filling_Polygon)
			{
				GOM_Points	rgPts;

				rgPts = new GOM_Points();

				for (int i = 0; i < filling.Points.Count; i++)
				{
					rgPts.Add(Points(filling.Points[i].id));
				}

				return new GOM_Filling_Polygon(rgPts, FillingStyles(filling.Style.id));
			}

			if (filling is GOM_Filling_Rectangle)
			{
				return new GOM_Filling_Rectangle(Points(filling.Points[0].id), Points(filling.Points[1].id), FillingStyles(filling.Style.id));
			}

			return null;
		}
		/// <summary>
		/// Return a point by its id
		/// </summary>
		/// <param name="name">The id of the point</param>
		/// <returns>If successful, a point is returned. Otherwise, null is returned.</returns>
		private GOM_Point Points(string name)
		{
			return rgPoints[name];
		}
		/// <summary>
		/// Return a drawing style by its id
		/// </summary>
		/// <param name="name">The id of the drawing style</param>
		/// <returns>If successful, a drawing style is returned. Otherwise, null is returned.</returns>
		private GOM_Style_Drawing DrawingStyles(string name)
		{
			return rgDrawingStyles[name];
		}
		/// <summary>
		/// Return a filling style by its id
		/// </summary>
		/// <param name="name">The id of the filling style</param>
		/// <returns>If successful, a filling style is returned. Otherwise, null is returned.</returns>
		private GOM_Style_Filling FillingStyles(string name)
		{
			return rgFillingStyles[name];
		}
		/// <summary>
		/// Save this primitive object to a XML string
		/// </summary>
		/// <returns>A string that represents the template</returns>
		public string SaveToXML()
		{
			System.Xml.XmlTextWriter	writer;
			System.IO.StringWriter		strXML;

			strXML = new System.IO.StringWriter();
			writer = new System.Xml.XmlTextWriter(strXML);

			SaveToXML(writer);
			writer.Close();

			return strXML.ToString();
		}
		/// <summary>
		/// Save this primitive object to a file
		/// </summary>
		/// <param name="fileName">The name of file</param>
		public void SaveToXML(string fileName)
		{
			System.Xml.XmlTextWriter	writer;

			writer = new System.Xml.XmlTextWriter(fileName, System.Text.Encoding.UTF8);
			SaveToXML(writer);
			writer.Close();
		}
		/// <summary>
		/// Save this primitive object to XML
		/// </summary>
		/// <param name="writer">The XML writer</param>
		public void SaveToXML(System.Xml.XmlWriter writer)
		{
			writer.WriteStartElement(GOM_TAGS.GRAPHIC_OBJECT);

			writer.WriteAttributeString(GOM_TAGS.ID, this.id);
			writer.WriteAttributeString(GOM_TAGS.TYPE, "primitive");
			writer.WriteAttributeString(GOM_TAGS.TEMPLATE, this.template.id);
			writer.WriteAttributeString(GOM_TAGS.X_OFFSET, this.xOffset.ToString());
			writer.WriteAttributeString(GOM_TAGS.Y_OFFSET, this.yOffset.ToString());
			writer.WriteAttributeString(GOM_TAGS.ROTATION, this.rotation.ToString());

			writer.WriteStartElement(GOM_TAGS.POINTS);
			for (int i = 0; i < this.rgPoints.Count; i++)
			{
				writer.WriteStartElement(GOM_TAGS.POINT);
				writer.WriteAttributeString(GOM_TAGS.ID, this.rgPoints[i].id);
				writer.WriteAttributeString(GOM_TAGS.X, this.rgPoints[i].x.ToString());
				writer.WriteAttributeString(GOM_TAGS.Y, this.rgPoints[i].y.ToString());
				writer.WriteEndElement();
			}
			writer.WriteEndElement();

			writer.WriteStartElement(GOM_TAGS.STYLES);
			for (int i = 0; i < this.rgDrawingStyles.Count; i++)
			{
				this.rgDrawingStyles[i].SaveToXML(writer);
			}
			for (int i = 0; i < this.rgFillingStyles.Count; i++)
			{
				this.rgFillingStyles[i].SaveToXML(writer);
			}
			writer.WriteEndElement();

			writer.WriteStartElement(GOM_TAGS.TEXT);

			writer.WriteStartElement(GOM_TAGS.CONTENT);
			writer.WriteString(this.innerText);
			writer.WriteEndElement();

			writer.WriteStartElement(GOM_TAGS.FONT);
			writer.WriteAttributeString(GOM_TAGS.FAMILY, this.font.FontFamily.Name);
			writer.WriteAttributeString(GOM_TAGS.SIZE, this.font.Size.ToString());
			writer.WriteAttributeString(GOM_TAGS.BOLD, this.font.Bold.ToString());
			writer.WriteAttributeString(GOM_TAGS.ITALIC, this.font.Italic.ToString());
			writer.WriteAttributeString(GOM_TAGS.STRIKEOUT, this.font.Strikeout.ToString());
			writer.WriteAttributeString(GOM_TAGS.UNDERLINE, this.font.Underline.ToString());
			writer.WriteAttributeString(GOM_TAGS.RED, this.fontColor.R.ToString());
			writer.WriteAttributeString(GOM_TAGS.GREEN, this.fontColor.G.ToString());
			writer.WriteAttributeString(GOM_TAGS.BLUE, this.fontColor.B.ToString());
			writer.WriteEndElement();

			writer.WriteEndElement();

			writer.WriteEndElement();

		}
		public void LoadFromXML(System.Xml.XmlNode node, GOM_ResourceArrays resources)
		{
			GOM_Utility.VerifyXmlNode(node, GOM_TAGS.GRAPHIC_OBJECT);

			//Load properties of the graphic object
			for (int i = 0; i < node.Attributes.Count; i++)
			{
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.ID, true) == 0)
				{
					id = node.Attributes[i].Value;
				}
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.TYPE, true) == 0)
				{
					if (!node.Attributes[i].Value.Equals("primitive"))
					{
						throw new Exception("Invalid input. Not a primitive object node!");
					}
				}
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.X_OFFSET, true) == 0)
				{
					m_xOffset = float.Parse(node.Attributes[i].Value);
				}
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.Y_OFFSET, true) == 0)
				{
					m_yOffset = float.Parse(node.Attributes[i].Value);
				}
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.ROTATION, true) == 0)
				{
					m_rotation = float.Parse(node.Attributes[i].Value);
				}
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.TEMPLATE, true) == 0)
				{
					for (int j = 0; j < resources.Templates.Count; j++)
					{
						if (resources.Templates[j].id.Equals(node.Attributes[i].Value))
						{
							m_template = resources.Templates[j];
						}
					}

					if (m_template == null)
					{
						throw new Exception("Unknown template");
					}
				}
			}

			if (m_template == null)
			{
				throw new Exception("Can not find template of the graphic object");
			}
			//Initialize the graphic object according to the template
			InitializeFromTemplate(m_template);
			//Update status of the graphic object
			for (int i = 0; i < node.ChildNodes.Count; i++)
			{
				if (System.String.Compare(node.ChildNodes[i].Name, GOM_TAGS.POINTS, true) == 0)
				{
					UpdatePoints(node.ChildNodes[i]);
				}
				if (System.String.Compare(node.ChildNodes[i].Name, GOM_TAGS.STYLES, true) == 0)
				{
					UpdateStyles(node.ChildNodes[i]);
				}
				if (System.String.Compare(node.ChildNodes[i].Name, GOM_TAGS.TEXT, true) == 0)
				{
					LoadText(node.ChildNodes[i]);
				}
			}

			CalculateBoundingBox();
			SynchronizeConnectorPosition();
		}

		/// <summary>
		/// Draw the graphic object on the canvas according to current offset and rotation
		/// </summary>
		/// <param name="canvas">The canvas on which the graphic object is drawn</param>
		public void Draw(System.Drawing.Graphics canvas, bool ShowConnectPoint)
		{
			System.Drawing.Drawing2D.Matrix	orgTrans	= canvas.Transform;
			System.Drawing.RectangleF		rc			= this.BoundingBox;

			canvas.TranslateTransform(this.xOffset, this.yOffset);
			canvas.TranslateTransform((rc.Left + rc.Right) / 2, (rc.Top + rc.Bottom) / 2);
			canvas.RotateTransform(this.rotation);
			canvas.TranslateTransform(-(rc.Left + rc.Right) / 2, -(rc.Top + rc.Bottom) / 2);

			for (int i = 0; i < rgFillings.Count; i++)
			{
				rgFillings[i].Fill(canvas);
			}

			for (int i = 0; i < rgDrawings.Count; i++)
			{
				rgDrawings[i].Draw(canvas);
			}

			if (ShowConnectPoint)
			{
				System.Drawing.Pen ConnectBoxPen;

				ConnectBoxPen = new System.Drawing.Pen(System.Drawing.Color.Gray, 1);
				ConnectBoxPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

				for (int i = 0; i < rgPoints.Count; i++)
				{
					if (rgPoints[i].Connectable)
					{
						canvas.DrawRectangle(ConnectBoxPen, rgPoints[i].x - 2, rgPoints[i].y - 2, 4, 4);
					}
				}
			
				if (this.extConnect)
				{
					canvas.DrawRectangle(ConnectBoxPen, rc.X - 5, (rc.Top + rc.Bottom) / 2 - 2, 4, 4);
					canvas.DrawRectangle(ConnectBoxPen, (rc.Left + rc.Right) / 2 - 2, rc.Bottom + 1, 4, 4);
					canvas.DrawRectangle(ConnectBoxPen, (rc.Left + rc.Right) / 2 - 2, rc.Y - 5, 4, 4);
					canvas.DrawRectangle(ConnectBoxPen, rc.Right + 1, (rc.Top + rc.Bottom) / 2 - 2, 4, 4);
				}
			}

			canvas.Transform = orgTrans;
		}
		/// <summary>
		/// Draw the graphic object when selected
		/// </summary>
		/// <param name="canvas">The canvas on which the graphic object is drawn</param>
		/// <param name="BoundingBoxPen">Pen to draw the bounding box</param>
		/// <param name="ResizeBoxPen">Pen to draw the resize box</param>
		/// <param name="ConnectBoxPen">Pen to draw the connect box</param>
		/// <param name="ControlBoxPen">Pen to draw the control box</param>
		public void DrawSelected(System.Drawing.Graphics canvas, System.Drawing.Pen BoundingBoxPen, System.Drawing.Pen ResizeBoxPen, System.Drawing.Pen ConnectBoxPen, System.Drawing.Pen ControlBoxPen)
		{
			System.Drawing.Drawing2D.Matrix	orgTrans = canvas.Transform;
			System.Drawing.RectangleF		rc;

			rc = this.BoundingBox;

			canvas.TranslateTransform(this.xOffset, this.yOffset);
			canvas.TranslateTransform((rc.Left + rc.Right) / 2, (rc.Top + rc.Bottom) / 2);
			canvas.RotateTransform(this.rotation);
			canvas.TranslateTransform(-(rc.Left + rc.Right) / 2, -(rc.Top + rc.Bottom) / 2);

			for (int i = 0; i < rgFillings.Count; i++)
			{
				rgFillings[i].Fill(canvas);
			}

			for (int i = 0; i < rgDrawings.Count; i++)
			{
				rgDrawings[i].Draw(canvas);
			}

			canvas.DrawRectangle(BoundingBoxPen, rc.X - 3, rc.Y - 3, rc.Width + 6, rc.Height + 6);

			if (this.extScale)
			{
				canvas.DrawRectangle(ResizeBoxPen, rc.X - 5, rc.Y - 5, 4, 4);
				canvas.DrawRectangle(ResizeBoxPen, rc.X - 5, rc.Bottom + 1, 4, 4);
				canvas.DrawRectangle(ResizeBoxPen, rc.Right + 1, rc.Y - 5, 4, 4);
				canvas.DrawRectangle(ResizeBoxPen, rc.Right + 1, rc.Bottom + 1, 4, 4);
			}

			if (this.extRotate)
			{
				canvas.DrawEllipse(ControlBoxPen, (rc.Left + rc.Right) / 2 - 3, rc.Y - 30, 6, 6);
				canvas.DrawLine(ControlBoxPen, (rc.Left + rc.Right) / 2, rc.Y - 5, (rc.Left + rc.Right) / 2, rc.Y - 24);
			}

			if (this.extConnect)
			{
				canvas.DrawRectangle(ConnectBoxPen, rc.X - 5, (rc.Top + rc.Bottom) / 2 - 2, 4, 4);
				canvas.DrawRectangle(ConnectBoxPen, (rc.Left + rc.Right) / 2 - 2, rc.Bottom + 1, 4, 4);
				canvas.DrawRectangle(ConnectBoxPen, (rc.Left + rc.Right) / 2 - 2, rc.Y - 5, 4, 4);
				canvas.DrawRectangle(ConnectBoxPen, rc.Right + 1, (rc.Top + rc.Bottom) / 2 - 2, 4, 4);
			}

			for (int i = 0; i < rgPoints.Count; i++)
			{
				if (rgPoints[i].Connectable)
				{
					canvas.DrawRectangle(ConnectBoxPen, rgPoints[i].x - 2, rgPoints[i].y - 2, 4, 4);
				}
				if (rgPoints[i].Controllable)
				{
					canvas.DrawRectangle(ControlBoxPen, rgPoints[i].x - 2, rgPoints[i].y - 2, 4, 4);
				}
			}

			canvas.Transform = orgTrans;
		}

		public GOM_Point GetPointByName(string name)
		{
			GOM_Point point = rgPoints[name];
			if ( point != null )
			{
				return point;
			}

			GOM_Points others = new GOM_Points();
			others.Add(m_Top_Connector);
			others.Add(m_Bottom_Connector);
			others.Add(m_Left_Connector);
			others.Add(m_Right_Connector);
			others.Add(m_Rotation_Point);
			others.Add(m_SW_Resize_Point);
			others.Add(m_NE_Resize_Point);
			others.Add(m_SE_Resize_Point);
			others.Add(m_NW_Resize_Point);
			return others[name];
		}

		private System.Drawing.RectangleF	m_boundingBox;

		private GOM_Point			m_Top_Connector;
		private GOM_Point			m_Bottom_Connector;
		private GOM_Point			m_Left_Connector;
		private GOM_Point			m_Right_Connector;
		/// <summary>The south west resizing point</summary>
		private GOM_Point			m_Rotation_Point;
		/// <summary>The south west resizing point</summary>
		private GOM_Point			m_SW_Resize_Point;
		/// <summary>The north east resizing point</summary>
		private GOM_Point			m_NE_Resize_Point;
		/// <summary>The south east resizing point</summary>
		private GOM_Point			m_SE_Resize_Point;
		/// <summary>The north west resizing point</summary>
		private GOM_Point			m_NW_Resize_Point;
		/// <summary>The id of the primitive object</summary>
		private string				m_id;
		/// <summary>The x offset of the primitive object</summary>
		private float				m_xOffset;
		/// <summary>The y offset of the primitive object</summary>
		private float				m_yOffset;
		/// <summary>The angel of rotation of the primitive object</summary>
		private float				m_rotation;
		/// <summary>The template of the primitive object</summary>
		private GOM_Template		m_template;
		/// <summary>The inside text</summary>
		public string				innerText;
		/// <summary>The font of the inside text</summary>
		public System.Drawing.Font	font;
		/// <summary>The color of the inside text</summary>
		public System.Drawing.Color	fontColor;
		/// <summary>Whether the object created from this template can be scaled by external application</summary>
		public bool					extScale;
		/// <summary>Whether the object created from this template can be rotated by external application</summary>
		public bool					extRotate;
		/// <summary>Whether the object created from this template can be connected by external application</summary>
		public bool					extConnect;
		/// <summary>Whether the aspect ration of the object created from this template should be kept by external application</summary>
		public bool					keepAspectRatio;
		/// <summary>The list of points in the template</summary>
		public GOM_Points			rgPoints;
		/// <summary>The list of editing modes in the template</summary>
		public GOM_Strings			rgEditingModes;
		/// <summary>The list of drawing operations in the template</summary>
		public GOM_Drawings			rgDrawings;
		/// <summary>The list of filling operations in the template</summary>
		public GOM_Fillings			rgFillings;
		/// <summary>The list of drawing styles in the template</summary>
		public GOM_Drawing_Styles	rgDrawingStyles;
		/// <summary>The list of filling styles in the template</summary>
		public GOM_Filling_Styles	rgFillingStyles;
	}
	/// <summary>
	/// The group object
	/// </summary>
	public class GOM_Object_Group: GOM_Interface_Graphic_Object
	{
		public GOM_Object_Group()
		{
			m_id		= Guid.NewGuid().ToString("D");
			m_xOffset	= 0;
			m_yOffset	= 0;
			m_rotation	= 0;

			m_boundingBox = new System.Drawing.RectangleF();

			rgObjects	= new GOM_Objects();

			GOM_Constraint_Set	constraintSet;

			m_Top_Connector = new GOM_Point();
			m_Top_Connector.id = GOM_Special_Point_Name.TOP_CONNECTOR;
			m_Top_Connector.Connectable = true;

			m_Bottom_Connector = new GOM_Point();
			m_Bottom_Connector.id = GOM_Special_Point_Name.BOTTOM_CONNECTOR;
			m_Bottom_Connector.Connectable = true;

			m_Left_Connector = new GOM_Point();
			m_Left_Connector.id = GOM_Special_Point_Name.LEFT_CONNECTOR;
			m_Left_Connector.Connectable = true;

			m_Right_Connector = new GOM_Point();
			m_Right_Connector.id = GOM_Special_Point_Name.RIGHT_CONNECTOR;
			m_Right_Connector.Connectable = true;

			m_SE_Resize_Point			= new GOM_Point();
			m_SE_Resize_Point.id		= GOM_Special_Point_Name.SE_RESIZING_POINT;
			constraintSet				= new GOM_Constraint_Set();
			constraintSet.EditingMode	= "default";
			constraintSet.Add(new GOM_Resizing_SE_Constraint(this, m_SE_Resize_Point));
			m_SE_Resize_Point.Constraints.Add(constraintSet);

			m_NW_Resize_Point			= new GOM_Point();
			m_NW_Resize_Point.id		= GOM_Special_Point_Name.NW_RESIZING_POINT;
			constraintSet				= new GOM_Constraint_Set();
			constraintSet.EditingMode	= "default";
			constraintSet.Add(new GOM_Resizing_NW_Constraint(this, m_NW_Resize_Point));
			m_NW_Resize_Point.Constraints.Add(constraintSet);

			m_SW_Resize_Point			= new GOM_Point();
			m_SW_Resize_Point.id		= GOM_Special_Point_Name.SW_RESIZING_POINT;
			constraintSet				= new GOM_Constraint_Set();
			constraintSet.EditingMode	= "default";
			constraintSet.Add(new GOM_Resizing_SW_Constraint(this, m_SW_Resize_Point));
			m_SW_Resize_Point.Constraints.Add(constraintSet);

			m_NE_Resize_Point			= new GOM_Point();
			m_NE_Resize_Point.id		= GOM_Special_Point_Name.NE_RESIZING_POINT;
			constraintSet				= new GOM_Constraint_Set();
			constraintSet.EditingMode	= "default";
			constraintSet.Add(new GOM_Resizing_NE_Constraint(this, m_NE_Resize_Point));
			m_NE_Resize_Point.Constraints.Add(constraintSet);

			m_Rotation_Point			= new GOM_Point();
			m_Rotation_Point.id			= GOM_Special_Point_Name.ROTATION_POINT;
			constraintSet				= new GOM_Constraint_Set();
			constraintSet.EditingMode	= "default";
			constraintSet.Add(new GOM_Rotation_Constraint(this, m_Rotation_Point));
			m_Rotation_Point.Constraints.Add(constraintSet);
		}

		/// <summary>
		/// Synchronize the position of external connectors
		/// </summary>
		private void SynchronizeConnectorPosition()
		{
			System.Drawing.RectangleF rc = this.BoundingBox;

			this.m_Top_Connector.x = (rc.Left + rc.Right) / 2;
			this.m_Top_Connector.y = rc.Top - 3;

			this.m_Bottom_Connector.x = (rc.Left + rc.Right) / 2;
			this.m_Bottom_Connector.y = rc.Bottom + 3;

			this.m_Left_Connector.x = rc.Left - 3;
			this.m_Left_Connector.y = (rc.Top + rc.Bottom) / 2;

			this.m_Right_Connector.x = rc.Right + 3;
			this.m_Right_Connector.y = (rc.Top + rc.Bottom) / 2;
		}

		/// <summary>
		/// Create a copy of current graphic object
		/// </summary>
		/// <returns></returns>
		public GOM_Interface_Graphic_Object Clone()
		{
			GOM_Object_Group	obj;

			obj = new GOM_Object_Group();

			obj.m_id		= this.m_id;
			obj.m_xOffset	= this.m_xOffset;
			obj.m_yOffset	= this.m_yOffset;
			obj.m_rotation	= this.m_rotation;

			for (int i = 0; i < this.rgObjects.Count; i++)
			{
				obj.rgObjects.Add(this.rgObjects[i].Clone());
			}

			return obj;
		}

		public string id
		{
			get
			{
				return m_id;
			}
			set
			{
				m_id = value;
			}
		}

		public GOMLib.GraphicObjectType type
		{
			get
			{
				return GraphicObjectType.Group;
			}
		}

		public bool KeepAspectRatio
		{
			get
			{
				return true;
			}
			set
			{
				if (value == false)
				{
					throw new ArgumentException("KeepAspectRatio is not allowed be false in a group object!");
				}
			}
		}

		public float xOffset
		{
			get
			{
				return m_xOffset;
			}
			set
			{
				m_xOffset = value;
			}
		}

		public float yOffset
		{
			get
			{
				return m_yOffset;
			}
			set
			{
				m_yOffset = value;
			}
		}

		public float width
		{
			get
			{
				return this.BoundingBox.Width;
			}
			set
			{
				System.Drawing.RectangleF rc = this.BoundingBox;
				float scale = value / rc.Width;

				for (int i = 0; i < rgObjects.Count; i++)
				{
					rgObjects[i].xOffset = (rgObjects[i].xOffset - rc.Left) * scale + rc.Left;
					rgObjects[i].width *= scale;
				}

				SynchronizeConnectorPosition();
				CalculateBoundingBox();
			}
		}

		public float height
		{
			get
			{
				return this.BoundingBox.Height;
			}
			set
			{
				System.Drawing.RectangleF rc = this.BoundingBox;
				float scale = value / rc.Height;

				for (int i = 0; i < rgObjects.Count; i++)
				{
					rgObjects[i].yOffset = (rgObjects[i].yOffset - rc.Top) * scale + rc.Top;
					rgObjects[i].height	*= scale;
				}

				SynchronizeConnectorPosition();
				CalculateBoundingBox();
			}
		}

		public float rotation
		{
			get
			{
				return m_rotation;
			}
			set
			{
				m_rotation = value;
			}
		}

		public void Draw(System.Drawing.Graphics canvas, bool ShowConnectPoint)
		{
			System.Drawing.Drawing2D.Matrix	orgTrans	= canvas.Transform;
			System.Drawing.RectangleF		rc			= this.BoundingBox;

			canvas.TranslateTransform(this.xOffset, this.yOffset);
			canvas.TranslateTransform((rc.Left + rc.Right) / 2, (rc.Top + rc.Bottom) / 2);
			canvas.RotateTransform(this.rotation);
			canvas.TranslateTransform(-(rc.Left + rc.Right) / 2, -(rc.Top + rc.Bottom) / 2);

			for (int i = 0; i < rgObjects.Count; i++)
			{
				rgObjects[i].Draw(canvas, false);
			}

			if (ShowConnectPoint)
			{
				System.Drawing.Pen ConnectBoxPen;

				ConnectBoxPen = new System.Drawing.Pen(System.Drawing.Color.Gray, 1);
				ConnectBoxPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

				canvas.DrawRectangle(ConnectBoxPen, rc.X - 5, (rc.Top + rc.Bottom) / 2 - 2, 4, 4);
				canvas.DrawRectangle(ConnectBoxPen, (rc.Left + rc.Right) / 2 - 2, rc.Bottom + 1, 4, 4);
				canvas.DrawRectangle(ConnectBoxPen, (rc.Left + rc.Right) / 2 - 2, rc.Y - 5, 4, 4);
				canvas.DrawRectangle(ConnectBoxPen, rc.Right + 1, (rc.Top + rc.Bottom) / 2 - 2, 4, 4);
			}

			canvas.Transform = orgTrans;
		}

		public void DrawSelected(System.Drawing.Graphics canvas, System.Drawing.Pen BoundingBoxPen, System.Drawing.Pen ResizeBoxPen, System.Drawing.Pen ConnectBoxPen, System.Drawing.Pen ControlBoxPen)
		{
			System.Drawing.Drawing2D.Matrix	orgTrans = canvas.Transform;
			System.Drawing.RectangleF		rc;

			rc = this.BoundingBox;

			canvas.TranslateTransform(this.xOffset, this.yOffset);
			canvas.TranslateTransform((rc.Left + rc.Right) / 2, (rc.Top + rc.Bottom) / 2);
			canvas.RotateTransform(this.rotation);
			canvas.TranslateTransform(-(rc.Left + rc.Right) / 2, -(rc.Top + rc.Bottom) / 2);

			for (int i = 0; i < rgObjects.Count; i++)
			{
				rgObjects[i].Draw(canvas, false);
			}

			canvas.DrawRectangle(BoundingBoxPen, rc.X - 3, rc.Y - 3, rc.Width + 6, rc.Height + 6);

			canvas.DrawRectangle(ResizeBoxPen, rc.X - 5, rc.Y - 5, 4, 4);
			canvas.DrawRectangle(ResizeBoxPen, rc.X - 5, rc.Bottom + 1, 4, 4);
			canvas.DrawRectangle(ResizeBoxPen, rc.Right + 1, rc.Y - 5, 4, 4);
			canvas.DrawRectangle(ResizeBoxPen, rc.Right + 1, rc.Bottom + 1, 4, 4);

			canvas.DrawEllipse(ControlBoxPen, (rc.Left + rc.Right) / 2 - 3, rc.Y - 30, 6, 6);
			canvas.DrawLine(ControlBoxPen, (rc.Left + rc.Right) / 2, rc.Y - 5, (rc.Left + rc.Right) / 2, rc.Y - 24);

			canvas.DrawRectangle(ConnectBoxPen, rc.X - 5, (rc.Top + rc.Bottom) / 2 - 2, 4, 4);
			canvas.DrawRectangle(ConnectBoxPen, (rc.Left + rc.Right) / 2 - 2, rc.Bottom + 1, 4, 4);
			canvas.DrawRectangle(ConnectBoxPen, (rc.Left + rc.Right) / 2 - 2, rc.Y - 5, 4, 4);
			canvas.DrawRectangle(ConnectBoxPen, rc.Right + 1, (rc.Top + rc.Bottom) / 2 - 2, 4, 4);

			canvas.Transform = orgTrans;
		}

		public bool IsPointInObject(int x, int y)
		{
			System.Drawing.RectangleF	rc;
			System.Drawing.PointF		pt;

			rc = this.BoundingBox;
			pt = PointToObject(new System.Drawing.PointF(x, y));
			
			if ((rc.Left <= pt.X) && (pt.X <= rc.Right) && (rc.Top <= pt.Y) && (pt.Y <= rc.Bottom))
			{
				return true;
			}
			else
			{
				return false;
			}		
		}

		public System.Drawing.RectangleF BoundingBox
		{
			get
			{
				return m_boundingBox;
			}
		}

		public void CalculateBoundingBox()
		{
			float	minX, minY, maxX, maxY;
			System.Drawing.RectangleF	rc;
			System.Drawing.PointF		pt;

			minX = 0;
			minY = 0;
			maxX = 0;
			maxY = 0;
			
			if (rgObjects.Count > 0)
			{
				rc = rgObjects[0].BoundingBox;
				pt = rgObjects[0].PointToCanvas(new System.Drawing.PointF(rc.Left, rc.Top));
			
				minX = pt.X;
				minY = pt.Y;
				maxX = pt.X;
				maxY = pt.Y;

				for (int i = 0; i < rgObjects.Count; i++)
				{
					rc = rgObjects[i].BoundingBox;

					pt = rgObjects[i].PointToCanvas(new System.Drawing.PointF(rc.Left, rc.Top));
					minX = Math.Min(minX, pt.X);
					minY = Math.Min(minY, pt.Y);
					maxX = Math.Max(maxX, pt.X);
					maxY = Math.Max(maxY, pt.Y);

					pt = rgObjects[i].PointToCanvas(new System.Drawing.PointF(rc.Left, rc.Bottom));
					minX = Math.Min(minX, pt.X);
					minY = Math.Min(minY, pt.Y);
					maxX = Math.Max(maxX, pt.X);
					maxY = Math.Max(maxY, pt.Y);

					pt = rgObjects[i].PointToCanvas(new System.Drawing.PointF(rc.Right, rc.Top));
					minX = Math.Min(minX, pt.X);
					minY = Math.Min(minY, pt.Y);
					maxX = Math.Max(maxX, pt.X);
					maxY = Math.Max(maxY, pt.Y);

					pt = rgObjects[i].PointToCanvas(new System.Drawing.PointF(rc.Right, rc.Bottom));
					minX = Math.Min(minX, pt.X);
					minY = Math.Min(minY, pt.Y);
					maxX = Math.Max(maxX, pt.X);
					maxY = Math.Max(maxY, pt.Y);
				}
			}

			m_boundingBox = new System.Drawing.RectangleF(minX, minY, maxX - minX, maxY - minY);
		}

		/// <summary>
		/// Return the bounding region of the graphic object in global coordinate
		/// </summary>
		public System.Drawing.Region BoundingRegion
		{
			get
			{
				System.Drawing.Drawing2D.Matrix	matrix;
				System.Drawing.RectangleF	rc;
				System.Drawing.Region		rgn;

				rc = this.BoundingBox;
				rgn = new System.Drawing.Region(rc);

				matrix = new System.Drawing.Drawing2D.Matrix();
				matrix.Translate(-(rc.Left + rc.Right) / 2, -(rc.Top + rc.Bottom) / 2);
				rgn.Transform(matrix);

				matrix.Reset();
				matrix.Rotate(this.rotation);
				rgn.Transform(matrix);

				matrix.Reset();
				matrix.Translate(this.xOffset, this.yOffset);
				rgn.Transform(matrix);

				matrix.Reset();
				matrix.Translate((rc.Left + rc.Right) / 2, (rc.Top + rc.Bottom) / 2);
				rgn.Transform(matrix);

				return rgn;
			}
		}
		
		public GOM_Point GetMovablePointAt(int x, int y)
		{
			System.Drawing.RectangleF	rc;
			System.Drawing.PointF		pt;

			rc = this.BoundingBox;
			pt = PointToObject(new System.Drawing.PointF(x, y));

			this.m_SE_Resize_Point.x = rc.Right + 3;
			this.m_SE_Resize_Point.y = rc.Bottom + 3;
			if (GOM_Default_Values.IsMouseOnPoint(m_SE_Resize_Point.x, m_SE_Resize_Point.y, pt.X, pt.Y))
			{
				return m_SE_Resize_Point;
			}

			this.m_NW_Resize_Point.x = rc.Left - 3;
			this.m_NW_Resize_Point.y = rc.Top - 3;
			if (GOM_Default_Values.IsMouseOnPoint(m_NW_Resize_Point.x, m_NW_Resize_Point.y, pt.X, pt.Y))
			{
				return m_NW_Resize_Point;
			}

			this.m_SW_Resize_Point.x = rc.Left - 3;
			this.m_SW_Resize_Point.y = rc.Bottom + 3;
			if (GOM_Default_Values.IsMouseOnPoint(m_SW_Resize_Point.x, m_SW_Resize_Point.y, pt.X, pt.Y))
			{
				return m_SW_Resize_Point;
			}

			this.m_NE_Resize_Point.x = rc.Right + 3;
			this.m_NE_Resize_Point.y = rc.Top - 3;
			if (GOM_Default_Values.IsMouseOnPoint(m_NE_Resize_Point.x, m_NE_Resize_Point.y, pt.X, pt.Y))
			{
				return m_NE_Resize_Point;
			}

			this.m_Rotation_Point.x = (rc.Left + rc.Right) / 2;
			this.m_Rotation_Point.y = rc.Top - 27;
			if (GOM_Default_Values.IsMouseOnPoint(m_Rotation_Point.x, m_Rotation_Point.y, pt.X, pt.Y))
			{
				return m_Rotation_Point;
			}

			return null;
		}

		public GOM_Point GetConnectablePointAt(int x, int y)
		{
			System.Drawing.RectangleF	rc;
			System.Drawing.PointF		pt;

			rc = this.BoundingBox;
			pt = PointToObject(new System.Drawing.PointF(x, y));

			this.m_Top_Connector.x = (rc.Left + rc.Right) / 2;
			this.m_Top_Connector.y = rc.Top - 3;
			if (GOM_Default_Values.IsMouseOnPoint(m_Top_Connector.x, m_Top_Connector.y, pt.X, pt.Y))
			{
				return m_Top_Connector;
			}

			this.m_Bottom_Connector.x = (rc.Left + rc.Right) / 2;
			this.m_Bottom_Connector.y = rc.Bottom + 3;
			if (GOM_Default_Values.IsMouseOnPoint(m_Bottom_Connector.x, m_Bottom_Connector.y, pt.X, pt.Y))
			{
				return m_Bottom_Connector;
			}

			this.m_Left_Connector.x = rc.Left - 3;
			this.m_Left_Connector.y = (rc.Top + rc.Bottom) / 2;
			if (GOM_Default_Values.IsMouseOnPoint(m_Left_Connector.x, m_Left_Connector.y, pt.X, pt.Y))
			{
				return m_Left_Connector;
			}

			this.m_Right_Connector.x = rc.Right + 3;
			this.m_Right_Connector.y = (rc.Top + rc.Bottom) / 2;
			if (GOM_Default_Values.IsMouseOnPoint(m_Right_Connector.x, m_Right_Connector.y, pt.X, pt.Y))
			{
				return m_Right_Connector;
			}

			return null;
		}

		public System.Drawing.PointF PointToObject(System.Drawing.PointF pt)
		{
			System.Drawing.Drawing2D.Matrix	matrix;
			System.Drawing.RectangleF		rc;
			System.Drawing.PointF[]			rgPts;

			rc = this.BoundingBox;

			rgPts = new System.Drawing.PointF[1];
			rgPts[0].X = pt.X;
			rgPts[0].Y = pt.Y;

			matrix = new System.Drawing.Drawing2D.Matrix();
			matrix.Translate(-this.xOffset, -this.yOffset);
			matrix.TransformPoints(rgPts);

			matrix.Reset();
			matrix.Translate(-(rc.Left + rc.Right) / 2, -(rc.Top + rc.Bottom) / 2);
			matrix.TransformPoints(rgPts);

			matrix.Reset();
			matrix.Rotate(-this.rotation);
			matrix.TransformPoints(rgPts);

			matrix.Reset();
			matrix.Translate((rc.Left + rc.Right) / 2, (rc.Top + rc.Bottom) / 2);
			matrix.TransformPoints(rgPts);

			return rgPts[0];
		}

		public System.Drawing.PointF PointToCanvas(System.Drawing.PointF pt)
		{
			System.Drawing.Drawing2D.Matrix	matrix;
			System.Drawing.RectangleF		rc;
			System.Drawing.PointF[]			rgPts;

			rc = this.BoundingBox;

			rgPts = new System.Drawing.PointF[1];
			rgPts[0].X = pt.X;
			rgPts[0].Y = pt.Y;

			matrix = new System.Drawing.Drawing2D.Matrix();
			matrix.Translate(-(rc.Left + rc.Right) / 2, -(rc.Top + rc.Bottom) / 2);
			matrix.TransformPoints(rgPts);

			matrix.Reset();
			matrix.Rotate(this.rotation);
			matrix.TransformPoints(rgPts);

			matrix.Reset();
			matrix.Translate(this.xOffset, this.yOffset);
			matrix.TransformPoints(rgPts);

			matrix.Reset();
			matrix.Translate((rc.Left + rc.Right) / 2, (rc.Top + rc.Bottom) / 2);
			matrix.TransformPoints(rgPts);

			return rgPts[0];
		}

		static public GOM_Object_Group CreateGroupObject(GOM_Objects rgObjs)
		{
			GOM_Object_Group			group = null;
			System.Drawing.PointF		pt;
			System.Drawing.RectangleF	rc;

			if (rgObjs.Count > 0)
			{
				float	minX, minY;

				rc = rgObjs[0].BoundingBox;
				pt = rgObjs[0].PointToCanvas(new System.Drawing.PointF(rc.Left, rc.Top));

				minX = pt.X;
				minY = pt.Y;

				for (int i = 0; i < rgObjs.Count; i++)
				{
					rc = rgObjs[i].BoundingBox;

					pt = rgObjs[i].PointToCanvas(new System.Drawing.PointF(rc.Left, rc.Top));
					minX = System.Math.Min(minX, pt.X);
					minY = System.Math.Min(minY, pt.Y);

					pt = rgObjs[i].PointToCanvas(new System.Drawing.PointF(rc.Left, rc.Bottom));
					minX = System.Math.Min(minX, pt.X);
					minY = System.Math.Min(minY, pt.Y);

					pt = rgObjs[i].PointToCanvas(new System.Drawing.PointF(rc.Right, rc.Top));
					minX = System.Math.Min(minX, pt.X);
					minY = System.Math.Min(minY, pt.Y);

					pt = rgObjs[i].PointToCanvas(new System.Drawing.PointF(rc.Right, rc.Bottom));
					minX = System.Math.Min(minX, pt.X);
					minY = System.Math.Min(minY, pt.Y);
				}

				group = new GOM_Object_Group();

				for (int i = 0; i < rgObjs.Count; i++)
				{
					rgObjs[i].xOffset -= minX;
					rgObjs[i].yOffset -= minY;

					group.rgObjects.Add(rgObjs[i]);
				}

				group.xOffset = minX;
				group.yOffset = minY;
			}

			group.CalculateBoundingBox();

			return group;
		}

		public void DecomposeGroupObject(GOM_Objects rgObjs)
		{
			GOM_Interface_Graphic_Object	obj;
			System.Drawing.RectangleF		rc;
			System.Drawing.PointF			ptC, ptR;
			float[]							rgOffsetX, rgOffsetY, rgRotation;

			if (rgObjects.Count > 0)
			{
				rgOffsetX	= new float[rgObjects.Count];
				rgOffsetY	= new float[rgObjects.Count];
				rgRotation	= new float[rgObjects.Count];

				for (int i = 0; i < rgObjects.Count; i++)
				{
					obj = rgObjects[i];
					rc	= obj.BoundingBox;

					ptC	= obj.PointToCanvas(new System.Drawing.PointF((rc.Left + rc.Right) / 2, (rc.Top + rc.Bottom) / 2));
					ptC	= this.PointToCanvas(ptC);

					ptR = obj.PointToCanvas(new System.Drawing.PointF((rc.Left + rc.Right) / 2, rc.Top - 30));
					ptR = this.PointToCanvas(ptR);

					rgOffsetX[i]	= ptC.X - (rc.Left + rc.Right) / 2;
					rgOffsetY[i]	= ptC.Y - (rc.Top + rc.Bottom) / 2;
					rgRotation[i]	= (float)((System.Math.Atan2(ptR.X - ptC.X, ptC.Y - ptR.Y) / System.Math.PI) * 180);
				}

				for (int i = 0; i < rgObjects.Count; i++)
				{
					rgObjects[i].xOffset	= rgOffsetX[i];
					rgObjects[i].yOffset	= rgOffsetY[i];
					rgObjects[i].rotation	= rgRotation[i];

					rgObjs.Add(rgObjects[i]);
				}

				rgObjects.Clear();
			}
		}

		public void SaveToXML(System.Xml.XmlWriter writer)
		{
			writer.WriteStartElement(GOM_TAGS.GRAPHIC_OBJECT);
			writer.WriteAttributeString(GOM_TAGS.ID, this.id);
			writer.WriteAttributeString(GOM_TAGS.TYPE, "group");
			writer.WriteAttributeString(GOM_TAGS.X_OFFSET, this.xOffset.ToString());
			writer.WriteAttributeString(GOM_TAGS.Y_OFFSET, this.yOffset.ToString());
			writer.WriteAttributeString(GOM_TAGS.ROTATION, this.rotation.ToString());

			writer.WriteStartElement(GOM_TAGS.OBJECTS);
			for( int i=0; i<rgObjects.Count; i++ )
			{
				rgObjects[i].SaveToXML(writer);
			}
			writer.WriteEndElement();

			writer.WriteEndElement();
		}

		public void LoadFromXML(System.Xml.XmlNode node, GOM_ResourceArrays resources)
		{
			GOM_Utility.VerifyXmlNode(node, GOM_TAGS.GRAPHIC_OBJECT);

			//Load properties of the graphic object
			for (int i = 0; i < node.Attributes.Count; i++)
			{
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.ID, true) == 0)
				{
					id = node.Attributes[i].Value;
				}
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.TYPE, true) == 0)
				{
					if (!node.Attributes[i].Value.Equals("group"))
					{
						throw new Exception("Invalid input. Not a group object node!");
					}
				}
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.X_OFFSET, true) == 0)
				{
					m_xOffset = float.Parse(node.Attributes[i].Value);
				}
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.Y_OFFSET, true) == 0)
				{
					m_yOffset = float.Parse(node.Attributes[i].Value);
				}
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.ROTATION, true) == 0)
				{
					m_rotation = float.Parse(node.Attributes[i].Value);
				}
			}

			this.rgObjects.Clear();
			GOM_Utility.LoadObjectsFromXML(node.ChildNodes[0], resources, this.rgObjects);

			CalculateBoundingBox();
			SynchronizeConnectorPosition();
		}

		public GOM_Point GetPointByName(string name)
		{
			GOM_Points others = new GOM_Points();
			others.Add(m_Top_Connector);
			others.Add(m_Bottom_Connector);
			others.Add(m_Left_Connector);
			others.Add(m_Right_Connector);
			others.Add(m_Rotation_Point);
			others.Add(m_SW_Resize_Point);
			others.Add(m_NE_Resize_Point);
			others.Add(m_SE_Resize_Point);
			others.Add(m_NW_Resize_Point);
			return others[name];
		}

		private System.Drawing.RectangleF	m_boundingBox;

		private GOM_Point			m_Top_Connector;
		private GOM_Point			m_Bottom_Connector;
		private GOM_Point			m_Left_Connector;
		private GOM_Point			m_Right_Connector;
		/// <summary>The south west resizing point</summary>
		private GOM_Point			m_Rotation_Point;
		/// <summary>The south west resizing point</summary>
		private GOM_Point			m_SW_Resize_Point;
		/// <summary>The north east resizing point</summary>
		private GOM_Point			m_NE_Resize_Point;
		/// <summary>The south east resizing point</summary>
		private GOM_Point			m_SE_Resize_Point;
		/// <summary>The north west resizing point</summary>
		private GOM_Point			m_NW_Resize_Point;
		/// <summary>The id of the primitive object</summary>
		private string				m_id;
		/// <summary>The x offset of the primitive object</summary>
		private float				m_xOffset;
		/// <summary>The y offset of the primitive object</summary>
		private float				m_yOffset;
		/// <summary>The angel of rotation of the primitive object</summary>
		private float				m_rotation;

		public GOM_Objects	rgObjects;
	}

	public class SketchPoint : GOM_Interface_XmlPersistence
	{
		public SketchPoint()
		{
			x = 0;
			y = 0;
			time = 0;
		}

		public int	x;
		public int	y;
		public int	time;

		public void SaveToXML(System.Xml.XmlWriter writer)
		{
			writer.WriteStartElement(GOM_TAGS.SKETCH_POINT);

			writer.WriteAttributeString(GOM_TAGS.X, x.ToString());
			writer.WriteAttributeString(GOM_TAGS.Y, y.ToString());
			writer.WriteAttributeString(GOM_TAGS.TIME, time.ToString());

			writer.WriteEndElement();
		}

		public void LoadFromXML(System.Xml.XmlNode node, GOM_ResourceArrays resources)
		{
			GOM_Utility.VerifyXmlNode(node, GOM_TAGS.SKETCH_POINT);

			for( int i=0; i<node.Attributes.Count; i++ )
			{
				if ( string.Compare(node.Attributes[i].Name, GOM_TAGS.X, true) == 0 )
				{
					x = int.Parse(node.Attributes[i].Value);
				}
				if ( string.Compare(node.Attributes[i].Name, GOM_TAGS.Y, true) == 0 )
				{
					y = int.Parse(node.Attributes[i].Value);
				}
				if ( string.Compare(node.Attributes[i].Name, GOM_TAGS.TIME, true) == 0 )
				{
					time = int.Parse(node.Attributes[i].Value);
				}
			}
		}

	}

	public class GOM_Object_Sketch: GOM_Interface_Graphic_Object
	{

		public GOM_Object_Sketch()
		{
			m_id			= Guid.NewGuid().ToString("D");
			m_boundingBox	= new System.Drawing.RectangleF(0, 0, 0, 0);
			rgSketchSet		= new System.Collections.ArrayList();
			rgStrokeToSketch= new System.Collections.ArrayList();
			rgDrawings	= new GOM_Drawings();
			strokeStyle	= new GOM_Style_Drawing();
			strokeStyle.id = "default";
			strokeStyle.drawingStyle = new System.Drawing.Pen(System.Drawing.Color.Green, 1);
		}

		public string id
		{
			get
			{
				return m_id;
			}
			set
			{
				m_id = value;
			}
		}

		public GOMLib.GraphicObjectType type
		{
			get
			{
				return GraphicObjectType.Sketch;
			}
		}

		public bool KeepAspectRatio
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		public float xOffset
		{
			get
			{
				return 0;
			}
			set
			{
				System.Collections.ArrayList	rgStroke;
				SketchPoint						pt;

				for (int i = 0; i < rgSketchSet.Count; i++)
				{
					rgStroke = (System.Collections.ArrayList)rgSketchSet[i];

					for (int j = 0; j < rgStroke.Count; j++)
					{
						pt = (SketchPoint)rgStroke[j];
						pt.x += (int)value;
						rgStroke[j] = pt;
					}
				}
		
				for (int i = 0; i < rgDrawings.Count; i++)
				{
					for (int j = 0; j < rgDrawings[i].Points.Count; j++)
					{
						rgDrawings[i].Points[j].x += (int)value;
					}
				}

				this.CalculateBoundingBox();
			}
		}

		public float yOffset
		{
			get
			{
				return 0;
			}
			set
			{
				System.Collections.ArrayList	rgStroke;
				SketchPoint						pt;

				for (int i = 0; i < rgSketchSet.Count; i++)
				{
					rgStroke = (System.Collections.ArrayList)rgSketchSet[i];

					for (int j = 0; j < rgStroke.Count; j++)
					{
						pt = (SketchPoint)rgStroke[j];
						pt.y += (int)value;
						rgStroke[j] = pt;
					}
				}				
		
				for (int i = 0; i < rgDrawings.Count; i++)
				{
					for (int j = 0; j < rgDrawings[i].Points.Count; j++)
					{
						rgDrawings[i].Points[j].y += (int)value;
					}
				}

				this.CalculateBoundingBox();
			}
		}

		public float width
		{
			get
			{
				return this.BoundingBox.Width;
			}
			set
			{
			}
		}

		public float height
		{
			get
			{
				return this.BoundingBox.Height;
			}
			set
			{
			}
		}

		public float rotation
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public void Draw(System.Drawing.Graphics canvas, bool ShowConnectPoint)
		{
			System.Collections.ArrayList	rgStroke;
			SketchPoint						pt1, pt2;

			for (int i = 0; i < rgSketchSet.Count; i++)
			{
				rgStroke = (System.Collections.ArrayList)rgSketchSet[i];

				for (int j = 0; j < rgStroke.Count - 1; j++)
				{
					pt1 = (SketchPoint)rgStroke[j];
					pt2 = (SketchPoint)rgStroke[j + 1];
					canvas.DrawLine(System.Drawing.Pens.LightGray, pt1.x, pt1.y, pt2.x, pt2.y);
				}
			}	
			
			for (int i = 0; i < rgDrawings.Count; i++)
			{
				rgDrawings[i].Style = strokeStyle;
				rgDrawings[i].Draw(canvas);
			}
		}

		public void DrawSelected(System.Drawing.Graphics canvas, System.Drawing.Pen BoundingBoxPen, System.Drawing.Pen ResizeBoxPen, System.Drawing.Pen ConnectBoxPen, System.Drawing.Pen ControlBoxPen)
		{
			System.Collections.ArrayList	rgStroke;
			System.Drawing.RectangleF		rc;
			SketchPoint						pt1, pt2;

			for (int i = 0; i < rgSketchSet.Count; i++)
			{
				rgStroke = (System.Collections.ArrayList)rgSketchSet[i];

				for (int j = 0; j < rgStroke.Count - 1; j++)
				{
					pt1 = (SketchPoint)rgStroke[j];
					pt2 = (SketchPoint)rgStroke[j + 1];
					canvas.DrawLine(System.Drawing.Pens.LightGray, pt1.x, pt1.y, pt2.x, pt2.y);
				}
			}				

			for (int i = 0; i < rgDrawings.Count; i++)
			{
				rgDrawings[i].Style = strokeStyle;
				rgDrawings[i].Draw(canvas);
			}
			
			rc = this.BoundingBox;
			canvas.DrawRectangle(BoundingBoxPen, rc.Left, rc.Top, rc.Width, rc.Height);
		}

		public bool IsPointInObject(int x, int y)
		{
			System.Drawing.RectangleF	rc;

			rc = this.BoundingBox;

			if ((rc.Left <= x) && (x <= rc.Right) && (rc.Top <= y) && (y <= rc.Bottom))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public System.Drawing.RectangleF BoundingBox
		{
			get
			{
				return m_boundingBox;
			}
		}

		public void CalculateBoundingBox()
		{
			System.Collections.ArrayList	rgStroke;
			SketchPoint						pt;
			float							minX, minY, maxX, maxY;

			minX = 0;
			minY = 0;
			maxX = 0;
			maxY = 0;

			if (rgSketchSet.Count > 0)
			{
				rgStroke = (System.Collections.ArrayList)rgSketchSet[0];
				pt = (SketchPoint)rgStroke[0];

				minX = pt.x;
				minY = pt.y;
				maxX = pt.x;
				maxY = pt.y;

				for (int i = 0; i < rgSketchSet.Count; i++)
				{
					rgStroke = (System.Collections.ArrayList)rgSketchSet[i];

					for (int j = 0; j < rgStroke.Count; j++)
					{
						pt = (SketchPoint)rgStroke[j];

						minX = System.Math.Min(minX, pt.x);
						minY = System.Math.Min(minY, pt.y);
						maxX = System.Math.Max(maxX, pt.x);
						maxY = System.Math.Max(maxY, pt.y);
					}
				}
			}

			m_boundingBox = new System.Drawing.RectangleF(minX, minY, maxX - minX, maxY - minY);
		}

		public GOM_Point GetPointByName(string name)
		{
			return null;
		}
		public System.Drawing.Region BoundingRegion
		{
			get
			{
				return new System.Drawing.Region(this.BoundingBox);
			}
		}

		public GOM_Point GetMovablePointAt(int x, int y)
		{
			return null;
		}

		public GOM_Point GetConnectablePointAt(int x, int y)
		{
			return null;
		}

		public System.Drawing.PointF PointToObject(System.Drawing.PointF pt)
		{
			return pt;
		}

		public System.Drawing.PointF PointToCanvas(System.Drawing.PointF pt)
		{
			return pt;
		}

		public string ExportRecognitionResultToXML()
		{
			string	strokeXML;
			
			strokeXML = "<?xml version=\"1.0\"?><output><strokes candidatenum=\"1\">";
			strokeXML += "<candidate similarity=\"1\" strokenum=\"" + rgDrawings.Count.ToString() + "\">";

			for (int i = 0; i < rgDrawings.Count; i++)
			{
				if (rgDrawings[i] is GOM_Drawing_Line)
				{
					strokeXML += "<line>";
					strokeXML += "<startpt x=\"" + rgDrawings[i].Points[0].x.ToString() + "\" y=\"" + rgDrawings[i].Points[0].y.ToString() + "\"/>";
					strokeXML += "<endpt x=\"" + rgDrawings[i].Points[1].x.ToString() + "\" y=\"" + rgDrawings[i].Points[1].y.ToString() + "\"/>";
					strokeXML += "</line>";
				}

				if (rgDrawings[i] is GOM_Drawing_Arc)
				{
					strokeXML += "<arc rotate=\"" + ((GOM_Drawing_Arc)rgDrawings[i]).RotateAngle.ToString() + "\">";
					strokeXML += "<ltpt x=\"" + rgDrawings[i].Points[0].x.ToString() + "\" y=\"" + rgDrawings[i].Points[0].y.ToString() + "\"/>";
					strokeXML += "<rbpt x=\"" + rgDrawings[i].Points[1].x.ToString() + "\" y=\"" + rgDrawings[i].Points[1].y.ToString() + "\"/>";
					strokeXML += "<startpt x=\"" + rgDrawings[i].Points[2].x.ToString() + "\" y=\"" + rgDrawings[i].Points[2].y.ToString() + "\"/>";
					strokeXML += "<endpt x=\"" + rgDrawings[i].Points[3].x.ToString() + "\" y=\"" + rgDrawings[i].Points[3].y.ToString() + "\"/>";
					strokeXML += "</arc>";
				}
			}

			strokeXML +="</candidate></strokes></output>";

			return strokeXML;
		}

		public GOM_Interface_Graphic_Object Clone()
		{
			GOM_Object_Sketch	obj;

			obj = new GOM_Object_Sketch();

			System.Collections.ArrayList	rgStroke, rgStrokeNew;
			SketchPoint						pt1, pt2;

			for (int i = 0; i < rgSketchSet.Count; i++)
			{
				rgStroke = (System.Collections.ArrayList)rgSketchSet[i];
				rgStrokeNew = new System.Collections.ArrayList();

				for (int j = 0; j < rgStroke.Count; j++)
				{
					pt1 = (SketchPoint)rgStroke[j];
					pt2 = new SketchPoint();
					pt2.x = pt1.x;
					pt2.y = pt1.y;
					pt2.time = pt1.time;
					rgStrokeNew.Add(pt2);
				}

				obj.rgSketchSet.Add(rgStrokeNew);
			}

			return obj;
		}

		public void SaveToXML(System.Xml.XmlWriter writer)
		{
			writer.WriteStartElement(GOM_TAGS.GRAPHIC_OBJECT);
			writer.WriteAttributeString(GOM_TAGS.ID, this.id);
			writer.WriteAttributeString(GOM_TAGS.TYPE, "sketch");

			// sketchStrokes
			writer.WriteStartElement(GOM_TAGS.SKETCH_STROKES);
			for( int i=0; i<rgSketchSet.Count; i++)
			{
				writer.WriteStartElement(GOM_TAGS.SKETCH_STROKE);

				System.Collections.ArrayList sketchSet = (System.Collections.ArrayList)rgSketchSet[i];
				for( int j=0; j<sketchSet.Count; j++ )
				{
					((SketchPoint)sketchSet[j]).SaveToXML(writer);
				}

				writer.WriteEndElement();
			}
			writer.WriteEndElement();

			// points
			GOM_Points points = GetDrawingPoints();
			writer.WriteStartElement(GOM_TAGS.POINTS);
			for( int i=0; i<points.Count; i++ )
			{
				writer.WriteStartElement(GOM_TAGS.POINT);
				writer.WriteAttributeString(GOM_TAGS.ID, points[i].id);
				writer.WriteAttributeString(GOM_TAGS.X,  points[i].x.ToString());
				writer.WriteAttributeString(GOM_TAGS.Y,  points[i].y.ToString());
				writer.WriteEndElement();
			}
			writer.WriteEndElement();

			// drawings
			writer.WriteStartElement(GOM_TAGS.DRAWINGS);
			for( int i=0; i<rgDrawings.Count; i++ )
			{
				rgDrawings[i].SaveToXML(writer);
			}
			writer.WriteEndElement();

			writer.WriteEndElement();
		}

		public void LoadFromXML(System.Xml.XmlNode node, GOM_ResourceArrays resources)
		{
			// Basic properties
			for( int i=0; i<node.Attributes.Count; i++ )
			{
				if ( string.Compare(node.Attributes[i].Name, GOM_TAGS.ID, true) == 0 )
				{
					this.id = node.Attributes[i].Value;
				}
				if ( string.Compare(node.Attributes[i].Name, GOM_TAGS.TYPE, true) == 0 )
				{
					if ( string.Compare(node.Attributes[i].Value, "sketch", true) != 0 )
					{
						throw new Exception("Invalid input. Not a sketch object node!");
					}
				}
			}

			// 1st pass
			//   sketchStrokes
			//   points]
			GOM_Points points = null;
			for( int i=0; i<node.ChildNodes.Count; i++ )
			{
				if ( string.Compare(node.ChildNodes[i].Name, GOM_TAGS.SKETCH_STROKES, true) == 0 )
				{
					LoadSketchStrokesFromXML(node.ChildNodes[i]);
				}
				if ( string.Compare(node.ChildNodes[i].Name, GOM_TAGS.POINTS, true) == 0 )
				{
					points = LoadPointsFromXML(node.ChildNodes[i]);
				}
			}

			// 2nd pass
			//   drawings
			rgDrawings.Clear();
			if ( points != null )
			{
				GOM_Drawing_Styles styles = new GOM_Drawing_Styles();
				styles.Add(strokeStyle);
				GOM_ResourceArrays res = new GOM_ResourceArrays(points,styles,null);
				for( int i=0; i<node.ChildNodes.Count; i++ )
				{
					if ( string.Compare(node.ChildNodes[i].Name, GOM_TAGS.DRAWINGS, true) == 0 )
					{
						for ( int j=0; j<node.ChildNodes[i].ChildNodes.Count; j++ )
						{
							GOM_Utility.LoadDrawingFromXML(node.ChildNodes[i].ChildNodes[j], rgDrawings, res);
						}
					}
				}
			}

			CalculateBoundingBox();
		}

		private void LoadSketchStrokesFromXML(System.Xml.XmlNode node)
		{
			rgSketchSet.Clear();

			for( int i=0; i<node.ChildNodes.Count; i++ )
			{
				if ( string.Compare(node.ChildNodes[i].Name, GOM_TAGS.SKETCH_STROKE, true) == 0 )
				{
					System.Collections.ArrayList stroke = new System.Collections.ArrayList();

					System.Xml.XmlNodeList points = node.ChildNodes[i].ChildNodes;
					for ( int j=0; j<points.Count; j++ )
					{
						if ( string.Compare(points[j].Name, GOM_TAGS.SKETCH_POINT,true) == 0 )
						{
							SketchPoint pt = new SketchPoint();
							pt.LoadFromXML(points[j], null);
							stroke.Add(pt);
						}
					}

					if ( stroke.Count > 0 )
					{
						rgSketchSet.Add(stroke);
					}
				}
			}
		}

		private GOM_Points LoadPointsFromXML(System.Xml.XmlNode node)
		{
			GOM_Points points = new GOM_Points();
			for( int i=0; i<node.ChildNodes.Count; i++ )
			{
				if ( string.Compare(node.ChildNodes[i].Name,GOM_TAGS.POINT,true) == 0 )
				{
					GOM_Point pt = new GOM_Point();
					pt.LoadFromXML(node.ChildNodes[i], null);
					points.Add(pt);
				}
			}
			return points;
		}

		private GOM_Points GetDrawingPoints()
		{
			GOM_Points points = new GOM_Points();
			for( int i=0; i<rgDrawings.Count; i++ )
			{
				GOM_Interface_Drawing drawing = rgDrawings[i];
				for( int j=0; j<drawing.Points.Count; j++ )
				{
					if ( drawing.Points[j].id == null || drawing.Points[j].id.Length<= 0 )
					{
						drawing.Points[j].id = Guid.NewGuid().ToString("D");
					}

					if ( points[drawing.Points[j].id] == null )
					{
						points.Add( drawing.Points[j] );
					}
				}
			}
			return points;
		}

		private string	m_id;

		public System.Drawing.RectangleF	m_boundingBox;
		public System.Collections.ArrayList rgSketchSet;
		public System.Collections.ArrayList	rgStrokeToSketch;
		public GOM_Drawings					rgDrawings;
		public GOM_Style_Drawing			strokeStyle;
	}

	/// <summary>
	/// The image object
	/// </summary>
	public class GOM_Object_Image
	{
	}
	/// <summary>
	/// The embedded object
	/// </summary>
	public class GOM_Object_Embedded
	{
		public GOM_Object_Embedded()
		{
		}
	}
	
	public class GOM_Object_LinkNode : GOM_Interface_Graphic_Object
	{

		/// <summary>The bounding box of this object.</summary>
		private System.Drawing.RectangleF	m_boundingBox;
		/// <summary>The id of the primitive object</summary>
		private string				m_id;
		/// <summary>The x offset of the primitive object</summary>
		private float				m_xOffset;
		/// <summary>The y offset of the primitive object</summary>
		private float				m_yOffset;
		/// <summary>The angel of rotation of the primitive object</summary>
		private float				m_rotation;
		/// <summary>The only point in this object. It is connectable and movable.</summary>
		private GOM_Point			m_point;

		public GOM_Object_LinkNode()
		{
			m_boundingBox	= new System.Drawing.RectangleF(0, 0, 0, 0);
			m_id			= Guid.NewGuid().ToString("D");;
			m_xOffset		= 0;
			m_yOffset		= 0;
			m_rotation		= 0;

			m_point			= new GOM_Point();
			m_point.id		= GOM_Special_Point_Name.LINKNODE_POINT;
			m_point.Connectable = true;
			m_point.Controllable = false;
		}

		public GOM_Object_LinkNode( float x, float y ) : this()
		{
			m_xOffset = x;
			m_yOffset = y;
			CalculateBoundingBox();
		}

		public GOM_Point LinkPoint
		{
			get { return m_point; }
		}

		public string id
		{
			get { return m_id; }
			set { m_id = value; }
		}

		public GOMLib.GraphicObjectType type
		{
			get { return GraphicObjectType.LinkNode; }
		}

		public bool KeepAspectRatio
		{
			get { return true; }
			set {}
		}

		public float xOffset
		{
			get { return m_xOffset; }
			set { m_xOffset = value; }
		}

		public float yOffset
		{
			get { return m_yOffset; }
			set { m_yOffset = value; }
		}

		public float width
		{
			get 
			{
				return this.BoundingBox.Width;
			}
			set
			{
				// The width cannot be set.
			}
		}

		public float height
		{
			get
			{
				return this.BoundingBox.Height;
			}
			set
			{
				// The height cannot be set.
			}
		}

		public float rotation
		{
			get { return m_rotation; }
			set { m_rotation = value; }
		}

		public void Draw(System.Drawing.Graphics canvas, bool ShowConnectPoint)
		{
			System.Drawing.Drawing2D.Matrix	orgTrans	= canvas.Transform;
			System.Drawing.RectangleF		rc			= this.BoundingBox;

			canvas.TranslateTransform(this.xOffset, this.yOffset);
			canvas.TranslateTransform((rc.Left + rc.Right) / 2, (rc.Top + rc.Bottom) / 2);
			canvas.RotateTransform(this.rotation);
			canvas.TranslateTransform(-(rc.Left + rc.Right) / 2, -(rc.Top + rc.Bottom) / 2);

			System.Drawing.Brush brush = new System.Drawing.SolidBrush(System.Drawing.Color.Gray);
			canvas.FillEllipse(brush, m_point.x - 3, m_point.y - 3, 6, 6);

			canvas.Transform = orgTrans;
		}

		public void DrawSelected(System.Drawing.Graphics canvas, System.Drawing.Pen BoundingBoxPen, System.Drawing.Pen ResizeBoxPen, System.Drawing.Pen ConnectBoxPen, System.Drawing.Pen ControlBoxPen)
		{
			System.Drawing.Drawing2D.Matrix	orgTrans	= canvas.Transform;
			System.Drawing.RectangleF		rc			= this.BoundingBox;

			canvas.TranslateTransform(this.xOffset, this.yOffset);
			canvas.TranslateTransform((rc.Left + rc.Right) / 2, (rc.Top + rc.Bottom) / 2);
			canvas.RotateTransform(this.rotation);
			canvas.TranslateTransform(-(rc.Left + rc.Right) / 2, -(rc.Top + rc.Bottom) / 2);

			System.Drawing.Brush brush = new System.Drawing.SolidBrush(System.Drawing.Color.Blue);
			canvas.FillEllipse(brush, m_point.x - 3, m_point.y - 3, 6, 6);

			canvas.Transform = orgTrans;
		}

		public bool IsPointInObject(int x, int y)
		{
			System.Drawing.RectangleF	rc;
			System.Drawing.PointF		pt;

			rc = this.BoundingBox;
			pt = PointToObject(new System.Drawing.PointF(x, y));

//			if ((rc.Left <= pt.X) && (pt.X <= rc.Right) && (rc.Top <= pt.Y) && (pt.Y <= rc.Bottom))
			if ((-3 <= pt.X) && (pt.X <= 3) && (-3 <= pt.Y) && (pt.Y <= 3))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public System.Drawing.RectangleF BoundingBox
		{
			get
			{
				return m_boundingBox;
			}
		}

		public System.Drawing.Region BoundingRegion
		{
			get
			{
				System.Drawing.Drawing2D.Matrix	matrix;
				System.Drawing.RectangleF	rc;
				System.Drawing.Region		rgn;

				rc = this.BoundingBox;
				rgn = new System.Drawing.Region(rc);

				matrix = new System.Drawing.Drawing2D.Matrix();
				matrix.Translate(-(rc.Left + rc.Right) / 2, -(rc.Top + rc.Bottom) / 2);
				rgn.Transform(matrix);

				matrix.Reset();
				matrix.Rotate(this.rotation);
				rgn.Transform(matrix);

				matrix.Reset();
				matrix.Translate(this.xOffset, this.yOffset);
				rgn.Transform(matrix);

				matrix.Reset();
				matrix.Translate((rc.Left + rc.Right) / 2, (rc.Top + rc.Bottom) / 2);
				rgn.Transform(matrix);

				return rgn;
			}
		}

		public GOM_Point GetMovablePointAt(int x, int y)
		{
			System.Drawing.RectangleF	rc;
			System.Drawing.PointF		pt;

			rc = this.BoundingBox;
			pt = PointToObject(new System.Drawing.PointF(x, y));

			if (GOM_Default_Values.IsMouseOnPoint(m_point.x, m_point.y, pt.X, pt.Y))
			{
				return m_point;
			}
			return null;
		}

		public GOM_Point GetConnectablePointAt(int x, int y)
		{
			System.Drawing.RectangleF	rc;
			System.Drawing.PointF		pt;

			rc = this.BoundingBox;
			pt = PointToObject(new System.Drawing.PointF(x, y));

			if (GOM_Default_Values.IsMouseOnPoint(m_point.x, m_point.y, pt.X, pt.Y))
			{
				return m_point;
			}
			return null;
		}

		public System.Drawing.PointF PointToObject(System.Drawing.PointF pt)
		{
			System.Drawing.Drawing2D.Matrix	matrix;
			System.Drawing.RectangleF		rc;
			System.Drawing.PointF[]			rgPts;

			rc = this.BoundingBox;

			rgPts = new System.Drawing.PointF[1];
			rgPts[0].X = pt.X;
			rgPts[0].Y = pt.Y;

			matrix = new System.Drawing.Drawing2D.Matrix();
			matrix.Translate(-this.xOffset, -this.yOffset);
			matrix.TransformPoints(rgPts);

			matrix.Reset();
			matrix.Translate(-(rc.Left + rc.Right) / 2, -(rc.Top + rc.Bottom) / 2);
			matrix.TransformPoints(rgPts);

			matrix.Reset();
			matrix.Rotate(-this.rotation);
			matrix.TransformPoints(rgPts);

			matrix.Reset();
			matrix.Translate((rc.Left + rc.Right) / 2, (rc.Top + rc.Bottom) / 2);
			matrix.TransformPoints(rgPts);

			return rgPts[0];
		}

		public System.Drawing.PointF PointToCanvas(System.Drawing.PointF pt)
		{
			System.Drawing.Drawing2D.Matrix	matrix;
			System.Drawing.RectangleF		rc;
			System.Drawing.PointF[]			rgPts;

			rc = this.BoundingBox;

			rgPts = new System.Drawing.PointF[1];
			rgPts[0].X = pt.X;
			rgPts[0].Y = pt.Y;

			matrix = new System.Drawing.Drawing2D.Matrix();
			matrix.Translate(-(rc.Left + rc.Right) / 2, -(rc.Top + rc.Bottom) / 2);
			matrix.TransformPoints(rgPts);

			matrix.Reset();
			matrix.Rotate(this.rotation);
			matrix.TransformPoints(rgPts);

			matrix.Reset();
			matrix.Translate((rc.Left + rc.Right) / 2, (rc.Top + rc.Bottom) / 2);
			matrix.TransformPoints(rgPts);

			matrix.Reset();
			matrix.Translate(this.xOffset, this.yOffset);
			matrix.TransformPoints(rgPts);

			return rgPts[0];
		}

		public GOM_Interface_Graphic_Object Clone()
		{
			System.Diagnostics.Debug.Assert(true, "LinkNode object cannot be cloned.");
			return null;
		}

		public void CalculateBoundingBox()
		{
			m_boundingBox = new System.Drawing.RectangleF(m_point.x-3, m_point.y-3, 6, 6);
//			m_boundingBox = new System.Drawing.RectangleF(m_xOffset-3, m_yOffset-3, 6, 6);
		}

		public GOM_Point GetPointByName(string name)
		{
			if ( string.Compare(name, m_point.id, true) == 0 )
			{
				return m_point;
			}
			return null;
		}

		public void SaveToXML(System.Xml.XmlWriter writer)
		{
			writer.WriteStartElement(GOM_TAGS.GRAPHIC_OBJECT);

			writer.WriteAttributeString(GOM_TAGS.ID, this.id);
			writer.WriteAttributeString(GOM_TAGS.TYPE, "linkNode");
			writer.WriteAttributeString(GOM_TAGS.X_OFFSET, this.xOffset.ToString());
			writer.WriteAttributeString(GOM_TAGS.Y_OFFSET, this.yOffset.ToString());
			writer.WriteAttributeString(GOM_TAGS.ROTATION, this.rotation.ToString());

			// m_point
			writer.WriteStartElement(GOM_TAGS.POINTS);
			writer.WriteStartElement(GOM_TAGS.POINT);
			writer.WriteAttributeString(GOM_TAGS.ID, this.m_point.id);
			writer.WriteAttributeString(GOM_TAGS.X, this.m_point.x.ToString());
			writer.WriteAttributeString(GOM_TAGS.Y, this.m_point.y.ToString());
			writer.WriteEndElement();
			writer.WriteEndElement();

			writer.WriteEndElement();
		}

		public void LoadFromXML(System.Xml.XmlNode node, GOM_ResourceArrays resources)
		{
			GOM_Utility.VerifyXmlNode(node, GOM_TAGS.GRAPHIC_OBJECT);

			//Load properties of the graphic object
			for (int i = 0; i < node.Attributes.Count; i++)
			{
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.ID, true) == 0)
				{
					id = node.Attributes[i].Value;
				}
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.TYPE, true) == 0)
				{
					if (!node.Attributes[i].Value.Equals("linkNode"))
					{
						throw new Exception("Invalid input. Not a primitive object node!");
					}
				}
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.X_OFFSET, true) == 0)
				{
					m_xOffset = float.Parse(node.Attributes[i].Value);
				}
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.Y_OFFSET, true) == 0)
				{
					m_yOffset = float.Parse(node.Attributes[i].Value);
				}
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.ROTATION, true) == 0)
				{
					m_rotation = float.Parse(node.Attributes[i].Value);
				}
			}

			//Load m_point
			for (int i = 0; i < node.ChildNodes.Count; i++)
			{
				if (System.String.Compare(node.ChildNodes[i].Name, GOM_TAGS.POINTS, true) == 0)
				{
					for ( int j=0; j<node.ChildNodes[i].ChildNodes.Count; j++ )
					{
						m_point.LoadFromXML(node.ChildNodes[i].ChildNodes[j], resources);
						// Jump out of the loop, since there is only one point.
						break;
					}
				}
			}

			CalculateBoundingBox();
		}

	}


	/// <summary>
	/// A dynamic list of interface of graphic object 
	/// </summary>
	public class GOM_Objects
	{
		/// <summary>
		/// The constructor of GOM_Objects
		/// </summary>
		public GOM_Objects()
		{
			rgObject = new System.Collections.ArrayList();
		}
		/// <summary>
		/// Return the graphic object by its index
		/// </summary>
		public GOM_Interface_Graphic_Object this[int idx]
		{
			get
			{
				return (GOM_Interface_Graphic_Object)rgObject[idx];
			}
			set
			{
				rgObject[idx] = value;
			}
		}
		/// <summary>
		/// Return an object by its name.
		/// </summary>
		public GOM_Interface_Graphic_Object this[string name]
		{
			get
			{
				for (int i = 0; i < rgObject.Count; i++)
				{
					if ( ((GOM_Interface_Graphic_Object)rgObject[i]).id.Equals(name) )
					{
						return (GOM_Interface_Graphic_Object)rgObject[i];
					}
				}
				return null;
			}
		}
		/// <summary>
		/// Number of graphic object stored in the list
		/// </summary>
		public int Count
		{
			get
			{
				return rgObject.Count;
			}
		}
		/// <summary>
		/// Add a graphic object into the list
		/// </summary>
		/// <param name="val">The graphic object being stored</param>
		public void Add(GOM_Interface_Graphic_Object val)
		{
			rgObject.Add(val);
		}
		public void Insert(GOM_Interface_Graphic_Object val, int idx)
		{
			rgObject.Insert(idx, val);
		}
		/// <summary>
		/// Remove a graphic object by its index
		/// </summary>
		/// <param name="idx">The index of the graphic object</param>
		public void RemoveAt(int idx)
		{
			rgObject.RemoveAt(idx);
		}
		public void Remove(GOM_Interface_Graphic_Object val)
		{
			rgObject.Remove(val);
		}
		/// <summary>
		/// Empty this list
		/// </summary>
		public void Clear()
		{
			rgObject.Clear();
		}
		public bool Contains(GOM_Interface_Graphic_Object val)
		{
			return rgObject.Contains(val);
		}
		/// <summary>
		/// Dynamic array of graphic object
		/// </summary>
		private System.Collections.ArrayList rgObject;
	}
}