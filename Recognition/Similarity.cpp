#include "stdafx.h"

#include <Windows.h>
#include <vector>

#include "Primitives.h"
#include "Relation.h"
#include "Matrix.h"
#include "Similarity.h"

bool IsConjointConflict(int i, int j, int k, std::vector<int> &rgMatch, Matrix<unsigned int>* lpRelModel, Matrix<unsigned int>* lpRelUser)
{
	unsigned int uRelationMask = RELATION_CONJOINT_0 | RELATION_CONJOINT_1;

	if ((((*lpRelUser)[i][k] & RELATION_CONJOINT) != 0) && 
		(((*lpRelUser)[j][k] & RELATION_CONJOINT) != 0) &&
		(((*lpRelModel)[rgMatch[i]][rgMatch[k]] & RELATION_CONJOINT) != 0) &&
		(((*lpRelModel)[rgMatch[j]][rgMatch[k]] & RELATION_CONJOINT) != 0))
	{
		//Conflict in conjoint relation
		if ((((*lpRelUser)[i][k] & uRelationMask) == ((*lpRelUser)[j][k] & uRelationMask)) !=
			(((*lpRelModel)[rgMatch[i]][rgMatch[k]] & uRelationMask) == ((*lpRelModel)[rgMatch[j]][rgMatch[k]] & uRelationMask)))
		{
			return true;
		}
	}

	return false;
}

bool IsAreaConflict(int i, int j, int k, std::vector<int> &rgMatch, Matrix<unsigned int>* lpRelModel, Matrix<unsigned int>* lpRelUser)
{
	unsigned int uRelationMask = RELATION_POSITIVE | RELATION_NEGATIVE;

	if ((((*lpRelUser)[i][k] & uRelationMask) != 0) && 
		(((*lpRelUser)[j][k] & uRelationMask) != 0) &&
		(((*lpRelModel)[rgMatch[i]][rgMatch[k]] & uRelationMask) != 0) &&
		(((*lpRelModel)[rgMatch[j]][rgMatch[k]] & uRelationMask) != 0))
	{
		//Conflict in area relation
		if ((((*lpRelUser)[i][k] & uRelationMask) == ((*lpRelUser)[j][k] & uRelationMask)) !=
			(((*lpRelModel)[rgMatch[i]][rgMatch[k]] & uRelationMask) == ((*lpRelModel)[rgMatch[j]][rgMatch[k]] & uRelationMask)))
		{
			return true;
		}
	}

	return false;
}

bool IsRelationConflict(std::vector<int> &rgMatch, int iLength, Matrix<unsigned int>* lpRelModel, Matrix<unsigned int>* lpRelUser)
{
	int k = iLength;
	unsigned int uAreaMask = RELATION_INSIDE | RELATION_OUTSIDE;

	for (int i = 0; i < iLength; i++)
	{
		//Conflict in intersected relation
		if (((*lpRelModel)[rgMatch[i]][rgMatch[k]] & RELATION_INTERSECTED) != ((*lpRelUser)[i][k] & RELATION_INTERSECTED))
		{
			return true;
		}
		//Conflict in conjoint relation
		if (((*lpRelModel)[rgMatch[i]][rgMatch[k]] & RELATION_CONJOINT) != ((*lpRelUser)[i][k] & RELATION_CONJOINT))
		{
			return true;
		}
		//Conflict in inside/outside relation
		if (((*lpRelModel)[rgMatch[i]][rgMatch[k]] & uAreaMask) != ((*lpRelUser)[i][k] & uAreaMask))
		{
			return true;
		}

		for (int j = 0; j < iLength; j++)
		{
			if (i != j)
			{
				if (IsConjointConflict(i, j, k, rgMatch, lpRelModel, lpRelUser) ||
					IsConjointConflict(i, k, j, rgMatch, lpRelModel, lpRelUser) ||
					IsConjointConflict(j, k, i, rgMatch, lpRelModel, lpRelUser))
				{
					return true;
				}

				if (IsAreaConflict(i, j, k, rgMatch, lpRelModel, lpRelUser) ||
					IsAreaConflict(i, k, j, rgMatch, lpRelModel, lpRelUser) ||
					IsAreaConflict(j, k, i, rgMatch, lpRelModel, lpRelUser))
				{
					return true;
				}
			}
		}
	}
	return false;
}

