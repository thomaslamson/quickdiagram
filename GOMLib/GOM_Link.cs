using System;

namespace GOMLib
{
	/// <summary>
	/// Summary description for GOM_Link.
	/// </summary>
	public class GOM_Link : GOM_Interface_XmlPersistence
	{
		public GOM_Link(System.Xml.XmlNode node, GOM_ResourceArrays resources)
		{
			m_startStyle	= GOMLib.GOM_Terminal_Style.None;
			m_endStyle		= GOMLib.GOM_Terminal_Style.Triangle;
			m_drawingStyle	= new GOMLib.GOM_Style_Drawing();
			m_linkingStyle	= GOM_Linking_Style.Line;
			m_keyPts		= new GOM_Points();

			m_drawingStyle.id = "default";

			LoadFromXML(node, resources);
		}
		public GOM_Link(GOM_Interface_Graphic_Object startObj, GOM_Point startPt, GOM_Interface_Graphic_Object endObj, GOM_Point endPt, GOM_Style_Drawing drawingStyle, GOM_Linking_Style linkingStyle, GOM_Terminal_Style startStyle, GOM_Terminal_Style endStyle)
		{
			m_startObj		= startObj;
			m_endObj		= endObj;
			m_startPt		= startPt;
			m_endPt			= endPt;
			m_startStyle	= startStyle;
			m_endStyle		= endStyle;
			m_drawingStyle	= drawingStyle;
			m_linkingStyle	= linkingStyle;
			m_keyPts		= new GOM_Points();

			m_drawingStyle.id = "default";
		}

		/// <summary>
		/// Draw unselected link.
		/// </summary>
		/// <param name="canvas">Canvas which link is drawn on.</param>
		/// <param name="rgObjs">GOM object collection.</param>
		public void Draw(System.Drawing.Graphics canvas, GOM_Objects rgObjs)
		{
			switch (m_linkingStyle)
			{
				case GOM_Linking_Style.Line:
				{
					System.Drawing.PointF pt1 = StartPointInCanvas(rgObjs);
					System.Drawing.PointF pt2 = EndPointInCanvas(rgObjs);

					canvas.DrawLine(m_drawingStyle.drawingStyle, pt1, pt2);

					DrawTerminal(canvas, m_startStyle, pt1, pt2);
					DrawTerminal(canvas, m_endStyle, pt2, pt1);

					break;
				}
				case GOM_Linking_Style.Polyline:
				{
					System.Drawing.PointF pt1 = StartPointInCanvas(rgObjs);
					System.Drawing.PointF pt2 = EndPointInCanvas(rgObjs);

					if ( m_keyPts.Count > 0 )
					{
						System.Drawing.PointF firstKeyPt = new System.Drawing.PointF(m_keyPts[0].x, m_keyPts[0].y);
						System.Drawing.PointF lastKeyPt = new System.Drawing.PointF(m_keyPts[m_keyPts.Count-1].x, m_keyPts[m_keyPts.Count-1].y);

						canvas.DrawLine(m_drawingStyle.drawingStyle, pt1, firstKeyPt);
						if ( m_keyPts.Count >= 2 )
						{
							for( int i=0; i<(m_keyPts.Count-1); i++ )
							{
								canvas.DrawLine(m_drawingStyle.drawingStyle, m_keyPts[i].x, m_keyPts[i].y, m_keyPts[i+1].x, m_keyPts[i+1].y);
							}
						}
						canvas.DrawLine(m_drawingStyle.drawingStyle, lastKeyPt, pt2);
						DrawTerminal(canvas, m_startStyle, pt1, firstKeyPt);
						DrawTerminal(canvas, m_endStyle, pt2, lastKeyPt);
					}
					else
					{
						canvas.DrawLine(m_drawingStyle.drawingStyle, pt1, pt2);
						DrawTerminal(canvas, m_startStyle, pt1, pt2);
						DrawTerminal(canvas, m_endStyle, pt2, pt1);
					}
					break;
				}
				case GOM_Linking_Style.Curve:
				{
					break;
				}
				default:
					System.Diagnostics.Debug.Assert(false, "Unknown link style.");
					return;
			}
		}

		/// <summary>
		/// Draw selected link.
		/// </summary>
		/// <param name="canvas">Canvas which link is drawn on.</param>
		/// <param name="rgObjs">GOM object collections.</param>
		public void DrawSelected(System.Drawing.Graphics canvas, GOM_Objects rgObjs)
		{
			System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.BlueViolet, 2);
			pen.DashStyle = m_drawingStyle.drawingStyle.DashStyle;

