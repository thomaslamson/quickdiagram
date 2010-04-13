using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace QuickDiagramUI
{
    public partial class InfoForm : Form
    {

        private GOMLib.GOM_Object_Primitive m_selectedObject;
        public event DrawingInfoChanged DrawingInfoChanged;
        private CustomClass mp;
        //Initialization
        public InfoForm()
        {
            InitializeComponent();
        }

        //Assign the selected item/graphic to the information box
        public GOMLib.GOM_Object_Primitive SelectedObject
        {
            get
            {
                return m_selectedObject;
            }
            set
            {
                m_selectedObject = value;
                InitializeSelectedObject();
            }
        }

        // Initialize value with selected object
        private void InitializeSelectedObject()
        {
            mp = new CustomClass();
            pg1.SelectedObject = mp;
            mp.Add(new CustomProperty("","x", m_selectedObject.xOffset, typeof(float), false, true));
            mp.Add(new CustomProperty("","y", m_selectedObject.yOffset, typeof(float), false, true));
            GetVariable();
            pg1.Refresh();
        }

        private void OK_Click(object sender, EventArgs e)
        {
            SetVariable();
            DrawingInfoChanged();
        }


        private void plDrawTool_Paint_1(object sender, PaintEventArgs e)
        {
            System.Drawing.Drawing2D.Matrix orgTrans;

            orgTrans = e.Graphics.Transform;
            e.Graphics.TranslateTransform(plDrawTool.Width, pbDrawTool.Height);
            e.Graphics.RotateTransform(90);
            e.Graphics.DrawString("Information", this.Font, System.Drawing.Brushes.Black, 2, (plDrawTool.Width - this.Font.Height) / 2);

            e.Graphics.Transform = orgTrans;

        }

        public void GetVariable()
        {
           for (int i = 0; i < m_selectedObject.var_list.varlist.Count; i++)
           {
               GOMLib.XMLline templine = ((GOMLib.XMLline)m_selectedObject.var_list.varlist[i]);
               string t2 = templine.access("modify");
               if (t2 != "")
               {
                   string[] words = t2.Split(',');
                   for (int j = 0; j < words.Length; j++)
                   {
                       string t3 = templine.access(words[j]);
                       if (t3 != "")
                            mp.Add(new CustomProperty(templine.type ,words[j], t3, typeof(string), false, true));
                   }
               }
           }
        }

        public  void SetVariable()
        {
            for (int i = 0; i < mp.Count; i++)
            {
                switch (mp[i].SType)
                {
                    case "":
                        switch (mp[i].Name)
                        {
                            case "x":
                                m_selectedObject.xOffset = float.Parse(mp[i].Value.ToString()); break;
                            case "y":
                                m_selectedObject.yOffset = float.Parse(mp[i].Value.ToString()); break;
                        }
                        break;
                    default:
                        m_selectedObject.var_list.modify(mp[i].SType, mp[i].Name, mp[i].Value.ToString());
                        break;
                        
                }
            }
        }

        #region "3rd party grid config"
        /// <summary>
        /// CustomClass (Which is binding to property grid)
        /// </summary>
        public class CustomClass : CollectionBase, ICustomTypeDescriptor
            {
                /// <summary>
                /// Add CustomProperty to Collectionbase List
                /// </summary>
                /// <param name="Value"></param>
                public void Add(CustomProperty Value)
                {
                    base.List.Add(Value);
                }

                /// <summary>
                /// Remove item from List
                /// </summary>
                /// <param name="Name"></param>
                public void Remove(string Name)
                {
                    foreach (CustomProperty prop in base.List)
                    {
                        if (prop.Name == Name)
                        {
                            base.List.Remove(prop);
                            return;
                        }
                    }
                }

                /// <summary>
                /// Indexer
                /// </summary>
                public CustomProperty this[int index]
                {
                    get
                    {
                        return (CustomProperty)base.List[index];
                    }
                    set
                    {
                        base.List[index] = (CustomProperty)value;
                    }
                }


                #region "TypeDescriptor Implementation"
                /// <summary>
                /// Get Class Name
                /// </summary>
                /// <returns>String</returns>
                public String GetClassName()
                {
                    return TypeDescriptor.GetClassName(this, true);
                }

                /// <summary>
                /// GetAttributes
                /// </summary>
                /// <returns>AttributeCollection</returns>
                public AttributeCollection GetAttributes()
                {
                    return TypeDescriptor.GetAttributes(this, true);
                }

                /// <summary>
                /// GetComponentName
                /// </summary>
                /// <returns>String</returns>
                public String GetComponentName()
                {
                    return TypeDescriptor.GetComponentName(this, true);
                }

                /// <summary>
                /// GetConverter
                /// </summary>
                /// <returns>TypeConverter</returns>
                public TypeConverter GetConverter()
                {
                    return TypeDescriptor.GetConverter(this, true);
                }

                /// <summary>
                /// GetDefaultEvent
                /// </summary>
                /// <returns>EventDescriptor</returns>
                public EventDescriptor GetDefaultEvent()
                {
                    return TypeDescriptor.GetDefaultEvent(this, true);
                }

                /// <summary>
                /// GetDefaultProperty
                /// </summary>
                /// <returns>PropertyDescriptor</returns>
                public PropertyDescriptor GetDefaultProperty()
                {
                    return TypeDescriptor.GetDefaultProperty(this, true);
                }

                /// <summary>
                /// GetEditor
                /// </summary>
                /// <param name="editorBaseType">editorBaseType</param>
                /// <returns>object</returns>
                public object GetEditor(Type editorBaseType)
                {
                    return TypeDescriptor.GetEditor(this, editorBaseType, true);
                }

                public EventDescriptorCollection GetEvents(Attribute[] attributes)
                {
                    return TypeDescriptor.GetEvents(this, attributes, true);
                }

                public EventDescriptorCollection GetEvents()
                {
                    return TypeDescriptor.GetEvents(this, true);
                }

                public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
                {
                    PropertyDescriptor[] newProps = new PropertyDescriptor[this.Count];
                    for (int i = 0; i < this.Count; i++)
                    {
                        CustomProperty prop = (CustomProperty)this[i];
                        newProps[i] = new CustomPropertyDescriptor(ref prop, attributes);
                    }

                    return new PropertyDescriptorCollection(newProps);
                }

                public PropertyDescriptorCollection GetProperties()
                {
                    return TypeDescriptor.GetProperties(this, true);
                }

                public object GetPropertyOwner(PropertyDescriptor pd)
                {
                    return this;
                }
                #endregion

            }

            /// <summary>
            /// Custom property class 
            /// </summary>
            public class CustomProperty
            {
                private string sName = string.Empty;
                private bool bReadOnly = false;
                private bool bVisible = true;
                private object objValue = null;
                private Type type;
                private string sType;

                public CustomProperty(string sType, string sName, object value, Type type, bool bReadOnly, bool bVisible)
                {
                    this.sName = sName;
                    this.objValue = value;
                    this.type = type;
                    this.bReadOnly = bReadOnly;
                    this.bVisible = bVisible;
                    this.sType = sType;
                }

                public string SType
                {
                    get { return sType; }
                }

                public Type Type
                {
                    get { return type; }
                }

                public bool ReadOnly
                {
                    get
                    {
                        return bReadOnly;
                    }
                }

                public string Name
                {
                    get
                    {
                        return sName;
                    }
                }

                public bool Visible
                {
                    get
                    {
                        return bVisible;
                    }
                }

                public object Value
                {
                    get
                    {
                        return objValue;
                    }
                    set
                    {
                        objValue = value;
                    }
                }

            }


            /// <summary>
            /// Custom PropertyDescriptor
            /// </summary>
            public class CustomPropertyDescriptor : PropertyDescriptor
            {
                CustomProperty m_Property;
                public CustomPropertyDescriptor(ref CustomProperty myProperty, Attribute[] attrs)
                    : base(myProperty.Name, attrs)
                {
                    m_Property = myProperty;
                }

                #region PropertyDescriptor specific

                public override bool CanResetValue(object component)
                {
                    return false;
                }

                public override Type ComponentType
                {
                    get { return null; }
                }

                public override object GetValue(object component)
                {
                    return m_Property.Value;
                }

                public override string Description
                {
                    get { return m_Property.Name; }
                }

                public override string Category
                {
                    get { return string.Empty; }
                }

                public override string DisplayName
                {
                    get { return m_Property.Name; }
                }

                public override bool IsReadOnly
                {
                    get { return m_Property.ReadOnly; }
                }

                public override void ResetValue(object component)
                {
                    //Have to implement
                }

                public override bool ShouldSerializeValue(object component)
                {
                    return false;
                }

                public override void SetValue(object component, object value)
                {
                    m_Property.Value = value;
                }

                public override Type PropertyType
                {
                    get { return m_Property.Type; }
                }

                #endregion


            }
        #endregion

    }

    public delegate void DrawingInfoChanged();
}
