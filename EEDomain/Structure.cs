using System;
using System.Collections;
using System.Xml;
using System.Drawing;

namespace EEDomain
{
	public class Connection
	{
		Node start = new Node();
		Node end = new Node();

		public Connection(Node s,Node e)
		{
			SetNode(s,e);
		}	
		public void SetNode(Node s,Node e)
		{
			start = s;
			end = e;
		}
		public Node GetStart()
		{
			return (start);
		}
		public Node GetEnd()
		{
			return (end);
		}
	}	
	
	public class CurrentMap
	{
		private string currentName;
		private int currentPosition;

		public CurrentMap(String c_name, int c_pos)
		{	
			SetName(c_name);
			SetPosition(c_pos);
		}

		public void SetName(string c_name)
		{
			currentName = c_name;
		}
	
		public void SetPosition(int c_pos)
		{
			currentPosition = c_pos;
		}

		public string GetName()
		{	
			return(currentName);
		}

		public int GetPosition()
		{
			return(currentPosition);
		}
	}

	public class Node
	{
		private string name;
		private string objID;
		private string ptID;
		private double voltage;

		private string deviceName;
		private bool isStart = false;
		
		public Node()
		{
		}
		public Node(string obj, string pt)
		{	
			SetObjID(obj);
			SetPtID(pt);
			
		}
		public void SetDeviceName(string n)
		{	
			deviceName = n;		
		}
		public string getDeviceName()
		{
			return deviceName;
		}

		public void setstart(bool b)
		{
			isStart = b;
		}
		public bool getstart()
		{
			return isStart;
		}
		public void SetName(string n)
		{	
			name = n;
		}
		public void SetObjID(string obj)
		{
			objID = obj;
		}
		public void SetPtID(string pt)
		{	
			ptID = pt;
		}
		public string GetName()
		{	
			return(name);				
		}
		public string GetObjID()
		{	
			return(objID);	
			
		}
		public string GetPtID()
		{	
			return(ptID);	
			
		}
		public void SetVoltage(double v)
		{	
			if( v>=0 && v<=100)
			{	
				voltage = v;
			}
			else
			{	
				voltage = -9999;
				Console.WriteLine("voltage should be positive and less than 100");
			}
		}
	
		public double GetVoltage()
		{	
			return voltage;
		}
		/*
				public double GetCurrent()
				{	
					return current;
				}
				*/
	}
	
	public class MergeNode
	{
		private string mergeNodeName;
		private ArrayList elementList;
		

		public MergeNode(string name)
		{
			elementList = new ArrayList();
			mergeNodeName = name;
		}
		public void addNode(Node n)
		{
			elementList.Add(n);
		}
		public ArrayList getList()
		{
			return elementList;
		}
		public string GetName()
		{
			return mergeNodeName;
		}
		public void SetName(string name)
		{
			mergeNodeName = name;
		}
	}
	public class Equation
	{	//private Node node;
		private Node startNode;
		private Node endNode;
		private Device device;
		private Connection conn;
		private ArrayList kclNodes;
		
		public Equation()
		{}
		
		public Equation(Device d, Node start, Node end)
		{	
			startNode = start;
			endNode = end;
			device =d;
		}

		public Equation(Connection c)
		{
			conn = c;
		}

		public Equation(ArrayList kcl)
		{
			kclNodes = kcl;
		}
        public string OutObjEquation()
        {
            if ((device.GetType().ToString()).Equals("EEDomain.Resistor"))
            {
                if (startNode.GetName() != null && endNode.GetName() != null)
                {	//Console.WriteLine("this is Resistor's equation");
                    //string equation = "V" + startNode.GetName() + " - " + "V" + endNode.GetName() + " = " + "I" + device.GetName() +"*"+ ((Resistor)device).GetResistance();
                    string equation = "V" + startNode.GetName() + " - " + "V" + endNode.GetName() + " = " + /*"-I" +*/ device.GetQName() + "*" + ((Resistor)device).GetResistance();

                    return (equation);
                }
                else
                    return null;
            }
            else if ((device.GetType().ToString()).Equals("EEDomain.VsourceDC"))
            {
                if (startNode.GetName() != null && endNode.GetName() != null)
                {	//Console.WriteLine("this is VsourceDC's equation");
                    string equation = "V" + startNode.GetName() + " - " + "V" + endNode.GetName() + " = " + ((VsourceDC)device).GetVoltage();
                    return (equation);
                }
                else
                    return null;
            }
            else
                return null;
        }