			switch (m_linkingStyle)
			{
				case GOM_Linking_Style.Line:
				{
					System.Drawing.PointF pt1 = StartPointInCanvas(rgObjs);
					System.Drawing.PointF pt2 = EndPointInCanvas(rgObjs);

					canvas.DrawLine(pen, pt1, pt2);

					DrawTerminal(canvas, m_startStyle, pt1, pt2);
					DrawTerminal(canvas, m_endStyle, pt2, pt1);

					break;
				}
				case GOM_Linking_Style.Polyline:
				{
					System.Drawing.PointF pt1 = StartPointInCanvas(rgObjs);
					System.Drawing.PointF pt2 = EndPointInCanvas(rgObjs);

					if ( m_keyPts.Count > 0 )
					{
						System.Drawing.PointF firstKeyPt = new System.Drawing.PointF(m_keyPts[0].x, m_keyPts[0].y);
						System.Drawing.PointF lastKeyPt = new System.Drawing.PointF(m_keyPts[m_keyPts.Count-1].x, m_keyPts[m_keyPts.Count-1].y);

						canvas.DrawLine(pen, pt1, firstKeyPt);
						if ( m_keyPts.Count >= 2 )
						{
							for( int i=0; i<(m_keyPts.Count-1); i++ )
							{
								canvas.DrawLine(pen, m_keyPts[i].x, m_keyPts[i].y, m_keyPts[i+1].x, m_keyPts[i+1].y);
							}
						}
						canvas.DrawLine(pen, lastKeyPt, pt2);

						// Draw key points
						for( int i=0; i<m_keyPts.Count; i++ )
						{
							canvas.FillEllipse(System.Drawing.Brushes.YellowGreen, m_keyPts[i].x-3, m_keyPts[i].y-3, 7, 7);
						}

						DrawTerminal(canvas, m_startStyle, pt1, firstKeyPt);
						DrawTerminal(canvas, m_endStyle, pt2, lastKeyPt);
					}
					else
					{
						canvas.DrawLine(pen, pt1, pt2);
						DrawTerminal(canvas, m_startStyle, pt1, pt2);
						DrawTerminal(canvas, m_endStyle, pt2, pt1);
					}
					break;
				}
				case GOM_Linking_Style.Curve:
				{
					break;
				}
				default:
					System.Diagnostics.Debug.Assert(false, "Unknown link style.");
					return;
			}
		}

		/// <summary>
		/// Gets the start point in canvas coordinate.
		/// </summary>
		/// <param name="rgObjs">GOM object collection.</param>
		/// <returns>Start point in canvas coordinate.</returns>
		public System.Drawing.PointF StartPointInCanvas(GOM_Objects rgObjs)
		{
			System.Drawing.PointF	pt = new System.Drawing.PointF(m_startPt.x, m_startPt.y);
			if (!CalculatePointLocation(ref pt, this.m_startObj, rgObjs))
			{
				pt.X = -1;
				pt.Y = -1;
			}
			return pt;
		}

		/// <summary>
		/// Gets the end point in canvas coordinate.
		/// </summary>
		/// <param name="rgObjs">GOM object collection.</param>
		/// <returns>End point in canvas coordinate.</returns>
		public System.Drawing.PointF EndPointInCanvas(GOM_Objects rgObjs)
		{
			System.Drawing.PointF	pt = new System.Drawing.PointF(m_endPt.x, m_endPt.y);
			if (!CalculatePointLocation(ref pt, this.m_endObj, rgObjs))
			{
				pt.X = -1;
				pt.Y = -1;
			}
			return pt;
		}

		private bool CalculatePointLocation(ref System.Drawing.PointF pt, GOM_Interface_Graphic_Object obj, GOM_Objects rgObjs)
		{
			bool bTranslated = false;

			for (int i = 0; i < rgObjs.Count; i++)
			{
				if (rgObjs[i].Equals(obj))
				{
					bTranslated = true;
				}
				else if (rgObjs[i] is GOM_Object_Group)
				{
					bTranslated |= CalculatePointLocation(ref pt, obj, ((GOM_Object_Group)rgObjs[i]).rgObjects);
				}

				if (bTranslated)
				{
					pt = rgObjs[i].PointToCanvas(pt);
					break;
				}
			}

			return bTranslated;
		}

