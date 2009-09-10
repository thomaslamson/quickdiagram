#include "stdafx.h"

#include <Windows.h>

#include "CommonFunction.h"
#include "Primitives.h"
#include "Relation.h"
#include "Matrix.h"

typedef struct tagPrimitiveVertex
{
	double	x;
	double	y;
	bool	bMovable;
	std::vector<PrimitivePoint*>	rgPoints;
	std::vector<PrimitiveStroke*>	rgStrokes;
}
PrimitiveVertex;

bool IsStrokeSetCross(std::vector<PrimitiveStroke*> &rgStrokes1, std::vector<PrimitiveStroke*> &rgStrokes2)
{
	for (unsigned int i = 0; i < rgStrokes1.size(); i++)
	{
		for (unsigned int j = 0; j < rgStrokes2.size(); j++)
		{
			if (rgStrokes1[i] == rgStrokes2[j])
			{
				return true;
			}
		}
	}
	return false;
}

void UpdateVertex(std::vector<PrimitiveVertex> &rgVertex, PrimitiveStroke* lpStroke, PrimitivePoint* lpPoint, bool bMovable)
{
	for (unsigned int i = 0; i < rgVertex.size(); i++)
	{
		if (DOUBLE_EQUAL(rgVertex[i].x, lpPoint->x) && DOUBLE_EQUAL(rgVertex[i].y, lpPoint->y))
		{
			rgVertex[i].rgPoints.push_back(lpPoint);
			rgVertex[i].rgStrokes.push_back(lpStroke);
			rgVertex[i].bMovable &= bMovable;
			return;
		}
	}

	PrimitiveVertex vertex;

	vertex.x = lpPoint->x;
	vertex.y = lpPoint->y;
	vertex.bMovable = bMovable;
	vertex.rgPoints.push_back(lpPoint);
	vertex.rgStrokes.push_back(lpStroke);

	rgVertex.push_back(vertex);
}

void RefineStrokes_MergeVertex(std::vector<PrimitiveStroke*> &rgStrokes, double DistanceThreshold)
{
	std::vector<PrimitiveVertex>	rgVertex;

	for (unsigned int i = 0; i < rgStrokes.size(); i++)
	{
		switch (rgStrokes[i]->strokeType)
		{
		case PRIMITIVE_STROKE_SEGMENT:
			{
				UpdateVertex(rgVertex, rgStrokes[i], &((PrimitiveSegment*)rgStrokes[i])->startPoint, true);
				UpdateVertex(rgVertex, rgStrokes[i], &((PrimitiveSegment*)rgStrokes[i])->endPoint, true);
			}
			break;
		case PRIMITIVE_STROKE_ARC:
			{
				UpdateVertex(rgVertex, rgStrokes[i], &((PrimitiveArc*)rgStrokes[i])->startPoint, false);
				UpdateVertex(rgVertex, rgStrokes[i], &((PrimitiveArc*)rgStrokes[i])->endPoint, false);
			}
			break;
		}
	}

	for (unsigned int i = 0; i < rgVertex.size(); i++)
	{
		unsigned int j = i + 1;
		while (j < rgVertex.size())
		{
			if ((rgVertex[i].bMovable || rgVertex[j].bMovable) && (!IsStrokeSetCross(rgVertex[i].rgStrokes, rgVertex[j].rgStrokes)))
			{
				if (CalcDistance_Point_To_Point(*rgVertex[i].rgPoints[0], *rgVertex[j].rgPoints[0]) <= DistanceThreshold)
				{
					if (!rgVertex[j].bMovable)
					{
						rgVertex[i].bMovable = false;
						rgVertex[i].x = rgVertex[j].x;
						rgVertex[i].y = rgVertex[j].y;
					}
					else if (rgVertex[i].bMovable)
					{
						rgVertex[i].x = (rgVertex[i].x + rgVertex[j].x) / 2;
						rgVertex[i].y = (rgVertex[i].y + rgVertex[j].y) / 2;
					}

					rgVertex[i].rgPoints.insert(rgVertex[i].rgPoints.begin(), rgVertex[j].rgPoints.begin(), rgVertex[j].rgPoints.end());

					for (unsigned int k = 0; k < rgVertex[i].rgPoints.size(); k++)
					{
						rgVertex[i].rgPoints[k]->x = rgVertex[i].x;
						rgVertex[i].rgPoints[k]->y = rgVertex[i].y;
					}

					rgVertex.erase(rgVertex.begin() + j);
					continue;
				}
			}

			j++;
		}
	}
}