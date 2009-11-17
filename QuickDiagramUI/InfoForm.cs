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


        //Diagram xmls
        private EEDomain.ReadFromXml readXmls;
        public void setXmls(EEDomain.ReadFromXml xmlv)
        {
            readXmls = xmlv;
        }

        // Initialize value with selected object
        private void InitializeSelectedObject()
        {

            x_value.Text = Convert.ToString(m_selectedObject.xOffset);
            y_value.Text = Convert.ToString(m_selectedObject.yOffset);
            GetVariable();

        }

        private void OK_Click(object sender, EventArgs e)
        {

            m_selectedObject.xOffset = float.Parse(x_value.Text);
            m_selectedObject.yOffset = float.Parse(y_value.Text);
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
            ArrayList dList = readXmls.getDeviceList();
            IEnumerator dEnum = dList.GetEnumerator();
          
            while (dEnum.MoveNext())
            {
                if (((EEDomain.Device)dEnum.Current).GetID().ToString() == m_selectedObject.id.ToString())
                {
                    ee_name.Text = ((EEDomain.Device)dEnum.Current).GetQName();
                    ee_value.Text = ((EEDomain.Device)dEnum.Current).GetMainValue();
                } 
            } //end while
        }

        public  void SetVariable()
        {
            ArrayList dList = readXmls.getDeviceList();
            IEnumerator dEnum = dList.GetEnumerator();

            while (dEnum.MoveNext())
            {
                if (((EEDomain.Device)dEnum.Current).GetID().ToString() == m_selectedObject.id.ToString())
                {
                    //set name;
                    ((EEDomain.Device)dEnum.Current).SetQName(ee_name.Text);

                    if (((EEDomain.Device)dEnum.Current).GetType().ToString().Equals("EEDomain.JFET"))
                    {
                        //((EEDomain.JFET)dEnum.Current).SetModalName(this.objectAttribute.J3);
                    }
                    if (((EEDomain.Device)dEnum.Current).GetType().ToString().Equals("EEDomain.Diode"))
                    {
                        //((EEDomain.Diode)dEnum.Current).SetModalName(this.objectAttribute.D3);
                    }
                    if (((EEDomain.Device)dEnum.Current).GetType().ToString().Equals("EEDomain.Opamp"))
                    {
                        // ((EEDomain.Opamp)dEnum.Current).SetModalName(this.objectAttribute.X3);
                    }

                    if (((EEDomain.Device)dEnum.Current).GetType().ToString().Equals("EEDomain.VsourceDC"))
                    {
                        ((EEDomain.VsourceDC)dEnum.Current).SetVoltage(float.Parse(ee_value.Text));
                    }
                    if (((EEDomain.Device)dEnum.Current).GetType().ToString().Equals("EEDomain.Csource"))
                    {
                        //  ((EEDomain.Csource)dEnum.Current).SetCurrent(this.objectAttribute.Csource1);
                    }

                    if (((EEDomain.Device)dEnum.Current).GetType().ToString().Equals("EEDomain.VsourceAC"))
                    {
                       //  ((EEDomain.VsourceAC)dEnum.Current).SetVoltage(this.objectAttribute.VacValue1);
                    }

                    if (((EEDomain.Device)dEnum.Current).GetType().ToString().Equals("EEDomain.Resistor"))
                    {
                        ((EEDomain.Resistor)dEnum.Current).SetResistance(float.Parse(ee_value.Text));
                    }
                    if (((EEDomain.Device)dEnum.Current).GetType().ToString().Equals("EEDomain.Capacitor"))
                    {
                        //((EEDomain.Capacitor)dEnum.Current).SetCapacitance(this.objectAttribute.C1);
                    }
                    if (((EEDomain.Device)dEnum.Current).GetType().ToString().Equals("EEDomain.Inductor"))
                    {
                        //((EEDomain.Inductor)dEnum.Current).SetInductance(this.objectAttribute.L5);
                    }
                    if (((EEDomain.Device)dEnum.Current).GetType().ToString().Equals("EEDomain.Transitor"))
                    {
                        //((EEDomain.Transitor)dEnum.Current).SetModalName(this.objectAttribute.T5);
                    }
                } //end if match id
            } //end while
        }

        private void ee_value_TextChanged(object sender, EventArgs e)
        {

        }


    }

    public delegate void DrawingInfoChanged();
}
