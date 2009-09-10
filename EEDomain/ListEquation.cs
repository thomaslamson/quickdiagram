using System;
using System.Collections;

namespace EEDomain
{
	public class ListEquation
	{
		private ArrayList cList, dList, nList, eList, currentList, nList2, kclNodes;
		private IEnumerator cEnum, dEnum, nEnum, eEnum, nEnum2;
		private ReadFromXml readxml;
		private Node start, end, currentNodeStart, currentNodeEnd, targetNode, currentStart, currentEnd;
		private int count, countCurrent;
		private string currentName, currentObjID;
		private Equation equ;
	

		public ListEquation(ReadFromXml r)
		{
			readxml = r;
			cList = readxml.getConnectionList();
			dList = readxml.getDeviceList();
			nList = readxml.GetNodeList();
			eList = new ArrayList();
			currentList = new ArrayList();

			cEnum = cList.GetEnumerator();
			dEnum = dList.GetEnumerator();
			nEnum = nList.GetEnumerator();
			
			start = new Node();
			end = new Node();
	
		}

		public string Output()
		{
			while (dEnum.MoveNext()) //for each object in the device list
			{					
				currentName = ((Device)dEnum.Current).GetName(); //get the device name
				currentObjID = ((Device)dEnum.Current).GetID(); // get the device id
				currentNodeStart = null; 
				currentNodeEnd = null;
				
				while(nEnum.MoveNext()) //for each object in the node list
				{					
					if( currentObjID.Equals(((Node)nEnum.Current).GetObjID())) // check the node belongs to the current device
					{	
						if(((Node)nEnum.Current).getstart() == true)// check the node is a start node
						{
							currentNodeStart = ((Node)nEnum.Current);// assign this node to node_start
						}

						else if(((Node)nEnum.Current).getstart() == false)
						{
							currentNodeEnd = ((Node)nEnum.Current);
						}
						
						// to set the matrix Y and matrix I based on device equations
						if(currentNodeStart!=null && currentNodeEnd!=null)
						{							
							Device device = ((Device)dEnum.Current);
							
							if((device.GetType().ToString()).Equals("EEDomain.Resistor"))
							{
								if(currentNodeStart.GetName()!=null && currentNodeEnd.GetName()!=null)
								{								
									//CurrentMap cMap = new CurrentMap(device.GetName(),countCurrent);
									//currentList.Add(cMap);
									countCurrent++;
									equ = new Equation(((Device)dEnum.Current),currentNodeStart,currentNodeEnd);
									eList.Add(equ);
								}
						
							}
							else if((device.GetType().ToString()).Equals("EEDomain.VsourceDC"))
							{
								if(currentNodeStart.GetName()!=null && currentNodeEnd.GetName()!=null)
								{							
									equ = new Equation(((Device)dEnum.Current),currentNodeStart,currentNodeEnd);
									eList.Add(equ);
								}
							}
			
							currentNodeStart = null;
							currentNodeEnd = null;
						
							count++;
							
							break;
							
						}
						

					}
					

				}
				
				nEnum = nList.GetEnumerator();
				

			}

	
			//assume all connections are drawn accordingly
			//the equations of the test should be
			
			eEnum = eList.GetEnumerator();
			cEnum = cList.GetEnumerator();
			dEnum = dList.GetEnumerator();
			nEnum = nList.GetEnumerator();
			
			string deviceEquation="***Device Equations***\n";
			string connEquation="***Connection Equations***\n";
			string pointEquation="***Point Equations***\n";
			
			
			//for the devices
			Console.WriteLine("***Device Equations***");
			while (eEnum.MoveNext())
			{
					deviceEquation += ((Equation)eEnum.Current).OutObjEquation() + "\n";
				//Console.WriteLine(((Equation)eEnum.Current).OutObjEquation());
				//	Console.WriteLine("");
			}
			//for the connections
			Console.WriteLine("***Connection Equations***");
			while (cEnum.MoveNext())
			{
				equ = new Equation((Connection)cEnum.Current);
				connEquation += equ.OutConnEquation() + "\n";
				//Console.WriteLine(equ.OutConnEquation());
				count++;
			}

			cEnum = cList.GetEnumerator();

			dEnum = dList.GetEnumerator();
			nEnum = nList.GetEnumerator();
			
			nList2 = (ArrayList)nList.Clone();
			nEnum2 = nList2.GetEnumerator();

			//***********************************************************************
			ArrayList appearStart = new ArrayList();
			ArrayList appearEnd = new ArrayList();

			IEnumerator asEnum;
			IEnumerator aeEnum;

			//for the pt
			Console.WriteLine("***Node Equations***");
			kclNodes = new ArrayList();
			bool checkMerge = false;
			bool checkNode = false;
			while (nEnum.MoveNext())//for each node in nodeList
			{
				targetNode = (Node)nEnum.Current;

				while(cEnum.MoveNext())//for each connection in the conn list
				{	
					currentStart = ((Connection)cEnum.Current).GetStart();
					currentEnd = ((Connection)cEnum.Current).GetEnd();
					//if the target is the start node
					if(targetNode.GetObjID().Equals(currentStart.GetObjID()) && targetNode.GetPtID().Equals(currentStart.GetPtID()))
					{	//search in the device list to check the node belong to which device
						appearStart.Add(targetNode);
						aeEnum = appearEnd.GetEnumerator();
						while(aeEnum.MoveNext())
						{
							Node tempEnd = ((Connection)cEnum.Current).GetEnd();
							Node tempAppearEnd = (Node)aeEnum.Current;
							if(tempEnd.GetObjID().Equals(tempAppearEnd.GetObjID()) && tempEnd.GetPtID().Equals(tempAppearEnd.GetPtID()))
							{
								checkMerge = true;
								checkNode = true;
							}
						}
						if(!checkMerge)
						{ 

							dEnum = dList.GetEnumerator();
							while(dEnum.MoveNext())
							{
								if(currentEnd.GetObjID().Equals(((Device)dEnum.Current).GetID()))
								{
									((Device)dEnum.Current).SetMinus(false);
									kclNodes.Add((Device)dEnum.Current);
								}
							}
						}
						checkMerge = false;
					}
						//***********************************************************************
						//if the target is the start node
					else if(targetNode.GetObjID().Equals(currentEnd.GetObjID()) && targetNode.GetPtID().Equals(currentEnd.GetPtID()))
					{
						appearEnd.Add(targetNode);
						asEnum = appearStart.GetEnumerator();
						while(asEnum.MoveNext())
						{
							Node tempStart = ((Connection)cEnum.Current).GetStart();
							Node tempAppearStart = (Node)asEnum.Current;
							if(tempStart.GetObjID().Equals(tempAppearStart.GetObjID()) && tempStart.GetPtID().Equals(tempAppearStart.GetPtID()))
							{
								checkMerge = true;
								checkNode = true;							
							}
						}
						if(!checkMerge)
						{ 
							dEnum = dList.GetEnumerator();
							while(dEnum.MoveNext())
							{
								if(currentStart.GetObjID().Equals(((Device)dEnum.Current).GetID()))
								{
									((Device)dEnum.Current).SetMinus(true);
									kclNodes.Add((Device)dEnum.Current);
								}
							}
						}
						checkMerge = false;
					}
				}
				//*****************************************************************************************
				while(nEnum2.MoveNext())
				{	
					if((targetNode.GetObjID()).Equals(((Node)nEnum2.Current).GetObjID()))
					{	
						if(!targetNode.Equals((Node)nEnum2.Current))
						{
							if(!checkNode)//check if the equation need to have this entry
							{
								dEnum = dList.GetEnumerator();
								while(dEnum.MoveNext())
								{
									if(((Node)nEnum2.Current).GetObjID().Equals(((Device)dEnum.Current).GetID()))
									{
										((Device)dEnum.Current).SetMinus(true);
										kclNodes.Add((Device)dEnum.Current);
									}
								}
							}
							checkNode = false;
						}						
					}

				}
				//*****************************************************************************************
				nEnum2 = nList2.GetEnumerator();
				cEnum = cList.GetEnumerator();

				if(kclNodes.Count!=0)
				{
					equ = new Equation(kclNodes);
				
					if(equ.OutPtEquation()!=null)
					{
						pointEquation += equ.OutPtEquation()+"\n";
					}
				}
				kclNodes = new ArrayList();
			
			}
			return deviceEquation + connEquation + pointEquation;	
		}
		

	}
}
