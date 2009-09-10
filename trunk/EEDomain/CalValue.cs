using System;
using System.Xml;
using System.Collections;
using Bluebit.MatrixLibrary;
namespace EEDomain
{


	public class CalValue
	{
		private ReadFromXml readxml;
		public CalValue(ReadFromXml r)
		{
			readxml = r;

		}
		public string Output()
		{					
			ArrayList cList   = readxml.getConnectionList(); //connection list
			ArrayList dList   = readxml.getDeviceList(); //device list
			ArrayList nList   = readxml.GetNodeList(); // node list
			
			IEnumerator cEnum = cList.GetEnumerator();
			IEnumerator dEnum = dList.GetEnumerator();
			IEnumerator nEnum = nList.GetEnumerator();

			int countDevice	  = 0; // to record the number of VsourceDC
			
			//count the number of VsourceDC and store in the variable "countDevice"
			while(dEnum.MoveNext())
			{
				if(dEnum.Current.GetType().ToString().Equals("EEDomain.VsourceDC"))
				{	
					countDevice++;
				}
			}
			
			//set up the size of the double array Y and I
			double [,] Y = new double[nList.Count+dList.Count-countDevice,nList.Count+dList.Count-countDevice];
			double [,] I = new double[nList.Count+dList.Count-countDevice,1];
			
			int countRow = 0; //to record the current number of row in the matrix
			int countCurrent = 0; // to record the number of current varible needed to be calculated
			ArrayList currentList = new ArrayList(); // to record the current and matrix position pairs
			
			//The purpose is to get all the nodes of the particular device
			//and set up the matrix element for the device
			dEnum = dList.GetEnumerator();
			while (dEnum.MoveNext()) //for each object in the device list
			{					
				string currentName = ((Device)dEnum.Current).GetName(); //get the current device name
				string currentObjID = ((Device)dEnum.Current).GetID(); // get the current device id
								
				Node currentNodeStart = null; 
				Node currentNodeEnd = null;
				nEnum = nList.GetEnumerator();

				while(nEnum.MoveNext()) //for each object in the node list
				{					
					if( currentObjID.Equals(((Node)nEnum.Current).GetObjID())) // check the node belongs to the current device
					{	
						if(((Node)nEnum.Current).getstart() == true)// check the node is a start node of a connection
						{
							currentNodeStart = ((Node)nEnum.Current);// assign this node to currentNodeStart
							
						}

						else if(((Node)nEnum.Current).getstart() == false)// check the node is a end node of a connection
						{
							currentNodeEnd = ((Node)nEnum.Current);// assign this node to currentNodeEnd
						}
						
						// to set the matrix Y and matrix I based on device equations
						if(currentNodeStart!=null && currentNodeEnd!=null)
						{							
							Device device = ((Device)dEnum.Current);
							
							if((device.GetType().ToString()).Equals("EEDomain.Resistor"))
							{
								if(currentNodeStart.GetName()!=null && currentNodeEnd.GetName()!=null)
								{										
									Y[countRow,int.Parse(currentNodeStart.GetName())] = 1; 
									Y[countRow,int.Parse(currentNodeEnd.GetName())] = -1;
									CurrentMap cMap = new CurrentMap(device.GetName(),countCurrent); // to mark down the currentName and the position
									currentList.Add(cMap); //add the cMap to the currentlist to record the number of current to be calculated
									Y[countRow,countCurrent+nList.Count] = ((Resistor)device).GetResistance();
									countCurrent++;									
								}
						
							}
							else if((device.GetType().ToString()).Equals("EEDomain.VsourceDC"))
							{
								if(currentNodeStart.GetName()!=null && currentNodeEnd.GetName()!=null)
								{
									Y[countRow,int.Parse(currentNodeStart.GetName())] = 1;
									Y[countRow,int.Parse(currentNodeEnd.GetName())] = -1;
									I[countRow,0] = ((VsourceDC)device).GetVoltage();
								}
							}
			
							currentNodeStart = null;
							currentNodeEnd = null;
							countRow++;
							//after setting up the matrix element of the currentDevice
							//continue with the next device
							break;							
						}
					}
				}
			}

			//for the connections
			cEnum = cList.GetEnumerator();
			while (cEnum.MoveNext())
			{
				Y[countRow,int.Parse(((Connection)cEnum.Current).GetEnd().GetName())]=-1;
				Y[countRow,int.Parse(((Connection)cEnum.Current).GetStart().GetName())]=1;
				countRow++;
			}

			cEnum = cList.GetEnumerator();
			dEnum = dList.GetEnumerator();
			nEnum = nList.GetEnumerator();
			
			ArrayList nList2 = (ArrayList)nList.Clone();
			IEnumerator nEnum2 = nList2.GetEnumerator();

			//for the pt
		
			bool checkMerge = false;
			bool checkNode = false;
			bool haveConn = false;
			
			ArrayList appearStart = new ArrayList();
			ArrayList appearEnd = new ArrayList();
			IEnumerator asEnum;
			IEnumerator aeEnum;
			IEnumerator currEnum;

			int connNodeY1, connNodeX1, connNodeValue1, connNodeY2, connNodeX2, connNodeValue2;
			
			nEnum = nList.GetEnumerator();
			while (nEnum.MoveNext())//for each node in nodeList
			{
				Node targetNode = (Node)nEnum.Current;
				haveConn =false;
				checkNode = false;
				checkMerge = false;
				connNodeY1 =0;
				connNodeX1 =0;
				connNodeValue1 =0;
				connNodeY2 =0;
				connNodeX2 =0;
				connNodeValue2 =0;
				
				cEnum = cList.GetEnumerator();
				while(cEnum.MoveNext())//for each connection in the conn list
				{	
					Node currentStart = ((Connection)cEnum.Current).GetStart();
					Node currentEnd = ((Connection)cEnum.Current).GetEnd();
					//if the targetNode is the current connection startNode
					if(targetNode.GetObjID().Equals(currentStart.GetObjID()) && targetNode.GetPtID().Equals(currentStart.GetPtID()))
					{
						appearStart.Add(targetNode); //add this target node to the appearStart List to check merge
						aeEnum = appearEnd.GetEnumerator();
						//get the appearEnd list to check if there is a merge occur
						while(aeEnum.MoveNext())
						{
							Node tempEnd = ((Connection)cEnum.Current).GetEnd();
							Node tempAppearEnd = (Node)aeEnum.Current;
							//if there exist the opposite node of this connection appear in the appearEnd list
							//i.e. it should be a merge node
							if(tempEnd.GetObjID().Equals(tempAppearEnd.GetObjID()) && tempEnd.GetPtID().Equals(tempAppearEnd.GetPtID()))
							{
								checkMerge = true; //a merge occur in this target node
								checkNode = true; //to indicate this targetNode no need to find the other node of the targetNode's device
							}
						}
						if(!checkMerge) //if no merge occur
						{	
							dEnum = dList.GetEnumerator();
							while(dEnum.MoveNext())
							{
								if(currentEnd.GetObjID().Equals(((Device)dEnum.Current).GetID()))
								{
									currEnum = currentList.GetEnumerator();
									while(currEnum.MoveNext())
									{
										if((((CurrentMap)currEnum.Current).GetName()).Equals(((Device)dEnum.Current).GetName()))
										{													
											if(((Device)dEnum.Current).ToString().Equals("EEDomain.VsourceDC"))
											{
												continue;
											}
											else
											{	//haveConn indicated whether this targetNode record a connection node before
												if(haveConn==true)
												{
													connNodeX2 = countRow;
													connNodeY2 = nList.Count+((CurrentMap)currEnum.Current).GetPosition();
													connNodeValue2 = 1;
												}
												else
												{
													connNodeX1 = countRow;
													connNodeY1 = nList.Count+((CurrentMap)currEnum.Current).GetPosition();
													connNodeValue1 = 1;
												}
												haveConn = true;
												
											}
										}
									}
								}
							}
						}					
					}
					
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
									currEnum = currentList.GetEnumerator();
									while(currEnum.MoveNext())
									{
										if((((CurrentMap)currEnum.Current).GetName()).Equals(((Device)dEnum.Current).GetName()))
										{
											if(((Device)dEnum.Current).ToString().Equals("EEDomain.VsourceDC"))
											{
												continue;
											}
											else
											{
												if(haveConn==true)
												{
													connNodeX2 = countRow;
													connNodeY2 = nList.Count+((CurrentMap)currEnum.Current).GetPosition();
													connNodeValue2 = -1;
												}
												else
												{
													connNodeX1 = countRow;
													connNodeY1 = nList.Count+((CurrentMap)currEnum.Current).GetPosition();
													connNodeValue1 = -1;
												}
												haveConn = true;
												
											}
										}
									}
								}
							}
						}			
					}
				}
				
				//to add the another node of the targetNode's device
				//if no need to add the another node of the targetNode's device
				//i.e. it is a merge node. no need to put value's into the matrix

				if(!checkNode) 
				{	
					nEnum2 = nList2.GetEnumerator();
					while(nEnum2.MoveNext())
					{	
						if((targetNode.GetObjID()).Equals(((Node)nEnum2.Current).GetObjID()))
						{	
							if(!targetNode.Equals((Node)nEnum2.Current))
							{									
								dEnum = dList.GetEnumerator();
								while(dEnum.MoveNext())
								{
									if(((Node)nEnum2.Current).GetObjID().Equals(((Device)dEnum.Current).GetID()))
									{
										if(((Device)dEnum.Current).ToString().Equals("EEDomain.VsourceDC"))
										{
											continue;
										}
										else
										{
											currEnum = currentList.GetEnumerator();
											while(currEnum.MoveNext())
											{														
												if((((CurrentMap)currEnum.Current).GetName()).Equals(((Device)dEnum.Current).GetName()))
												{	
													if((connNodeX1!=0 && connNodeY1!=0)||(connNodeX2!=0 && connNodeY2!=0))
													{
														if(connNodeX1!=0 && connNodeY1!=0)
														{
															Y[connNodeX1,connNodeY1] = connNodeValue1;
																	
															connNodeX1 = 0;
															connNodeY1 = 0;
															connNodeValue1 =0;
														}
														if(connNodeX2!=0 && connNodeY2!=0)
														{
															Y[connNodeX2,connNodeY2] = connNodeValue2;
																	
															connNodeX2 = 0;
															connNodeY2 = 0;
															connNodeValue2 =0;
														}
														Y[countRow,nList.Count+((CurrentMap)currEnum.Current).GetPosition()] = -1;
														countRow++;
													}
												}
											}
										}
									}
								}
							}						
						}
					}
				}
			}
			
			for(int j=0;j<nList.Count+dList.Count-countDevice;j++)
			{	
				Y[nList.Count+dList.Count-countDevice-1,j] = 0.0;
			}
	
			Y[nList.Count+dList.Count-countDevice-1,0] = 1;
			dEnum = dList.GetEnumerator();
			while(dEnum.MoveNext())
			{
				if(dEnum.Current.GetType().ToString().Equals("EEDomain.VsourceDC"))
				{	
					I[nList.Count+dList.Count-countDevice-1,0] = ((VsourceDC)dEnum.Current).GetVoltage();
					break;
				}
			}

			Bluebit.MatrixLibrary.Matrix a = new Bluebit.MatrixLibrary.Matrix(Y);
			Bluebit.MatrixLibrary.Matrix b = new Bluebit.MatrixLibrary.Matrix(I);
			Bluebit.MatrixLibrary.Matrix c = new Bluebit.MatrixLibrary.Matrix();
			
			c = a.Solve(b);
	
			//Console.WriteLine(c.ToString("F2"));

			double[,] output = c.ToArray();
			double tempValue;
			int nodeCount = 0;	
			string tempCurrName = "" ;
			Device tempDev;
					
			currEnum = currentList.GetEnumerator();
			
			nEnum = nList.GetEnumerator();
			//feedback calculated voltage to the node
			while(nEnum.MoveNext())
			{
				tempValue = output[nodeCount,0];
				((Node)nEnum.Current).SetVoltage(tempValue);
				nodeCount++;
			}
			//feedback calculated current to the device
			while(currEnum.MoveNext())
			{	
				dEnum = dList.GetEnumerator();
				tempValue = output[nodeCount,0];
				tempCurrName = ((CurrentMap)currEnum.Current).GetName();
				
				while(dEnum.MoveNext())
				{	tempDev = ((Device)dEnum.Current);
					if(tempDev.GetName().Equals(tempCurrName))
					{
						tempDev.SetCurrent(tempValue);
						nodeCount++;
						break;
					}
				}
				
			}
			
			nEnum = nList.GetEnumerator();
			dEnum = dList.GetEnumerator();
			string outputResult="";
			string outputCurrentResult="";
			//check result valid
			/*
			while(nEnum.MoveNext())
			{
				outputResult +="VN"+((Node)nEnum.Current).GetName()+" ";
				outputResult +=((Node)nEnum.Current).GetVoltage()+ " \n";
				nodeCount++;
			}
			*/
			
			while(dEnum.MoveNext())
			{
				if(((Device)dEnum.Current).GetType().ToString().Equals("EEDomain.Resistor"))
				{	int temp1 = int.Parse(((Device)dEnum.Current).GetName());
					//outputResult +="VR"+((Device)dEnum.Current).GetName()+" ";
					outputResult +="VR"+temp1+" ";
					outputResult += (((Device)dEnum.Current).GetNode1().GetVoltage() - ((Device)dEnum.Current).GetNode2().GetVoltage()).ToString("0.00")+"V \n";
					//outputCurrentResult +="ID"+((Device)dEnum.Current).GetName()+" ";
					outputCurrentResult +="ID"+temp1+" ";
					outputCurrentResult +=((Device)dEnum.Current).GetCurrent().ToString("0.00")+"A \n";
					nodeCount++;
				}
			}
			
			return (outputResult+outputCurrentResult);
		}
		
	
	}
}
