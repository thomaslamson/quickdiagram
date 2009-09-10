// StrokeRefinement.cpp : Implementation of CStrokeRefinement

#include "stdafx.h"
#include "StrokeRefinement.h"

// CStrokeRefinement

double GetNamedAttributeDoubleValue(IXMLDOMNode *pNode, BSTR szAttributeName)
{
	IXMLDOMNamedNodeMap*	pNodeMap;
	IXMLDOMNode*			pAttrNode;
	VARIANT					value;
	double					dRet = 0;

	if (SUCCEEDED(pNode->get_attributes(&pNodeMap)))
	{
		if (SUCCEEDED(pNodeMap->getNamedItem(szAttributeName, &pAttrNode)))
		{
			pAttrNode->get_nodeValue(&value);
			dRet = _wtof(value.bstrVal);
			pAttrNode->Release();
		}
		pNodeMap->Release();
	}

	return dRet;
}

IXMLDOMNode* GetNamedChildFromList(IXMLDOMNodeList* pNodeList, BSTR szChildName)
{
	IXMLDOMNode*		pNode = NULL;
	BSTR				nodeName;
	long				n;

	if (SUCCEEDED(pNodeList->get_length(&n)))
	{
		for (int i = 0; i < n; i++)
		{
			if (SUCCEEDED(pNodeList->get_item(i, &pNode)))
			{
				if (SUCCEEDED(pNode->get_nodeName(&nodeName)))
				{
					if (CComBSTR(nodeName) == szChildName)
					{
						SysFreeString(nodeName);
						break;
					}
					else
					{
						SysFreeString(nodeName);
					}
				}
				pNode->Release();
				pNode = NULL;
			}
		}
	}

	return pNode;
}

IXMLDOMNode* GetNamedChild(IXMLDOMNode* lpNode, BSTR szChildName)
{
	IXMLDOMNodeList*	pList;
	IXMLDOMNode*		pRet = NULL;

	if (SUCCEEDED(lpNode->get_childNodes(&pList)))
	{
		pRet = GetNamedChildFromList(pList, szChildName);
		pList->Release();
	}

	return pRet;
}

IXMLDOMNode* GetNamedChild(IXMLDOMDocument* lpNode, BSTR szChildName)
{
	IXMLDOMNodeList*	pList;
	IXMLDOMNode*		pRet = NULL;

	if (SUCCEEDED(lpNode->get_childNodes(&pList)))
	{
		pRet = GetNamedChildFromList(pList, szChildName);
		pList->Release();
	}

	return pRet;
}

STDMETHODIMP CStrokeRefinement::Refine_MergeVertex(BSTR StrokeXML, DOUBLE DistanceThreshold, BSTR* ResultXML)
{
	// TODO: Add your implementation code here

	std::vector<PrimitiveStroke*>	rgStrokes;

	DecodeStrokeFromXML(rgStrokes, StrokeXML);

	RefineStrokes_MergeVertex(rgStrokes, DistanceThreshold);

	EncodeStrokeToXML(rgStrokes, ResultXML);

	for (unsigned int i = 0; i < rgStrokes.size(); i++)
	{
		delete rgStrokes[i];
	}

	return S_OK;
}

