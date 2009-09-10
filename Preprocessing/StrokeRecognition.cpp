// StrokeRecognition.cpp : Implementation of CStrokeRecognition

#include "stdafx.h"
#include "StrokeRecognition.h"
#include "CommonFunction.h"

#include <MsXml2.h>

// CStrokeRecognition


STDMETHODIMP CStrokeRecognition::RecognizeStroke(BSTR MouseTrack, BSTR* StrokeResult)
{
	std::vector<POINT>				rgPoints;
	std::vector<BasicStroke>		rgStrokes;

	DecodePointFromXML(rgPoints, MouseTrack);

	StrokeFitting(rgPoints, rgStrokes);

	EncodeStrokeToXML(rgStrokes, StrokeResult);

	return S_OK;
}

STDMETHODIMP CStrokeRecognition::get_RecognitionParam(BSTR* pVal)
{
	double		Segment_Error_Threshold;
	double		Arc_Error_Threshold;
	double		Arc_Min_Length;
	double		Arc_Min_Curve;
	double		Stroke_Min_Length;
	double		Min_Turning_Angle;
	double		Segmentation_Penalty;
	char		szTmp[1024];
	CComBSTR	szParam;

	GetFittingParam(Segment_Error_Threshold, Arc_Error_Threshold, Arc_Min_Length, Arc_Min_Curve, Stroke_Min_Length, Min_Turning_Angle, Segmentation_Penalty);

	sprintf(szTmp, "<param Segment_Error_Threshold=\"%f\" Arc_Error_Threshold=\"%f\" Arc_Min_Length=\"%f\" Arc_Min_Curve=\"%f\" Stroke_Min_Length=\"%f\" Min_Turning_Angle=\"%f\" Segmentation_Penalty=\"%f\"/>", (float)Segment_Error_Threshold, (float)Arc_Error_Threshold, (float)Arc_Min_Length, (float)Arc_Min_Curve, (float)Stroke_Min_Length, (float)Min_Turning_Angle, (float)Segmentation_Penalty);

	szParam = "<?xml version=\"1.0\"?>";
	szParam += szTmp;
	szParam.CopyTo(pVal);

	return S_OK;
}