        public string OutConnEquation()
        {
            startNode = conn.GetStart();
            endNode = conn.GetEnd();

            string equation = "V" + startNode.GetName() + " = " + "V" + endNode.GetName();
            return (equation);
        }

        public string OutPtEquation()
        {
            IEnumerator kclEnum = kclNodes.GetEnumerator();
            string equation = "";
            int count = 0;
            while (kclEnum.MoveNext())
            {
                count++;
                device = (Device)kclEnum.Current;
                if (device.GetType().ToString().Equals("EEDomain.VsourceDC"))
                    return null;
                else if (equation.Equals(""))
                {
                    //equation = "I-" + device.GetName();                          //revious equation
                    equation = device.GetQName();                                   //new rename equation
                }
                else
                {
                    if (device.GetMinus() == true)
                    {
                        //equation = equation + " - " + "I" + device.GetName();       //previous equation
                        equation = equation + " - " + device.GetQName();              //new rename equation
                    }
                    else
                    {
                        //equation = equation + " + " + "I" + device.GetName();     //previous equation
                        equation = equation + " + " + device.GetQName();            //new rename equation
                    }
                }

            }
            return (equation + " = 0 ");
        }
		
	
	}

	public class ReadFromXml
	{
		private Device		device = null;
		private ArrayList	cdList; //device list
		private ArrayList	cnList; //nodes list
		private ArrayList	ccList; //connection list
		private string		objID;
		private string		type;
		private string		template;

		
		public ReadFromXml(string instr)
		{	
			string sOID = ""; //start object id
			string sPID = ""; //start point id
			string eOID = ""; //end object id
			string ePID = ""; //end point id
			
			cdList = new ArrayList();
			cnList = new ArrayList();
			ccList = new ArrayList();
				
			Node a = new Node("123","789"); //dummy
			cnList.Add(a);

			//Create XML DOM instance
			XmlDocument document = new XmlDocument();
            document.LoadXml(instr);
			//Select all graphic object elements and connections
			XmlNodeList objList1 = document.SelectNodes("//graphicObject");
			XmlNodeList objList2 = document.SelectNodes("//connection");
			
			int countDevice=0;

			int countResistor=1;
			int countCapacitor=1;
			int countInductor=1;
			int countTransitor=1;
			int countVDC=1;
			int countVAC=1;
			int countCsource=1;
			int countGround =1;
			int countJFET =1;
			int countOpamp=1;
			int countDiode=1;
				

			foreach(XmlNode objNode in objList1)
			{	
				XmlNodeReader objNodeReader = new XmlNodeReader(objNode);

				while(objNodeReader.Read())	//read thru all child nodes of this node
				{	
					if(objNodeReader.NodeType == XmlNodeType.Element)	//	***READING NODES AND DEVICE***
					{	
						if(objNodeReader.Name == "graphicObject")
						{	
							objID = objNodeReader.GetAttribute("id");	// to read the id attribute of the element
							template = objNodeReader.GetAttribute("template");	// to read the template attribute of the element, test if it is Capacitor, Resistor or others
							type = objNodeReader.GetAttribute("type");	// to read the type attribute of the element, test if it is a link node.

							switch ( template )	// typecast to specific type of device.
							{	
								case "Resistor" :
                                    device = (Resistor)new Resistor(objID, "" + countDevice);    //chester
                                    countDevice++;
                                    countResistor++;
									break;
									
								case "VsourceDC" :
                                    device = (VsourceDC)new VsourceDC(objID, ""+countDevice);       //chester
									countDevice++;
                                    countVDC++;
									break;
								
								case "Inductor" :
									//device = (Inductor) new Inductor(objID,""+countDevice);
									device = (Inductor) new Inductor(objID,""+countInductor);
									countInductor++;
									//countDevice++;
									break;
								case "Capacitor" :
									//device = (Capacitor) new Capacitor(objID,""+countDevice);
                                    device = (Capacitor)new Capacitor(objID, "" + countCapacitor);
									countCapacitor++;
									//countDevice++;
									break;
								case "VsourceAC" :
									//device = (VsourceAC) new VsourceAC(objID,""+countDevice);
									device = (VsourceAC) new VsourceAC(objID,""+countVAC);
									countVAC++;
									//countDevice++;
									break;
								case "Csource" :
									//device = (Csource) new Csource(objID,""+countDevice);
									device = (Csource) new Csource(objID,""+countCsource);
									countCsource++;
									break;
								case "Diode" :
									device = (Diode) new Diode(objID,""+countDiode);
									countDiode++;
									break;
								case "Transitor" :
									//device = (Transitor) new Transitor(objID,""+countDevice);
                                    device = (Transitor)new Transitor(objID, "" + countTransitor);
									countTransitor++;
									//countDevice++;
									break;
								case "Ground" :
									device = (Ground) new Ground(objID,""+countGround);
									countGround++;
									break;
								case "JFET" :
									device = (JFET) new JFET(objID,""+countJFET);
									countJFET++;
									break;

								case "Opamp" :
									device = (Opamp) new Opamp(objID,""+countOpamp);
									countOpamp++;
									break;
                                default:
                                    device = new Device();
                                    device.SetID(objID);
                                    break;
                            }

                            #region "Chester for new device implementation"
                                 device.convert(objNodeReader);
                            #endregion

                            if (!cdList.Contains(device))
							{
                                cdList.Add(device);
							}
							
						}
					}
				}
			}
				
			//reading connection
			int countNode=0;
			IEnumerator cnEnum;

			foreach(XmlNode objNode in objList2)
			{	
				XmlNodeReader objNodeReader = new XmlNodeReader(objNode);
				Node startNode = new Node();
				Node endNode = new Node();
				
				int cnt=0;
				
				while(objNodeReader.Read())	//read thru all child nodes of this node
				{	
					if(objNodeReader.NodeType == XmlNodeType.Element)
					{	
										
						//Console.WriteLine(cnList.Count);
						if(objNodeReader.Name == "point" && cnt%2==0 )
						{	
							sOID = objNodeReader.GetAttribute("objectID");
							sPID = objNodeReader.GetAttribute("pointID");
							
							startNode.SetObjID(sOID);
							startNode.SetPtID(sPID);							
							startNode.setstart(true);
							cnEnum = cnList.GetEnumerator();
					
							int countAll1=1;
							while(cnEnum.MoveNext())
							{	
								string currentObjID = ((Node)cnEnum.Current).GetObjID();
								string currentPtID = ((Node)cnEnum.Current).GetPtID();
								if(sOID!=null && sPID!=null)
								{
									if(currentObjID.Equals(sOID) && currentPtID.Equals(sPID))
									{
										startNode.SetName(((Node)cnEnum.Current).GetName());
										break;
									}
									else
									{
										if(cnList.Count==countAll1)
										{
											//string name = "N"+countNode;
											string name = countNode+"";
											startNode.SetName(name);
											countNode++;
											cnList.Add(startNode);
											countAll1++;
											break;
										}
										
										
									}
								}
								countAll1++;
								//Console.WriteLine("cnList count "+cnList.Count+" count 1 " + countAll1 );
								
							}
							
							cnt++;
						}
						else if(objNodeReader.Name == "point")
						{
							eOID = objNodeReader.GetAttribute("objectID");
							ePID = objNodeReader.GetAttribute("pointID");
							endNode.SetObjID(eOID);
							endNode.SetPtID(ePID);
							cnEnum = cnList.GetEnumerator();
							
							int countAll2=1;
							while(cnEnum.MoveNext())
							{	
								string currentObjID = ((Node)cnEnum.Current).GetObjID();
								string currentPtID = ((Node)cnEnum.Current).GetPtID();
								if(eOID!=null && ePID!=null)
								{
									if(currentObjID.Equals(eOID) && currentPtID.Equals(ePID))
									{
										endNode.SetName(((Node)cnEnum.Current).GetName());
										break;
									}
									else
									{
										if(cnList.Count==countAll2)
										{	
											//string name = "N"+countNode;
											string name = ""+countNode;
											endNode.SetName(name);
											countNode++;								
											cnList.Add(endNode);
																					
											countAll2++;
											break;
											
										}
									}
									
								}
								countAll2++;
								//Console.WriteLine(cnList.Count+" count 2 " + countAll2 );
							}
							if(eOID!=null && ePID!=null)
							{
								Connection connection = new Connection(startNode,endNode);
								ccList.Add(connection);
								startNode = new Node();
								endNode = new Node();
							}
							cnt=0;
							
						}
						
					}
				}
			}
			cnList.RemoveAt(0);
			cnList.TrimToSize();
			
			IEnumerator cEnum = ccList.GetEnumerator();
			IEnumerator dEnum = cdList.GetEnumerator();
			IEnumerator nEnum = cnList.GetEnumerator();

		
			//to set the device name of the node
			while(nEnum.MoveNext())
			{
				dEnum = cdList.GetEnumerator();
				while(dEnum.MoveNext())
				{	
					if(((Node)nEnum.Current).GetObjID().Equals(((Device)dEnum.Current).GetID()))
					{
						((Node)nEnum.Current).SetDeviceName(((Device)dEnum.Current).GetName());
						
						if(((Device)dEnum.Current).GetNodeCount() == 0)
						{
							((Device)dEnum.Current).SetNode1((Node)nEnum.Current);
						}
						else if(((Device)dEnum.Current).GetNodeCount() == 1)
						{
							((Device)dEnum.Current).SetNode2((Node)nEnum.Current);
						}
						else if(((Device)dEnum.Current).GetNodeCount() == 2)
						{
							((Device)dEnum.Current).SetNode3((Node)nEnum.Current);
						}

					}
					
					

				}
			}
			cEnum = ccList.GetEnumerator();
			dEnum = cdList.GetEnumerator();
			nEnum = cnList.GetEnumerator();
			//to set nodes in to the device

		}
	