double CalcSimilarity_Internal(GraphicObject* lpObj, std::vector<int> rgMatch, int iLength, Matrix<unsigned int>* lpRelModel, Matrix<unsigned int>* lpRelUser)
{
	double length = 0;
	double dSimilarity = 0;
	double nModelFeature = 0;
	double nUserFeature = 0;
	double dBonus = 1;

	for (int i = 0; i < lpObj->Count; i++)
	{
		length += lpObj->rgStrokes[i]->sketchLength;
	}

	for (unsigned int i = 0; i < rgMatch.size(); i++)
	{
		dSimilarity += lpObj->rgStrokes[rgMatch[i]]->sketchLength / length;
	}

	for (unsigned int i = 0; i < rgMatch.size(); i++)
	{
		for (unsigned int j = 0; j < rgMatch.size(); j++)
		{
			if (i != j)
			{
				if (((*lpRelModel)[rgMatch[i]][rgMatch[j]] & RELATION_PERPENDICULAR) != 0)
				{
					nModelFeature++;
					if (((*lpRelUser)[i][j] & RELATION_PERPENDICULAR) != 0)
					{
						nUserFeature++;
					}
				}

				if (((*lpRelModel)[rgMatch[i]][rgMatch[j]] & RELATION_PARALLEL) != 0)
				{
					nModelFeature++;
					if (((*lpRelUser)[i][j] & RELATION_PARALLEL) != 0)
					{
						nUserFeature++;
					}
				}

				if (((*lpRelModel)[rgMatch[i]][rgMatch[j]] & RELATION_CONCENTRIC) != 0)
				{
					nModelFeature++;
					if (((*lpRelUser)[i][j] & RELATION_CONCENTRIC) != 0)
					{
						nUserFeature++;
					}
				}

				if (((*lpRelModel)[rgMatch[i]][rgMatch[j]] & RELATION_TANGENT) != 0)
				{
					nModelFeature++;
					if (((*lpRelUser)[i][j] & RELATION_TANGENT) != 0)
					{
						nUserFeature++;
					}
				}
			}
		}
	}

	if (nModelFeature > 0)
	{
		dBonus += 0.1 * (nUserFeature / nModelFeature);
	}

	return dSimilarity * dBonus;
}

bool CalcRotate_4_Line(PrimitiveSegment &objAxisLine, PrimitiveSegment &objRefLine, PrimitiveSegment &usrAxisLine, PrimitiveSegment &usrRefLine, double &dRotate)
{
	std::vector<PrimitivePoint>	rgIntersection;
	PrimitivePoint				pt;
	double						objRefAngle, usrRefAngle;

	rgIntersection.clear();
	CalcIntersection_Line_To_Line(&objAxisLine, &objRefLine, rgIntersection);

	if (rgIntersection.size() == 0) 
	{
		return false;
	}

	if ((CalcDistance_Point_To_Point(rgIntersection[0], objRefLine.startPoint) > 1.5) &&
		(CalcDistance_Point_To_Point(rgIntersection[0], objRefLine.endPoint) > 1.5))
	{
		if (IsPointInSegmentRange(rgIntersection[0], &objRefLine))
		{
			return false;
		}
	}

	if (CalcDistance_Point_To_Point(rgIntersection[0], objRefLine.startPoint) > 1.5)
	{
		pt.x = objRefLine.startPoint.x - rgIntersection[0].x;
		pt.y = objRefLine.startPoint.y - rgIntersection[0].y;

		objRefAngle = CalcAngle_Vector_To_X(pt);
	}
	else if (CalcDistance_Point_To_Point(rgIntersection[0], objRefLine.endPoint) > 1.5)
	{
		pt.x = objRefLine.endPoint.x - rgIntersection[0].x;
		pt.y = objRefLine.endPoint.y - rgIntersection[0].y;

		objRefAngle = CalcAngle_Vector_To_X(pt);
	}

	rgIntersection.clear();
	CalcIntersection_Line_To_Line(&usrAxisLine, &usrRefLine, rgIntersection);

	if (rgIntersection.size() == 0) 
	{
		return false;
	}

	if ((CalcDistance_Point_To_Point(rgIntersection[0], usrRefLine.startPoint) > 1.5) &&
		(CalcDistance_Point_To_Point(rgIntersection[0], usrRefLine.endPoint) > 1.5))
	{
		if (IsPointInSegmentRange(rgIntersection[0], &usrRefLine))
		{
			return false;
		}
	}

	if (CalcDistance_Point_To_Point(rgIntersection[0], usrRefLine.startPoint) > 1.5)
	{
		pt.x = usrRefLine.startPoint.x - rgIntersection[0].x;
		pt.y = usrRefLine.startPoint.y - rgIntersection[0].y;

		usrRefAngle = CalcAngle_Vector_To_X(pt);
	}
	else if (CalcDistance_Point_To_Point(rgIntersection[0], usrRefLine.endPoint) > 1.5)
	{
		pt.x = usrRefLine.endPoint.x - rgIntersection[0].x;
		pt.y = usrRefLine.endPoint.y - rgIntersection[0].y;

		usrRefAngle = CalcAngle_Vector_To_X(pt);
	}

	dRotate = usrRefAngle - objRefAngle;

	return true;
}

