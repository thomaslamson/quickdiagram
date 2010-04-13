using System;
using System.Collections;


namespace EEDomain
{
	public class Device
	{
		private string name;
		private string id;
		private double current;
		private Node node1;
		private Node node2;
		private Node node3;
		private MergeNode mergeNode1;
		private MergeNode mergeNode2;
		private MergeNode mergeNode3;
		private int nodeCount;
		private bool isMinus = false; //to indicate the current is minus or plus

        private string Qname;      //the short form of the name
        private string unit;        

        #region "Method to access variable"
        public Device()
		{
			nodeCount = 0;
            SetUnit("");
		}

		public void SetID(string i)
		{
			id = i ;
		}
		public string GetID()
		{
			return id;
		}
		public void SetName(string n)
		{
			name = n;
		}
		public string GetName()
		{
			return name;
		}

		public void SetMinus(bool i)
		{	
			isMinus = i;
		}
		public bool GetMinus()
		{
			return isMinus;
		}
		public void SetCurrent(double c)
		{
			current = c ;
		}

		public double GetCurrent()
		{
			return current;
		}
		public void SetNode1(Node n1)
		{
			node1 = n1;
			nodeCount = 1;
		}
		public void SetNode2(Node n2)
		{
			node2 = n2;
			nodeCount = 2;
		}
		public void SetNode3(Node n3)
		{
			node3 = n3;
			nodeCount = 3;
		}
		
		public void SetMergeNode1(MergeNode n1)
		{
			mergeNode1 = n1;
			
		}
		public void SetMergeNode2(MergeNode n2)
		{
			mergeNode2 = n2;
			
		}
		public void SetMergeNode3(MergeNode n3)
		{
			mergeNode3 = n3;
			
		}
		
		public Node GetNode1()
		{	
			return node1;
		}
		public Node GetNode2()
		{	
			return node2;
		}
		public Node GetNode3()
		{	
			return node3;
		}
		public MergeNode GetMergeNode1()
		{	
			return mergeNode1;
		}
		public MergeNode GetMergeNode2()
		{	
			return mergeNode2;
		}
		public MergeNode GetMergeNode3()
		{	
			return mergeNode3;
		}

		public int GetNodeCount()
		{
			return nodeCount;
        }

        //Unit get/set
        public string GetUnit()
        {
            return unit;
        }
        public void SetUnit(string n)
        {
            unit = n;
        }
        //Qname get/set
        public string GetQName()
        {
            return Qname;
        }
        public void SetQName(string n)
        {
            Qname = n;
        }
        #endregion

 
        ////Convert the new class to old class
        public void convert( System.Xml.XmlNodeReader node)
        {
            try
            {
                while (node.Read())
                {
                    //collect the attribute informatiom to class information
                    #region "Get the name information"
                    //Create the Internal atom list
                    if (node.GetAttribute("id") == "info")
                    {
                        SetName(node.GetAttribute("name"));
                        SetQName(node.GetAttribute("sname"));
                        switch (name)
                        {
                            case "JFET":
                                ((EEDomain.JFET)this).SetModalName(node.GetAttribute("modalName"));
                                break;
                            case "Diode":
                                ((EEDomain.Diode)this).SetModalName(node.GetAttribute("ModalName"));
                                ((EEDomain.Diode)this).SetCathode(node.GetAttribute("Cathode"));
                                ((EEDomain.Diode)this).SetAnode(node.GetAttribute("Anode"));
                                break;
                            case "Opamp":
                                ((EEDomain.Opamp)this).SetModalName(node.GetAttribute("modalName"));
                                break;
                            case "VsourceDC":
                                ((EEDomain.VsourceDC)this).SetVoltage(int.Parse(node.GetAttribute("voltage")));
                                break;
                            case "Csource":
                                // ((EEDomain.Csource)this).SetCurrent(int.Parse(node.GetAttribute("name")));
                                break;
                            case "VsourceAC":
                                ((EEDomain.VsourceAC)this).SetVoltage((int.Parse(node.GetAttribute("name"))));
                                break;
                            case "Resistor":
                                ((EEDomain.Resistor)this).SetResistance((int.Parse(node.GetAttribute("resistance"))));
                                break;
                            case "Capacitor":
                                ((EEDomain.Capacitor)this).SetCapacitance(node.GetAttribute("capacitance"));
                                break;
                            case "Inductor":
                                ((EEDomain.Inductor)this).SetInductance(node.GetAttribute("inductance"));
                                break;
                            case "Transitor":
                                ((EEDomain.Transitor)this).SetModalName(node.GetAttribute("modalName"));
                                ((EEDomain.Transitor)this).SetNB(node.GetAttribute("NB"));
                                ((EEDomain.Transitor)this).SetNC(node.GetAttribute("NC"));
                                ((EEDomain.Transitor)this).SetNE(node.GetAttribute("NE"));
                                break;
                        }
                        if (node.GetAttribute("unit") !=null)
                            SetUnit(node.GetAttribute("unit"));
                    }
                    #endregion
                }
            }
            catch { };
        }


    }

#region "Device Class"