		/// <summary>
		/// Draws link's terminal ponit.
		/// </summary>
		/// <param name="canvas"></param>
		/// <param name="style"></param>
		/// <param name="pt1"></param>
		/// <param name="pt2"></param>
		private void DrawTerminal(System.Drawing.Graphics canvas, GOM_Terminal_Style style, System.Drawing.PointF pt1, System.Drawing.PointF pt2)
		{
			System.Drawing.Drawing2D.Matrix	matrix;
			float angle;

			angle	= (float)(System.Math.Atan2(pt2.Y - pt1.Y, pt2.X - pt1.X) / System.Math.PI) * 180;
			matrix	= canvas.Transform;

			canvas.TranslateTransform(pt1.X, pt1.Y);
			canvas.RotateTransform(angle);
			switch (style)
			{
				case GOM_Terminal_Style.Circle:
				{
					canvas.FillEllipse(new System.Drawing.SolidBrush(m_drawingStyle.drawingStyle.Color), -5, -5, 10, 10);
					break;
				}
				case GOM_Terminal_Style.Arrow:
				{
					canvas.DrawLine(m_drawingStyle.drawingStyle, 0, 0, 10, 5);
					canvas.DrawLine(m_drawingStyle.drawingStyle, 0, 0, 10, -5);
					break;
				}
				case GOM_Terminal_Style.Triangle:
				{
					System.Drawing.Point[] rgPts;

					rgPts = new System.Drawing.Point[3];
					rgPts[0].X = 0;
					rgPts[0].Y = 0;
					rgPts[1].X = 10;
					rgPts[1].Y = 5;
					rgPts[2].X = 10;
					rgPts[2].Y = -5;

					canvas.FillPolygon(new System.Drawing.SolidBrush(m_drawingStyle.drawingStyle.Color), rgPts);
					break;
				}
				case GOM_Terminal_Style.Diamond:
				{
					System.Drawing.Point[] rgPts;

					rgPts = new System.Drawing.Point[4];
					rgPts[0].X = 0;
					rgPts[0].Y = 0;
					rgPts[1].X = 5;
					rgPts[1].Y = 5;
					rgPts[2].X = 10;
					rgPts[2].Y = 0;
					rgPts[3].X = 5;
					rgPts[3].Y = -5;

					canvas.FillPolygon(new System.Drawing.SolidBrush(m_drawingStyle.drawingStyle.Color), rgPts);
					break;
				}
			}
			canvas.Transform = matrix;
		}

		/// <summary>
		/// Indicates whether this link is linked with an specilized object.
		/// </summary>
		/// <param name="obj">The object.</param>
		/// <returns>Whether this link is linked with an specilized object.</returns>
		public bool LinkWith(GOM_Interface_Graphic_Object obj)
		{
			if (m_startObj.Equals(obj) || m_endObj.Equals(obj))
			{
				return true;
			}
			else if (obj is GOM_Object_Group)
			{
				GOM_Object_Group objGroup = ((GOM_Object_Group)obj);

				for (int i = 0; i < objGroup.rgObjects.Count; i++)
				{
					if (LinkWith(objGroup.rgObjects[i]))
					{
						return true;
					}
				}
			}

			return false;
		}

