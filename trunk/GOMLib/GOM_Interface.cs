using System;

namespace GOMLib
{
	public class GOM_ResourceArrays
	{
		public GOM_ResourceArrays()
		{
		}
		public GOM_ResourceArrays( GOM_Points points ) : this(points, null, null, null)
		{
		}
		public GOM_ResourceArrays( GOM_Objects objects ) : this(null, null, null, objects)
		{
		}
		public GOM_ResourceArrays( GOM_Templates templates )
		{
			m_templates = templates;
		}
		public GOM_ResourceArrays( GOM_Points points, GOM_Drawing_Styles drawingStyles, GOM_Filling_Styles fillingStyles ) : this(points, drawingStyles, fillingStyles, null)
		{
		}
		public GOM_ResourceArrays( GOM_Points points, GOM_Drawing_Styles drawingStyles, GOM_Filling_Styles fillingStyles, GOM_Objects objects )
		{
			m_points = points;
			m_drawingStyles = drawingStyles;
			m_fillingStyles = fillingStyles;
			m_objects = objects;
		}

		private GOM_Points			m_points = null;
		private GOM_Drawing_Styles	m_drawingStyles = null;
		private GOM_Filling_Styles	m_fillingStyles = null;
		private GOM_Objects			m_objects = null;
		private GOM_Templates		m_templates = null;

		public GOM_Points Points
		{
			get
			{
				return m_points;
			}
		}
		public GOM_Drawing_Styles DrawingStyles
		{
			get
			{
				return m_drawingStyles;
			}
		}
		public GOM_Filling_Styles FillingStyles
		{
			get
			{
				return m_fillingStyles;
			}
		}
		public GOM_Objects Objects
		{
			get
			{
				return m_objects;
			}
		}
		public GOM_Templates Templates
		{
			get
			{
				return m_templates;
			}
		}

	}

	/// <summary>
	/// The interface definition for xml persistence.
	/// Every class which should be saved to and loaded from xml file must implement this interface.
	/// </summary>
	public interface GOM_Interface_XmlPersistence
	{
		/// <summary>
		/// Save to XML.
		/// </summary>
		/// <param name="writer">The XML writer.</param>
		void SaveToXML(System.Xml.XmlWriter writer);
		/// <summary>
		/// Load from XML.
		/// </summary>
		/// <param name="node">The XML node.</param>
		void LoadFromXML(System.Xml.XmlNode node, GOM_ResourceArrays resources);
	}

