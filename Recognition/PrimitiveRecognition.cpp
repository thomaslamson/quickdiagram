// PrimitiveRecognition.cpp : Implementation of CPrimitiveRecognition

#include "stdafx.h"
#include "PrimitiveRecognition.h"

// CPrimitiveRecognition

typedef struct tagRecogResult
{
	GraphicObject*	lpTemplate;
	RECOG_RESULT	result;
}
RECOGNITION_RESULT;

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

PrimitiveStroke* CreateStrokeFromXML(IXMLDOMNode* lpNode)
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

void CreateStrokeListFromXML(std::vector<PrimitiveStroke*> &rgStrokes, IXMLDOMNode* pCandidate)
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

void DecodeStrokeFromXML(std::vector<PrimitiveStroke*> &rgStrokes, BSTR StrokeXML)
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

void CalcTemplateRelations(std::vector<GraphicObject*> &rgTemplates, std::vector<Matrix<unsigned int>*> &rgRelations)
{
	Matrix<unsigned int>*	lpMatrix;

	for (unsigned int i = 0; i < rgTemplates.size(); i++)
	{
		int n = rgTemplates[i]->Count;
		lpMatrix = new Matrix<unsigned int>(n, n);

		for (int j = 0; j < n; j++)
		{
			for (int k = 0; k < n; k++)
			{
				if (j != k)
				{
					(*lpMatrix)[j][k] = CalcRelation(rgTemplates[i]->rgStrokes[j], rgTemplates[i]->rgStrokes[k], 1.5, PI / 360);
				}
			}
		}
		rgRelations.push_back(lpMatrix);
	}
}

void RecognizeGraphicObjects(std::vector<PrimitiveStroke*> &rgPrimitives, std::vector<GraphicObject*> &rgTemplates, std::vector<Matrix<unsigned int>*> &rgRelations, std::vector<RECOGNITION_RESULT> &rgResults)
{
	unsigned int i, j;
	unsigned int nSegment = 0;
	unsigned int nEllipse = 0;
	unsigned int nArc = 0;

	if (rgPrimitives.size() == 0) return;

	for (i = 0; i < rgPrimitives.size(); i++)
	{
		switch (rgPrimitives[i]->strokeType)
		{
		case PRIMITIVE_STROKE_SEGMENT:
			nSegment++;
			break;
		case PRIMITIVE_STROKE_ELLIPSE:
			nEllipse++;
			break;
		case PRIMITIVE_STROKE_ARC:
			nArc++;
			break;
		}
	}

	Matrix<unsigned int>	mRelUser((int)rgPrimitives.size(), (int)rgPrimitives.size());

	for (i = 0; i < rgPrimitives.size(); i++)
	{
		for (j = 0; j < rgPrimitives.size(); j++)
		{
			if (i != j)
			{
				mRelUser[i][j] = CalcRelation(rgPrimitives[i], rgPrimitives[j], 5, PI / 90);
			}
		}
	}

	RECOGNITION_RESULT	result;

	for (i = 0; i < rgTemplates.size(); i++)
	{
		if (rgTemplates[i]->m_rgSegments.size() < nSegment) continue;
		if (rgTemplates[i]->m_rgEllipses.size() < nEllipse) continue;
		if (rgTemplates[i]->m_rgArcs.size() < nArc) continue;

		result.result = CalcSimilarity(rgTemplates[i], rgRelations[i], &rgPrimitives, &mRelUser);

		if (fabs(result.result.dSimilarity) > DOUBLE_ZERO)
		{
			result.lpTemplate = rgTemplates[i];
			rgResults.push_back(result);
		}
	}

	for (i = 0; i < rgResults.size(); i++)
	{
		for (j = i + 1; j < rgResults.size(); j++)
		{
			if (rgResults[i].result.dSimilarity < rgResults[j].result.dSimilarity)
			{
				result			= rgResults[i];
				rgResults[i]	= rgResults[j];
				rgResults[j]	= result;
			}
		}
	}
}