		/// <summary>
		/// Indicates whether a given point is on this link.
		/// </summary>
		/// <param name="x">X coordinate of the given point.</param>
		/// <param name="y">Y coordinate of the given point.</param>
		/// <param name="rgObjs">GOM object collection.</param>
		/// <returns>Whether a given point is on this link.</returns>
		public bool IsPointOnLink(float x, float y, GOM_Objects rgObjs)
		{
			switch (m_linkingStyle)
			{
				case GOM_Linking_Style.Line:
				{
					System.Drawing.Drawing2D.Matrix	matrix;
					System.Drawing.PointF[]			rgPts;
					System.Drawing.PointF			startPt, endPt;

					startPt	= StartPointInCanvas(rgObjs);
					endPt	= EndPointInCanvas(rgObjs);

					rgPts = new System.Drawing.PointF[2];
					rgPts[0].X = endPt.X;
					rgPts[0].Y = endPt.Y;
					rgPts[1].X = x;
					rgPts[1].Y = y;

					matrix = new System.Drawing.Drawing2D.Matrix();
					matrix.Translate(-startPt.X, -startPt.Y);
					matrix.TransformPoints(rgPts);

					float angle = (float)(System.Math.Atan2(endPt.Y - startPt.Y, endPt.X - startPt.X) / System.Math.PI) * 180;

					matrix.Reset();
					matrix.Rotate(-angle);
					matrix.TransformPoints(rgPts);

					if ((Math.Abs(rgPts[1].Y) < 2) && (-2 < rgPts[1].X) && (rgPts[1].X < rgPts[0].X + 2))
					{
						return true;
					}

					break;
				}
				case GOM_Linking_Style.Polyline:
				{
					System.Drawing.Drawing2D.Matrix	matrix;
					System.Drawing.PointF[]			rgPts, rgAllPts;
					System.Drawing.PointF			startPt, endPt;
					startPt	= StartPointInCanvas(rgObjs);
					endPt	= EndPointInCanvas(rgObjs);
					
					rgPts = new System.Drawing.PointF[2];
					rgAllPts = new System.Drawing.PointF[m_keyPts.Count+2];

					rgAllPts[0].X = startPt.X;
					rgAllPts[0].Y = startPt.Y;
					for(int i=0; i<m_keyPts.Count; i++)
					{
						rgAllPts[1+i].X = m_keyPts[i].x;
						rgAllPts[1+i].Y = m_keyPts[i].y;
					}
					rgAllPts[rgAllPts.Length-1].X = endPt.X;
					rgAllPts[rgAllPts.Length-1].Y = endPt.Y;

					for(int i=0; i<(rgAllPts.Length-1); i++)
					{
						rgPts[0].X = rgAllPts[i+1].X;
						rgPts[0].Y = rgAllPts[i+1].Y;
						rgPts[1].X = x;
						rgPts[1].Y = y;

						matrix = new System.Drawing.Drawing2D.Matrix();
						matrix.Translate(-rgAllPts[i].X, -rgAllPts[i].Y);
						matrix.TransformPoints(rgPts);

						float angle = (float)(System.Math.Atan2(rgAllPts[i+1].Y - rgAllPts[i].Y, rgAllPts[i+1].X - rgAllPts[i].X) / System.Math.PI) * 180;

						matrix.Reset();
						matrix.Rotate(-angle);
						matrix.TransformPoints(rgPts);

						if ((Math.Abs(rgPts[1].Y) < 2) && (-2 < rgPts[1].X) && (rgPts[1].X < rgPts[0].X + 2))
						{
							return true;
						}
					}

					break;
				}
				case GOM_Linking_Style.Curve:
				{
					break;
				}
				default:
					System.Diagnostics.Debug.Assert(false, "Unknown link style.");
					break;
			}

			return false;
		}

		/// <summary>
		/// Save the link into XmlWriter.
		/// </summary>
		/// <param name="writer">The XmlWriter.</param>
		public void SaveToXML( System.Xml.XmlWriter writer )
		{
			writer.WriteStartElement(GOM_TAGS.CONNECTION);
			writer.WriteAttributeString(GOM_TAGS.LINKING_STYLE, ConvertLinkingStyleToString(m_linkingStyle));

			// Start point
			writer.WriteStartElement(GOM_TAGS.POINT);
			writer.WriteAttributeString(GOM_TAGS.OBJECTID, this.m_startObj.id);
			writer.WriteAttributeString(GOM_TAGS.POINTID, this.m_startPt.id);
			writer.WriteAttributeString(GOM_TAGS.TERMINAL_STYLE, ConvertTerminalStyleToString(this.m_startStyle));
			writer.WriteEndElement();

			// End point
			writer.WriteStartElement(GOM_TAGS.POINT);
			writer.WriteAttributeString(GOM_TAGS.OBJECTID, this.m_endObj.id);
			writer.WriteAttributeString(GOM_TAGS.POINTID, this.m_endPt.id);
			writer.WriteAttributeString(GOM_TAGS.TERMINAL_STYLE, ConvertTerminalStyleToString(this.m_endStyle));
			writer.WriteEndElement();

			// Drawing style
			this.m_drawingStyle.SaveToXML(writer);

			// Key points
			writer.WriteStartElement(GOM_TAGS.KEYPOINTS);
			for( int i=0; i<m_keyPts.Count; i++ )
			{
				writer.WriteStartElement(GOM_TAGS.POINT);
				writer.WriteAttributeString(GOM_TAGS.X, m_keyPts[i].x.ToString());
				writer.WriteAttributeString(GOM_TAGS.Y, m_keyPts[i].y.ToString());
				writer.WriteEndElement();
			}
			writer.WriteEndElement();

			writer.WriteEndElement();
		}