	/// <summary>
	/// The interface definition for value type
	/// </summary>
	/// <remarks>Each node in a matchematic expression tree should support this interface.</remarks>
	public interface GOM_Interface_Value : GOM_Interface_XmlPersistence
	{
		/// <summary>
		/// The value of this node, which is calculated by the operator in this node and values of its child node. 
		/// </summary>
		float Value
		{
			get;
			set;
		}
		/// <summary>
		/// The type of this node, which is an enumerative value.
		/// </summary>
		ValueType Type
		{
			get;
		}
		/// <summary>
		/// The human readable description of the type of this node.
		/// </summary>
		string TypeDescription
		{
			get;
		}
		/// <summary>
		/// The human readable description of the value.
		/// </summary>
		string ValueDescription
		{
			get;
		}
		/// <summary>
		/// All child nodes in this node
		/// </summary>
		GOM_Values values
		{
			get;
		}
	}
	/// <summary>
	/// The interface definition for constraint
	/// </summary>
	/// <remarks>Each constraint must support this interface.</remarks>
	public interface GOM_Interface_Constraint : GOM_Interface_XmlPersistence
	{
		/// <summary>
		/// Apply this constraint.
		/// </summary>
		/// <remarks>Called when some changes related to this constraint happens.</remarks>
		void ApplyConstraint();
		/// <summary>
		/// The human readable description of this constraint.
		/// </summary>
		/// <returns>The description of this constraint</returns>
		string ConstraintDescription();
		/// <summary>
		/// All child nodes in this constraint
		/// </summary>
		GOM_Values values
		{
			get;
		}
	}
	/// <summary>
	/// The interface definition for drawing operation
	/// </summary>
	public interface GOM_Interface_Drawing : GOM_Interface_XmlPersistence
	{
		/// <summary>
		/// Draw the primitive shape on the canvas
		/// </summary>
		/// <param name="canvas">The canvas on which the primitive shape is drawn</param>
		void Draw(System.Drawing.Graphics canvas);
		/// <summary>
		/// The drawing style of this drawing operation
		/// </summary>
		GOM_Style_Drawing Style
		{
			get;
			set;
		}
		/// <summary>
		/// List of points used in this drawing operation
		/// </summary>
		GOM_Points Points
		{
			get;
		}
		/// <summary>
		/// Type of this drawing operation
		/// </summary>
		DrawingType	Type
		{
			get;
		}
		/// <summary>
		/// Return the bounding box of this drawing operation
		/// </summary>
		System.Drawing.RectangleF BoundingBox
		{
			get;
		}
	}
	/// <summary>
	/// The interface definition for the filling operation
	/// </summary>
	public interface GOM_Interface_Filling : GOM_Interface_XmlPersistence
	{
		/// <summary>
		/// Fill an area on the canvas
		/// </summary>
		/// <param name="canvas">The canvas in which an area will be filled</param>
		void Fill(System.Drawing.Graphics canvas);
		/// <summary>
		/// The filling style of this filling operation
		/// </summary>
		GOM_Style_Filling Style
		{
			get;
			set;
		}
		/// <summary>
		/// The points used to define the area of this filling operation
		/// </summary>
		GOM_Points Points
		{
			get;
		}	
		/// <summary>
		/// The type of the filling operation
		/// </summary>
		FillingType Type
		{
			get;
		}
		/// <summary>
		/// Return the bounding box of this filling operation
		/// </summary>
		System.Drawing.RectangleF BoundingBox
		{
			get;
		}
	}
	/// <summary>
	/// The interface definition for a graphic object
	/// </summary>
	public interface GOM_Interface_Graphic_Object : GOM_Interface_XmlPersistence
	{	
		/// <summary>
		/// ID of the graphic object
		/// </summary>
		string id
		{
			get;
			set;
		}
		/// <summary>
		/// Type of the graphic object
		/// </summary>
		GraphicObjectType type
		{
			get;
		}
		/// <summary>
		/// Whether the graphic object should keep its aspect ratio on resizing
		/// </summary>
		bool KeepAspectRatio
		{
			get;
			set;
		}
		/// <summary>
		/// X offset of the graphic object
		/// </summary>
		float xOffset
		{
			get;
			set;
		}
		/// <summary>
		/// Y offset of the graphic object
		/// </summary>
		float yOffset
		{
			get;
			set;
		}
		/// <summary>
		/// Width of the graphic object
		/// </summary>
		float width
		{
			get;
			set;
		}
		/// <summary>
		/// Height of the graphic object
		/// </summary>
		float height
		{
			get;
			set;
		}
		/// <summary>
		/// Angel of rotation of the graphic object
		/// </summary>
		float rotation
		{
			get;
			set;
		}
		/// <summary>
		/// Draw the graphic object on the canvas according to current offset and rotation
		/// </summary>
		/// <param name="canvas">The canvas on which the graphic object is drawn</param>
		void Draw(System.Drawing.Graphics canvas, bool ShowConnectPoint);
		/// <summary>
		/// Draw the graphic object when selected
		/// </summary>
		/// <param name="canvas">The canvas on which the graphic object is drawn</param>
		/// <param name="BoundingBoxPen">Pen to draw the bounding box</param>
		/// <param name="ResizeBoxPen">Pen to draw the resize box</param>
		/// <param name="ConnectBoxPen">Pen to draw the connect box</param>
		/// <param name="ControlBoxPen">Pen to draw the control box</param>
		void DrawSelected(System.Drawing.Graphics canvas, System.Drawing.Pen BoundingBoxPen, System.Drawing.Pen ResizeBoxPen, System.Drawing.Pen ConnectBoxPen, System.Drawing.Pen ControlBoxPen);
		/// <summary>
		/// Judge whether a point is inside the graphic object
		/// </summary>
		/// <param name="x">The x coordinate of the object</param>
		/// <param name="y">The y coordinate of the object</param>
		/// <returns>Return true is the point is inside the graphic object. Otherwise, false is returned</returns>
		bool IsPointInObject(int x, int y);
		/// <summary>
		/// Return the bounding box of the graphic object in local coordinate
		/// </summary>
		System.Drawing.RectangleF BoundingBox
		{
			get;
		}
		/// <summary>
		/// Return the bounding region of the graphic object in global coordinate
		/// </summary>
		System.Drawing.Region BoundingRegion
		{
			get;
		}
		/// <summary>
		/// Get a movable point according to the given coordinates
		/// </summary>
		/// <param name="x">The x coordinate</param>
		/// <param name="y">The y coordinate</param>
		/// <returns>Return the movable point if found, otherwise null is returned</returns>
		GOM_Point GetMovablePointAt(int x, int y);
		/// <summary>
		/// Get a connectable point according to the given coordinates
		/// </summary>
		/// <param name="x">The x coordinate</param>
		/// <param name="y">The y coordinate</param>
		/// <returns>Return the movable point if found, otherwise null is returned</returns>
		GOM_Point GetConnectablePointAt(int x, int y);
		/// <summary>
		/// Convert a point in gloabl coordinate into a point in local coordinate
		/// </summary>
		/// <param name="pt">The point in gloabl coordinate</param>
		/// <returns>The point in local coordinate</returns>
		System.Drawing.PointF PointToObject(System.Drawing.PointF pt);
		/// <summary>
		/// Convert a point in local coordinate into a point in global coordinate
		/// </summary>
		/// <param name="pt">The point in local coordinate</param>
		/// <returns>The point in global coordinate</returns>
		System.Drawing.PointF PointToCanvas(System.Drawing.PointF pt);
		/// <summary>
		/// Create a copy of current graphic object
		/// </summary>
		/// <returns></returns>
		GOM_Interface_Graphic_Object Clone();
		void CalculateBoundingBox();

		GOM_Point GetPointByName(string name);
	}
}