using System;
using System.Collections;

namespace EEDomain
{
	
	public class GenCode
	{
		private ReadFromXml readxml;
		private ArrayList cList;
		private ArrayList dList;
		private ArrayList nList;

		private IEnumerator cEnum;
		private IEnumerator dEnum;
		private IEnumerator nEnum;

		public GenCode(ReadFromXml r)
		{
			readxml = r;
			cList = readxml.getConnectionList();
			dList = readxml.getDeviceList();
			nList = readxml.GetNodeList();
			cEnum = cList.GetEnumerator();
			dEnum = dList.GetEnumerator();
			nEnum = nList.GetEnumerator();
		

		}
	
		public ArrayList GetMergeNodeList()
		{	
			Node currentNode;
			MergeNode mNode = null;
			ArrayList currentMergeNode = new ArrayList();
			ArrayList mergeList = new ArrayList();
			IEnumerator cmnEnum;
			IEnumerator mlEnum;
			int temp;
			
			bool isMerged = false;
			
			
			while (nEnum.MoveNext())//for each node in nodeList
			{
				Node targetNode = (Node)nEnum.Current;
				
				if(mergeList.Count==0)
				{	temp = int.Parse(targetNode.GetName())+1;
					mNode = new MergeNode(""+temp);
					mNode.addNode(targetNode);
				}
				else
				{
					mlEnum = mergeList.GetEnumerator();
					while(mlEnum.MoveNext())//for each in the 
					{	
						currentMergeNode = ((MergeNode)mlEnum.Current).getList(); // the current MergeNode's list
						cmnEnum = currentMergeNode.GetEnumerator();
						while(cmnEnum.MoveNext())
						{	
							currentNode = ((Node)cmnEnum.Current);

							if(!targetNode.GetName().Equals(currentNode.GetName()))
							{	temp = int.Parse(targetNode.GetName())+1;
								mNode = new MergeNode(""+temp);
								mNode.addNode(targetNode); // add the target node as the first node
							}
							else
							{
								isMerged = true;
								break;
							}
						}
						
						if(isMerged==true)
						{
							break;
						}
					}
				}

				cEnum = cList.GetEnumerator();
				if(isMerged == false) // if the target node is not a merge node
				{
					while(cEnum.MoveNext())//for each connection in the conn list
					{	
						Node currentStart = ((Connection)cEnum.Current).GetStart();
						Node currentEnd = ((Connection)cEnum.Current).GetEnd();
									
						if(targetNode.GetObjID().Equals(currentStart.GetObjID()) && targetNode.GetPtID().Equals(currentStart.GetPtID()))
						{
							mNode.addNode(currentEnd);
						}
					
						else if(targetNode.GetObjID().Equals(currentEnd.GetObjID()) && targetNode.GetPtID().Equals(currentEnd.GetPtID()))
						{						
							mNode.addNode(currentStart);
						}
					}
				
					mergeList.Add(mNode);
					mNode = null;
				}
				
				isMerged = false;
			}
			
			mlEnum = mergeList.GetEnumerator();
			while(mlEnum.MoveNext())//for each in the 
			{
				MergeNode current = (MergeNode)mlEnum.Current;
				currentMergeNode = ((MergeNode)mlEnum.Current).getList(); // the current MergeNode's list
				cmnEnum = currentMergeNode.GetEnumerator();
				while(cmnEnum.MoveNext())
				{	
					currentNode = ((Node)cmnEnum.Current);
					nEnum = nList.GetEnumerator();
					while(nEnum.MoveNext())
					{
						if(((Node)nEnum.Current).GetPtID().Equals(currentNode.GetPtID()) && ((Node)nEnum.Current).GetObjID().Equals(currentNode.GetObjID()))
						{	dEnum = dList.GetEnumerator();
							while(dEnum.MoveNext())
							{
								if(((Device)dEnum.Current).GetID().Equals(currentNode.GetObjID()))
								{
									if(((Device)dEnum.Current).GetType().ToString().Equals("EEDomain.Ground"))
									{
										current.SetName("0");
									}
								}
							}
						}
					}
				}
			}

			return mergeList;	
	
		}