double CalcRotate_Line_based(std::vector<PrimitiveSegment> &rgObjLine, std::vector<PrimitiveSegment> &rgUsrLine)
{
	double				dRotate = 0;
	std::vector<double>	rgRotate;

	for (unsigned int i = 0; i < rgObjLine.size(); i++)
	{
		for (unsigned int j = i + 1; j < rgObjLine.size(); j++)
		{
			if (CalcRotate_4_Line(rgObjLine[i], rgObjLine[j], rgUsrLine[i], rgUsrLine[j], dRotate))
			{
				rgRotate.push_back(dRotate);
			}
		}
	}

	dRotate = 0;

	for (unsigned int i = 0; i < rgRotate.size(); i++)
	{
		dRotate += rgRotate[i];
	}

	if (rgRotate.size() != 0)
	{
		return dRotate / rgRotate.size();
	}
	else
	{
		return 0;
	}
}

double CalcRotate(GraphicObject* lpObj, std::vector<int> &rgMatch, std::vector<PrimitiveStroke*> *lpStrokes)
{
	std::vector<PrimitiveSegment>	rgObjLine;
	std::vector<PrimitiveSegment>	rgUsrLine;

	for (unsigned int i = 0; i < lpStrokes->size(); i++)
	{
		if (lpObj->rgStrokes[i]->strokeType == PRIMITIVE_STROKE_SEGMENT)
		{
			rgObjLine.push_back(*((PrimitiveSegment*)lpObj->rgStrokes[rgMatch[i]]));
			rgUsrLine.push_back(*((PrimitiveSegment*)(*lpStrokes)[i]));
		}
	}

	for (unsigned int i = 0; i < lpStrokes->size(); i++)
	{
		if ((*lpStrokes)[i]->strokeType == PRIMITIVE_STROKE_SEGMENT)
		{
			for (unsigned int j = i + 1; j < lpStrokes->size(); j++)
			{
				if (((*lpStrokes)[j]->strokeType == PRIMITIVE_STROKE_ELLIPSE) ||
					((*lpStrokes)[j]->strokeType == PRIMITIVE_STROKE_ARC))
				{
					PrimitiveSegment	seg;
					PrimitiveEllipse*	lpEllipse;

					lpEllipse = (PrimitiveEllipse*)lpObj->rgStrokes[rgMatch[j]];
					seg.startPoint	= lpEllipse->center;
					seg.endPoint	= CalcProjectPoint(seg.startPoint, (PrimitiveSegment*)lpObj->rgStrokes[rgMatch[i]]);
					if (CalcDistance_Point_To_Point(seg.startPoint, seg.endPoint) > 5)
					{
						rgObjLine.push_back(seg);
					}
					else
					{
						continue;
					}

					lpEllipse = (PrimitiveEllipse*)(*lpStrokes)[j];
					seg.startPoint	= lpEllipse->center;
					seg.endPoint	= CalcProjectPoint(seg.startPoint, (PrimitiveSegment*)(*lpStrokes)[i]);
					if (CalcDistance_Point_To_Point(seg.startPoint, seg.endPoint) > 5)
					{
						rgUsrLine.push_back(seg);
					}
					else
					{
						rgObjLine.pop_back();
					}
				}
			}
		}
	}

	for (unsigned int i = 0; i < lpStrokes->size(); i++)
	{
		if (((*lpStrokes)[i]->strokeType == PRIMITIVE_STROKE_ELLIPSE) ||
			((*lpStrokes)[i]->strokeType == PRIMITIVE_STROKE_ARC))
		{
			for (unsigned int j = i + 1; j < lpStrokes->size(); j++)
			{
				if (((*lpStrokes)[j]->strokeType == PRIMITIVE_STROKE_ELLIPSE) ||
					((*lpStrokes)[j]->strokeType == PRIMITIVE_STROKE_ARC))
				{
					PrimitiveSegment	seg;

					seg.startPoint	= ((PrimitiveEllipse*)lpObj->rgStrokes[rgMatch[i]])->center;
					seg.endPoint	= ((PrimitiveEllipse*)lpObj->rgStrokes[rgMatch[j]])->center;
					if (CalcDistance_Point_To_Point(seg.startPoint, seg.endPoint) > 5)
					{
						rgObjLine.push_back(seg);
					}
					else
					{
						continue;
					}

					seg.startPoint	= ((PrimitiveEllipse*)(*lpStrokes)[i])->center;
					seg.endPoint	= ((PrimitiveEllipse*)(*lpStrokes)[j])->center;
					if (CalcDistance_Point_To_Point(seg.startPoint, seg.endPoint) > 5)
					{
						rgUsrLine.push_back(seg);
					}
					else
					{
						rgObjLine.pop_back();
					}
				}
			}
		}
	}

	return CalcRotate_Line_based(rgObjLine, rgUsrLine);
}