		/// <summary>
		/// Load the link from XmlNode.
		/// </summary>
		/// <param name="node">The XmlNode.</param>
		/// <param name="resources">GOM resource.</param>
		public void LoadFromXML(System.Xml.XmlNode node, GOM_ResourceArrays resources)
		{
			GOM_Utility.VerifyXmlNode(node, GOM_TAGS.CONNECTION);

			for( int i=0; i<node.Attributes.Count; i++ )
			{
//				if ( string.Compare(node.Attributes[i].Name,GOM_TAGS.DRAWING_STYLE) == 0 )
//				{
//					this.m_drawingStyle = resources.DrawingStyles[node.Attributes[i].Value];
//				}
				if ( string.Compare(node.Attributes[i].Name,GOM_TAGS.LINKING_STYLE) == 0 )
				{
					this.m_linkingStyle = ConvertStringToLinkingStyle(node.Attributes[i].Value);
				}
			}

			GOM_Utility.VerifyXmlNode(node.ChildNodes[0], GOM_TAGS.POINT);
			GOM_Utility.VerifyXmlNode(node.ChildNodes[1], GOM_TAGS.POINT);
			// 1st pass
			//   objectID
			for( int i=0; i<node.ChildNodes[0].Attributes.Count; i++ )
			{
				if ( string.Compare(node.ChildNodes[0].Attributes[i].Name, GOM_TAGS.OBJECTID) == 0 )
				{
					this.m_startObj = GOM_Utility.RecursiveFindObject(node.ChildNodes[0].Attributes[i].Value, resources.Objects);
				}
			}
			for( int i=0; i<node.ChildNodes[1].Attributes.Count; i++ )
			{
				if ( string.Compare(node.ChildNodes[1].Attributes[i].Name, GOM_TAGS.OBJECTID) == 0 )
				{
					this.m_endObj = GOM_Utility.RecursiveFindObject(node.ChildNodes[1].Attributes[i].Value, resources.Objects);
				}
			}
			if ( m_startObj==null || m_endObj==null )
			{
				throw new System.Xml.XmlException("Missing points in the connection.");
			}

//			if ( !(m_startObj is GOM_Object_Primitive) || !(m_endObj is GOM_Object_Primitive) )
//			{
//				throw new System.Xml.XmlException("Terminal points' objects are not all primitives.");
//			}

			// 2nd pass
			//   pointID
			//   terminalStyle
			for( int i=0; i<node.ChildNodes[0].Attributes.Count; i++ )
			{
				if ( string.Compare(node.ChildNodes[0].Attributes[i].Name, GOM_TAGS.POINTID) == 0 )
				{
					this.m_startPt = m_startObj.GetPointByName(node.ChildNodes[0].Attributes[i].Value);
				}
				if ( string.Compare(node.ChildNodes[0].Attributes[i].Name, GOM_TAGS.TERMINAL_STYLE) == 0 )
				{
					this.m_startStyle = ConvertStringToTerminalStyle(node.ChildNodes[0].Attributes[i].Value);
				}
			}
			for( int i=0; i<node.ChildNodes[1].Attributes.Count; i++ )
			{
				if ( string.Compare(node.ChildNodes[1].Attributes[i].Name, GOM_TAGS.POINTID) == 0 )
				{
					this.m_endPt = m_endObj.GetPointByName(node.ChildNodes[1].Attributes[i].Value);
				}
				if ( string.Compare(node.ChildNodes[1].Attributes[i].Name, GOM_TAGS.TERMINAL_STYLE) == 0 )
				{
					this.m_endStyle = ConvertStringToTerminalStyle(node.ChildNodes[1].Attributes[i].Value);
				}
			}

			if ( m_startPt==null || m_endPt==null )
			{
				throw new System.Xml.XmlException("Cannot find find terminal points.");
			}

			// Drawing style
			this.m_drawingStyle.LoadFromXML( node.ChildNodes[2], resources );

			// Key points
			GOM_Utility.VerifyXmlNode(node.ChildNodes[3], GOM_TAGS.KEYPOINTS);
			for(int i=0; i<node.ChildNodes[3].ChildNodes.Count; i++ )
			{
				if ( string.Compare(node.ChildNodes[3].ChildNodes[i].Name, GOM_TAGS.POINT, true) == 0 )
				{
					GOM_Point keyPoint = new GOM_Point();
					for(int j=0; j<node.ChildNodes[3].ChildNodes[i].Attributes.Count; j++ )
					{
						if ( string.Compare(node.ChildNodes[3].ChildNodes[i].Attributes[j].Name, GOM_TAGS.X, true) == 0 )
						{
							keyPoint.x = float.Parse(node.ChildNodes[3].ChildNodes[i].Attributes[j].Value);
						}
						if ( string.Compare(node.ChildNodes[3].ChildNodes[i].Attributes[j].Name, GOM_TAGS.Y, true) == 0 )
						{
							keyPoint.y = float.Parse(node.ChildNodes[3].ChildNodes[i].Attributes[j].Value);
						}
					}
					m_keyPts.Add(keyPoint);
				}
			}
		}

