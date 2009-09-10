#include "stdafx.h"

#include <MsXml2.h>
#include <atlbase.h>
#include <comutil.h>

#include "CommonFunction.h"
#include "Primitives.h"
#include "Relation.h"

typedef struct tagNamedPoint
{
	PrimitivePoint	location;
	CComBSTR		name;
}
NamedPoint;

TransformationMatrix::TransformationMatrix()
{
	SetIdentityMatrix();
}

void TransformationMatrix::SetIdentityMatrix()
{
	ZeroMemory(matrix, sizeof(matrix));
	matrix[0][0] = 1;
	matrix[1][1] = 1;
	matrix[2][2] = 1;
}

void TransformationMatrix::Offset(double offsetX, double offsetY)
{
	double m[3][3];
	/*
	|1		0		0|
	|0		1		0|
	|tx		ty		1|
	*/
	ZeroMemory(m, sizeof(m));
	m[0][0] = 1;
	m[1][1] = 1;
	m[2][2] = 1;
	m[2][0] = offsetX;
	m[2][1] = offsetY;

	MultiplyMatrix(m);
}

void TransformationMatrix::Rotate(double angle)
{
	double m[3][3];
	/*	Rotate counterclockwise by angle
	|cos	sin		0|
	|-sin	cos		0|
	|0		0		1|
	*/
	ZeroMemory(m, sizeof(m));
	m[0][0] = cos(angle);
	m[0][1] = sin(angle);
	m[1][0] = -sin(angle);
	m[1][1] = cos(angle);
	m[2][2] = 1;

	MultiplyMatrix(m);
}

void TransformationMatrix::Skew(double angleX, double angleY)
{
	double m[3][3];
	/*
	|1		0		0|
	|tan	1		0|
	|0		0		1|
	*/
	ZeroMemory(m, sizeof(m));
	m[0][0] = 1;
	m[1][1] = 1;
	m[2][2] = 1;
	m[1][0] = tan(angleX);

	MultiplyMatrix(m);
	/*
	|1		tan		0|
	|0		1		0|
	|0		0		1|
	*/
	m[0][1] = 0;
	m[0][1] = tan(angleY);

	MultiplyMatrix(m);
}

void TransformationMatrix::Scale(double scaleX, double scaleY)
{
	double m[3][3];
	/*
	|sx		0		0|
	|0		sy		0|
	|0		0		1|
	*/
	ZeroMemory(m, sizeof(m));
	m[0][0] = scaleX;
	m[1][1] = scaleY;
	m[2][2] = 1;

	MultiplyMatrix(m);
}

bool TransformationMatrix::Inverse()
{
	double	m[3][6];
	double	tmp;
	int		i, j;
	/*
	|a		b		c		1		0		0|
	|e		f		g		0		1		0|
	|h		i		j		0		0		1|
	*/
	ZeroMemory(m, sizeof(m));
	for (i = 0; i < 3; i++)
	{
		for (j = 0; j < 3; j++)
		{
			m[i][j] = matrix[i][j];
		}
		m[i][i + 3] = 1;
	}
	//Calculate inverse matrix
	for (i = 0; i < 3; i++)
	{	//Search for non-zero element on ith column
		for (j = i; j < 3; j++)
		{
			if (m[j][i] != 0) break;
		}
		//Can not find the non-zero element, fail to calculate the inverse matrix
		if (j == 3) return false;
		//If the non-zero element is on jth row and i<>j, swap ith and jth row
		if (j != i)
		{
			for (int k = 0; k < 6; j++)
			{
				tmp = m[i][k];
				m[i][k] = m[j][k];
				m[j][k] = tmp;
			}
		}
		//Normalize the ith row in order to make m[i][i] to be 1
		tmp = m[i][i];
		for (j = 0; j < 6; j++)
		{
			m[i][j] /= tmp;
		}
		//Substract other rows to make other m[*][i] == 0
		for (j = 0; j < 3; j++)
		{
			if (j != i)
			{
				tmp = m[j][i];
				if (tmp != 0)
				{
					for (int k = 0; k < 6; k++)
					{
						m[j][k] -= m[i][k] * tmp;
					}
				}
			}
		}
	}
	/*
	|1		0		0		a'		b'		c'|
	|0		1		0		e'		f'		g'|
	|0		0		1		h'		i'		j'|
	*/
	for (i = 0; i < 3; i++)
	{
		for (j = 0; j < 3; j++)
		{
			matrix[i][j] = m[i][j + 3];
		}
	}
	return true;
}