RECOG_RESULT CalcSimilarity(GraphicObject* lpObj, Matrix<unsigned int>* lpRelModel, std::vector<PrimitiveStroke*> *lpStrokes, Matrix<unsigned int>* lpRelUser)
{
	std::vector<int>	rgMatch;
	std::vector<bool>	rgUsed;
	RECOG_RESULT		ret;
	double				dSimilarity = 0;

	ret.dRotate		= 0;
	ret.dSimilarity	= 0;

	for (int i = 0; i < lpObj->Count; i++)
	{
		rgUsed.push_back(false);
	}

	for (unsigned int i = 0; i < lpStrokes->size(); i++)
	{
		rgMatch.push_back(-1);
	}

	int idx = 0;

	while (idx >= 0)
	{
		do 
		{
			rgMatch[idx]++;
			if (rgMatch[idx] < lpObj->Count)
			{
				if (!rgUsed[rgMatch[idx]])
				{
					if ((*lpStrokes)[idx]->strokeType == lpObj->rgStrokes[rgMatch[idx]]->strokeType)
					{
						if (!IsRelationConflict(rgMatch, idx, lpRelModel, lpRelUser))
						{
							break;
						}
					}
				}
			}
		} while (rgMatch[idx] < lpObj->Count);

		if (rgMatch[idx] < lpObj->Count)
		{
			rgUsed[rgMatch[idx]] = true;

			if (idx < ((int)lpStrokes->size() - 1))
			{
				idx++;
				rgMatch[idx] = -1;
			}
			else
			{
				dSimilarity = CalcSimilarity_Internal(lpObj, rgMatch, idx, lpRelModel, lpRelUser);
				if (dSimilarity > ret.dSimilarity)
				{
//					ret.dRotate		= CalcRotate(lpObj, rgMatch, lpStrokes);
					ret.dSimilarity	= dSimilarity;
				}
				rgUsed[rgMatch[idx]] = false;
			}
		}
		else
		{
			idx--;
			if (idx >= 0)
			{
				rgUsed[rgMatch[idx]] = false;
			}
		}
	}

	return ret;
}