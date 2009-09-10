using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace QuickDiagramUI
{
	/// <summary>
	/// Summary description for InputForm.
	/// </summary>
	public class InputForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.PropertyGrid propertyGrid1;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private EEDomain.ReadFromXml readXml;
		private System.Windows.Forms.Button OK;
		private ObjectClass objectAttribute;


		public InputForm(EEDomain.ReadFromXml r)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			readXml = r;
			objectAttribute = new ObjectClass();
			this.propertyGrid1.SelectedObject = this.objectAttribute;
			


			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
			this.OK = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// propertyGrid1
			// 
			this.propertyGrid1.CommandsVisibleIfAvailable = true;
			this.propertyGrid1.LargeButtons = false;
			this.propertyGrid1.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.propertyGrid1.Location = new System.Drawing.Point(8, 9);
			this.propertyGrid1.Name = "propertyGrid1";
			this.propertyGrid1.Size = new System.Drawing.Size(272, 360);
			this.propertyGrid1.TabIndex = 0;
			this.propertyGrid1.Text = "propertyGrid1";
			this.propertyGrid1.ViewBackColor = System.Drawing.SystemColors.Window;
			this.propertyGrid1.ViewForeColor = System.Drawing.SystemColors.WindowText;
			this.propertyGrid1.Click += new System.EventHandler(this.propertyGrid1_Click);
			// 
			// OK
			// 
			this.OK.Location = new System.Drawing.Point(104, 388);
			this.OK.Name = "OK";
			this.OK.Size = new System.Drawing.Size(72, 27);
			this.OK.TabIndex = 1;
			this.OK.Text = "OK";
			this.OK.Click += new System.EventHandler(this.OK_Click);
			// 
			// InputForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
			this.ClientSize = new System.Drawing.Size(288, 422);
			this.Controls.Add(this.OK);
			this.Controls.Add(this.propertyGrid1);
			this.Name = "InputForm";
			this.Text = "InputForm";
			this.Load += new System.EventHandler(this.InputForm_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void propertyGrid1_Click(object sender, System.EventArgs e)
		{
			
		}

		public void SetVariable()
		{	
			ArrayList dList = readXml.getDeviceList();
			IEnumerator dEnum = dList.GetEnumerator();
			int countVdc=1;
			int countVac=1;
			int countCsource=1;
			int countR=1;
			int countC=1;
			int countL=1;
			int countT=1;
			int countJ=1;
			int countX=1;
			int countD=1;
			while(dEnum.MoveNext())
			{	//JFET
			
				if(((EEDomain.Device)dEnum.Current).GetType().ToString().Equals("EEDomain.JFET"))
				{	
					if(countJ==1)
					{
						((EEDomain.JFET)dEnum.Current).SetModalName(this.objectAttribute.J1);
					}
					if(countJ==2)
					{
						((EEDomain.JFET)dEnum.Current).SetModalName(this.objectAttribute.J2);
					}
					if(countJ==3)
					{
						((EEDomain.JFET)dEnum.Current).SetModalName(this.objectAttribute.J3);
					}
					countJ++;
				}
				//Diode
			
				if(((EEDomain.Device)dEnum.Current).GetType().ToString().Equals("EEDomain.Diode"))
				{	
					if(countD==1)
					{
						((EEDomain.Diode)dEnum.Current).SetModalName(this.objectAttribute.D1);
					}
					if(countD==2)
					{
						((EEDomain.Diode)dEnum.Current).SetModalName(this.objectAttribute.D2);
					}
					if(countD==3)
					{
						((EEDomain.Diode)dEnum.Current).SetModalName(this.objectAttribute.D3);
					}
					countD++;
				}
				//Opamp
				if(((EEDomain.Device)dEnum.Current).GetType().ToString().Equals("EEDomain.Opamp"))
				{	
					if(countX==1)
					{
						((EEDomain.Opamp)dEnum.Current).SetModalName(this.objectAttribute.X1);
					}
					if(countX==2)
					{
						((EEDomain.Opamp)dEnum.Current).SetModalName(this.objectAttribute.X2);
					}
					if(countX==3)
					{
						((EEDomain.Opamp)dEnum.Current).SetModalName(this.objectAttribute.X3);
					}
					countX++;
				}
				
				if(((EEDomain.Device)dEnum.Current).GetType().ToString().Equals("EEDomain.VsourceDC"))
				{
					if(countVdc==1)
					{
						((EEDomain.VsourceDC)dEnum.Current).SetVoltage(this.objectAttribute.Vdc1);
					}
					
					if(countVdc==2)
					{
						((EEDomain.VsourceDC)dEnum.Current).SetVoltage(this.objectAttribute.Vdc2);
					}
					countVdc++;
				}
				
				if(((EEDomain.Device)dEnum.Current).GetType().ToString().Equals("EEDomain.Csource"))
				{
					if(countCsource==1)
					{
						((EEDomain.Csource)dEnum.Current).SetCurrent(this.objectAttribute.Csource1);
					}
				
				}
			
				if(((EEDomain.Device)dEnum.Current).GetType().ToString().Equals("EEDomain.VsourceAC"))
				{
					if(countVac==1)
					{
						((EEDomain.VsourceAC)dEnum.Current).SetVoltage(this.objectAttribute.VacValue1);
						//((EEDomain.VsourceAC)dEnum.Current).SetPhase(this.objectAttribute.VacPhase1);
					}
					/*
					if(countVac==2)
					{
						((EEDomain.VsourceAC)dEnum.Current).SetVoltage(this.objectAttribute.VacValue2);
						((EEDomain.VsourceAC)dEnum.Current).SetPhase(this.objectAttribute.VacPhase2);
					}
					*/
					countVac++;
				}
				
				if(((EEDomain.Device)dEnum.Current).GetType().ToString().Equals("EEDomain.Resistor"))
				{	
					if(countR==1)
					{
						((EEDomain.Resistor)dEnum.Current).SetResistance(this.objectAttribute.R1);
					}
					if(countR==2)
					{
						((EEDomain.Resistor)dEnum.Current).SetResistance(this.objectAttribute.R2);
					}
					
					if(countR==3)
					{
						((EEDomain.Resistor)dEnum.Current).SetResistance(this.objectAttribute.R3);
					}
					
					if(countR==4)
					{
						((EEDomain.Resistor)dEnum.Current).SetResistance(this.objectAttribute.R4);
					}
					
					if(countR==5)
					{
						((EEDomain.Resistor)dEnum.Current).SetResistance(this.objectAttribute.R5);
					}
					if(countR==6)
					{
						((EEDomain.Resistor)dEnum.Current).SetResistance(this.objectAttribute.R6);
					}
					
					if(countR==7)
					{
						((EEDomain.Resistor)dEnum.Current).SetResistance(this.objectAttribute.R7);
					}
					if(countR==8)
					{
						((EEDomain.Resistor)dEnum.Current).SetResistance(this.objectAttribute.R8);
					}
					
					if(countR==9)
					{
						((EEDomain.Resistor)dEnum.Current).SetResistance(this.objectAttribute.R9);
					}
				
					countR++;
				}
	
				if(((EEDomain.Device)dEnum.Current).GetType().ToString().Equals("EEDomain.Capacitor"))
				{	
					if(countC==1)
					{
						((EEDomain.Capacitor)dEnum.Current).SetCapacitance(this.objectAttribute.C1);
					}
					if(countC==2)
					{
						((EEDomain.Capacitor)dEnum.Current).SetCapacitance(this.objectAttribute.C2);
					}
					
					if(countC==3)
					{
						((EEDomain.Capacitor)dEnum.Current).SetCapacitance(this.objectAttribute.C3);
					}
					if(countC==4)
					{
						((EEDomain.Capacitor)dEnum.Current).SetCapacitance(this.objectAttribute.C4);
					}
					if(countC==5)
					{
						((EEDomain.Capacitor)dEnum.Current).SetCapacitance(this.objectAttribute.C5);
					}
					
					countC++;
				}
			
				if(((EEDomain.Device)dEnum.Current).GetType().ToString().Equals("EEDomain.Inductor"))
				{	
					if(countL==1)
					{
						((EEDomain.Inductor)dEnum.Current).SetInductance(this.objectAttribute.L1);
					}
					if(countL==2)
					{
						((EEDomain.Inductor)dEnum.Current).SetInductance(this.objectAttribute.L2);
					}
					if(countL==3)
					{
						((EEDomain.Inductor)dEnum.Current).SetInductance(this.objectAttribute.L3);
					}
					if(countL==4)
					{
						((EEDomain.Inductor)dEnum.Current).SetInductance(this.objectAttribute.L4);
					}
					if(countL==5)
					{
						((EEDomain.Inductor)dEnum.Current).SetInductance(this.objectAttribute.L5);
					}
					
					countL++;
				}
				
				if(((EEDomain.Device)dEnum.Current).GetType().ToString().Equals("EEDomain.Transitor"))
				{	
					if(countT==1)
					{
						((EEDomain.Transitor)dEnum.Current).SetModalName(this.objectAttribute.T1);
					}
					
					if(countT==2)
					{
						((EEDomain.Transitor)dEnum.Current).SetModalName(this.objectAttribute.T2);
					}
					if(countT==3)
					{
						((EEDomain.Transitor)dEnum.Current).SetModalName(this.objectAttribute.T3);
					}
					if(countT==4)
					{
						((EEDomain.Transitor)dEnum.Current).SetModalName(this.objectAttribute.T4);
					}
					if(countT==5)
					{
						((EEDomain.Transitor)dEnum.Current).SetModalName(this.objectAttribute.T5);
					
					countT++;
					}
				}
			
			}
		}
		
		private void OK_Click(object sender, System.EventArgs e)
		{
			SetVariable();	
		}

		private void InputForm_Load(object sender, System.EventArgs e)
		{
		
		}

	}

	public class ObjectClass
	{
		double r1,r2,r3,r4,r5,r6,r7,r8,r9;
		string l1,l2,l3,l4,l5;
		string c1,c2,c3,c4,c5;
		string t1,t2,t3,t4,t5;
		string x1,x2,x3;
		string j1,j2,j3;
		string d1,d2,d3;
		double csource1;

		double vdc1,vdc2;
		double vacValue1;
        //double vacValue2;
        //double vacPhase1,vacPhase2;

		public ObjectClass()
		{	r1=0;
			vdc1=0;
			
		}
		[CategoryAttribute("Diode")]
		public string D1
		{
			get 
			{
				return d1;
			}
			set 
			{
				d1 = value;
			}
		}
		[CategoryAttribute("Diode")]
		public string D2
		{
			get 
			{
				return d2;
			}
			set 
			{
				d2 = value;
			}
		}
		[CategoryAttribute("Diode")]
		public string D3
		{
			get 
			{
				return d3;
			}
			set 
			{
				d3 = value;
			}
		}
		[CategoryAttribute("JFET")]
		public string J1
		{
			get 
			{
				return j1;
			}
			set 
			{
				j1 = value;
			}
		}
		[CategoryAttribute("JFET")]
		public string J2
		{
			get 
			{
				return j2;
			}
			set 
			{
				j2 = value;
			}
		}
		[CategoryAttribute("JFET")]
		public string J3
		{
			get 
			{
				return j3;
			}
			set 
			{
				j3 = value;
			}
		}

		[CategoryAttribute("Op-amp")]
		public string X1
		{
			get 
			{
				return x1;
			}
			set 
			{
				x1 = value;
			}
		}
		[CategoryAttribute("Op-amp")]
		public string X2
		{
			get 
			{
				return x2;
			}
			set 
			{
				x2 = value;
			}
		}
		[CategoryAttribute("Op-amp")]
		public string X3
		{
			get 
			{
				return x3;
			}
			set 
			{
				x3 = value;
			}
		}




		[CategoryAttribute("Csource")]
		public double Csource1
		{
			get 
			{
				return csource1;
			}
			set 
			{
				csource1 = value;
			}
		}
	
		[CategoryAttribute("Resistor")]
		public double R1
		{
			get 
			{
				return r1;
			}
			set 
			{
				r1 = value;
			}
		}
		[CategoryAttribute("Resistor")]
		public double R2
		{
			get 
			{
				return r2;
			}
			set 
			{
				r2 = value;
			}
		}
		
		[CategoryAttribute("Resistor")]
		public double R3
		{
			get 
			{
				return r3;
			}
			set 
			{
				r3 = value;
			}
		}
		[CategoryAttribute("Resistor")]
		public double R4
		{
			get 
			{
				return r4;
			}
			set 
			{
				r4 = value;
			}
		}
		
		[CategoryAttribute("Resistor")]
		public double R5
		{
			get 
			{
				return r5;
			}
			set 
			{
				r5 = value;
			}
		}
		[CategoryAttribute("Resistor")]
		public double R6
		{
			get 
			{
				return r6;
			}
			set 
			{
				r6 = value;
			}
		}
		
		[CategoryAttribute("Resistor")]
		public double R7
		{
			get 
			{
				return r7;
			}
			set 
			{
				r7 = value;
			}
		}
		[CategoryAttribute("Resistor")]
		public double R8
		{
			get 
			{
				return r8;
			}
			set 
			{
				r8 = value;
			}
		}

		[CategoryAttribute("Resistor")]
		public double R9
		{
			get 
			{
				return r9;
			}
			set 
			{
				r9 = value;
			}
		}

		[CategoryAttribute("Capacitor")]
		public string C1
		{
			get 
			{
				return c1;
			}
			set 
			{
				c1 = value;
			}
		}
		[CategoryAttribute("Capacitor")]
		public string C2
		{
			get 
			{
				return c2;
			}
			set 
			{
				c2 = value;
			}
		}
		
		[CategoryAttribute("Capacitor")]
		public string C3
		{
			get 
			{
				return c3;
			}
			set 
			{
				c3 = value;
			}
		}
		[CategoryAttribute("Capacitor")]
		public string C4
		{
			get 
			{
				return c4;
			}
			set 
			{
				c4 = value;
			}
		}
		[CategoryAttribute("Capacitor")]
		public string C5
		{
			get 
			{
				return c5;
			}
			set 
			{
				c5 = value;
			}
		}
				
		[CategoryAttribute("Inductor")]
		public string L1
		{
			get 
			{
				return l1;
			}
			set 
			{
				l1 = value;
			}
		}
		[CategoryAttribute("Inductor")]
		public string L2
		{
			get 
			{
				return l2;
			}
			set 
			{
				l2 = value;
			}
		}
	
		[CategoryAttribute("Inductor")]
		public string L3
		{
			get 
			{
				return l3;
			}
			set 
			{
				l3 = value;
			}
		}
		[CategoryAttribute("Inductor")]
		public string L4
		{
			get 
			{
				return l4;
			}
			set 
			{
				l4 = value;
			}
		}
		[CategoryAttribute("Inductor")]
		public string L5
		{
			get 
			{
				return l5;
			}
			set 
			{
				l5 = value;
			}
		}

		[CategoryAttribute("Transitor")]
		public string T1
		{
			get 
			{
				return t1;
			}
			set 
			{
				t1 = value;
			}
		}
	
		[CategoryAttribute("Transitor")]
		public string T2
		{
			get 
			{
				return t2;
			}
			set 
			{
				t2 = value;
			}
		}
		[CategoryAttribute("Transitor")]
		public string T3
		{
			get 
			{
				return t3;
			}
			set 
			{
				t3 = value;
			}
		}
		[CategoryAttribute("Transitor")]
		public string T4
		{
			get 
			{
				return t4;
			}
			set 
			{
				t4 = value;
			}
		}
		[CategoryAttribute("Transitor")]
		public string T5
		{
			get 
			{
				return t5;
			}
			set 
			{
				t5 = value;
			}
		}

		[CategoryAttribute("VsourceDC")]
		public double Vdc1
		{
			get 
			{
				return vdc1;
			}
			set 
			{
				vdc1 = value;
			}
		}
	
		
		[CategoryAttribute("VsourceDC")]
		public double Vdc2
		{
			get 
			{
				return vdc2;
			}
			set 
			{
				vdc2 = value;
			}
		}


		[CategoryAttribute("VsourceAC")]
		public double VacValue1
		{
			get 
			{
				return vacValue1;
			}
			set 
			{
				vacValue1 = value;
			}
		}
	
		/*
		[CategoryAttribute("VsourceAC")]
		public double VacPhase1
		{
			get 
			{
				return vacPhase1;
			}
			set 
			{
				vacPhase1 = value;
			}
		}
		
		[CategoryAttribute("VsourceAC")]
		public double VacValue2
		{
			get 
			{
				return vacValue2;
			}
			set 
			{
				vacValue2 = value;
			}
		}
		
		[CategoryAttribute("VsourceAC")]
		public double VacPhase2
		{
			get 
			{
				return vacPhase2;
			}
			set 
			{
				vacPhase2 = value;
			}
		}
		*/
	}

}