void TransformationMatrix::MultiplyMatrix(double m[3][3])
{
	double d[3][3];
	memcpy(d, matrix, sizeof(matrix));
	ZeroMemory(matrix, sizeof(matrix));

	for (int i = 0; i < 3; i++)
	{
		for (int j = 0; j < 3; j++)
		{
			for (int k = 0; k < 3; k++)
			{
				matrix[i][j] += d[i][k] * m[k][j];
			}
		}
	}
}

TransformationMatrix & TransformationMatrix::operator = (const TransformationMatrix &rvalue)
{
	memcpy(matrix, rvalue.matrix, sizeof(matrix));
	return *this;
}

double* TransformationMatrix::operator[](const int idx)
{
	return matrix[idx];
}

PrimitivePoint::PrimitivePoint()
{
	x = 0;
	y = 0;
}

bool PrimitivePoint::operator==(PrimitivePoint& value)
{
	return ((value.x == x) && (value.y == y));
}

void PrimitivePoint::Transform(TransformationMatrix &matrix)
{
	double dx = x;
	double dy = y;

	x = dx * matrix[0][0] + dy * matrix[1][0] + matrix[2][0];
	y = dx * matrix[0][1] + dy * matrix[1][1] + matrix[2][1];
}

PrimitiveStroke::PrimitiveStroke()
{
	m_iStrokeType = 0;
	m_dSketchLength = 0;
}

int PrimitiveStroke::get_strokeType()
{
	return m_iStrokeType;
}

double PrimitiveStroke::get_sketchLength()
{
	return m_dSketchLength;
}

void PrimitiveStroke::put_sketchLength(double value)
{
	m_dSketchLength = value;
}

PrimitiveSegment::PrimitiveSegment()
{
	PrimitivePoint	pt;

	m_rgPoints.push_back(pt);
	m_rgPoints.push_back(pt);

	m_iStrokeType = PRIMITIVE_STROKE_SEGMENT;
}

PrimitivePoint& PrimitiveSegment::get_startPoint()
{
	return m_rgPoints[0];
}

void PrimitiveSegment::put_startPoint(PrimitivePoint value)
{
	m_rgPoints[0] = value;
}

PrimitivePoint& PrimitiveSegment::get_endPoint()
{
	return m_rgPoints[1];
}

void PrimitiveSegment::put_endPoint(PrimitivePoint value)
{
	m_rgPoints[1] = value;
}

void PrimitiveSegment::Transform(TransformationMatrix &matrix)
{
	for (unsigned int i = 0; i < m_rgPoints.size(); i++)
	{
		m_rgPoints[i].Transform(matrix);
	}
}

PrimitiveEllipse::PrimitiveEllipse()
{
	PrimitivePoint	pt;

	m_rgPoints.push_back(pt);
	m_rgPoints.push_back(pt);

	m_rotateAngle = 0;
	m_iStrokeType = PRIMITIVE_STROKE_ELLIPSE;
}

double PrimitiveEllipse::get_x_axis()
{
	return fabs(m_rgPoints[0].x - m_rgPoints[1].x);
}

double PrimitiveEllipse::get_y_axis()
{
	return fabs(m_rgPoints[0].y - m_rgPoints[1].y);
}

PrimitivePoint PrimitiveEllipse::get_center()
{
	PrimitivePoint	pt;

	pt.x = (m_rgPoints[0].x + m_rgPoints[1].x) / 2;
	pt.y = (m_rgPoints[0].y + m_rgPoints[1].y) / 2;

	return pt;
}

PrimitivePoint& PrimitiveEllipse::get_leftTop()
{
	return m_rgPoints[0];
}

void PrimitiveEllipse::put_leftTop(PrimitivePoint value)
{
	m_rgPoints[0] = value;
}

