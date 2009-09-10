using System;

namespace GOMLib
{
	/// <summary>
	/// Enumerative value for value types
	/// </summary>
	public enum ValueType 
	{
		/// <summary> Unknown value type </summary>
		Error_Value, 
		/// <summary> Numeric value type </summary>
		Num_Value, 
		/// <summary> X coordinate of a point </summary>
		X_Value, 
		/// <summary> Y coordinate of a point </summary>
		Y_Value, 
		/// <summary> Sin value of a number </summary>
		Sin_Value, 
		/// <summary> Cos value of a number </summary>
		Cos_Value, 
		/// <summary> Tan value of a number </summary>
		Tan_Value, 
		/// <summary> Square root of a number </summary>
		Sqrt_Value, 
		/// <summary> Negative value of a number </summary>
		Neg_Value, 
		/// <summary> Sum of two numbers </summary>
		Add_Value, 
		/// <summary> Difference of two numbers </summary>
		Minus_Value, 
		/// <summary> Quotient of two numbers </summary>
		Divide_Value, 
		/// <summary> Product of two numbers </summary>
		Multiply_Value, 
		/// <summary> Max value of two numbers </summary>
		Max_Value, 
		/// <summary> Min value of two numbers </summary>
		Min_Value, 
		/// <summary> An assignment expression</summary>
		Assign_Value};
	/// <summary>
	/// Enumerative value for drawing types
	/// </summary>
	public enum DrawingType {Line, Arc, Bezier};
	/// <summary>
	/// Enumerative value for filling types
	/// </summary>
	public enum FillingType {Ellipse, Rectangle, Pie, Polygon, Path};
	/// <summary>
	/// Enumerative value for graphic object types
	/// </summary>
	public enum GraphicObjectType {Primitive, Group, Image, Embedded, Sketch, LinkNode };
	/// <summary>
	/// Name of some special points
	/// </summary>
	public class GOM_Special_Point_Name
	{
		public static string SE_RESIZING_POINT	= "SE_Resize_Point";
		public static string SW_RESIZING_POINT	= "SW_Resize_Point";
		public static string NE_RESIZING_POINT	= "NE_Resize_Point";
		public static string NW_RESIZING_POINT	= "NW_Resize_Point";
		public static string ROTATION_POINT		= "Rotation_Point";
		public static string TOP_CONNECTOR		= "Top_Connector";
		public static string LEFT_CONNECTOR		= "Left_Connector";
		public static string RIGHT_CONNECTOR	= "Right_Connector";
		public static string BOTTOM_CONNECTOR	= "Bottom_Connector";
		public static string LINKNODE_POINT		= "LinkNode_Point";
	}
	/// <summary>
	/// All tags used in the GOM (Graphic Object Model) specification
	/// </summary>
	public class GOM_TAGS
	{
		/// <summary>graphicTemplate</summary>
		public static string GRAPHIC_TEMPLATE	= "graphicTemplate";
		/// <summary>graphicObject</summary>
		public static string GRAPHIC_OBJECT		= "graphicObject";
		/// <summary>x</summary>
		public static string X					= "x";
		/// <summary>y</summary>
		public static string Y					= "y";
		/// <summary>id</summary>
		public static string ID					= "id";
		/// <summary>true</summary>
		public static string TRUE				= "true";
		/// <summary>false</summary>
		public static string FALSE				= "false";
		/// <summary>extScale</summary>
		public static string EXTERNAL_SCALE		= "extScale";
		/// <summary>extRotate</summary>
		public static string EXTERNAL_ROTATE	= "extRotate";
		/// <summary>extConnect</summary>
		public static string EXTERNAL_CONNECT	= "extConnect";
		/// <summary>keepAspectRatio</summary>
		public static string KEEP_ASPECT_RATIO	= "keepAspectRatio";
		/// <summary>point</summary>
		public static string POINT				= "point";
		/// <summary>points</summary>
		public static string POINTS				= "points";
		/// <summary>key points</summary>
		public static string KEYPOINTS			= "keyPoints";
		/// <summary>drawings</summary>
		public static string DRAWINGS			= "drawings";
		/// <summary>fillings</summary>
		public static string FILLINGS			= "fillings";
		/// <summary>red</summary>
		public static string RED				= "red";
		/// <summary>gree</summary>
		public static string GREEN				= "green";
		/// <summary>blue</summary>
		public static string BLUE				= "blue";
		/// <summary>style</summary>
		public static string STYLE				= "style";
		/// <summary>styles</summary>
		public static string STYLES				= "styles";
		/// <summary>drawingStyle</summary>
		public static string DRAWING_STYLE		= "drawingStyle";
		/// <summary>fillingStyle</summary>
		public static string FILLING_STYLE		= "fillingStyle";
		/// <summary>editingMode</summary>
		public static string EDITING_MODE		= "editingMode";
		/// <summary>editingModes</summary>
		public static string EDITING_MODES		= "editingModes";
		/// <summary>controllable</summary>
		public static string CONTROLLABLE		= "controllable";
		/// <summary>connectable</summary>
		public static string CONNECTABLE		= "connectable";
		/// <summary>onPositionChange</summary>
		public static string ON_POSITION_CHANGE	= "onPositionChange";
		/// <summary>line</summary>
		public static string LINE				= "line";
		/// <summary>arc</summary>
		public static string ARC				= "arc";
		/// <summary>bezier</summary>
		public static string BEZIER				= "bezier";
		/// <summary>vertex</summary>
		public static string VERTEX				= "vertex";
		/// <summary>startPoint</summary>
		public static string START_POINT		= "startPoint";
		/// <summary>endPoint</summary>
		public static string END_POINT			= "endPoint";
		/// <summary>leftTop</summary>
		public static string LEFT_TOP			= "leftTop";
		/// <summary>rightDown</summary>
		public static string RIGHT_DOWN			= "rightDown";
		/// <summary>ellipse</summary>
		public static string ELLIPSE			= "ellipse";
		/// <summary>rectangle</summary>
		public static string RECTANGLE			= "rectangle";
		/// <summary>pie</summary>
		public static string PIE				= "pie";
		/// <summary>polygon</summary>
		public static string POLYGON			= "polygon";
		/// <summary>fillPath</summary>
		public static string FILLPATH			= "fillPath";
		/// <summary>assign</summary>
		public static string ASSIGN				= "assign";
		/// <summary>value</summary>
		public static string VALUE				= "value";
		/// <summary>lvalue</summary>
		public static string L_VALUE			= "lvalue";
		/// <summary>rvalue</summary>
		public static string R_VALUE			= "rvalue";
		/// <summary>xvalue</summary>
		public static string X_VALUE			= "xvalue";
		/// <summary>yvalue</summary>
		public static string Y_VALUE			= "yvalue";
		/// <summary>sin</summary>
		public static string SIN				= "sin";
		/// <summary>cos</summary>
		public static string COS				= "cos";
		/// <summary>tan</summary>
		public static string TAN				= "tan";
		/// <summary>sqrt</summary>
		public static string SQRT				= "sqrt";
		/// <summary>neg</summary>
		public static string NEGATIVE			= "neg";
		/// <summary>add</summary>
		public static string ADD				= "add";
		/// <summary>minus</summary>
		public static string MINUS				= "minus";
		/// <summary>multiply</summary>
		public static string MULTIPLY			= "multiply";
		/// <summary>divide</summary>
		public static string DIVIDE				= "divide";
		/// <summary>max</summary>
		public static string MAX				= "max";
		/// <summary>min</summary>
		public static string MIN				= "min";
		/// <summary>type</summary>
		public static string TYPE				= "type";
		/// <summary>xoffset</summary>
		public static string X_OFFSET			= "xoffset";
		/// <summary>yoffset</summary>
		public static string Y_OFFSET			= "yoffset";
		/// <summary>width</summary>
		public static string WIDTH				= "width";
		/// <summary>height</summary>
		public static string HEIGHT				= "height";
		/// <summary>rotation</summary>
		public static string ROTATION			= "rotation";
		/// <summary>template</summary>
		public static string TEMPLATE			= "template";
		/// <summary>templates</summary>
		public static string TEMPLATES			= "templates";
		/// <summary>text</summary>
		public static string TEXT				= "text";
		/// <summary>content</summary>
		public static string CONTENT			= "content";
		/// <summary>font</summary>
		public static string FONT				= "font";
		/// <summary>layout</summary>
		public static string LAYOUT				= "layout";
		/// <summary>family</summary>
		public static string FAMILY				= "family";
		/// <summary>size</summary>
		public static string SIZE				= "size";
		/// <summary>direction</summary>
		public static string DIRECTION			= "direction";
		/// <summary>align</summary>
		public static string ALIGN				= "align";
		/// <summary>bold</summary>
		public static string BOLD				= "bold";
		/// <summary>italic</summary>
		public static string ITALIC				= "italic";
		/// <summary>strikeout</summary>
		public static string STRIKEOUT			= "strikeout";
		/// <summary>underline</summary>
		public static string UNDERLINE			= "underline";
		/// <summary>diagram</summary>
		public static string DIAGRAM			= "diagram";
		/// <summary>objects</summary>
		public static string OBJECTS			= "objects";
		/// <summary>connection</summary>
		public static string CONNECTION			= "connection";
		/// <summary>links</summary>
		public static string CONNECTIONS		= "connections";
		/// <summary>objectID</summary>
		public static string OBJECTID			= "objectID";
		/// <summary>pointID</summary>
		public static string POINTID			= "pointID";
		/// <summary>linkingStyle</summary>
		public static string LINKING_STYLE		= "linkingStyle";
		/// <summary>terminalStyle</summary>
		public static string TERMINAL_STYLE		= "terminalStyle";
		/// <summary>sketchPoint</summary>
		public static string SKETCH_POINT		= "sketchPoint";
		/// <summary>sketchStroke</summary>
		public static string SKETCH_STROKE		= "sketchStroke";
		/// <summary>sketchStrokes</summary>
		public static string SKETCH_STROKES		= "sketchStrokes";
		/// <summary>time</summary>
		public static string TIME				= "time";
	}