		public ArrayList getDeviceList()
		{
			return(cdList);
		}
		public ArrayList GetNodeList()
		{	
			return(cnList);
		}
		public ArrayList getConnectionList()
		{
			return(ccList);
        }

        #region Chester
     
        //show the name of device and info in diagram
        public void Show_info(Graphics canvas, GOMLib.GOM_Objects m_rgObjects)
        {
            ArrayList templist = getDeviceList();
            IEnumerator enumtmp = templist.GetEnumerator();

            Font myFont = new Font("Times New Roman", 10);
            RectangleF m_boundingBox = new System.Drawing.RectangleF(0, 0, 10, 10);
            System.Drawing.Drawing2D.LinearGradientBrush myBrush = new System.Drawing.Drawing2D.LinearGradientBrush(m_boundingBox, Color.Black, Color.Black, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            try
            {
                while (enumtmp.MoveNext())
                {
                    for (int i = 0; i < m_rgObjects.Count; i++)
                    {
                        string tname, tvalue, tunit;
                        if (m_rgObjects[i].id.ToString() == ((EEDomain.Device)enumtmp.Current).GetID().ToString())
                        {
                            try
                            {
                                tname = ((EEDomain.Device)enumtmp.Current).GetQName();

                                if (tname == null)
                                {
                                    return;
                                }
                            }
                            catch (Exception)
                            {
                                tname = "D" + ((EEDomain.Device)enumtmp.Current).GetName();
                            }

                            //tvalue = ((EEDomain.Device)enumtmp.Current).GetMainValue();
                            tvalue = "";
                            tunit = ((EEDomain.Device)enumtmp.Current).GetUnit();

                            canvas.DrawString(tname + " = " + tvalue + tunit, myFont, myBrush, m_rgObjects[i].xOffset - 5, m_rgObjects[i].yOffset - 20);
                            break;
                        }
                    }
                }
            }
            catch { }
        }

        #endregion
    }
	
}