PrimitivePoint& PrimitiveEllipse::get_rightBottom()
{
	return m_rgPoints[1];
}

void PrimitiveEllipse::put_rightBottom(PrimitivePoint value)
{
	m_rgPoints[1] = value;
}

double& PrimitiveEllipse::get_rotateAngle()
{
	return m_rotateAngle;
}

void PrimitiveEllipse::put_rotateAngle(double value)
{
	m_rotateAngle = value;
}

PrimitiveArc::PrimitiveArc() : PrimitiveEllipse()
{
	PrimitivePoint	pt;

	m_rgPoints.push_back(pt);
	m_rgPoints.push_back(pt);

	m_iStrokeType = PRIMITIVE_STROKE_ARC;
}

PrimitivePoint& PrimitiveArc::get_startPoint()
{
	return m_rgPoints[2];
}

void PrimitiveArc::put_startPoint(PrimitivePoint value)
{
	m_rgPoints[2] = value;
}

PrimitivePoint& PrimitiveArc::get_endPoint()
{
	return m_rgPoints[3];
}

void PrimitiveArc::put_endPoint(PrimitivePoint value)
{
	m_rgPoints[3] = value;
}

PrimitiveStroke* GraphicObject::get_primitiveStroke(int i)
{
	if (i < (int)m_rgSegments.size())
	{
		return &m_rgSegments[i];
	}
	else if (i < (int)(m_rgSegments.size() + m_rgEllipses.size()))
	{
		return &m_rgEllipses[i - m_rgSegments.size()];
	}
	else if (i <(int)(m_rgSegments.size() + m_rgEllipses.size() + m_rgArcs.size()))
	{
		return &m_rgArcs[i - m_rgSegments.size() - m_rgEllipses.size()];
	}
	else
	{
		return NULL;
	}
}

int GraphicObject::get_strokeCount()
{
	return (int)(m_rgSegments.size() + m_rgEllipses.size() + m_rgArcs.size());
}

GraphicObject& GraphicObject::operator=(GraphicObject& value)
{
	this->m_rgArcs.clear();
	this->m_rgEllipses.clear();
	this->m_rgSegments.clear();

	this->m_rgArcs.assign(value.m_rgArcs.begin(), value.m_rgArcs.end());
	this->m_rgEllipses.assign(value.m_rgEllipses.begin(), value.m_rgEllipses.end());
	this->m_rgSegments.assign(value.m_rgSegments.begin(), value.m_rgSegments.end());

	this->m_id = value.m_id;

	return *this;
}

void LoadPointsFromXML(IXMLDOMNode* pPoints, std::vector<NamedPoint>& rgPoints)
{
	IXMLDOMNamedNodeMap*	pNodeMap;
	IXMLDOMNodeList*		pNodeList;
	IXMLDOMNode*			pNode;
	IXMLDOMNode*			pAttrNode;

	NamedPoint	pt;
	VARIANT		value;
	BSTR		nodeName;
	long		n;

	if (SUCCEEDED(pPoints->get_childNodes(&pNodeList)))
	{
		if (SUCCEEDED(pNodeList->get_length(&n)))
		{
			for (int i = 0; i < n; i++)
			{
				if (SUCCEEDED(pNodeList->get_item(i, &pNode)))
				{
					if (SUCCEEDED(pNode->get_nodeName(&nodeName)))
					{
						if (CComBSTR(nodeName) == "point")
						{
							if (SUCCEEDED(pNode->get_attributes(&pNodeMap)))
							{
								if (SUCCEEDED(pNodeMap->getNamedItem(L"x", &pAttrNode)))
								{
									pAttrNode->get_nodeValue(&value);
									pt.location.x = _wtof(value.bstrVal);
									pAttrNode->Release();
								}
								if (SUCCEEDED(pNodeMap->getNamedItem(L"y", &pAttrNode)))
								{
									pAttrNode->get_nodeValue(&value);
									pt.location.y = _wtof(value.bstrVal);
									pAttrNode->Release();
								}
								if (SUCCEEDED(pNodeMap->getNamedItem(L"id", &pAttrNode)))
								{
									pAttrNode->get_nodeValue(&value);
									pt.name = value.bstrVal;
									pAttrNode->Release();
								}
								pNodeMap->Release();
							}
							rgPoints.push_back(pt);
						}
						SysFreeString(nodeName);
					}
					pNode->Release();
				}
			}
		}
		pNodeList->Release();
	}
}

