using System;

namespace GOMLib
{
	/// <summary>
	/// The graphic object template
	/// </summary>
	public class GOM_Template : GOM_Interface_XmlPersistence
	{
		/// <summary>
		/// Create a template object from a string
		/// </summary>
		/// <param name="strXML">The string that represent the graphic object template</param>
		/// <returns>If successful, a graphic object template is returned. Otherwise, an exception will be thrown out.</returns>
		static public GOM_Template LoadFromString(string strXML)
		{
			System.Xml.XmlDocument	doc;

			doc = new System.Xml.XmlDocument();
			doc.LoadXml(strXML);

			return CreateTemplateFromXML(doc.DocumentElement);
		}
		/// <summary>
		/// Create a template object from a file
		/// </summary>
		/// <param name="fileName">The name of file which constaints definition of the graphic object template</param>
		/// <returns>If successful, a graphic object template is returned. Otherwise, an exception will be thrown out.</returns>
		static public GOM_Template LoadFromFile(string fileName)
		{
			System.Xml.XmlDocument	doc;

			doc = new System.Xml.XmlDocument();
			doc.Load(fileName);

			return CreateTemplateFromXML(doc.DocumentElement);
		}
		/// <summary>
		/// Create a template object from an XML tree
		/// </summary>
		/// <param name="node">The root node of the XML tree</param>
		/// <returns>If successful, a graphic object template is returned. Otherwise, an exception will be thrown out.</returns>
		static private GOM_Template CreateTemplateFromXML(System.Xml.XmlNode node)
		{
			if (System.String.Compare(node.Name, GOM_TAGS.GRAPHIC_TEMPLATE) != 0)
			{
				throw new Exception("Invalid input. Not a graphic template node!");
			}
			GOM_Template	template = new GOM_Template();
			template.LoadFromXML(node, null);
			return template;
		}