STDMETHODIMP CStrokeRecognition::put_RecognitionParam(BSTR newVal)
{
	double		Segment_Error_Threshold;
	double		Arc_Error_Threshold;
	double		Arc_Min_Length;
	double		Arc_Min_Curve;
	double		Stroke_Min_Length;
	double		Min_Turning_Angle;
	double		Segmentation_Penalty;
	VARIANT		value;
	BSTR		nodeName;
	long		n;

	IXMLDOMDocument*		pDoc;
	IXMLDOMNodeList*		pNodeList;
	IXMLDOMNode*			pNode;
	IXMLDOMNode*			pParamNode;
	IXMLDOMNode*			pAttrNode;
	IXMLDOMNamedNodeMap*	pNodeMap;
	VARIANT_BOOL			bLoaded;

	GetFittingParam(Segment_Error_Threshold, Arc_Error_Threshold, Arc_Min_Length, Arc_Min_Curve, Stroke_Min_Length, Min_Turning_Angle, Segmentation_Penalty);

	if (SUCCEEDED(CoCreateInstance(CLSID_DOMDocument, NULL, CLSCTX_INPROC_SERVER, IID_IXMLDOMDocument, (LPVOID*)&pDoc)))
	{
		pDoc->put_async(VARIANT_FALSE);
		pDoc->loadXML(newVal, &bLoaded);

		if (bLoaded == VARIANT_TRUE)
		{
			if (SUCCEEDED(pDoc->get_childNodes(&pNodeList)))
			{
				pParamNode = NULL;

				pNodeList->get_length(&n);

				for (int i = 0; i < n; i++)
				{
					if (SUCCEEDED(pNodeList->get_item(i, &pNode)))
					{
						nodeName = NULL;
						if (SUCCEEDED(pNode->get_nodeName(&nodeName)))
						{
							if (CComBSTR(nodeName) == L"param")
							{
								pParamNode = pNode;
								pParamNode->AddRef();
								break;
							}
							SysFreeString(nodeName);
						}
						pNode->Release();
					}
				}

				pNodeList->Release();

				if (pParamNode != NULL)
				{
					if (SUCCEEDED(pParamNode->get_attributes(&pNodeMap)))
					{
						if (SUCCEEDED(pNodeMap->getNamedItem(L"Segment_Error_Threshold", &pAttrNode)))
						{
							pAttrNode->get_nodeValue(&value);
							Segment_Error_Threshold = _wtof(value.bstrVal);
							pAttrNode->Release();
						}
						if (SUCCEEDED(pNodeMap->getNamedItem(L"Arc_Error_Threshold", &pAttrNode)))
						{
							pAttrNode->get_nodeValue(&value);
							Arc_Error_Threshold = _wtof(value.bstrVal);
							pAttrNode->Release();
						}
						if (SUCCEEDED(pNodeMap->getNamedItem(L"Arc_Min_Length", &pAttrNode)))
						{
							pAttrNode->get_nodeValue(&value);
							Arc_Min_Length = _wtof(value.bstrVal);
							pAttrNode->Release();
						}
						if (SUCCEEDED(pNodeMap->getNamedItem(L"Arc_Min_Curve", &pAttrNode)))
						{
							pAttrNode->get_nodeValue(&value);
							Arc_Min_Curve = _wtof(value.bstrVal);
							pAttrNode->Release();
						}
						if (SUCCEEDED(pNodeMap->getNamedItem(L"Stroke_Min_Length", &pAttrNode)))
						{
							pAttrNode->get_nodeValue(&value);
							Stroke_Min_Length = _wtof(value.bstrVal);
							pAttrNode->Release();
						}
						if (SUCCEEDED(pNodeMap->getNamedItem(L"Min_Turning_Angle", &pAttrNode)))
						{
							pAttrNode->get_nodeValue(&value);
							Min_Turning_Angle = _wtof(value.bstrVal);
							pAttrNode->Release();
						}
						if (SUCCEEDED(pNodeMap->getNamedItem(L"Segmentation_Penalty", &pAttrNode)))
						{
							pAttrNode->get_nodeValue(&value);
							Segmentation_Penalty = _wtof(value.bstrVal);
							pAttrNode->Release();
						}

						pNodeMap->Release();
					}

					pParamNode->Release();
				}
			}
		}

		pDoc->Release();
	}

	SetFittingParam(Segment_Error_Threshold, Arc_Error_Threshold, Arc_Min_Length, Arc_Min_Curve, Stroke_Min_Length, Min_Turning_Angle, Segmentation_Penalty);

	return S_OK;
}

void CStrokeRecognition::DecodePointFromXML(std::vector<POINT> &rgPoints, BSTR strXML)
{
	IXMLDOMDocument*		pDoc;
	IXMLDOMNodeList*		pNodeList;
	IXMLDOMNode*			pNode;
	IXMLDOMNode*			pInputNode;
	IXMLDOMNode*			pAttrNode;
	IXMLDOMNamedNodeMap*	pNodeMap;
	VARIANT_BOOL			bLoaded;
	VARIANT					value;
	BSTR					nodeName;
	POINT					pt;
	long					n;

	if (SUCCEEDED(CoCreateInstance(CLSID_DOMDocument, NULL, CLSCTX_INPROC_SERVER, IID_IXMLDOMDocument, (LPVOID*)&pDoc)))
	{
		pDoc->put_async(VARIANT_FALSE);
		pDoc->loadXML(strXML, &bLoaded);

		if (bLoaded == VARIANT_TRUE)
		{
			if (SUCCEEDED(pDoc->get_childNodes(&pNodeList)))
			{
				pInputNode = NULL;

				pNodeList->get_length(&n);

				for (int i = 0; i < n; i++)
				{
					if (SUCCEEDED(pNodeList->get_item(i, &pNode)))
					{
						nodeName = NULL;
						if (SUCCEEDED(pNode->get_nodeName(&nodeName)))
						{
							if (CComBSTR(nodeName) == L"input")
							{
								pInputNode = pNode;
								pInputNode->AddRef();
								break;
							}
							SysFreeString(nodeName);
						}
						pNode->Release();
					}
				}

				pNodeList->Release();

				if (pInputNode != NULL)
				{
					if (SUCCEEDED(pInputNode->get_childNodes(&pNodeList)))
					{
						pNodeList->get_length(&n);

						for (int i = 0; i < n; i++)
						{
							if (SUCCEEDED(pNodeList->get_item(i, &pNode)))
							{
								pt.x = 0;
								pt.y = 0;

								if (SUCCEEDED(pNode->get_attributes(&pNodeMap)))
								{
									if (SUCCEEDED(pNodeMap->getNamedItem(L"x", &pAttrNode)))
									{
										pAttrNode->get_nodeValue(&value);
										pt.x = _wtoi(value.bstrVal);
										pAttrNode->Release();
									}

									if (SUCCEEDED(pNodeMap->getNamedItem(L"y", &pAttrNode)))
									{
										pAttrNode->get_nodeValue(&value);
										pt.y = _wtoi(value.bstrVal);
										pAttrNode->Release();
									}

									pNodeMap->Release();
								}

								rgPoints.push_back(pt);

								pNode->Release();
							}
						}

						pNodeList->Release();
					}

					pInputNode->Release();
				}
			}
		}

		pDoc->Release();
	}
}

