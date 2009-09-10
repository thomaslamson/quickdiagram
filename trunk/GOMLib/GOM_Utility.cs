using System;

namespace GOMLib
{
	/// <summary>
	/// Static class providing some helpful functions.
	/// </summary>
	public sealed class GOM_Utility
	{
		private GOM_Utility()
		{}

		public static GOM_Interface_Graphic_Object RecursiveFindObject(string name, GOM_Objects rgObjects)
		{
			for ( int i=0; i<rgObjects.Count; i++ )
			{
				if ( string.Compare(rgObjects[i].id, name) == 0 )
				{
					return rgObjects[i];
				}
				if ( rgObjects[i] is GOM_Object_Group )
				{
					GOM_Interface_Graphic_Object graphicObject = null;
					graphicObject = RecursiveFindObject(name, ((GOM_Object_Group)rgObjects[i]).rgObjects);
					if ( graphicObject != null )
					{
						return graphicObject;
					}
				}
			}
			return null;
		}

		public static void VerifyXmlNode(System.Xml.XmlNode node, string expectedNodeName)
		{
			VerifyXmlNode(node, expectedNodeName, true);
		}

		public static void VerifyXmlNode(System.Xml.XmlNode node, string expectedNodeName, bool ignoreCase)
		{
			if ( string.Compare(node.Name, expectedNodeName, ignoreCase) != 0 )
			{
				throw new System.Xml.XmlException("Unexpected XML node["+node.Name+"]. Need node["+expectedNodeName+"].");
			}
		}
		/// <summary>
		/// Load a value object recursively
		/// </summary>
		/// <param name="node">The node that represents a value object</param>
		/// <returns>If successful, a value object is returned. Otherwize, an exception will be thrown out.</returns>
		public static GOM_Interface_Value LoadValueFromXML(System.Xml.XmlNode node, GOM_Points points )
		{
			if (System.String.Compare(node.Name, GOM_TAGS.VALUE, true) == 0)
			{
				return LoadValueFromXML(node.ChildNodes[0], points);
			}

			if (System.String.Compare(node.Name, GOM_TAGS.X_VALUE, true) == 0)
			{
				GOM_Point	pt = null;

				for (int i = 0; i < node.Attributes.Count; i++)
				{
					if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.POINT, true) == 0)
					{
						pt = points[node.Attributes[i].Value];
					}
				}

				if (pt == null)
				{
					throw new Exception("Missing point in x value");
				}

				return new GOM_Point_Value(pt, ValueType.X_Value);
			}