	public class GOM_Default_Values
	{
		public static int	Mouse_Sensitive_Area	= 8;
		public static int	Default_Object_Size		= 100;

		public static bool IsMouseOnPoint(float ptX, float ptY, float mouseX, float mouseY)
		{
			return ((ptX - Mouse_Sensitive_Area < mouseX) && (mouseX < ptX + Mouse_Sensitive_Area) &&
					(ptY - Mouse_Sensitive_Area < mouseY) && (mouseY < ptY + Mouse_Sensitive_Area));
		}

		public static bool IsPointOnLink(GOM_Point pt, GOM_Link link, GOM_Objects rgObjs)
		{
			return link.IsPointOnLink(pt.x, pt.y, rgObjs);
		}

		public static GOM_Link CreateLink(GOM_Interface_Graphic_Object startObj, GOM_Point startPt, GOM_Interface_Graphic_Object endObj, GOM_Point endPt)
		{
			return CreateLink(startObj, startPt, endObj, endPt, null);
		}

		public static GOM_Link CreateLink(GOM_Interface_Graphic_Object startObj, GOM_Point startPt, GOM_Interface_Graphic_Object endObj, GOM_Point endPt, GOM_Points keyPoints)
		{
			GOM_Style_Drawing drawing = new GOMLib.GOM_Style_Drawing();
			drawing.drawingStyle.Color = System.Drawing.Color.Gray;
			GOM_Link link = new GOMLib.GOM_Link(startObj, startPt,
				endObj, endPt,
				drawing,
				GOMLib.GOM_Linking_Style.Line,
				GOMLib.GOM_Terminal_Style.None,
				GOMLib.GOM_Terminal_Style.None);
			if ( keyPoints!=null && keyPoints.Count>0 )
			{
				link.m_linkingStyle = GOMLib.GOM_Linking_Style.Polyline;
				link.m_keyPts = keyPoints;
			}
			return link;
		}

		public static void ScaleObject(GOM_Interface_Graphic_Object obj, float maxWidth, float maxHeight)
		{
			float	ratio;

			ratio = System.Math.Min(maxWidth / obj.width, maxHeight / obj.height);

			obj.width		*= ratio;
			obj.height	*= ratio;
		}

	}
}