void CStrokeRecognition::EncodeStrokeToXML(std::vector<BasicStroke> &rgStrokes, BSTR* StrokeResult)
{
	CComBSTR	strokeXML;
	char		szTmp[1024];

	strokeXML = "<?xml version=\"1.0\"?><output><strokes candidatenum=\"1\">";
	sprintf(szTmp, "<candidate similarity=\"1\" strokenum=\"%d\">", rgStrokes.size());
	strokeXML += szTmp;

	for (unsigned int i = 0; i < rgStrokes.size(); i++)
	{
		switch (rgStrokes[i].iStrokeType)
		{
		case PRIMITIVE_SEGMENT:
			{
				sprintf(szTmp, "<line><startpt x=\"%d\" y=\"%d\"/><endpt x=\"%d\" y=\"%d\"/></line>", 
						rgStrokes[i].segment.startPoint.x, rgStrokes[i].segment.startPoint.y, 
						rgStrokes[i].segment.endPoint.x, rgStrokes[i].segment.endPoint.y);
				strokeXML += szTmp;
			}
			break;
		case PRIMITIVE_ARC:
			{
				double	x1, y1, x2, y2;
				double	cx, cy, a, b;

				cx	= (rgStrokes[i].arc.rcBounding.left + rgStrokes[i].arc.rcBounding.right) / 2;
				cy	= (rgStrokes[i].arc.rcBounding.top + rgStrokes[i].arc.rcBounding.bottom) / 2;
				a	= (rgStrokes[i].arc.rcBounding.right - rgStrokes[i].arc.rcBounding.left) / 2;
				b	= (rgStrokes[i].arc.rcBounding.bottom - rgStrokes[i].arc.rcBounding.top) / 2;

				CalcEllipsePointByAngle(cx, cy, a, b, 0, ConvertToEllipseAngle(rgStrokes[i].arc.startAngle, a, b), x1, y1);
				CalcEllipsePointByAngle(cx, cy, a, b, 0, ConvertToEllipseAngle(rgStrokes[i].arc.endAngle, a, b), x2, y2);

				if (rgStrokes[i].arc.endAngle - rgStrokes[i].arc.startAngle >= 1.9 * PI)
				{
					sprintf(szTmp, "<arc rotate=\"%f\"><startpt x=\"%d\" y=\"%d\"/><endpt x=\"%d\" y=\"%d\"/><ltpt x=\"%d\" y=\"%d\"/><rbpt x=\"%d\" y=\"%d\"/></arc>", 
						(float)(rgStrokes[i].arc.rotateAngle), 
						(int)x1, (int)y1, (int)x1, (int)y1,
						rgStrokes[i].arc.rcBounding.left, rgStrokes[i].arc.rcBounding.top, 
						rgStrokes[i].arc.rcBounding.right, rgStrokes[i].arc.rcBounding.bottom);
				}
				else
				{
					sprintf(szTmp, "<arc rotate=\"%f\"><startpt x=\"%d\" y=\"%d\"/><endpt x=\"%d\" y=\"%d\"/><ltpt x=\"%d\" y=\"%d\"/><rbpt x=\"%d\" y=\"%d\"/></arc>", 
						(float)(rgStrokes[i].arc.rotateAngle), 
						(int)x1, (int)y1, (int)x2, (int)y2,
						rgStrokes[i].arc.rcBounding.left, rgStrokes[i].arc.rcBounding.top, 
						rgStrokes[i].arc.rcBounding.right, rgStrokes[i].arc.rcBounding.bottom);
				}
				strokeXML += szTmp;
			}
			break;
		}
	}

	strokeXML += "</candidate></strokes></output>";

	strokeXML.CopyTo(StrokeResult);
}