		public void SetDeviceMergeNode()
		{	
			ArrayList MergeNodeList = GetMergeNodeList();
			Device currentDevice = new Device();
			Node deviceNode1 = new Node();
			Node deviceNode2 = new Node();
			Node deviceNode3 = new Node();
			Node currentNode = new Node();
			MergeNode mNode;
			
			dEnum = dList.GetEnumerator();
			IEnumerator mnlEnum = MergeNodeList.GetEnumerator();
			IEnumerator mnEnum = MergeNodeList.GetEnumerator();

			//for each device in the device list
			while(dEnum.MoveNext())
			{
				currentDevice = (Device)dEnum.Current;
				deviceNode1 = currentDevice.GetNode1();
				deviceNode2 = currentDevice.GetNode2();
				
				//for each mergeNode in the mergeNodeList
				mnlEnum = MergeNodeList.GetEnumerator();
				while(mnlEnum.MoveNext())
				{
					mNode = (MergeNode)mnlEnum.Current;
					
					mnEnum = mNode.getList().GetEnumerator();
					//for each node in the mergeNode
					while(mnEnum.MoveNext())
					{	
						currentNode = (Node)mnEnum.Current;
						if(currentNode.GetName().Equals(deviceNode1.GetName()))
						{
							currentDevice.SetMergeNode1(mNode);
						}
						if(currentDevice.GetNode2()!=null)
						{
							if(currentNode.GetName().Equals(deviceNode2.GetName()))
							{
								currentDevice.SetMergeNode2(mNode);
							}
						}
						if(currentDevice.GetNode3()!=null)
						{
							deviceNode3 = currentDevice.GetNode3();
						
							if(currentNode.GetName().Equals(deviceNode3.GetName()))
							{
								currentDevice.SetMergeNode3(mNode);
							}
						}
								
					}
				}
			}

			
		}
	