		public void LoadFromXML(System.Xml.XmlNode node, GOM_ResourceArrays resources)
		{
			//Load following attributes for tempalte
			//	id
			//	extScale
			//	extRotate
			//	extConnect
			//	keepAspectRatio
			for (int i = 0; i < node.Attributes.Count; i++)
			{
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.ID, true) == 0)
				{
					id = node.Attributes[i].Value;
				}
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.EXTERNAL_SCALE, true) == 0)
				{
					extScale = bool.Parse(node.Attributes[i].Value);
				}
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.EXTERNAL_ROTATE, true) == 0)
				{
					extRotate= bool.Parse(node.Attributes[i].Value);
				}
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.EXTERNAL_CONNECT, true) == 0)
				{
					extConnect= bool.Parse(node.Attributes[i].Value);
				}
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.KEEP_ASPECT_RATIO, true) == 0)
				{
					keepAspectRatio= bool.Parse(node.Attributes[i].Value);
				}
			}

			//1st pass load
			//	Basic properties of points
			//	Editing modes
			//	Drawing and filling styles
			for (int i = 0; i < node.ChildNodes.Count; i++)
			{
				if (System.String.Compare(node.ChildNodes[i].Name, GOM_TAGS.POINTS, true) == 0)
				{
					for (int j = 0; j < node.ChildNodes[i].ChildNodes.Count; j++)
					{
						LoadPointFromXML(node.ChildNodes[i].ChildNodes[j]);
					}
				}
				if (System.String.Compare(node.ChildNodes[i].Name, GOM_TAGS.EDITING_MODES, true) == 0)
				{
					for (int j = 0; j < node.ChildNodes[i].ChildNodes.Count; j++)
					{
						LoadEditingModeFromXML(node.ChildNodes[i].ChildNodes[j]);
					}
				}
				if (System.String.Compare(node.ChildNodes[i].Name, GOM_TAGS.STYLES, true) == 0)
				{
					for (int j = 0; j < node.ChildNodes[i].ChildNodes.Count; j++)
					{
						LoadStyleFromXML(node.ChildNodes[i].ChildNodes[j]);
					}
                }
                #region new_modfied
                if (System.String.Compare(node.ChildNodes[i].Name, "attribute", true) == 0)
                {
                    for (int j = 0; j < node.ChildNodes[i].ChildNodes.Count; j++)
                    {
                        LoadVarFromXML(node.ChildNodes[i].ChildNodes[j], j);
                    }
                }

                if (System.String.Compare(node.ChildNodes[i].Name, "restrictions", true) == 0)
                {
                    for (int j = 0; j < node.ChildNodes[i].ChildNodes.Count; j++)
                    {
                        LoadRestrictionFromXML(node.ChildNodes[i].ChildNodes[j], j);
                    }
                }
                #endregion
            }

			//2nd pass load
			//  Constraints of points
			//	Drawing operations
			//	Filling operations
			GOM_ResourceArrays resourceArrays = new GOM_ResourceArrays(rgPoints, rgDrawingStyles, rgFillingStyles);

			for (int i = 0; i < node.ChildNodes.Count; i++)
			{
				if (System.String.Compare(node.ChildNodes[i].Name, GOM_TAGS.POINTS, true) == 0)
				{
					for (int j = 0; j < node.ChildNodes[i].ChildNodes.Count; j++)
					{
						LoadPointConstraintFromXML(node.ChildNodes[i].ChildNodes[j]);
					}
				}
				if (System.String.Compare(node.ChildNodes[i].Name, GOM_TAGS.DRAWINGS, true) == 0)
				{
					for (int j = 0; j < node.ChildNodes[i].ChildNodes.Count; j++)
					{
						GOM_Utility.LoadDrawingFromXML(node.ChildNodes[i].ChildNodes[j], rgDrawings, resourceArrays);
					}
				}
				if (System.String.Compare(node.ChildNodes[i].Name, GOM_TAGS.FILLINGS, true) == 0)
				{
					for (int j = 0; j < node.ChildNodes[i].ChildNodes.Count; j++)
					{
						GOM_Utility.LoadFillingFromXML(node.ChildNodes[i].ChildNodes[j], rgFillings, resourceArrays);
					}
				}
			}
		}

		/// <summary>
		/// Load the constraints inside a point
		/// </summary>
		/// <param name="node">The XML node that represents the point</param>
		private void LoadPointConstraintFromXML(System.Xml.XmlNode node)
		{
			GOM_Point					pt = null;

			if (System.String.Compare(node.Name, GOM_TAGS.POINT, true) != 0) return;

			for (int i = 0; i < node.Attributes.Count; i++)
			{
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.ID, true) == 0)
				{
					pt = rgPoints[node.Attributes[i].Value];
					break;
				}
			}

			if (pt != null)
			{
				pt.LoadConstraintsFromXML( node, new GOM_ResourceArrays(rgPoints) );
			}
		}

		/// <summary>
		/// Add a point to current template and load all properties except the constraints
		/// </summary>
		/// <param name="node">The XML node that represents the point</param>
		private void LoadPointFromXML(System.Xml.XmlNode node)
		{
			GOMLib.GOM_Point	pt;

			if (System.String.Compare(node.Name, GOM_TAGS.POINT, true) == 0)
			{
				pt = new GOM_Point();
				pt.LoadFromXML( node, new GOM_ResourceArrays(rgPoints) );
				rgPoints.Add(pt);
			}
		}

		/// <summary>
		/// Add a drawing or filling style to current template
		/// </summary>
		/// <param name="node">The XML node that represents a drawing or filling style</param>
		private void LoadStyleFromXML(System.Xml.XmlNode node)
		{
			if (System.String.Compare(node.Name, GOM_TAGS.DRAWING_STYLE, true) == 0)
			{
				GOM_Style_Drawing	style = new GOM_Style_Drawing();

				style.LoadFromXML( node, null );

				rgDrawingStyles.AddAndReplace(style);
			}

			if (System.String.Compare(node.Name, GOM_TAGS.FILLING_STYLE, true) == 0)
			{
				GOM_Style_Filling	style = new GOM_Style_Filling();

				style.LoadFromXML( node, null );

				rgFillingStyles.AddAndReplace(style);
			}
		}

		/// <summary>
		/// Add an editing mode to current template
		/// </summary>
		/// <param name="node">The node that represents the editing mode</param>
		private void LoadEditingModeFromXML(System.Xml.XmlNode node)
		{
			if (System.String.Compare(node.Name, GOM_TAGS.EDITING_MODE, true) == 0)
			{
				for (int i = 0; i < node.Attributes.Count; i++)
				{
					if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.ID, true) == 0)
					{
						rgEditingModes.Add(node.Attributes[i].Value);
					}
				}
			}
        }

        //Load value from template
        #region new_modified
        private void LoadVarFromXML(System.Xml.XmlNode node, int index)
        {
            if (System.String.Compare(node.Name, "variable", true) == 0)
            {
                XMLline templine = new XMLline();
                if (node.Attributes.GetNamedItem("id") != null)
                    templine.type = node.Attributes.GetNamedItem("id").Value;
                for (int j = 0; j < node.Attributes.Count; j++)
                {
                    XMLatt temp_class = new XMLatt();
                    temp_class.aname  = node.Attributes[j].Name;
                    temp_class.acontent  = node.Attributes[j].Value;
                    templine.attributelist.Add(temp_class);
                }
                var_list.varlist.Add(templine);
            }
        }
        private void LoadRestrictionFromXML(System.Xml.XmlNode node, int index)
        {
            if (System.String.Compare(node.Name, "rules", true) == 0)
            {
                XMLline templine = new XMLline();
                if (node.Attributes.GetNamedItem("type") != null)
                    templine.type = node.Attributes.GetNamedItem("type").Value;
                for (int j = 0; j < node.Attributes.Count; j++)
                {
                    XMLatt temp_class = new XMLatt();
                    temp_class.aname = node.Attributes[j].Name;
                    temp_class.acontent = node.Attributes[j].Value;
                    templine.attributelist.Add(temp_class); //add to the line
                }
                res_list.varlist.Add(templine);
            }
        }
        #endregion



        /// <summary>
		/// Save this templete to a XML string
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
		/// Save this template to a file
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
		/// Save this tempalte to XML
		/// </summary>
		/// <param name="writer">The XML writer</param>
		public void SaveToXML(System.Xml.XmlWriter writer)
		{
			writer.WriteStartElement(GOM_TAGS.GRAPHIC_TEMPLATE);
			writer.WriteAttributeString(GOM_TAGS.ID, this.id);
			writer.WriteAttributeString(GOM_TAGS.EXTERNAL_ROTATE, this.extConnect.ToString());
			writer.WriteAttributeString(GOM_TAGS.EXTERNAL_SCALE, this.extConnect.ToString());
			writer.WriteAttributeString(GOM_TAGS.EXTERNAL_CONNECT, this.extConnect.ToString());
			writer.WriteAttributeString(GOM_TAGS.KEEP_ASPECT_RATIO, this.extConnect.ToString());

			writer.WriteStartElement(GOM_TAGS.POINTS);
			for (int i = 0; i < this.rgPoints.Count; i++)
			{
				this.rgPoints[i].SaveToXML(writer);
			}
			writer.WriteEndElement();

			writer.WriteStartElement(GOM_TAGS.DRAWINGS);
			for (int i = 0; i < this.rgDrawings.Count; i++)
			{
				this.rgDrawings[i].SaveToXML(writer);
			}
			writer.WriteEndElement();

			writer.WriteStartElement(GOM_TAGS.FILLINGS);
			for (int i = 0; i < this.rgFillings.Count; i++)
			{
				this.rgFillings[i].SaveToXML(writer);
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

			writer.WriteStartElement(GOM_TAGS.EDITING_MODES);
			for (int i = 0; i < this.rgEditingModes.Count; i++)
			{
				writer.WriteStartElement(GOM_TAGS.EDITING_MODE);
				writer.WriteAttributeString(GOM_TAGS.ID, this.rgEditingModes[i]);
				writer.WriteEndElement();
			}
			writer.WriteEndElement();

			writer.WriteEndElement();
		}
		/// <summary>
		/// The constructor of GOM_Template
		/// </summary>
		public GOM_Template()
		{
			id				= "";
			extScale		= false;
			extRotate		= false;
			extConnect		= false;
			keepAspectRatio	= false;

			rgPoints		= new GOM_Points();
			rgEditingModes	= new GOM_Strings();
			rgDrawings		= new GOM_Drawings();
			rgFillings		= new GOM_Fillings();
			rgDrawingStyles = new GOM_Drawing_Styles();
			rgFillingStyles = new GOM_Filling_Styles();

			GOM_Style_Drawing	drawing;
			GOM_Style_Filling	filling;

			drawing		= new GOM_Style_Drawing();
			drawing.id	= "default";
			rgDrawingStyles.Add(drawing);

			filling		= new GOM_Style_Filling();
			filling.id	= "default";
			rgFillingStyles.Add(filling);
		}
		/// <summary>
		/// The bounding box of this template
		/// </summary>
		System.Drawing.RectangleF BoundingBox
		{
			get
			{
				System.Drawing.RectangleF rc;
				float minX, minY, maxX, maxY;

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

				return new System.Drawing.RectangleF(minX, minY, maxX - minX, maxY - minY);
			}
		}
		/// <summary>The id of the template</summary>
		public string	id;
		/// <summary>Whether the object created from this template can be scaled by external application</summary>
		public bool		extScale;
		/// <summary>Whether the object created from this template can be rotated by external application</summary>
		public bool		extRotate;
		/// <summary>Whether the object created from this template can be connected by external application</summary>
		public bool		extConnect;
		/// <summary>Whether the aspect ration of the object created from this template should be kept by external application</summary>
		public bool		keepAspectRatio;
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

        #region new_modifed
        /// <summary>The list of variables in the template</summary>
        public Variablelist var_list = new Variablelist();
        /// <summary>The list of restriction in the template</summary>
        public Variablelist res_list = new Variablelist();
        #endregion

    }
	/// <summary>
	/// A dynamic list of GOM_Template
	/// </summary>
	public class GOM_Templates
	{
		/// <summary>
		/// The constructor of GOM_Templates
		/// </summary>
		public GOM_Templates()
		{
			rgTemplates = new System.Collections.ArrayList();
		}
		/// <summary>
		/// Return the template by its index
		/// </summary>
		public GOM_Template this[int idx]
		{
			get
			{
				return (GOM_Template)rgTemplates[idx];
			}
			set
			{
				rgTemplates[idx] = value;
			}
		}
		/// <summary>
		/// Return a template by its name.
		/// </summary>
		public GOM_Template this[string name]
		{
			get
			{
				for (int i = 0; i < rgTemplates.Count; i++)
				{
					if ( ((GOM_Template)rgTemplates[i]).id.Equals(name) )
					{
						return (GOM_Template)rgTemplates[i];
					}
				}
				return null;
			}
		}
		/// <summary>
		/// Number of templates stored in the list
		/// </summary>
		public int Count
		{
			get
			{
				return rgTemplates.Count;
			}
		}
		/// <summary>
		/// Add a template into the list
		/// </summary>
		/// <param name="val">The template being stored</param>
		public void Add(GOM_Template val)
		{
			rgTemplates.Add(val);
		}
		/// <summary>
		/// Remove a template by its index
		/// </summary>
		/// <param name="idx">The index of the template</param>
		public void RemoveAt(int idx)
		{
			rgTemplates.RemoveAt(idx);
		}
		/// <summary>
		/// Empty this list
		/// </summary>
		public void Clear()
		{
			rgTemplates.Clear();
		}
		/// <summary>
		/// Dynamic array of template
		/// </summary>
		private System.Collections.ArrayList rgTemplates;
	}
}