			if (System.String.Compare(node.Name, GOM_TAGS.Y_VALUE, true) == 0)
			{
				GOM_Point	pt = null;

				for (int i = 0; i < node.Attributes.Count; i++)
				{
					if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.POINT, true) == 0)
					{
						pt = points[node.Attributes[i].Value];
					}
				}

				if (pt == null)
				{
					throw new Exception("Missing point in x value");
				}

				return new GOM_Point_Value(pt, ValueType.Y_Value);
			}

			if (System.String.Compare(node.Name, GOM_TAGS.SIN, true) == 0)
			{
				return new GOM_Unary_Value(LoadValueFromXML(node.ChildNodes[0],points), ValueType.Sin_Value);
			}

			if (System.String.Compare(node.Name, GOM_TAGS.COS, true) == 0)
			{
				return new GOM_Unary_Value(LoadValueFromXML(node.ChildNodes[0],points), ValueType.Cos_Value);
			}

			if (System.String.Compare(node.Name, GOM_TAGS.TAN, true) == 0)
			{
				return new GOM_Unary_Value(LoadValueFromXML(node.ChildNodes[0],points), ValueType.Tan_Value);
			}

			if (System.String.Compare(node.Name, GOM_TAGS.SQRT, true) == 0)
			{
				return new GOM_Unary_Value(LoadValueFromXML(node.ChildNodes[0],points), ValueType.Sqrt_Value);
			}

			if (System.String.Compare(node.Name, GOM_TAGS.NEGATIVE, true) == 0)
			{
				return new GOM_Unary_Value(LoadValueFromXML(node.ChildNodes[0],points), ValueType.Neg_Value);
			}

			if (System.String.Compare(node.Name, GOM_TAGS.ADD, true) == 0)
			{
				GOM_Values	rgValues;

				rgValues = new GOM_Values();

				for (int i = 0; i < node.ChildNodes.Count; i++)
				{
					GOM_Interface_Value Value = LoadValueFromXML(node.ChildNodes[i],points);
					if (Value != null)
					{
						rgValues.Add(Value);
						Value = null;
					}
				}

				if (rgValues.Count != 2)
				{
					throw new Exception("Incorrect number of arguments in add value");
				}

				return new GOM_Binary_Value(rgValues[0], rgValues[1], ValueType.Add_Value); 
			}

			if (System.String.Compare(node.Name, GOM_TAGS.MINUS, true) == 0)
			{
				GOM_Values	rgValues;

				rgValues = new GOM_Values();

				for (int i = 0; i < node.ChildNodes.Count; i++)
				{
					GOM_Interface_Value Value = LoadValueFromXML(node.ChildNodes[i],points);
					if (Value != null)
					{
						rgValues.Add(Value);
						Value = null;
					}
				}

				if (rgValues.Count != 2)
				{
					throw new Exception("Incorrect number of arguments in minus value");
				}

				return new GOM_Binary_Value(rgValues[0], rgValues[1], ValueType.Minus_Value); 
			}

			if (System.String.Compare(node.Name, GOM_TAGS.MULTIPLY, true) == 0)
			{
				GOM_Values	rgValues;

				rgValues = new GOM_Values();

				for (int i = 0; i < node.ChildNodes.Count; i++)
				{
					GOM_Interface_Value Value = LoadValueFromXML(node.ChildNodes[i],points);
					if (Value != null)
					{
						rgValues.Add(Value);
						Value = null;
					}
				}

				if (rgValues.Count != 2)
				{
					throw new Exception("Incorrect number of arguments in multiply value");
				}

				return new GOM_Binary_Value(rgValues[0], rgValues[1], ValueType.Multiply_Value); 
			}

			if (System.String.Compare(node.Name, GOM_TAGS.DIVIDE, true) == 0)
			{
				GOM_Values	rgValues;

				rgValues = new GOM_Values();

				for (int i = 0; i < node.ChildNodes.Count; i++)
				{
					GOM_Interface_Value Value = LoadValueFromXML(node.ChildNodes[i],points);
					if (Value != null)
					{
						rgValues.Add(Value);
						Value = null;
					}
				}

				if (rgValues.Count != 2)
				{
					throw new Exception("Incorrect number of arguments in divide value");
				}

				return new GOM_Binary_Value(rgValues[0], rgValues[1], ValueType.Divide_Value); 
			}

			if (System.String.Compare(node.Name, GOM_TAGS.MAX, true) == 0)
			{
				GOM_Values	rgValues;

				rgValues = new GOM_Values();

				for (int i = 0; i < node.ChildNodes.Count; i++)
				{
					GOM_Interface_Value Value = LoadValueFromXML(node.ChildNodes[i],points);
					if (Value != null)
					{
						rgValues.Add(Value);
						Value = null;
					}
				}

				if (rgValues.Count != 2)
				{
					throw new Exception("Incorrect number of arguments in max value");
				}

				return new GOM_Binary_Value(rgValues[0], rgValues[1], ValueType.Max_Value); 
			}

			if (System.String.Compare(node.Name, GOM_TAGS.MIN, true) == 0)
			{
				GOM_Values	rgValues;

				rgValues = new GOM_Values();

				for (int i = 0; i < node.ChildNodes.Count; i++)
				{
					GOM_Interface_Value Value = LoadValueFromXML(node.ChildNodes[i],points);
					if (Value != null)
					{
						rgValues.Add(Value);
						Value = null;
					}
				}

				if (rgValues.Count != 2)
				{
					throw new Exception("Incorrect number of arguments in min value");
				}

				return new GOM_Binary_Value(rgValues[0], rgValues[1], ValueType.Min_Value); 
			}

			if (node.ChildNodes.Count == 0)
			{
				GOM_Num_Value Value = new GOM_Num_Value();
				Value.LoadFromXML( node, null );
				return Value;
			}

			return null;
		}

		/// <summary>
		/// Load a constraint
		/// </summary>
		/// <param name="node">The node that represent the constraint</param>
		/// <returns>If successful, a constraint object is returned. Otherwise, a exception will be thrown out.</returns>
		public static GOM_Interface_Constraint LoadConstraintFromXML(System.Xml.XmlNode node, GOM_Points points)
		{
			GOM_Interface_Constraint	constraint = null;

			if (System.String.Compare(node.Name, GOM_TAGS.ASSIGN, true) == 0)
			{
				GOM_Interface_Value	lvalue = null;
				GOM_Interface_Value	rvalue = null;

				for (int i = 0; i < node.ChildNodes.Count; i++)
				{
					if (System.String.Compare(node.ChildNodes[i].Name, GOM_TAGS.L_VALUE, true) == 0)
					{
						lvalue = LoadValueFromXML(node.ChildNodes[i].ChildNodes[0],points);
					}

					if (System.String.Compare(node.ChildNodes[i].Name, GOM_TAGS.R_VALUE, true) == 0)
					{
						rvalue = LoadValueFromXML(node.ChildNodes[i].ChildNodes[0],points);
					}
				}

				if ((lvalue == null) || (rvalue == null))
				{
					throw new Exception("Missing value in assignment constraint");
				}

				constraint = new GOM_Assignment_Constraint(lvalue, rvalue);
			}

			return constraint;
		}


		/// <summary>
		/// Add a drawing operation to the list of drawing operatoin
		/// </summary>
		/// <param name="node">The XML node that represents the drawing operation</param>
		/// <param name="drawings">The list of drawing operation</param>
		public static void LoadDrawingFromXML(System.Xml.XmlNode node, GOM_Drawings drawings, GOM_ResourceArrays resources)
		{
			if (System.String.Compare(node.Name, GOM_TAGS.LINE, true) == 0)
			{
				GOM_Drawing_Line	line = new GOM_Drawing_Line( node, resources );
				if ( line != null )
				{
					drawings.Add(line);
				}
			}

			if (System.String.Compare(node.Name, GOM_TAGS.ARC, true) == 0)
			{
				GOM_Drawing_Arc		arc = new GOM_Drawing_Arc(node, resources);
				if  ( arc != null )
				{
					drawings.Add(arc);
				}
			}

			if (System.String.Compare(node.Name, GOM_TAGS.BEZIER, true) == 0)
			{
				GOM_Drawing_Bezier	bezier = new GOM_Drawing_Bezier(node, resources);
				if ( bezier != null )
				{
					drawings.Add(bezier);
				}
			}
		}

		/// <summary>
		/// Add a filling operation to current template
		/// </summary>
		/// <param name="node">The XML that represents the filling operation</param>
		public static void LoadFillingFromXML(System.Xml.XmlNode node, GOM_Fillings fillings, GOM_ResourceArrays resources)
		{
			if (System.String.Compare(node.Name, GOM_TAGS.ELLIPSE, true) == 0)
			{
				GOM_Filling_Ellipse	ellipse = new GOM_Filling_Ellipse(node,resources);
				if ( ellipse != null )
				{
					fillings.Add(ellipse);
				}
			}

			if (System.String.Compare(node.Name, GOM_TAGS.RECTANGLE, true) == 0)
			{
				GOM_Filling_Rectangle	rect = new GOM_Filling_Rectangle(node, resources);
				if ( rect != null )
				{
					fillings.Add(rect);
				}
			}

			if (System.String.Compare(node.Name, GOM_TAGS.PIE, true) == 0)
			{
				GOM_Filling_Pie		pie = new  GOM_Filling_Pie(node, resources);
				if ( pie != null )
				{
					fillings.Add(pie);
				}
			}

			if (System.String.Compare(node.Name, GOM_TAGS.POLYGON, true) == 0)
			{
				GOM_Filling_Polygon	polygon = new GOM_Filling_Polygon(node, resources);
				if ( polygon != null )
				{
					fillings.Add(polygon);
				}
			}

			if (System.String.Compare(node.Name, GOM_TAGS.FILLPATH, true) == 0)
			{
				GOM_Filling_FillPath	path = new GOM_Filling_FillPath(node, resources);
				if ( path != null )
				{
					fillings.Add(path);
				}
			}
		}


		/// <summary>
		/// Save templates to xml writer.
		/// </summary>
		/// <param name="writer">The XML writer.</param>
		public static void SaveTemplatesToXML(System.Xml.XmlWriter writer, GOM_Templates rgTemplates)
		{
			writer.WriteStartElement(GOM_TAGS.TEMPLATES);
			for( int i=0; i<rgTemplates.Count; i++ )
			{
				rgTemplates[i].SaveToXML(writer);
			}
			writer.WriteEndElement();
		}

		/// <summary>
		/// Load templates from XML node.
		/// </summary>
		/// <param name="node">The XML node.</param>
		/// <param name="resources">GOM resource arrays.</param>
		/// <param name="rgTemplates">GOM template list into which loaded templates will be stored.</param>
		public static void LoadTemplatesFromXML(System.Xml.XmlNode node, GOM_ResourceArrays resources, GOM_Templates rgTemplates)
		{
			VerifyXmlNode(node, GOM_TAGS.TEMPLATES);

			for( int i=0; i<node.ChildNodes.Count; i++ )
			{
				GOM_Template template = new GOM_Template();
				template.LoadFromXML(node.ChildNodes[i], resources);
				rgTemplates.Add(template);
			}
		}

		/// <summary>
		/// Save graphic objects to xml writer.
		/// </summary>
		/// <param name="writer">The XML writer.</param>
		/// <param name="rgObjects">The graphic objects.</param>
		public static void SaveObjectsToXML(System.Xml.XmlWriter writer, GOM_Objects rgObjects)
		{
			writer.WriteStartElement(GOM_TAGS.OBJECTS);
			for( int i=0; i<rgObjects.Count; i++ )
			{
				rgObjects[i].SaveToXML(writer);
			}
			writer.WriteEndElement();
		}

		/// <summary>
		/// Load objects from XML node.
		/// </summary>
		/// <param name="node">The XML node.</param>
		/// <param name="resources">GOM resource arrays.</param>
		/// <param name="rgObjects">GOM object list into which loaded objects will be stored.</param>
		public static void LoadObjectsFromXML(System.Xml.XmlNode node, GOM_ResourceArrays resources, GOM_Objects rgObjects)
		{
			VerifyXmlNode(node, GOM_TAGS.OBJECTS);

			for( int i=0; i<node.ChildNodes.Count; i++ )
			{
				System.Xml.XmlNode childNode = node.ChildNodes[i];

				VerifyXmlNode(childNode, GOM_TAGS.GRAPHIC_OBJECT);

				System.Xml.XmlAttribute attribute = childNode.Attributes[GOM_TAGS.TYPE];
				if ( attribute == null )
				{
					throw new System.Xml.XmlException("["+GOM_TAGS.GRAPHIC_OBJECT+"] needs attribute["+GOM_TAGS.TYPE+"].");
				}

				GOM_Interface_Graphic_Object graphicObject = null;

				if ( string.Compare( attribute.Value, "primitive", true) == 0 )
				{
					// Primitive object
					graphicObject = new GOM_Object_Primitive();
				}
				else if ( string.Compare( attribute.Value, "group", true) == 0 )
				{
					// Group object
					graphicObject = new GOM_Object_Group();
				}
				else if ( string.Compare( attribute.Value, "image", true) == 0 )
				{
					// Image object
				}
				else if ( string.Compare( attribute.Value, "sketch", true) == 0 )
				{
					// Sketch object
					graphicObject = new GOM_Object_Sketch();
				}
				else if ( string.Compare( attribute.Value, "embedded", true) == 0 )
				{
					// Embedded object
				}
				else if ( string.Compare( attribute.Value, "linkNode", true) == 0 )
				{
					// LinkNode object
					graphicObject = new GOM_Object_LinkNode();
				}

				if ( graphicObject != null )
				{
					graphicObject.LoadFromXML(childNode, resources);
					rgObjects.Add(graphicObject);
				}
			}
		}

		/// <summary>
		/// Save links to xml writer.
		/// </summary>
		/// <param name="writer">The XML writer.</param>
		/// <param name="rgLinks">The GOM links.</param>
		public static void SaveLinksToXML(System.Xml.XmlWriter writer, GOM_Links rgLinks)
		{
			writer.WriteStartElement(GOM_TAGS.CONNECTIONS);
			for( int i=0; i<rgLinks.Count; i++ )
			{
				rgLinks[i].SaveToXML(writer);
			}
			writer.WriteEndElement();
		}
		/// <summary>
		/// Load links from XML node.
		/// </summary>
		/// <param name="writer">The XML node.</param>
		/// <param name="resources">GOM resource arrays.</param>
		/// <param name="rgLinks">GOM link list into which loaded links will be stored.</param>
		public static void LoadLinksFromXML(System.Xml.XmlNode node, GOM_ResourceArrays resources, GOM_Links rgLinks)
		{
			VerifyXmlNode(node, GOM_TAGS.CONNECTIONS);

			for ( int i=0; i<node.ChildNodes.Count; i++ )
			{
				GOM_Link link = new GOM_Link(node.ChildNodes[i], resources);
				rgLinks.Add(link);
			}
		}
	}
}