PrimitiveStroke* CStrokeRefinement::CreateStrokeFromXML(IXMLDOMNode* lpNode)
{
	PrimitiveStroke*	lpRet = NULL;
	BSTR				nodeName;

	if (SUCCEEDED(lpNode->get_nodeName(&nodeName)))
	{
		if (CComBSTR(nodeName) == L"line")
		{
			IXMLDOMNode*		lpStartPt = GetNamedChild(lpNode, L"startpt");
			IXMLDOMNode*		lpEndPt = GetNamedChild(lpNode, L"endpt");
			PrimitiveSegment*	lpSeg = new PrimitiveSegment();

			if (lpStartPt)
			{
				lpSeg->startPoint.x = GetNamedAttributeDoubleValue(lpStartPt, L"x");
				lpSeg->startPoint.y = GetNamedAttributeDoubleValue(lpStartPt, L"y");
				lpStartPt->Release();
			}

			if (lpEndPt)
			{
				lpSeg->endPoint.x = GetNamedAttributeDoubleValue(lpEndPt, L"x");
				lpSeg->endPoint.y = GetNamedAttributeDoubleValue(lpEndPt, L"y");
				lpEndPt->Release();
			}

			lpRet = lpSeg;
		}

		if (CComBSTR(nodeName) == L"arc")
		{
			PrimitivePoint	ltPt, rbPt, startPt, endPt;
			IXMLDOMNode*	lpStartPt = GetNamedChild(lpNode, L"startpt");
			IXMLDOMNode*	lpEndPt = GetNamedChild(lpNode, L"endpt");
			IXMLDOMNode*	lpltPt = GetNamedChild(lpNode, L"ltpt");
			IXMLDOMNode*	lprbPt = GetNamedChild(lpNode, L"rbpt");
			double			rotateAngle;

			rotateAngle = GetNamedAttributeDoubleValue(lpNode, L"rotate");

			if (lpStartPt)
			{
				startPt.x = GetNamedAttributeDoubleValue(lpStartPt, L"x");
				startPt.y = GetNamedAttributeDoubleValue(lpStartPt, L"y");
				lpStartPt->Release();
			}

			if (lpEndPt)
			{
				endPt.x = GetNamedAttributeDoubleValue(lpEndPt, L"x");
				endPt.y = GetNamedAttributeDoubleValue(lpEndPt, L"y");
				lpEndPt->Release();
			}

			if (lpltPt)
			{
				ltPt.x = GetNamedAttributeDoubleValue(lpltPt, L"x");
				ltPt.y = GetNamedAttributeDoubleValue(lpltPt, L"y");
				lpltPt->Release();
			}

			if (lprbPt)
			{
				rbPt.x = GetNamedAttributeDoubleValue(lprbPt, L"x");
				rbPt.y = GetNamedAttributeDoubleValue(lprbPt, L"y");
				lprbPt->Release();
			}

			if (CalcDistance_Point_To_Point(startPt, endPt) < 1)
			{
				PrimitiveEllipse*	lpEllipse = new PrimitiveEllipse();

				lpEllipse->leftTop.x		= ltPt.x;
				lpEllipse->leftTop.y		= ltPt.y;
				lpEllipse->rightBottom.x	= rbPt.x;
				lpEllipse->rightBottom.y	= rbPt.y;
				lpEllipse->rotateAngle		= rotateAngle;

				lpRet = lpEllipse;
			}
			else
			{
				PrimitiveArc*	lpArc = new PrimitiveArc();

				lpArc->leftTop.x		= ltPt.x;
				lpArc->leftTop.y		= ltPt.y;
				lpArc->rightBottom.x	= rbPt.x;
				lpArc->rightBottom.y	= rbPt.y;
				lpArc->startPoint.x		= startPt.x;
				lpArc->startPoint.y		= startPt.y;
				lpArc->endPoint.x		= endPt.x;
				lpArc->endPoint.y		= endPt.y;
				lpArc->rotateAngle		= rotateAngle;

				lpRet = lpArc;
			}
		}
	}

	return lpRet;
}

void CStrokeRefinement::CreateStrokeListFromXML(std::vector<PrimitiveStroke*> &rgStrokes, IXMLDOMNode* pCandidate)
{
	IXMLDOMNodeList*	pNodeList;
	IXMLDOMNode*		pNode;
	long				n;

	if (SUCCEEDED(pCandidate->get_childNodes(&pNodeList)))
	{
		if (SUCCEEDED(pNodeList->get_length(&n)))
		{
			for (int i = 0; i < n; i++)
			{
				if (SUCCEEDED(pNodeList->get_item(i, &pNode)))
				{
					rgStrokes.push_back(CreateStrokeFromXML(pNode));
					pNode->Release();
				}
			}
		}
		pNodeList->Release();
	}
}