		/// <summary>
		/// Convert the LinkingStyle to a string.
		/// </summary>
		/// <param name="linkingStyle">The LinkingStyle.</param>
		/// <returns>A string</returns>
		public string ConvertLinkingStyleToString( GOM_Linking_Style linkingStyle )
		{
			switch(linkingStyle)
			{
				case GOM_Linking_Style.Line:
					return "line";
				case GOM_Linking_Style.Polyline:
					return "polyline";
				case GOM_Linking_Style.Curve:
					return "curve";
			}
			throw new ArgumentException("Unknown LinkingStyle["+linkingStyle.ToString()+"].");
		}

		/// <summary>
		/// Convert the string to a LinkingStyle.
		/// </summary>
		/// <param name="str">The string.</param>
		/// <returns>A LinkingStyle.</returns>
		public GOM_Linking_Style ConvertStringToLinkingStyle(string str)
		{
			switch(str.ToLower())
			{
				case "line":
					return GOM_Linking_Style.Line;
				case "polyline":
					return GOM_Linking_Style.Polyline;
				case "curve":
					return GOM_Linking_Style.Curve;
			}
			throw new ArgumentException("Unknown LinkingStyle string["+str+"].");
		}

		/// <summary>
		/// Convert the TerminalStyle to a string.
		/// </summary>
		/// <param name="terminalStyle">The TermianlStyel.</param>
		/// <returns>A string.</returns>
		public string ConvertTerminalStyleToString( GOM_Terminal_Style terminalStyle )
		{
			switch(terminalStyle)
			{
				case GOM_Terminal_Style.None:
					return "none";
				case GOM_Terminal_Style.Circle:
					return "circle";
				case GOM_Terminal_Style.Arrow:
					return "arrow";
				case GOM_Terminal_Style.Triangle:
					return "triangle";
				case GOM_Terminal_Style.Diamond:
					return "diamond";
			}
			throw new ArgumentException("Unknown TerminalStyle["+terminalStyle.ToString()+"].");
		}

		/// <summary>
		/// Convert the string to a TerminalStyle.
		/// </summary>
		/// <param name="str">The string.</param>
		/// <returns>A TerminalStyle.</returns>
		public GOM_Terminal_Style ConvertStringToTerminalStyle(string str)
		{
			switch(str.ToLower())
			{
				case "none":
					return GOM_Terminal_Style.None;
				case "circle":
					return GOM_Terminal_Style.Circle;
				case "arrow":
					return GOM_Terminal_Style.Arrow;
				case "triangle":
					return GOM_Terminal_Style.Triangle;
				case "diamond":
					return GOM_Terminal_Style.Diamond;
			}
			throw new ArgumentException("Unknown TerminalStyle string["+str+"].");
		}