PrimitivePoint GetPointByName(std::vector<NamedPoint>& rgPoints, BSTR pointName)
{
	PrimitivePoint	pt;

	for (unsigned int i = 0; i < rgPoints.size(); i++)
	{
		if (rgPoints[i].name == pointName)
		{
			pt = rgPoints[i].location;
		}
	}

	return pt;
}

void LoadStrokesFromXML(IXMLDOMNode* pStrokes, std::vector<NamedPoint>& rgPoints, GraphicObject* lpObj)
{
	IXMLDOMNamedNodeMap*	pNodeMap;
	IXMLDOMNodeList*		pNodeList;
	IXMLDOMNode*			pNode;
	IXMLDOMNode*			pAttrNode;

	NamedPoint	pt;
	VARIANT		value;
	BSTR		nodeName;
	long		n;

	if (SUCCEEDED(pStrokes->get_childNodes(&pNodeList)))
	{
		if (SUCCEEDED(pNodeList->get_length(&n)))
		{
			for (int i = 0; i < n; i++)
			{
				if (SUCCEEDED(pNodeList->get_item(i, &pNode)))
				{
					if (SUCCEEDED(pNode->get_nodeName(&nodeName)))
					{
						if (CComBSTR(nodeName) == "line")
						{
							PrimitiveSegment	segment;

							if (SUCCEEDED(pNode->get_attributes(&pNodeMap)))
							{
								if (SUCCEEDED(pNodeMap->getNamedItem(L"startPoint", &pAttrNode)))
								{
									pAttrNode->get_nodeValue(&value);
									segment.startPoint = GetPointByName(rgPoints, value.bstrVal);
									pAttrNode->Release();
								}
								if (SUCCEEDED(pNodeMap->getNamedItem(L"endPoint", &pAttrNode)))
								{
									pAttrNode->get_nodeValue(&value);
									segment.endPoint = GetPointByName(rgPoints, value.bstrVal);
									pAttrNode->Release();
								}
								pNodeMap->Release();
							}

							segment.sketchLength = CalcDistance_Point_To_Point(segment.startPoint, segment.endPoint);
							lpObj->m_rgSegments.push_back(segment);
						}

						if (CComBSTR(nodeName) == "arc")
						{
							PrimitiveArc	arc;

							if (SUCCEEDED(pNode->get_attributes(&pNodeMap)))
							{
								if (SUCCEEDED(pNodeMap->getNamedItem(L"startPoint", &pAttrNode)))
								{
									pAttrNode->get_nodeValue(&value);
									arc.startPoint = GetPointByName(rgPoints, value.bstrVal);
									pAttrNode->Release();
								}
								if (SUCCEEDED(pNodeMap->getNamedItem(L"endPoint", &pAttrNode)))
								{
									pAttrNode->get_nodeValue(&value);
									arc.endPoint = GetPointByName(rgPoints, value.bstrVal);
									pAttrNode->Release();
								}
								if (SUCCEEDED(pNodeMap->getNamedItem(L"leftTop", &pAttrNode)))
								{
									pAttrNode->get_nodeValue(&value);
									arc.leftTop = GetPointByName(rgPoints, value.bstrVal);
									pAttrNode->Release();
								}
								if (SUCCEEDED(pNodeMap->getNamedItem(L"rightDown", &pAttrNode)))
								{
									pAttrNode->get_nodeValue(&value);
									arc.rightBottom = GetPointByName(rgPoints, value.bstrVal);
									pAttrNode->Release();
								}
								pNodeMap->Release();
							}

							if ((arc.startPoint.x == arc.endPoint.x) && (arc.startPoint.y == arc.endPoint.y))
							{
								PrimitiveEllipse	ellipse;

								ellipse.leftTop		= arc.leftTop;
								ellipse.rightBottom	= arc.rightBottom;
								ellipse.sketchLength= CalcDistance_Elliptic_Arc(&ellipse, ellipse.leftTop, ellipse.leftTop);

								lpObj->m_rgEllipses.push_back(ellipse);
							}
							else
							{
								arc.sketchLength = CalcDistance_Elliptic_Arc((PrimitiveEllipse*)&arc, arc.startPoint, arc.endPoint);
								lpObj->m_rgArcs.push_back(arc);
							}
						}
						SysFreeString(nodeName);
					}
					pNode->Release();
				}
			}
		}
		pNodeList->Release();
	}
}