void EncodeResultToXML(std::vector<PrimitiveStroke*> &rgStrokes, std::vector<RECOGNITION_RESULT> &rgResults, BSTR* resultXML)
{
	CComBSTR	szResult;
	char		szTmp[1024];

	if (rgStrokes.size() == 0) return;

	double left, right, top, bottom;

	left = right = rgStrokes[0]->m_rgPoints[0].x;
	top = bottom = rgStrokes[0]->m_rgPoints[0].y;

	for (unsigned int i = 0; i < rgStrokes.size(); i++)
	{
		for (unsigned int j = 0; j < rgStrokes[i]->m_rgPoints.size(); j++)
		{
			left	= min(left, rgStrokes[i]->m_rgPoints[j].x);
			right	= max(right, rgStrokes[i]->m_rgPoints[j].x);
			top		= min(top, rgStrokes[i]->m_rgPoints[j].y);
			bottom	= max(bottom, rgStrokes[i]->m_rgPoints[j].y);
		}
	}

	szResult = "<?xml version=\"1.0\"?><rec_result>";

	sprintf(szTmp, "<number>%d</number>", rgResults.size());
	szResult += szTmp;

	for (unsigned int i = 0; i < rgResults.size(); i++)
	{
		sprintf(szTmp, "<result><id>%s</id><location><left>%f</left><top>%f</top><right>%f</right><bottom>%f</bottom></location><orientation>%f</orientation><similarity>%f</similarity></result>", rgResults[i].lpTemplate->m_id.c_str(), left, top, right, bottom, rgResults[i].result.dRotate, rgResults[i].result.dSimilarity);
		szResult += szTmp;
	}

	szResult += "</rec_result>";
	szResult.CopyTo(resultXML);
}

void CPrimitiveRecognition::FreeTemplates()
{
	for (unsigned int i = 0; i < m_rgTemplates.size(); i++)
	{
		delete m_rgTemplates[i];
	}

	for (unsigned int i = 0; i < m_rgRelations.size(); i++)
	{
		delete m_rgRelations[i];
	}

	m_rgTemplates.clear();
	m_rgRelations.clear();
}

void CPrimitiveRecognition::LoadTemplates(char* szPath)
{
	char			szFile[MAX_PATH];
	char			szFolder[MAX_PATH];
	HANDLE			hFindFile;
	GraphicObject*	lpObj;
	WIN32_FIND_DATA	wfdFile;

	if (szPath[strlen(szPath) - 1] != '\\') strcat(szPath, "\\");

	sprintf(szFile, "%s*.xml", szPath);

	hFindFile = FindFirstFileEx(szFile, FindExInfoStandard, &wfdFile, FindExSearchNameMatch, NULL, 0);

	if (hFindFile != INVALID_HANDLE_VALUE)
	{
		do 
		{
			sprintf(szFile, "%s%s", szPath, wfdFile.cFileName);

			if (lpObj = LoadFromFile(szFile))
			{
				m_rgTemplates.push_back(lpObj);
			}
		} 
		while(FindNextFile(hFindFile, &wfdFile));

		FindClose(hFindFile);
	}

	sprintf(szFolder, "%s*.*", szPath);

	hFindFile = FindFirstFileEx(szFolder, FindExInfoStandard, &wfdFile, FindExSearchLimitToDirectories, NULL, 0);
	
	if (hFindFile != INVALID_HANDLE_VALUE)
	{
		do 
		{
			if ((strcmp(wfdFile.cFileName, ".") != 0) && (strcmp(wfdFile.cFileName, "..") != 0))
			{
				sprintf(szFolder, "%s%s\\", szPath, wfdFile.cFileName);

				LoadTemplates(szFolder);
			}
		} 
		while(FindNextFile(hFindFile, &wfdFile));

		FindClose(hFindFile);
	}
}

STDMETHODIMP CPrimitiveRecognition::LoadTemplates(BSTR TemplatePath)
{
	USES_CONVERSION;

	FreeTemplates();
	LoadTemplates(OLE2T(TemplatePath));
	CalcTemplateRelations(m_rgTemplates, m_rgRelations);

	return S_OK;
}

STDMETHODIMP CPrimitiveRecognition::RecognizeObjects(BSTR strokeXML, BSTR* resultXML)
{
	std::vector<PrimitiveStroke*>	rgStrokes;
	std::vector<RECOGNITION_RESULT> rgResults;

	DecodeStrokeFromXML(rgStrokes, strokeXML);
	RecognizeGraphicObjects(rgStrokes, m_rgTemplates, m_rgRelations, rgResults);
	EncodeResultToXML(rgStrokes, rgResults, resultXML);

	for (unsigned int i = 0; i < rgStrokes.size(); i++)
	{
		delete rgStrokes[i];
	}

	return S_OK;
}