	public class Resistor : Device
	{	
		private double resistance;
		public Resistor(string id,string name)
		{
            SetID(id);
            SetName(name);
		}

		public void SetResistance(double r)
		{
			if(r>=0)
			{	
				resistance = r;
			}
			else
			{
				Console.WriteLine("set resistance of "+ GetName());
				resistance = int.Parse(Console.ReadLine());
				//Console.WriteLine("resistance should be positive");
			}
		}
		
		public double GetResistance()
		{	
			return resistance;
		}
	}

	public class VsourceDC : Device
	{	private double voltage;

		public VsourceDC(string id, string name)
		{
            SetName(name);
			SetID(id);	
		}

		public void SetVoltage(double v)
		{
			if(voltage>=0)
				voltage = v;
			else 
			{	
				Console.WriteLine("set voltage of "+ GetName());
				voltage = int.Parse(Console.ReadLine());
				
			}
		}

		public double GetVoltage()
		{
				return voltage;
		}
	}

	public class Csource : Device
	{
		
		public Csource(string id, string name)
		{	
			SetName(name);
			SetID(id);
				
		}


	}

	public class Inductor : Device
	{	
		private string inductance;
			
		public Inductor(string id,string name)
		{
			SetName(name);
			SetID(id);
		}

		public void SetInductance(string i)
		{	/*
			if(i>=0)
			{	
				inductance = i;
			}
			else
			{
				Console.WriteLine("set inductance of "+ GetName());
				inductance = int.Parse(Console.ReadLine());
				//Console.WriteLine("resistance should be positive");
			}
			*/
			inductance = i;
		}
		
		public string GetInductance()
		{	
			return inductance;
		}
	}

	public class Capacitor : Device
	{	
		private string capacitance;
			
		public Capacitor(string id,string name)
		{
			SetName(name);
			SetID(id);
		}

		public void SetCapacitance(string c)
		{	/*
			if(c>=0)
			{	
				capacitance = c;
			}
			else
			{
				Console.WriteLine("set capacitance of "+ GetName());
				capacitance = int.Parse(Console.ReadLine());
				//Console.WriteLine("resistance should be positive");
			}
			*/
			capacitance = c;
		}
		
		public string GetCapacitance()
		{	
			return capacitance;
		}
	}

	public class VsourceAC : Device
	{
		private double voltage;
		private double phase;

		public VsourceAC(string id, string name)
		{	
			SetName(name);
			SetID(id);
		}

		public void SetVoltage(double v)
		{
			if(voltage>=0)
				voltage = v;
			else 
			{	
				Console.WriteLine("set voltage of "+ GetName());
				voltage = int.Parse(Console.ReadLine());
			}
		}
		
		public void SetPhase(double p)
		{
			if(phase>=0)
				phase = p;
			else 
			{	
				Console.WriteLine("set phase of "+ GetName());
				phase = int.Parse(Console.ReadLine());
			}
		}

		public double GetVoltage()
		{
			return voltage;
		}
		public double GetPhase()
		{
			return phase;
		}
	}

	public class Diode : Device
	{	
		private string modalName;
		private string anode;
		private string cathode;
			
		public Diode(string id,string name)
		{
			SetName(name);
			SetID(id);
			SetModalName("");
			
		}

		public void SetModalName(string n)
		{
			modalName = n;
		}
		
		public string GetModalName()
		{	
			return modalName;
		}
		public void SetAnode(string n)
		{
			anode = n;
		}
		
		public string GetAnode()
		{	
			return anode;
		}
		public void SetCathode(string n)
		{
			cathode = n;
		}
		
		public string GetCathode()
		{	
			return cathode;
		}
	}

	public class Transitor : Device
	{	private string NC;
		private string NB;
		private string NE;
		private string modalName;
		public Transitor(string id,string name)
		{
			SetName(name);
			SetID(id);
			SetModalName("npn");
			
		}
		public void SetModalName(string n)
		{
			modalName = n;
		}
		
		public string GetModalName()
		{	
			return modalName;
		}
		public void SetNC(string n)
		{
			NC = n;
		}
		
		public string GetNC()
		{	
			return NC;
		}
		public void SetNB(string n)
		{
			NB = n;
		}
		
		public string GetNB()
		{	
			return NB;
		}
		public void SetNE(string n)
		{
			NE = n;
		}
		
		public string GetNE()
		{	
			return NE;
		}
	}

	public class Ground : Device
	{
		public Ground(string id,string name)
		{
			SetName(name);
			SetID(id);
		}
	}
	public class JFET : Device
	{
		private string modalName;
		public JFET(string id,string name)
		{
			SetName(name);
			SetID(id);
			SetModalName("");
			
		}
		public void SetModalName(string n)
		{
			modalName = n;
		}
		
		public string GetModalName()
		{	
			return modalName;
		}
	}
	
	public class Opamp : Device
	{
		private string modalName;
		public Opamp(string id,string name)
		{
			SetName(name);
			SetID(id);
			SetModalName("");
			
		}
		public void SetModalName(string n)
		{
			modalName = n;
		}
		
		public string GetModalName()
		{	
			return modalName;
		}
    }

#endregion
}