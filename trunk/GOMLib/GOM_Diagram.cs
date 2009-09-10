using System;
using System.Collections;

namespace GOMLib
{
	/// <summary>
	/// The diagram object
	/// </summary>
	public class GOM_Diagram : GOM_Interface_XmlPersistence
	{
		public string			id = Guid.NewGuid().ToString("D");
		public int				width = 1024;
		public int				height = 768;
		private GOM_Objects		m_rgObjects = new GOM_Objects();
		private GOM_Links		m_rgLinks = new GOM_Links();

		public GOM_Objects Objects
		{
			get
			{
				return m_rgObjects;
			}
		}
		public GOM_Links Links
		{
			get
			{
				return m_rgLinks;
			}
		}

		public GOM_Diagram(GOM_Objects rgObjects, GOM_Links rgLinks)
		{
			m_rgObjects = rgObjects;
			m_rgLinks = rgLinks;
		}

		public GOM_Diagram()
		{
		}

		/// <summary>
		/// Save the diagram to XmlWriter.
		/// </summary>
		/// <param name="writer">The XmlWriter.</param>
		public void SaveToXML(System.Xml.XmlWriter writer)
		{
			writer.WriteStartElement(GOM_TAGS.DIAGRAM);
			writer.WriteAttributeString(GOM_TAGS.ID, this.id);
			writer.WriteAttributeString(GOM_TAGS.WIDTH, this.width.ToString());
			writer.WriteAttributeString(GOM_TAGS.HEIGHT, this.height.ToString());

			// Templates
			GOM_Templates templates = new GOM_Templates();
			FindAllReferencedTemplates(m_rgObjects, templates);
			GOM_Utility.SaveTemplatesToXML(writer, templates);

			// Objects
			GOM_Utility.SaveObjectsToXML(writer, m_rgObjects);

			// Connections
			GOM_Utility.SaveLinksToXML(writer, m_rgLinks);

			writer.WriteEndElement();
		}

		/// <summary>
		/// Load the diagram from XmlNode.
		/// </summary>
		/// <param name="node">The XmlNode.</param>
		/// <param name="resources">GOM resource.</param>
		public void LoadFromXML(System.Xml.XmlNode node, GOM_ResourceArrays resources)
		{
			GOM_Utility.VerifyXmlNode(node, GOM_TAGS.DIAGRAM);

			GOM_Templates rgTemplates = new GOM_Templates();
			// 1st pass
			//   templates
			for ( int i=0; i<node.ChildNodes.Count; i++ )
			{
				if ( string.Compare(node.ChildNodes[i].Name, GOM_TAGS.TEMPLATES) == 0 )
				{
					GOM_Utility.LoadTemplatesFromXML(node.ChildNodes[i], resources, rgTemplates);
					break;
				}
			}

			// 2nd pass
			//   objects
			m_rgObjects.Clear();
			for ( int i=0; i<node.ChildNodes.Count; i++ )
			{
				if ( string.Compare(node.ChildNodes[i].Name, GOM_TAGS.OBJECTS) == 0 )
				{
					GOM_Utility.LoadObjectsFromXML(node.ChildNodes[i], new GOM_ResourceArrays(rgTemplates), m_rgObjects);
					break;
				}
			}

			// 3rd pass
			//   links
			m_rgLinks.Clear();
			for ( int i=0; i<node.ChildNodes.Count; i++ )
			{
				if ( string.Compare(node.ChildNodes[i].Name, GOM_TAGS.CONNECTIONS) == 0 )
				{
					GOM_Utility.LoadLinksFromXML(node.ChildNodes[i], new GOM_ResourceArrays(m_rgObjects), m_rgLinks);
					break;
				}
			}

		}

		/// <summary>
		/// Find all the templates referenced by the primitive objects.
		/// </summary>
		/// <param name="rgObjects">The primitive objects</param>
		/// <returns>All the referenced templates</returns>
		private void FindAllReferencedTemplates(GOM_Objects rgObjects, GOM_Templates rgTemplates)
		{
			for( int i=0; i<rgObjects.Count; i++ )
			{
				if ( rgObjects[i] is GOM_Object_Primitive )
				{
					GOM_Object_Primitive primitive = rgObjects[i] as GOM_Object_Primitive;
					if ( primitive != null )
					{
						if ( rgTemplates[primitive.template.id] == null )
						{
							rgTemplates.Add(primitive.template);
						}
					}
				}
				else if ( rgObjects[i] is GOM_Object_Group )
				{
					GOM_Object_Group group = rgObjects[i] as GOM_Object_Group;
					if ( group != null )
					{
						FindAllReferencedTemplates(group.rgObjects, rgTemplates);
					}
				}
			}
		}

	}
}