GraphicObject* LoadFromFile(char* szFileName)
{
	IXMLDOMDocument*	pDoc;
	IXMLDOMNodeList*	pNodeList;
	IXMLDOMNode*		pNode;
	IXMLDOMNode*		pTemplate;
	IXMLDOMNode*		pPoints;
	IXMLDOMNode*		pStrokes;
	VARIANT_BOOL		bRet;
	GraphicObject*		lpObj = NULL;

	std::vector<NamedPoint>	rgPoints;
	std::string				szObjID;

	BSTR	nodeName;
	long	n;

	CoInitialize(NULL);

	USES_CONVERSION;

	if (SUCCEEDED(CoCreateInstance(CLSID_DOMDocument, NULL, CLSCTX_INPROC_SERVER, IID_IXMLDOMDocument, (LPVOID*)&pDoc)))
	{
		pDoc->put_async(VARIANT_FALSE);
		pDoc->load(_variant_t(szFileName), &bRet);

		if (bRet == VARIANT_TRUE)
		{
			if (SUCCEEDED(pDoc->get_childNodes(&pNodeList)))
			{
				if (SUCCEEDED(pNodeList->get_length(&n)))
				{
					for (int i = 0; i < n; i++)
					{
						if (SUCCEEDED(pNodeList->get_item(i, &pNode)))
						{
							if (SUCCEEDED(pNode->get_nodeName(&nodeName)))
							{
								if (CComBSTR(nodeName) == "graphicTemplate")
								{
									pTemplate = pNode;
									pTemplate->AddRef();
									SysFreeString(nodeName);
									break;
								}
								SysFreeString(nodeName);
							}
							pNode->Release();
						}
					}
				}
				pNodeList->Release();

				if (pTemplate != NULL)
				{

					IXMLDOMNamedNodeMap*	pNodeMap;
					IXMLDOMNode*			pAttrNode;
					VARIANT					value;

					if (SUCCEEDED(pNode->get_attributes(&pNodeMap)))
					{
						if (SUCCEEDED(pNodeMap->getNamedItem(L"id", &pAttrNode)))
						{
							pAttrNode->get_nodeValue(&value);
							szObjID = OLE2T(value.bstrVal);
							pAttrNode->Release();
						}
						pNodeMap->Release();
					}

					if (SUCCEEDED(pTemplate->get_childNodes(&pNodeList)))
					{
						if (SUCCEEDED(pNodeList->get_length(&n)))
						{
							for (int i = 0; i < n; i++)
							{
								if (SUCCEEDED(pNodeList->get_item(i, &pNode)))
								{
									if (SUCCEEDED(pNode->get_nodeName(&nodeName)))
									{
										if (CComBSTR(nodeName) == "points")
										{
											pPoints = pNode;
											pPoints->AddRef();
										}
										if (CComBSTR(nodeName) == "drawings")
										{
											pStrokes = pNode;
											pStrokes->AddRef();
										}
										SysFreeString(nodeName);
									}
									pNode->Release();
								}
							}
						}
						pNodeList->Release();
					}
					pTemplate->Release();
				}

				if ((pPoints != NULL) && (pStrokes != NULL))
				{
					lpObj = new GraphicObject();
					lpObj->m_id = szObjID;

					LoadPointsFromXML(pPoints, rgPoints);
					LoadStrokesFromXML(pStrokes, rgPoints, lpObj);
				}

				if (pPoints != NULL)
				{
					pPoints->Release();
				}

				if (pStrokes != NULL)
				{
					pStrokes->Release();
				}
			}
		}

		pDoc->Release();
	}

	CoUninitialize();

	return lpObj;
}