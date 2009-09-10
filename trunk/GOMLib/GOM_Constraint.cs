using System;

namespace GOMLib
{
	/// <summary>
	/// A dynamic list of GOM_Interface_Value
	/// </summary>
	public class GOM_Values
	{
		/// <summary>
		/// The constructor of GOM_Values
		/// </summary>
		public GOM_Values()
		{
			rgValues = new System.Collections.ArrayList();
		}
		/// <summary>
		/// Return the interface by its index
		/// </summary>
		public GOM_Interface_Value this[int idx]
		{
			get
			{
				return (GOM_Interface_Value)rgValues[idx];
			}
			set
			{
				rgValues[idx] = value;
			}
		}
		/// <summary>
		/// Number of interfaces stored in the list
		/// </summary>
		public int Count
		{
			get
			{
				return rgValues.Count;
			}
		}
		/// <summary>
		/// Add a interface into the list
		/// </summary>
		/// <param name="val">The interface being stored</param>
		public void Add(GOM_Interface_Value val)
		{
			rgValues.Add(val);
		}
		/// <summary>
		/// Remove an interface by its index
		/// </summary>
		/// <param name="idx">The index of the interface</param>
		public void RemoveAt(int idx)
		{
			rgValues.RemoveAt(idx);
		}
		/// <summary>
		/// Empty this list
		/// </summary>
		public void Clear()
		{
			rgValues.Clear();
		}
		/// <summary>
		/// Dynamic array of interfaces
		/// </summary>
		private System.Collections.ArrayList rgValues;
	}
	/// <summary>
	/// A set of constraints for an editing mode.
	/// </summary>
	public class GOM_Constraint_Set
	{
		/// <summary>
		/// The constructor of GOM_Constraint_Set
		/// </summary>
		public GOM_Constraint_Set()
		{
			rgConstraints = new System.Collections.ArrayList();
		}
		/// <summary>
		/// The name of the editing mode
		/// </summary>
		public string EditingMode;
		/// <summary>
		/// Apply all constraints in this constraint set.
		/// </summary>
		/// <remarks>Called when some changes related to this constraint set happens.</remarks>
		public void ApplyConstraints()
		{
			for (int i = 0; i < rgConstraints.Count; i++)
			{
				((GOM_Interface_Constraint)rgConstraints[i]).ApplyConstraint();
			}
		}
		/// <summary>
		/// Return the constraint by its index
		/// </summary>
		public GOM_Interface_Constraint this[int idx]
		{
			get
			{
				return (GOM_Interface_Constraint)rgConstraints[idx];
			}
			set
			{
				rgConstraints[idx] = value;
			}
		}
		/// <summary>
		/// Number of constraints stored in the set
		/// </summary>
		public int Count
		{
			get
			{
				return rgConstraints.Count;
			}
		}
		/// <summary>
		/// Add a constraint into the set
		/// </summary>
		/// <param name="constraint">The constraint being stored</param>
		public void Add(GOM_Interface_Constraint constraint)
		{
			rgConstraints.Add(constraint);
		}
		/// <summary>
		/// Remove a constraint by its index
		/// </summary>
		/// <param name="idx">The index of the constraint</param>
		public void RemoveAt(int idx)
		{
			rgConstraints.RemoveAt(idx);
		}
		/// <summary>
		/// Empty this set
		/// </summary>
		public void Clear()
		{
			rgConstraints.Clear();
		}
		/// <summary>
		/// Dynamic array of constraints
		/// </summary>
		private System.Collections.ArrayList rgConstraints;
	}
	/// <summary>
	/// A set of constraint sets.
	/// </summary>
	public class GOM_Constraint_Sets
	{
		/// <summary>
		/// The constructor of GOM_Constraint_Sets
		/// </summary>
		public GOM_Constraint_Sets()
		{
			rgConstraintSets = new System.Collections.ArrayList();
		}
		/// <summary>
		/// Return the constraint set by its index
		/// </summary>
		public GOM_Constraint_Set this[int idx]
		{
			get
			{
				return (GOM_Constraint_Set)rgConstraintSets[idx];
			}
			set
			{
				rgConstraintSets[idx] = value;
			}
		}
		/// <summary>
		/// Number of constraint sets stored in the set
		/// </summary>
		public int Count
		{
			get
			{
				return rgConstraintSets.Count;
			}
		}
		/// <summary>
		/// Add a constraint set into the set
		/// </summary>
		/// <param name="constraintSet">The constraint set being stored</param>
		public void Add(GOM_Constraint_Set constraintSet)
		{
			rgConstraintSets.Add(constraintSet);
		}
		/// <summary>
		/// Remove a constraint set by its index
		/// </summary>
		/// <param name="idx">The index of the constraint set</param>
		public void RemoveAt(int idx)
		{
			rgConstraintSets.RemoveAt(idx);
		}
		/// <summary>
		/// Empty this set
		/// </summary>
		public void Clear()
		{
			rgConstraintSets.Clear();
		}
		/// <summary>
		/// Dynamic array of constraint sets
		/// </summary>
		private System.Collections.ArrayList rgConstraintSets;
	}
	/// <summary>
	/// A dynamic list of strings
	/// </summary>
	public class GOM_Strings
	{
		/// <summary>
		/// The constructor of GOM_Strings
		/// </summary>
		public GOM_Strings()
		{
			rgStrings = new System.Collections.ArrayList();
		}
		/// <summary>
		/// Return the string by its index
		/// </summary>
		public string this[int idx]
		{
			get
			{
				return (string)rgStrings[idx];
			}
			set
			{
				rgStrings[idx] = value;
			}
		}
		/// <summary>
		/// Number of strings stored in the list
		/// </summary>
		public int Count
		{
			get
			{
				return rgStrings.Count;
			}
		}
		/// <summary>
		/// Add a string into the list
		/// </summary>
		/// <param name="str">The string being stored</param>
		public void Add(string str)
		{
			rgStrings.Add(str);
		}
		/// <summary>
		/// Remove a string by its index
		/// </summary>
		/// <param name="idx">The index of the string</param>
		public void RemoveAt(int idx)
		{
			rgStrings.RemoveAt(idx);
		}
		/// <summary>
		/// Empty the list
		/// </summary>
		public void Clear()
		{
			rgStrings.Clear();
		}
		/// <summary>
		/// The dynamic array of strings
		/// </summary>
		private System.Collections.ArrayList rgStrings;
	}
	/// <summary>
	/// The class for a point
	/// </summary>
	public class GOM_Point : GOM_Interface_XmlPersistence
	{
		/// <summary>
		/// The constructor of GOM_Point
		/// </summary>
		public GOM_Point()
		{
			id	= null;
			x	= 0;
			y	= 0;
			Controllable	= false;
			Connectable		= false;
			Constraints		= new GOM_Constraint_Sets();
		}
		/// <summary>
		/// Save the point into XML
		/// </summary>
		/// <param name="writer">The XML writer</param>
		public void SaveToXML(System.Xml.XmlWriter writer)
		{
			writer.WriteStartElement(GOM_TAGS.POINT);
			writer.WriteAttributeString(GOM_TAGS.ID, this.id);
			writer.WriteAttributeString(GOM_TAGS.X, this.x.ToString());
			writer.WriteAttributeString(GOM_TAGS.Y, this.y.ToString());
			writer.WriteAttributeString(GOM_TAGS.CONNECTABLE, this.Connectable.ToString());
			writer.WriteAttributeString(GOM_TAGS.CONTROLLABLE, this.Controllable.ToString());
			for (int i = 0; i < this.Constraints.Count; i++)
			{
				writer.WriteStartElement(GOM_TAGS.ON_POSITION_CHANGE);
				writer.WriteAttributeString(GOM_TAGS.EDITING_MODE, this.Constraints[i].EditingMode);
				for (int j = 0; j < this.Constraints[i].Count; j++)
				{
					this.Constraints[i][j].SaveToXML(writer);
				}
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
		}
		public void LoadFromXML(System.Xml.XmlNode node, GOM_ResourceArrays resources)
		{
			// Basic properties
			for (int i = 0; i < node.Attributes.Count; i++)
			{
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.ID, true) == 0)
				{
					id = node.Attributes[i].Value;
				}
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.X, true) == 0)
				{
					x = float.Parse(node.Attributes[i].Value);
				}
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.Y, true) == 0)
				{
					y = float.Parse(node.Attributes[i].Value);
				}
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.CONNECTABLE, true) == 0)
				{
					Connectable = bool.Parse(node.Attributes[i].Value);
				}
				if (System.String.Compare(node.Attributes[i].Name, GOM_TAGS.CONTROLLABLE, true) == 0)
				{
					Controllable = bool.Parse(node.Attributes[i].Value);
				}
			}
		}

		public void LoadConstraintsFromXML(System.Xml.XmlNode node, GOM_ResourceArrays resources)
		{
			GOM_Interface_Constraint	constraint;
			GOM_Constraint_Set			constraint_set;
			string						editingMode;
			for (int i = 0; i < node.ChildNodes.Count; i++)
			{
				if (System.String.Compare(node.ChildNodes[i].Name, GOM_TAGS.ON_POSITION_CHANGE, true) == 0)
				{
					editingMode = "default";
					for (int j = 0; j < node.ChildNodes[i].Attributes.Count; j++)
					{
						if (System.String.Compare(node.ChildNodes[i].Attributes[j].Name, GOM_TAGS.EDITING_MODE, true) == 0)
						{
							editingMode = node.ChildNodes[i].Attributes[j].Value;
						}
					}

					constraint_set = new GOM_Constraint_Set();
					constraint_set.EditingMode = editingMode;

					for (int j = 0; j < node.ChildNodes[i].ChildNodes.Count; j++)
					{
						constraint = GOM_Utility.LoadConstraintFromXML(node.ChildNodes[i].ChildNodes[j],resources.Points);
						if (constraint != null)
						{
							constraint_set.Add(constraint);
						}
					}

					Constraints.Add(constraint_set);
				}
			}
		}

		/// <summary>The id of this point</summary>
		public string	id;
		/// <summary>The x coordinate of this point</summary>
		public float	x;
		/// <summary>The y coordinate of this point</summary>
		public float	y;
		/// <summary>Whether this point is controllable by external application</summary>
		public bool		Controllable;
		/// <summary>Whether this point is connectable in external application</summary>
		public bool		Connectable;
		/// <summary>The constraints related to this point</summary>
		public GOM_Constraint_Sets Constraints;
	}
	/// <summary>
	/// A dynamic list of points
	/// </summary>
	public class GOM_Points
	{
		/// <summary>
		/// The constructor of GOM_Points
		/// </summary>
		public GOM_Points()
		{
			rgPoints = new System.Collections.ArrayList();
		}
		/// <summary>
		/// Return a point by its index
		/// </summary>
		public GOM_Point this[int idx]
		{
			get
			{
				return (GOM_Point)rgPoints[idx];
			}
			set
			{
				rgPoints[idx] = value;
			}
		}
		/// <summary>
		/// Return a point by its name.
		/// </summary>
		public GOM_Point this[string name]
		{
			get
			{
				for (int i = 0; i < rgPoints.Count; i++)
				{
					if (((GOM_Point)rgPoints[i]).id.Equals(name))
					{
						return (GOM_Point)rgPoints[i];
					}
				}
				return null;
			}
		}
		/// <summary>
		/// Number of points stored in the list
		/// </summary>
		public int Count
		{
			get
			{
				return rgPoints.Count;
			}
		}
		/// <summary>
		/// Add a point into the list
		/// </summary>
		/// <param name="point">The point being stored</param>
		public void Add(GOM_Point point)
		{
			rgPoints.Add(point);
		}
		public void Insert(int index, GOM_Point point)
		{
			rgPoints.Insert(index, point);
		}
		/// <summary>
		/// Remove a point by its index
		/// </summary>
		/// <param name="idx">The index of the point</param>
		public void RemoveAt(int idx)
		{
			rgPoints.RemoveAt(idx);
		}
		/// <summary>
		/// Empty the list
		/// </summary>
		public void Clear()
		{
			rgPoints.Clear();
		}
		/// <summary>
		/// The dynamic list of points
		/// </summary>
		private System.Collections.ArrayList rgPoints;
	}	
	/// <summary>
	/// A node that represents a numeric value
	/// </summary>
	public class GOM_Num_Value: GOM_Interface_Value
	{
		/// <summary>
		/// The constructor of GOM_Num_Value
		/// </summary>
		public GOM_Num_Value()
		{
			m_value	= 0;
			m_values = new GOM_Values();
		}
		/// <summary>
		/// Save the node to XML
		/// </summary>
		/// <param name="writer">The XML writer</param>
		public void SaveToXML(System.Xml.XmlWriter writer)
		{
			writer.WriteStartElement(GOM_TAGS.VALUE);
			writer.WriteString(m_value.ToString());
			writer.WriteEndElement();
		}

		public void LoadFromXML(System.Xml.XmlNode node, GOM_ResourceArrays resources)
		{
			m_value = float.Parse(node.InnerText);
		}

		/// <summary>
		/// Return the type of this node (Num_Value)
		/// </summary>
		public ValueType Type
		{
			get
			{
				return ValueType.Num_Value;
			}
		}
		/// <summary>
		/// Return the value of this node
		/// </summary>
		public float Value
		{
			get
			{
				return m_value;
			}
			set
			{
				m_value = value;
			}
		}
		/// <summary>
		/// The human readable description of the type of this node (Numeric value)
		/// </summary>
		public string TypeDescription
		{
			get
			{
				return "Numeric value";
			}
		}
		/// <summary>
		/// The human readble description of the value of this node
		/// </summary>
		public string ValueDescription
		{
			get
			{
				return m_value.ToString();
			}
		}
		/// <summary>
		/// Child node of this node (Empty)
		/// </summary>
		public GOM_Values values
		{
			get
			{
				return m_values;
			}
		}
		/// <summary>The numeric value</summary>
		private float		m_value;
		/// <summary>Empty list of children</summary>
		private GOM_Values	m_values;
	}
	/// <summary>
	/// A node that represents the x or y coordinates of a point
	/// </summary>
	public class GOM_Point_Value: GOM_Interface_Value
	{
		/// <summary>
		/// The constractor of GOM_Point_Value
		/// </summary>
		/// <param name="point">The point referred to</param>
		/// <param name="type">The type of this node (x/y)</param>
		public GOM_Point_Value(GOM_Point point, ValueType type)
		{
			m_point = point;
			m_type	= type;
			m_values= new GOM_Values();

			if ((type != ValueType.X_Value) && (type != ValueType.Y_Value))
			{
				m_type = ValueType.Error_Value;
				throw new ArgumentException("Invalid type");
			}
		}
		/// <summary>
		/// Save this node to XML
		/// </summary>
		/// <param name="writer">The XML writer</param>
		public void SaveToXML(System.Xml.XmlWriter writer)
		{
			switch (m_type)
			{
				case ValueType.X_Value:
				{
					writer.WriteStartElement(GOM_TAGS.X_VALUE);
					writer.WriteAttributeString(GOM_TAGS.POINT, m_point.id);
					writer.WriteEndElement();
					break;
				}
				case ValueType.Y_Value:
				{
					writer.WriteStartElement(GOM_TAGS.Y_VALUE);
					writer.WriteAttributeString(GOM_TAGS.POINT, m_point.id);
					writer.WriteEndElement();
					break;
				}
			}
		}
		public void LoadFromXML(System.Xml.XmlNode node, GOM_ResourceArrays resources)
		{
			// TODO: LoadFromXML
		}
		/// <summary>
		/// Return the type of this node
		/// </summary>
		public ValueType Type
		{
			get
			{
				return m_type;
			}
		}
		/// <summary>
		/// Return the value of x or y coordinate of the point
		/// </summary>
		public float Value
		{
			get
			{
				switch (m_type)
				{
					case ValueType.X_Value:
					{
						return m_point.x;
					}
					case ValueType.Y_Value:
					{
						return m_point.y;
					}
					default:
					{
						return 0;
					}
				}
			}
			set
			{
				switch (m_type)
				{
					case ValueType.X_Value:
					{
						m_point.x = value;
						break;
					}
					case ValueType.Y_Value:
					{
						m_point.y = value;
						break;
					}
				}
			}
		}
		/// <summary>
		/// The human readable description of the type of this node (X/Y Value)
		/// </summary>
		public string TypeDescription
		{
			get
			{
				switch (m_type)
				{
					case ValueType.X_Value:
					{
						return "X Value";
					}
					case ValueType.Y_Value:
					{
						return "Y Value";
					}
					default:
					{
						return "Incorrect Type";
					}
				}
			}
		}
		/// <summary>
		/// The human readable description of the value of this node (e.g. p1.x)
		/// </summary>
		public string ValueDescription
		{
			get
			{
				switch (m_type)
				{
					case ValueType.X_Value:
					{
						return m_point.id + ".x";
					}
					case ValueType.Y_Value:
					{
						return m_point.id + ".y";
					}
					default:
					{
						return "[Incorrect Type]";
					}
				}
			}
		}
		/// <summary>
		/// The child nodes of this node (Empty)
		/// </summary>
		public GOM_Values values
		{
			get
			{
				return m_values;
			}
		}
		/// <summary>
		/// The point referred to
		/// </summary>
		public GOM_Point point
		{
			get
			{
				return m_point;
			}
		}
		/// <summary>The point referred to</summary>
		private GOM_Point	m_point;
		/// <summary>Empty list of children</summary>
		private GOM_Values	m_values;
		/// <summary>The value type of this node</summary>
		private ValueType	m_type;
	}
	/// <summary>
	/// A node that represents a unary operator
	/// </summary>
	public class GOM_Unary_Value: GOM_Interface_Value
	{
		/// <summary>
		/// The constructor of GOM_Unary_Value
		/// </summary>
		/// <param name="val">The operand</param>
		/// <param name="type">The type of the operator</param>
		public GOM_Unary_Value(GOM_Interface_Value val, ValueType type)
		{
			m_values = new GOM_Values();
			m_values.Add(val);
			m_type	= type;

			if ((type != ValueType.Sin_Value) && (type != ValueType.Cos_Value) &&
				(type != ValueType.Tan_Value) && (type != ValueType.Sqrt_Value) &&
				(type != ValueType.Neg_Value))
			{
				m_type = ValueType.Error_Value;
				throw new ArgumentException("Invalid type");
			}
		}
		/// <summary>
		/// Save this node to XML
		/// </summary>
		/// <param name="writer">The XML writer</param>
		public void SaveToXML(System.Xml.XmlWriter writer)
		{
			switch (m_type)
			{
				case ValueType.Sin_Value:
				{
					writer.WriteStartElement(GOM_TAGS.SIN);
					m_values[0].SaveToXML(writer);
					writer.WriteEndElement();
					break;
				}
				case ValueType.Cos_Value:
				{
					writer.WriteStartElement(GOM_TAGS.COS);
					m_values[0].SaveToXML(writer);
					writer.WriteEndElement();
					break;
				}
				case ValueType.Tan_Value:
				{
					writer.WriteStartElement(GOM_TAGS.TAN);
					m_values[0].SaveToXML(writer);
					writer.WriteEndElement();
					break;
				}
				case ValueType.Sqrt_Value:
				{
					writer.WriteStartElement(GOM_TAGS.SQRT);
					m_values[0].SaveToXML(writer);
					writer.WriteEndElement();
					break;
				}
				case ValueType.Neg_Value:
				{
					writer.WriteStartElement(GOM_TAGS.NEGATIVE);
					m_values[0].SaveToXML(writer);
					writer.WriteEndElement();
					break;
				}
			}
		}
		public void LoadFromXML(System.Xml.XmlNode node, GOM_ResourceArrays resources)
		{
			// TODO: LoadFromXML
		}
		/// <summary>
		/// The type of the operator in this node
		/// </summary>
		public ValueType Type
		{
			get
			{
				return m_type;
			}
		}
		/// <summary>
		/// The value calculated by the operator in this node the the value of its child
		/// </summary>
		public float Value
		{
			get
			{
				switch (m_type)
				{
					case ValueType.Sin_Value:
					{
						return (float)System.Math.Sin(m_values[0].Value);
					}
					case ValueType.Cos_Value:
					{
						return (float)System.Math.Cos(m_values[0].Value);
					}
					case ValueType.Tan_Value:
					{
						return (float)System.Math.Tan(m_values[0].Value);
					}
					case ValueType.Sqrt_Value:
					{
						return (float)System.Math.Sqrt(m_values[0].Value);
					}
					case ValueType.Neg_Value:
					{
						return -(m_values[0].Value);
					}
					default:
					{
						return 0;
					}
				}
			}
			set
			{
				throw new NotSupportedException("Assignment operation is not allowed");
			}
		}
		/// <summary>
		/// The human readable description of the type of the operator in this node
		/// </summary>
		public string TypeDescription
		{
			get
			{
				switch (m_type)
				{
					case ValueType.Sin_Value:
					{
						return "Sin Value";
					}
					case ValueType.Cos_Value:
					{
						return "Cos Value";
					}
					case ValueType.Tan_Value:
					{
						return "Tan Value";
					}
					case ValueType.Sqrt_Value:
					{
						return "Sqrt Value";
					}
					case ValueType.Neg_Value:
					{
						return "Negative Value";
					}
					default:
					{
						return "Incorrect Type";
					}
				}
			}
		}
		/// <summary>
		/// The human readable description of the value in this node
		/// </summary>
		public string ValueDescription
		{
			get
			{
				switch (m_type)
				{
					case ValueType.Sin_Value:
					{
						return "Sin(" + m_values[0].ValueDescription + ")";
					}
					case ValueType.Cos_Value:
					{
						return "Cos(" + m_values[0].ValueDescription + ")";
					}
					case ValueType.Tan_Value:
					{
						return "Tan(" + m_values[0].ValueDescription + ")";
					}
					case ValueType.Sqrt_Value:
					{
						return "Sqrt(" + m_values[0].ValueDescription + ")";
					}
					case ValueType.Neg_Value:
					{
						return "-(" + m_values[0].ValueDescription + ")";
					}
					default:
					{
						return "[Incorrect Type]";
					}
				}
			}
		}
		/// <summary>
		/// The child nodes of this node
		/// </summary>
		public GOM_Values values
		{
			get
			{
				return m_values;
			}
		}
		/// <summary>List of child nodes</summary>
		private GOM_Values	m_values;
		/// <summary>The type of operator in this node</summary>
		private ValueType	m_type;
	}
	/// <summary>
	/// A node that represents a binary operator
	/// </summary>
	public class GOM_Binary_Value: GOM_Interface_Value
	{
		/// <summary>
		/// The constructor of GOM_Binary_Value
		/// </summary>
		/// <param name="val1">The first operand of this node</param>
		/// <param name="val2">The second operand of this node</param>
		/// <param name="type">The type of the operator</param>
		public GOM_Binary_Value(GOM_Interface_Value val1, GOM_Interface_Value val2, ValueType type)
		{
			m_values = new GOM_Values();
			m_values.Add(val1);
			m_values.Add(val2);
			m_type	= type;

			if ((type != ValueType.Add_Value) && (type != ValueType.Minus_Value) &&
				(type != ValueType.Divide_Value) && (type != ValueType.Multiply_Value) &&
				(type != ValueType.Max_Value) && (type != ValueType.Min_Value))
			{
				m_type = ValueType.Error_Value;
				throw new ArgumentException("Invalid type");
			}
		}
		/// <summary>
		/// Save this node to XML
		/// </summary>
		/// <param name="writer">The XML writer</param>
		public void SaveToXML(System.Xml.XmlWriter writer)
		{
			switch (m_type)
			{
				case ValueType.Add_Value:
				{
					writer.WriteStartElement(GOM_TAGS.ADD);
					m_values[0].SaveToXML(writer);
					m_values[1].SaveToXML(writer);
					writer.WriteEndElement();
					break;
				}
				case ValueType.Minus_Value:
				{
					writer.WriteStartElement(GOM_TAGS.MINUS);
					m_values[0].SaveToXML(writer);
					m_values[1].SaveToXML(writer);
					writer.WriteEndElement();
					break;
				}
				case ValueType.Divide_Value:
				{
					writer.WriteStartElement(GOM_TAGS.DIVIDE);
					m_values[0].SaveToXML(writer);
					m_values[1].SaveToXML(writer);
					writer.WriteEndElement();
					break;
				}
				case ValueType.Multiply_Value:
				{
					writer.WriteStartElement(GOM_TAGS.MULTIPLY);
					m_values[0].SaveToXML(writer);
					m_values[1].SaveToXML(writer);
					writer.WriteEndElement();
					break;
				}
				case ValueType.Max_Value:
				{
					writer.WriteStartElement(GOM_TAGS.MAX);
					m_values[0].SaveToXML(writer);
					m_values[1].SaveToXML(writer);
					writer.WriteEndElement();
					break;
				}
				case ValueType.Min_Value:
				{
					writer.WriteStartElement(GOM_TAGS.MIN);
					m_values[0].SaveToXML(writer);
					m_values[1].SaveToXML(writer);
					writer.WriteEndElement();
					break;
				}
			}
		}
		public void LoadFromXML(System.Xml.XmlNode node, GOM_ResourceArrays resources)
		{
			// TODO: LoadFromXML
		}
		/// <summary>
		/// The type of the operator
		/// </summary>
		public ValueType Type
		{
			get
			{
				return m_type;
			}
		}
		/// <summary>
		/// The value calculated by the operator in this node and the value of its children
		/// </summary>
		public float Value
		{
			get
			{
				switch (m_type)
				{
					case ValueType.Add_Value:
					{
						return m_values[0].Value + m_values[1].Value;
					}
					case ValueType.Minus_Value:
					{
						return m_values[0].Value - m_values[1].Value;
					}
					case ValueType.Divide_Value:
					{
						return m_values[0].Value / m_values[1].Value;
					}
					case ValueType.Multiply_Value:
					{
						return m_values[0].Value * m_values[1].Value;
					}
					case ValueType.Max_Value:
					{
						return System.Math.Max(m_values[0].Value, m_values[1].Value);
					}
					case ValueType.Min_Value:
					{
						return System.Math.Min(m_values[0].Value, m_values[1].Value);
					}
					default:
					{
						return 0;
					}
				}
			}
			set
			{
				throw new NotSupportedException("Assignment operation is not allowed");
			}
		}
		/// <summary>
		/// The human readable description of the type of this node
		/// </summary>
		public string TypeDescription
		{
			get
			{
				switch (m_type)
				{
					case ValueType.Add_Value:
					{
						return "Add Value";
					}
					case ValueType.Minus_Value:
					{
						return "Minus Value";
					}
					case ValueType.Divide_Value:
					{
						return "Divide Value";
					}
					case ValueType.Multiply_Value:
					{
						return "Multiply Value";
					}
					case ValueType.Max_Value:
					{
						return "Max Value";
					}
					case ValueType.Min_Value:
					{
						return "Min Value";
					}
					default:
					{
						return "[Incorrect Type]";
					}
				}
			}
		}
		/// <summary>
		/// The human description of the value of this node
		/// </summary>
		public string ValueDescription
		{
			get
			{
				switch (m_type)
				{
					case ValueType.Add_Value:
					{
						return "(" + m_values[0].ValueDescription + ") + (" + m_values[1].ValueDescription + ")";
					}
					case ValueType.Minus_Value:
					{
						return "(" + m_values[0].ValueDescription + ") - (" + m_values[1].ValueDescription + ")";
					}
					case ValueType.Divide_Value:
					{
						return "(" + m_values[0].ValueDescription + ") / (" + m_values[1].ValueDescription + ")";
					}
					case ValueType.Multiply_Value:
					{
						return "(" + m_values[0].ValueDescription + ") * (" + m_values[1].ValueDescription + ")";
					}
					case ValueType.Max_Value:
					{
						return "Max(" + m_values[0].ValueDescription + " , " + m_values[1].ValueDescription + ")";
					}
					case ValueType.Min_Value:
					{
						return "Min(" + m_values[0].ValueDescription + " , " + m_values[1].ValueDescription + ")";
					}
					default:
					{
						return "Incorrect Type";
					}
				}
			}
		}
		/// <summary>
		/// The child nodes of this node
		/// </summary>
		public GOM_Values values
		{
			get
			{
				return m_values;
			}
		}
		/// <summary>List of child nodes</summary>
		private GOM_Values	m_values;
		/// <summary>Type of the operator in this node</summary>
		private	ValueType	m_type;
	}
	/// <summary>
	/// The constraint used to resize a graphic object at the south east point
	/// </summary>
	public class GOM_Resizing_SE_Constraint: GOM_Interface_Constraint
	{
		/// <summary>
		/// The constructor of GOM_Resizing_SE_Constraint
		/// </summary>
		/// <param name="obj">The graphic object to be resized</param>
		/// <param name="pt">The south east resizing point</param>
		public GOM_Resizing_SE_Constraint(GOM_Interface_Graphic_Object obj, GOM_Point pt)
		{
			m_obj	= obj;
			m_pt	= pt;
		}
		/// <summary>
		/// Not implemented
		/// </summary>
		/// <param name="writer"></param>
		public void SaveToXML(System.Xml.XmlWriter writer)
		{
		}
		/// <summary>
		/// Not iimplemented
		/// </summary>
		/// <param name="node"></param>
		public void LoadFromXML(System.Xml.XmlNode node, GOM_ResourceArrays resources)
		{
		}
		/// <summary>
		/// Resize the graphic object
		/// </summary>
		public void ApplyConstraint()
		{
			System.Drawing.RectangleF	rc = m_obj.BoundingBox;
			System.Drawing.PointF		pt1, pt2;
			float						newWidth, newHeight;

			newWidth	= System.Math.Max(5, m_pt.x - rc.Left - 3);
			newHeight	= System.Math.Max(5, m_pt.y - rc.Top - 3);

			if (m_obj.KeepAspectRatio)
			{
				newHeight = rc.Height * (newWidth / rc.Width);
			}

			pt1 = m_obj.PointToCanvas(new System.Drawing.PointF(rc.Left, rc.Top));

			m_obj.width		= newWidth;
			m_obj.height	= newHeight;

			rc	= m_obj.BoundingBox;
			pt2 = m_obj.PointToCanvas(new System.Drawing.PointF(rc.Left, rc.Top));

			m_obj.xOffset	+= pt1.X - pt2.X;
			m_obj.yOffset	+= pt1.Y - pt2.Y;
		}
		/// <summary>
		/// Return a human readable description of the constraint
		/// </summary>
		/// <returns>The description of the constraint</returns>
		public string ConstraintDescription()
		{
			return "South-East resize constraint for graphic object " + m_obj.id;
		}
		/// <summary>
		/// Return null
		/// </summary>
		public GOM_Values values
		{
			get
			{
				return null;
			}
		}
		/// <summary>The graphic object to be resized</summary>
		GOM_Interface_Graphic_Object	m_obj;
		/// <summary>The south east resizing point</summary>
		GOM_Point						m_pt;
	}
	/// <summary>
	/// The constraint used to resize a graphic object at the north west point
	/// </summary>
	public class GOM_Resizing_NW_Constraint: GOM_Interface_Constraint
	{
		/// <summary>
		/// The constructor of GOM_Resizing_NW_Constraint
		/// </summary>
		/// <param name="obj">The graphic object to be resized</param>
		/// <param name="pt">The south east resizing point</param>
		public GOM_Resizing_NW_Constraint(GOM_Interface_Graphic_Object obj, GOM_Point pt)
		{
			m_obj	= obj;
			m_pt	= pt;
		}
		/// <summary>
		/// Not implemented
		/// </summary>
		/// <param name="writer"></param>
		public void SaveToXML(System.Xml.XmlWriter writer)
		{
		}
		/// <summary>
		/// Not iimplemented
		/// </summary>
		/// <param name="node"></param>
		public void LoadFromXML(System.Xml.XmlNode node, GOM_ResourceArrays resources)
		{
		}
		/// <summary>
		/// Resize the graphic object
		/// </summary>
		public void ApplyConstraint()
		{
			System.Drawing.RectangleF	rc = m_obj.BoundingBox;
			System.Drawing.PointF		pt1, pt2;
			float						newWidth, newHeight;

			newWidth	= System.Math.Max(5, rc.Right - m_pt.x - 3);
			newHeight	= System.Math.Max(5, rc.Bottom - m_pt.y - 3);

			if (m_obj.KeepAspectRatio)
			{
				newHeight = rc.Height * (newWidth / rc.Width);
			}

			pt1 = m_obj.PointToCanvas(new System.Drawing.PointF(rc.Right, rc.Bottom));

			m_obj.width		= newWidth;
			m_obj.height	= newHeight;

			rc	= m_obj.BoundingBox;
			pt2 = m_obj.PointToCanvas(new System.Drawing.PointF(rc.Right, rc.Bottom));

			m_obj.xOffset	+= pt1.X - pt2.X;
			m_obj.yOffset	+= pt1.Y - pt2.Y;
		}
		/// <summary>
		/// Return a human readable description of the constraint
		/// </summary>
		/// <returns>The description of the constraint</returns>
		public string ConstraintDescription()
		{
			return "North-West resize constraint for graphic object " + m_obj.id;
		}
		/// <summary>
		/// Return null
		/// </summary>
		public GOM_Values values
		{
			get
			{
				return null;
			}
		}
		/// <summary>The graphic object to be resized</summary>
		GOM_Interface_Graphic_Object	m_obj;
		/// <summary>The south east resizing point</summary>
		GOM_Point						m_pt;
	}
	/// <summary>
	/// The constraint used to resize a graphic object at the north east point
	/// </summary>
	public class GOM_Resizing_NE_Constraint: GOM_Interface_Constraint
	{
		/// <summary>
		/// The constructor of GOM_Resizing_NE_Constraint
		/// </summary>
		/// <param name="obj">The graphic object to be resized</param>
		/// <param name="pt">The south east resizing point</param>
		public GOM_Resizing_NE_Constraint(GOM_Interface_Graphic_Object obj, GOM_Point pt)
		{
			m_obj	= obj;
			m_pt	= pt;
		}
		/// <summary>
		/// Not implemented
		/// </summary>
		/// <param name="writer"></param>
		public void SaveToXML(System.Xml.XmlWriter writer)
		{
		}
		/// <summary>
		/// Not iimplemented
		/// </summary>
		/// <param name="node"></param>
		public void LoadFromXML(System.Xml.XmlNode node, GOM_ResourceArrays resources)
		{
		}
		/// <summary>
		/// Resize the graphic object
		/// </summary>
		public void ApplyConstraint()
		{
			System.Drawing.RectangleF	rc = m_obj.BoundingBox;
			System.Drawing.PointF		pt1, pt2;
			float						newWidth, newHeight;

			newWidth	= System.Math.Max(5, m_pt.x - rc.Left - 3);
			newHeight	= System.Math.Max(5, rc.Bottom - m_pt.y - 3);

			if (m_obj.KeepAspectRatio)
			{
				newHeight = rc.Height * (newWidth / rc.Width);
			}

			pt1 = m_obj.PointToCanvas(new System.Drawing.PointF(rc.Left, rc.Bottom));

			m_obj.width		= newWidth;
			m_obj.height	= newHeight;

			rc	= m_obj.BoundingBox;
			pt2 = m_obj.PointToCanvas(new System.Drawing.PointF(rc.Left, rc.Bottom));

			m_obj.xOffset	+= pt1.X - pt2.X;
			m_obj.yOffset	+= pt1.Y - pt2.Y;
		}
		/// <summary>
		/// Return a human readable description of the constraint
		/// </summary>
		/// <returns>The description of the constraint</returns>
		public string ConstraintDescription()
		{
			return "North-East resize constraint for graphic object " + m_obj.id;
		}
		/// <summary>
		/// Return null
		/// </summary>
		public GOM_Values values
		{
			get
			{
				return null;
			}
		}
		/// <summary>The graphic object to be resized</summary>
		GOM_Interface_Graphic_Object	m_obj;
		/// <summary>The south east resizing point</summary>
		GOM_Point						m_pt;
	}
	/// <summary>
	/// The constraint used to resize a graphic object at the south west point
	/// </summary>
	public class GOM_Resizing_SW_Constraint: GOM_Interface_Constraint
	{
		/// <summary>
		/// The constructor of GOM_Resizing_SW_Constraint
		/// </summary>
		/// <param name="obj">The graphic object to be resized</param>
		/// <param name="pt">The south east resizing point</param>
		public GOM_Resizing_SW_Constraint(GOM_Interface_Graphic_Object obj, GOM_Point pt)
		{
			m_obj	= obj;
			m_pt	= pt;
		}
		/// <summary>
		/// Not implemented
		/// </summary>
		/// <param name="writer"></param>
		public void SaveToXML(System.Xml.XmlWriter writer)
		{
		}
		/// <summary>
		/// Not iimplemented
		/// </summary>
		/// <param name="node"></param>
		public void LoadFromXML(System.Xml.XmlNode node, GOM_ResourceArrays resources)
		{
		}
		/// <summary>
		/// Resize the graphic object
		/// </summary>
		public void ApplyConstraint()
		{
			System.Drawing.RectangleF	rc = m_obj.BoundingBox;
			System.Drawing.PointF		pt1, pt2;
			float						newWidth, newHeight;

			newWidth	= System.Math.Max(5, rc.Right - m_pt.x - 3);
			newHeight	= System.Math.Max(5, m_pt.y - rc.Top - 3);

			if (m_obj.KeepAspectRatio)
			{
				newHeight = rc.Height * (newWidth / rc.Width);
			}

			pt1 = m_obj.PointToCanvas(new System.Drawing.PointF(rc.Right, rc.Top));

			m_obj.width		= newWidth;
			m_obj.height	= newHeight;

			rc	= m_obj.BoundingBox;
			pt2 = m_obj.PointToCanvas(new System.Drawing.PointF(rc.Right, rc.Top));

			m_obj.xOffset	+= pt1.X - pt2.X;
			m_obj.yOffset	+= pt1.Y - pt2.Y;
		}
		/// <summary>
		/// Return a human readable description of the constraint
		/// </summary>
		/// <returns>The description of the constraint</returns>
		public string ConstraintDescription()
		{
			return "South-West resize constraint for graphic object " + m_obj.id;
		}
		/// <summary>
		/// Return null
		/// </summary>
		public GOM_Values values
		{
			get
			{
				return null;
			}
		}
		/// <summary>The graphic object to be resized</summary>
		GOM_Interface_Graphic_Object	m_obj;
		/// <summary>The south east resizing point</summary>
		GOM_Point						m_pt;
	}
	/// <summary>
	/// The constraint used to rotate a graphic object
	/// </summary>
	public class GOM_Rotation_Constraint: GOM_Interface_Constraint
	{
		/// <summary>
		/// The constructor of GOM_Rotation_Constraint
		/// </summary>
		/// <param name="obj">The graphic object to be resized</param>
		/// <param name="pt">The south east resizing point</param>
		public GOM_Rotation_Constraint(GOM_Interface_Graphic_Object obj, GOM_Point pt)
		{
			m_obj	= obj;
			m_pt	= pt;
		}
		/// <summary>
		/// Not implemented
		/// </summary>
		/// <param name="writer"></param>
		public void SaveToXML(System.Xml.XmlWriter writer)
		{
		}
		/// <summary>
		/// Not iimplemented
		/// </summary>
		/// <param name="node"></param>
		public void LoadFromXML(System.Xml.XmlNode node, GOM_ResourceArrays resources)
		{
		}
		/// <summary>
		/// Resize the graphic object
		/// </summary>
		public void ApplyConstraint()
		{
			double	angle;
			System.Drawing.PointF		pt1, pt2;
			System.Drawing.RectangleF	rc;

			rc				= m_obj.BoundingBox;
			pt1				= m_obj.PointToCanvas(new System.Drawing.PointF(m_pt.x, m_pt.y));
			pt2				= m_obj.PointToCanvas(new System.Drawing.PointF((rc.Left + rc.Right) / 2, (rc.Top + rc.Bottom) / 2));
			angle			= System.Math.Atan2(pt1.X - pt2.X, pt2.Y - pt1.Y);
			m_obj.rotation	= (float)(angle / System.Math.PI) * 180;
		}
		/// <summary>
		/// Return a human readable description of the constraint
		/// </summary>
		/// <returns>The description of the constraint</returns>
		public string ConstraintDescription()
		{
			return "Rotation constraint for graphic object " + m_obj.id;
		}
		/// <summary>
		/// Return null
		/// </summary>
		public GOM_Values values
		{
			get
			{
				return null;
			}
		}
		/// <summary>The graphic object to be resized</summary>
		GOM_Interface_Graphic_Object	m_obj;
		/// <summary>The south east resizing point</summary>
		GOM_Point						m_pt;
	}
	/// <summary>
	/// A node that represent the assignment constraint
	/// </summary>
	public class GOM_Assignment_Constraint: GOM_Interface_Constraint
	{
		/// <summary>
		/// The constructor of GOM_Assignment_Constraint
		/// </summary>
		/// <param name="lvalue">The lvalue of the assignment</param>
		/// <param name="rvalue">The rvalue of the assignment</param>
		public GOM_Assignment_Constraint(GOM_Interface_Value lvalue, GOM_Interface_Value rvalue)
		{
			m_values = new GOM_Values();
			m_values.Add(lvalue);
			m_values.Add(rvalue);
		}
		/// <summary>
		/// Save this node to XML
		/// </summary>
		/// <param name="writer">The XML writer</param>
		public void SaveToXML(System.Xml.XmlWriter writer)
		{
			writer.WriteComment(this.ConstraintDescription());
			writer.WriteStartElement(GOM_TAGS.ASSIGN);
			writer.WriteStartElement(GOM_TAGS.L_VALUE);
			m_values[0].SaveToXML(writer);
			writer.WriteEndElement();
			writer.WriteStartElement(GOM_TAGS.R_VALUE);
			m_values[1].SaveToXML(writer);
			writer.WriteEndElement();
			writer.WriteEndElement();
		}
		/// <summary>
		/// Load assignment constraint from xml node.
		/// </summary>
		/// <param name="node">The xml node</param>
		public void LoadFromXML(System.Xml.XmlNode node, GOM_ResourceArrays resources)
		{
			// TODO: LoadFromXML
		}
		/// <summary>
		/// Apply the constraint specified by this node
		/// </summary>
		public void ApplyConstraint()
		{
			m_values[0].Value = m_values[1].Value;
		}
		/// <summary>
		/// Return a human readable description of the constraint on this node
		/// </summary>
		/// <returns>The description of the constraint</returns>
		public string ConstraintDescription()
		{
			return m_values[0].ValueDescription + " = " + m_values[1].ValueDescription;
		}
		/// <summary>
		/// The child nodes of this node
		/// </summary>
		public GOM_Values values
		{
			get
			{
				return m_values;
			}
		}
		/// <summary>The list of child nodes</summary>
		private GOM_Values	m_values;
	}
}