void CStrokeRefinement::DecodeStrokeFromXML(std::vector<PrimitiveStroke*> &rgStrokes, BSTR StrokeXML)
{
	IXMLDOMDocument*	pDoc;
	IXMLDOMNodeList*	pNodeList;
	IXMLDOMNode*		pOutput;
	IXMLDOMNode*		pStrokes;
	IXMLDOMNode*		pCandidate;
	VARIANT_BOOL		bRet;
	long				n;

	if (SUCCEEDED(CoCreateInstance(CLSID_DOMDocument, NULL, CLSCTX_INPROC_SERVER, IID_IXMLDOMDocument, (LPVOID*)&pDoc)))
	{
		pDoc->put_async(VARIANT_FALSE);
		pDoc->loadXML(StrokeXML, &bRet);
		
		if (bRet == VARIANT_TRUE)
		{
			if (pOutput = GetNamedChild(pDoc, L"output"))
			{
				if (SUCCEEDED(pOutput->get_childNodes(&pNodeList)))
				{
					if (SUCCEEDED(pNodeList->get_length(&n)))
					{
						for (int i = 0; i < n; i++)
						{
							if (SUCCEEDED(pNodeList->get_item(i, &pStrokes)))
							{
								if (pCandidate = GetNamedChild(pStrokes, L"candidate"))
								{
									CreateStrokeListFromXML(rgStrokes, pCandidate);
									pCandidate->Release();
								}
								pStrokes->Release();
							}
						}
					}
					pNodeList->Release();
				}
				pOutput->Release();
			}
		}
		pDoc->Release();
	}
}

void CStrokeRefinement::EncodeStrokeToXML(std::vector<PrimitiveStroke*> &rgStrokes, BSTR* ResultXML)
{
	CComBSTR	strokeXML;
	char		szTmp[1024];

	strokeXML = "<?xml version=\"1.0\"?><output><strokes candidatenum=\"1\">";
	sprintf(szTmp, "<candidate similarity=\"1\" strokenum=\"%d\">", rgStrokes.size());
	strokeXML += szTmp;

	for (unsigned int i = 0; i < rgStrokes.size(); i++)
	{
		switch (rgStrokes[i]->strokeType)
		{
		case PRIMITIVE_STROKE_SEGMENT:
			{
				PrimitiveSegment*	lpSeg = (PrimitiveSegment*)rgStrokes[i];

				sprintf(szTmp, "<line><startpt x=\"%d\" y=\"%d\"/><endpt x=\"%d\" y=\"%d\"/></line>", 
					(int)lpSeg->startPoint.x, (int)lpSeg->startPoint.y, (int)lpSeg->endPoint.x, (int)lpSeg->endPoint.y);
				strokeXML += szTmp;
			}
			break;
		case PRIMITIVE_STROKE_ELLIPSE:
			{
				PrimitiveEllipse*	lpEllipse = (PrimitiveEllipse*)rgStrokes[i];
				PrimitivePoint		pt = CalcEllipsePointByAngle(lpEllipse, 0);

				sprintf(szTmp, "<arc rotate=\"%f\"><startpt x=\"%d\" y=\"%d\"/><endpt x=\"%d\" y=\"%d\"/><ltpt x=\"%d\" y=\"%d\"/><rbpt x=\"%d\" y=\"%d\"/></arc>", 
					(float)lpEllipse->rotateAngle, (int)pt.x, (int)pt.y, (int)pt.x, (int)pt.y,
					(int)lpEllipse->leftTop.x, (int)lpEllipse->leftTop.y,
					(int)lpEllipse->rightBottom.x, (int)lpEllipse->rightBottom.y);
				strokeXML += szTmp;
			}
			break;
		case PRIMITIVE_STROKE_ARC:
			{
				PrimitiveArc*	lpArc = (PrimitiveArc*)rgStrokes[i];

				sprintf(szTmp, "<arc rotate=\"%f\"><startpt x=\"%d\" y=\"%d\"/><endpt x=\"%d\" y=\"%d\"/><ltpt x=\"%d\" y=\"%d\"/><rbpt x=\"%d\" y=\"%d\"/></arc>", 
						(float)lpArc->rotateAngle, 
						(int)lpArc->startPoint.x, (int)lpArc->startPoint.y, 
						(int)lpArc->endPoint.x, (int)lpArc->endPoint.y, 
						(int)lpArc->leftTop.x, (int)lpArc->leftTop.y,
						(int)lpArc->rightBottom.x, (int)lpArc->rightBottom.y);
				strokeXML += szTmp;
			}
			break;
		}
	}

	strokeXML += "</candidate></strokes></output>";
	strokeXML.CopyTo(ResultXML);
}