		/// <summary>
		/// Add a key point into this polyline link.
		/// </summary>
		/// <param name="x">X coordinate of the key point.</param>
		/// <param name="y">Y coordinate of the key point.</param>
		/// <param name="rgObjs">GOM object collection.</param>
		public void AddKeyPoint(float x, float y, GOM_Objects rgObjs)
		{
			if ( m_linkingStyle != GOM_Linking_Style.Polyline )
			{
				return;
			}

			System.Drawing.Drawing2D.Matrix	matrix;
			System.Drawing.PointF[]			rgPts, rgAllPts;
			System.Drawing.PointF			startPt, endPt;
			startPt	= StartPointInCanvas(rgObjs);
			endPt	= EndPointInCanvas(rgObjs);
					
			rgPts = new System.Drawing.PointF[2];
			rgAllPts = new System.Drawing.PointF[m_keyPts.Count+2];

			rgAllPts[0].X = startPt.X;
			rgAllPts[0].Y = startPt.Y;
			for(int i=0; i<m_keyPts.Count; i++)
			{
				rgAllPts[1+i].X = m_keyPts[i].x;
				rgAllPts[1+i].Y = m_keyPts[i].y;
			}
			rgAllPts[rgAllPts.Length-1].X = endPt.X;
			rgAllPts[rgAllPts.Length-1].Y = endPt.Y;

			for(int i=0; i<(rgAllPts.Length-1); i++)
			{
				rgPts[0].X = rgAllPts[i+1].X;
				rgPts[0].Y = rgAllPts[i+1].Y;
				rgPts[1].X = x;
				rgPts[1].Y = y;

				matrix = new System.Drawing.Drawing2D.Matrix();
				matrix.Translate(-rgAllPts[i].X, -rgAllPts[i].Y);
				matrix.TransformPoints(rgPts);

				float angle = (float)(System.Math.Atan2(rgAllPts[i+1].Y - rgAllPts[i].Y, rgAllPts[i+1].X - rgAllPts[i].X) / System.Math.PI) * 180;

				matrix.Reset();
				matrix.Rotate(-angle);
				matrix.TransformPoints(rgPts);

				if ((Math.Abs(rgPts[1].Y) < 2) && (-2 < rgPts[1].X) && (rgPts[1].X < rgPts[0].X + 2))
				{
					GOM_Point point = new GOM_Point();
					point.x = x;
					point.y = y;
					m_keyPts.Insert(i, point);
					return;
				}
			}
		}

		public GOM_Interface_Graphic_Object	m_startObj;
		public GOM_Interface_Graphic_Object	m_endObj;
		public GOM_Point					m_startPt;
		public GOM_Point					m_endPt;
		public GOM_Terminal_Style			m_startStyle;
		public GOM_Terminal_Style			m_endStyle;
		public GOM_Style_Drawing			m_drawingStyle;
		public GOM_Linking_Style			m_linkingStyle;
		public GOM_Points					m_keyPts;
	}

	public enum GOM_Terminal_Style	{None, Circle, Arrow, Triangle, Diamond}
	public enum GOM_Linking_Style	{Line, Polyline, Curve}


	/// <summary>
	/// A dynamic list of GOM_Link
	/// </summary>
	public class GOM_Links
	{
		/// <summary>
		/// The constructor of GOM_Links
		/// </summary>
		public GOM_Links()
		{
			rgLinks = new System.Collections.ArrayList();
		}
		/// <summary>
		/// Return the template by its index
		/// </summary>
		public GOM_Link this[int idx]
		{
			get
			{
				return (GOM_Link)rgLinks[idx];
			}
			set
			{
				rgLinks[idx] = value;
			}
		}
//		/// <summary>
//		/// Return a template by its name.
//		/// </summary>
//		public GOM_Link this[string name]
//		{
//			get
//			{
//				for (int i = 0; i < rgLinks.Count; i++)
//				{
//					if ( ((GOM_Link)rgLinks[i]).id.Equals(name) )
//					{
//						return (GOM_Link)rgLinks[i];
//					}
//				}
//				return null;
//			}
//		}
		/// <summary>
		/// Number of templates stored in the list
		/// </summary>
		public int Count
		{
			get
			{
				return rgLinks.Count;
			}
		}
		/// <summary>
		/// Add a template into the list
		/// </summary>
		/// <param name="val">The template being stored</param>
		public void Add(GOM_Link val)
		{
			rgLinks.Add(val);
		}
		/// <summary>
		/// Remove a template by its index
		/// </summary>
		/// <param name="idx">The index of the template</param>
		public void RemoveAt(int idx)
		{
			rgLinks.RemoveAt(idx);
		}
		public void Remove(GOM_Link link)
		{
			rgLinks.Remove(link);
		}
		/// <summary>
		/// Empty this list
		/// </summary>
		public void Clear()
		{
			rgLinks.Clear();
		}
		/// <summary>
		/// Dynamic array of template
		/// </summary>
		private System.Collections.ArrayList rgLinks;
	}

}