		public string Output()
		{	
			string output = "";
			string sumDeviceDes = "";
			string sumCalculateVoltageDes = "";
			string sumCalculateCurrentDes = "";
			string deviceDes = "";
			string calculateVoltageDes = "";
			string calculateCurrentDes ="";
			string type = "";
			//int temp1,temp2,temp3;
			Device currentDevice;
			

			SetDeviceMergeNode();
			dEnum = dList.GetEnumerator();
			while(dEnum.MoveNext())
			{
				currentDevice = (Device)dEnum.Current;
				type = currentDevice.ToString();
				switch (type)
				{
					case "EEDomain.VsourceDC" : //node placement
						//temp1=int.Parse(currentDevice.GetMergeNode1().GetName())+1;
						//temp2=int.Parse(currentDevice.GetMergeNode2().GetName())+1;
						deviceDes = "Vcc"+currentDevice.GetName()+" "+currentDevice.GetMergeNode1().GetName()+" "+currentDevice.GetMergeNode2().GetName()+" DC "+ ((VsourceDC)currentDevice).GetVoltage()+"\n";
						//deviceDes = "V"+currentDevice.GetName()+" "+temp1+" "+temp2+" DC "+ ((VsourceDC)currentDevice).GetVoltage()+"\n";
						//calculateVoltageDes = ".DC " +"V"+currentDevice.GetName()+" "+((VsourceDC)currentDevice).GetVoltage()+" "+((VsourceDC)currentDevice).GetVoltage()+" " +"1\n";
						break;
					
					case "EEDomain.VsourceAC" : //node placement
						//temp1=int.Parse(currentDevice.GetMergeNode1().GetName())+1;
						//temp2=int.Parse(currentDevice.GetMergeNode2().GetName())+1;
						//deviceDes = "V"+currentDevice.GetName()+" "+temp1+" "+temp2+" AC "+ ((VsourceAC)currentDevice).GetVoltage()+" "+((VsourceAC)currentDevice).GetPhase()+"\n";
						//deviceDes = "V"+currentDevice.GetName()+" "+temp1+" "+temp2+" AC "+ ((VsourceAC)currentDevice).GetVoltage()+" "+((VsourceAC)currentDevice).GetPhase()+"\n";
						deviceDes = "Vin"+currentDevice.GetName()+" "+currentDevice.GetMergeNode1().GetName()+" "+currentDevice.GetMergeNode2().GetName()+" AC "+ ((VsourceAC)currentDevice).GetVoltage()+"\n";//+" "+((VsourceAC)currentDevice).GetPhase()+"\n";
						break;
					
					case "EEDomain.Csource" : //node placement
						//temp1=int.Parse(currentDevice.GetMergeNode1().GetName())+1;
						//temp2=int.Parse(currentDevice.GetMergeNode2().GetName())+1;
						deviceDes = "I"+currentDevice.GetName()+" "+currentDevice.GetMergeNode1().GetName()+" "+currentDevice.GetMergeNode2().GetName()+" DC "+ ((Csource)currentDevice).GetCurrent()+"\n";
						break;
					
					case "EEDomain.Resistor" :
						//temp1=int.Parse(currentDevice.GetMergeNode1().GetName())+1;
						//temp2=int.Parse(currentDevice.GetMergeNode2().GetName())+1;
						deviceDes = "R"+currentDevice.GetName()+" "+currentDevice.GetMergeNode1().GetName()+" "+currentDevice.GetMergeNode2().GetName() +" "+ ((Resistor)currentDevice).GetResistance()+"\n";
						//calculateVoltageDes = ".PRINT DC " + "V(R"+currentDevice.GetName()+")\n"; 
						//calculateCurrentDes = ".PRINT DC " + "I(R"+currentDevice.GetName()+")\n";;
						break;
					
					case "EEDomain.Capacitor" :
						//temp1=int.Parse(currentDevice.GetMergeNode1().GetName())+1;
						//temp2=int.Parse(currentDevice.GetMergeNode2().GetName())+1;
						deviceDes = "C"+currentDevice.GetName()+" "+currentDevice.GetMergeNode1().GetName()+" "+currentDevice.GetMergeNode2().GetName() +" "+ ((Capacitor)currentDevice).GetCapacitance()+"\n";
						break;
					
					case "EEDomain.Inductor" :
						//temp1=int.Parse(currentDevice.GetMergeNode1().GetName())+1;
						//temp2=int.Parse(currentDevice.GetMergeNode2().GetName())+1;
						//deviceDes = "L"+currentDevice.GetName()+" "+temp1+" "+temp2 +" "+ ((Inductor)currentDevice).GetInductance()+"\n";
						deviceDes = "L"+currentDevice.GetName()+" "+currentDevice.GetMergeNode1().GetName()+" "+currentDevice.GetMergeNode2().GetName() +" "+ ((Inductor)currentDevice).GetInductance()+"\n";
						break;
					
					case "EEDomain.Diode" :
						//temp1=int.Parse(currentDevice.GetMergeNode1().GetName())+1;
						//temp2=int.Parse(currentDevice.GetMergeNode2().GetName())+1;
						deviceDes = "D"+currentDevice.GetName()+" "+currentDevice.GetMergeNode1().GetName()+" "+currentDevice.GetMergeNode2().GetName() +" "+((Diode)currentDevice).GetModalName() + "\n";
						break;
					case "EEDomain.Transitor" :
						//temp1=int.Parse(currentDevice.GetMergeNode1().GetName())+1;
						//temp2=int.Parse(currentDevice.GetMergeNode2().GetName())+1;
						//temp3=int.Parse(currentDevice.GetMergeNode3().GetName())+1;
						//deviceDes = "Q"+currentDevice.GetName()+" "+((Transitor)currentDevice).GetNC()+" "+((Transitor)currentDevice).GetNB() + ((Transitor)currentDevice).GetNE() +" ModalName" + "\n" +
						//deviceDes = "Q"+currentDevice.GetName()+" "+temp2+" "+temp1 +" "+temp3+" ModalName"+"\n" +
						deviceDes = "Q"+currentDevice.GetName()+" "+currentDevice.GetMergeNode2().GetName()+" "+currentDevice.GetMergeNode1().GetName() +" "+currentDevice.GetMergeNode3().GetName()+" TModel"+"\n" +
						".Model TModel " + ((Transitor)currentDevice).GetModalName()+ "\n";
						break;
					
					case "EEDomain.Opamp" :
						deviceDes = "X"+currentDevice.GetName()+" "+currentDevice.GetMergeNode1().GetName()+" "+currentDevice.GetMergeNode2().GetName() +" "+currentDevice.GetMergeNode3().GetName()+ " "+((Opamp)currentDevice).GetModalName() +"\n";
						break;
					case "EEDomain.JFET" :
						deviceDes = "J"+currentDevice.GetName()+" "+currentDevice.GetMergeNode2().GetName()+" "+currentDevice.GetMergeNode1().GetName() +" "+currentDevice.GetMergeNode3().GetName()+((JFET)currentDevice).GetModalName()+"\n";
						break;
							

			   
				}
		
				sumDeviceDes = sumDeviceDes + deviceDes;
				sumCalculateVoltageDes = sumCalculateVoltageDes + calculateVoltageDes;
				sumCalculateCurrentDes = sumCalculateCurrentDes + calculateCurrentDes;
				deviceDes = "";
				calculateVoltageDes="";
				calculateCurrentDes="";
			}
			output = sumDeviceDes + sumCalculateVoltageDes +sumCalculateCurrentDes;

			return output;
		}
	}
}


