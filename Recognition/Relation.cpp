#include "stdafx.h"

#include <math.h>

#include "Primitives.h"
#include "Relation.h"

#define min(a,b) (((a) < (b)) ? (a) : (b))
#define max(a,b) (((a) > (b)) ? (a) : (b))

double sqr(double a)
{
	return a * a;
}

void CalcLineParam(PrimitiveSegment* lpSeg, double &A, double &B, double &C)
{
	A = lpSeg->startPoint.y - lpSeg->endPoint.y;
	B = lpSeg->endPoint.x - lpSeg->startPoint.x;
	C = lpSeg->startPoint.x * lpSeg->endPoint.y - lpSeg->endPoint.x * lpSeg->startPoint.y;
}

void CalcEllipseParam(PrimitiveEllipse* lpEllipse, double &a, double &b, double &c, double &d, double &e, double &f)
{
	double A2 = sqr((lpEllipse->rightBottom.x - lpEllipse->leftTop.x) / 2);
	double B2 = sqr((lpEllipse->rightBottom.y - lpEllipse->leftTop.y) / 2);
	double cosT = cos(lpEllipse->rotateAngle);
	double sinT = sin(lpEllipse->rotateAngle);
	double cosT2 = sqr(cosT);
	double sinT2 = sqr(sinT);
	double cosTsinT = cosT * sinT;
	double cx = lpEllipse->center.x;
	double cy = lpEllipse->center.y;

	a = cosT2 / A2 + sinT2 / B2;
	b = 2 * cosTsinT * (1 / A2 - 1 / B2);
	c = cosT2 / B2 + sinT2 / A2;
	d = 2 * cy * cosTsinT * (1 / B2 - 1 / A2) - 2 * cx * (cosT2 / A2 + sinT2 / B2);
	e = 2 * cx * cosTsinT * (1 / B2 - 1 / A2) - 2 * cy * (cosT2 / B2 + sinT2 / A2);
	f = 2 * cx * cy * cosTsinT * (1 / A2 - 1 / B2) + sqr(cx) * (cosT2 / A2 + sinT2 / B2) + sqr(cy) * (cosT2 / B2 + sinT2 / A2) - 1;
}

PrimitivePoint CalcProjectPoint(PrimitivePoint pt, PrimitiveSegment* lpSeg)
{
	double	A, B, C;

	CalcLineParam(lpSeg, A, B, C);

	double D = B * pt.x - A * pt.y;
	double A2B2 = A * A + B * B;

	pt.x = 0;
	pt.y = 0;

	if (fabs(A2B2) > DOUBLE_ZERO)
	{
		pt.x = -(A * C - B * D) / A2B2;
		pt.y = -(A * D + B * C) / A2B2;
	}

	return pt;
}

double CalcPointRatio(PrimitivePoint pt, PrimitiveSegment* lpSeg)
{
	double a = lpSeg->endPoint.x - lpSeg->startPoint.x;
	double b = lpSeg->endPoint.y - lpSeg->startPoint.y;

	if (fabs(a) > fabs(b))
	{
		return (pt.x - lpSeg->startPoint.x) / a;
	}
	else
	{
		return (pt.y - lpSeg->startPoint.y) / b;
	}
}

double CalcProjectPointRatio(PrimitivePoint pt, PrimitiveSegment* lpSeg)
{
	PrimitivePoint	p = CalcProjectPoint(pt, lpSeg);

	return CalcPointRatio(p, lpSeg);
}

double CalcEllipseAngle(PrimitiveEllipse* lpEllipse, double angle)
{
	double a = (lpEllipse->rightBottom.x - lpEllipse->leftTop.x) / 2;
	double b = (lpEllipse->rightBottom.y - lpEllipse->leftTop.y) / 2;
	double tx = a * cos(angle);
	double ty = a * sin(angle) * a / b;

	return atan2(ty, tx);
}

PrimitivePoint CalcEllipsePointByAngle(PrimitiveEllipse* lpEllipse, double angle)
{
	double cx = lpEllipse->center.x;
	double cy = lpEllipse->center.y;
	double a = (lpEllipse->rightBottom.x - lpEllipse->leftTop.x) / 2;
	double b = (lpEllipse->rightBottom.y - lpEllipse->leftTop.y) / 2;
	double rotateAngle = lpEllipse->rotateAngle;
	double cosT = cos(rotateAngle);
	double sinT = sin(rotateAngle);
	double tx	= a * cos(angle);
	double ty	= b * sin(angle);

	PrimitivePoint pt;

	pt.x = (int)(cx + tx * cosT - ty * sinT);
	pt.y = (int)(cy + tx * sinT + ty * cosT);

	return pt;
}

double CalcAngle_Line_To_X(PrimitiveSegment* lpSeg)
{
	double x = lpSeg->endPoint.x - lpSeg->startPoint.x;
	double y = lpSeg->endPoint.y - lpSeg->startPoint.y;

	if (y < 0)
	{
		y = -y;
		x = -x;
	}

	return atan2(y, x);
}

double CalcAngle_Vector_To_X(PrimitivePoint pt)
{
	double a = atan2(pt.y, pt.x);

	if (a < 0)
	{
		a += 2 * PI;
	}

	return a;
}

double CalcDistance_Point_To_Point(PrimitivePoint pt1, PrimitivePoint pt2)
{
	return sqrt(sqr(pt1.x - pt2.x) + sqr(pt1.y - pt2.y));
}

double CalcDistance_Point_To_Line(PrimitivePoint pt, PrimitiveSegment* lpSeg)
{
	double	A, B, C;

	CalcLineParam(lpSeg, A, B, C);

	return fabs(A * pt.x + B * pt.y + C) / sqrt(sqr(A) + sqr(B));
}

double CalcDistance_Point_To_Ellipse(PrimitivePoint pt, PrimitiveEllipse* lpEllipse)
{
	TransformationMatrix	matrix;
	PrimitivePoint			p;

	double a = (lpEllipse->rightBottom.x - lpEllipse->leftTop.x) / 2;
	double b = (lpEllipse->rightBottom.y - lpEllipse->leftTop.y) / 2;

	p = pt;

	matrix.Offset(-lpEllipse->center.x, -lpEllipse->center.y);
	matrix.Rotate(-lpEllipse->rotateAngle);

	p.Transform(matrix);

	p = CalcEllipsePointByAngle(lpEllipse, CalcEllipseAngle(lpEllipse, CalcAngle_Vector_To_X(p)));

	return CalcDistance_Point_To_Point(pt, p);
}

double CalcDistance_Elliptic_Arc(PrimitiveEllipse* lpEllipse, PrimitivePoint startPoint, PrimitivePoint endPoint)
{
	PrimitivePoint	pt, pt2;

	double dt	= PI / 1000;
	double cx	= lpEllipse->center.x;
	double cy	= lpEllipse->center.y;
	double a	= lpEllipse->x_axis;
	double b	= lpEllipse->y_axis;
	double cosT	= cos(lpEllipse->rotateAngle);
	double sinT	= sin(lpEllipse->rotateAngle);
	double startAngle;
	double endAngle;
	double dDistance = 0;

	if (startPoint == endPoint)
	{
		startAngle = 0;
		endAngle = 2 * PI;
	}
	else
	{
		TransformationMatrix matrix;

		matrix.Offset(-lpEllipse->center.x, -lpEllipse->center.y);
		matrix.Rotate(-lpEllipse->rotateAngle);

		pt = startPoint;
		pt.Transform(matrix);
		startAngle = CalcEllipseAngle(lpEllipse, CalcAngle_Vector_To_X(pt));

		pt = endPoint;
		pt.Transform(matrix);
		endAngle = CalcEllipseAngle(lpEllipse, CalcAngle_Vector_To_X(pt));

		while (endAngle <= startAngle)
		{
			endAngle += 2 * PI;
		}
	}

	pt = CalcEllipsePointByAngle(lpEllipse, startAngle);

	for (double t = startAngle; t <= endAngle; t+= dt)
	{
		pt2 = CalcEllipsePointByAngle(lpEllipse, t);
		dDistance += CalcDistance_Point_To_Point(pt, pt2);
		pt = pt2;
	}

	return dDistance;
}

bool IsPointInSegmentRange(PrimitivePoint pt, PrimitiveSegment* lpSeg)
{
	double minX = min(lpSeg->startPoint.x, lpSeg->endPoint.x);
	double maxX = max(lpSeg->startPoint.x, lpSeg->endPoint.x);
	double minY = min(lpSeg->startPoint.y, lpSeg->endPoint.y);
	double maxY = max(lpSeg->startPoint.y, lpSeg->endPoint.y);

	if (fabs(maxX - minX) < DOUBLE_ZERO)
	{
		minX -= 0.1;
		maxX += 0.1;
	}

	if (fabs(maxY - minY) < DOUBLE_ZERO)
	{
		minY -= 0.1;
		maxY += 0.1;
	}

	return ((minX <= pt.x) && (pt.x <= maxX) && (minY <= pt.y) && (pt.y <=maxY));
}

bool IsPointInArcRange(PrimitivePoint pt, PrimitiveArc* lpArc)
{
	TransformationMatrix matrix;
	PrimitivePoint	p;
	double startAngle;
	double endAngle;
	double angle;

	matrix.Offset(-lpArc->center.x, -lpArc->center.y);
	matrix.Rotate(-lpArc->rotateAngle);

	p = lpArc->startPoint;
	p.Transform(matrix);
	startAngle = CalcAngle_Vector_To_X(p);

	p = lpArc->endPoint;
	p.Transform(matrix);
	endAngle = CalcAngle_Vector_To_X(p);

	pt.Transform(matrix);
	angle = CalcAngle_Vector_To_X(pt);

	while (endAngle <= startAngle) endAngle += 2 * PI;
	while (angle < startAngle + DOUBLE_ZERO) angle += 2 * PI;

	return (angle <= endAngle);
}

void CalcIntersection_Line_To_Line(PrimitiveSegment* lpSeg1, PrimitiveSegment* lpSeg2, std::vector<PrimitivePoint>& rgIntersection)
{
	double A1, B1, C1;
	double A2, B2, C2;

	CalcLineParam(lpSeg1, A1, B1, C1);
	CalcLineParam(lpSeg2, A2, B2, C2);

	double d = A2 * B1 - A1 * B2;

	if (fabs(d) > DOUBLE_ZERO)
	{
		PrimitivePoint	pt;

		pt.x = ((B2 * C1 - B1 * C2) / d);
		pt.y = ((A1 * C2 - A2 * C1) / d);

		rgIntersection.clear();
		rgIntersection.push_back(pt);
	}
}

void CalcIntersection_Segment_To_Segment(PrimitiveSegment* lpSeg1, PrimitiveSegment* lpSeg2, std::vector<PrimitivePoint>& rgIntersection)
{
	std::vector<PrimitivePoint> rgCandidate;

	CalcIntersection_Line_To_Line(lpSeg1, lpSeg2, rgCandidate);

	for (unsigned int i = 0; i < rgCandidate.size(); i++)
	{
		if (IsPointInSegmentRange(rgCandidate[i], lpSeg1) && (IsPointInSegmentRange(rgCandidate[i], lpSeg2)))
		{
			rgIntersection.push_back(rgCandidate[i]);
		}
	}
}

void CalcIntersection_Line_To_Ellipse(PrimitiveSegment* lpSeg, PrimitiveEllipse* lpEllipse, std::vector<PrimitivePoint>& rgIntersection)
{
	double A, B, C;
	double a, b, c, d, e, f;
	PrimitivePoint	p1, p2;

	CalcLineParam(lpSeg, A, B, C);
	CalcEllipseParam(lpEllipse, a, b, c, d, e, f);

	if (A != 0)
	{
		double pA = -A * b * B + a * sqr(B) + sqr(A) * c;
		double pB = 2 * a * B * C - A * b * C - A * B * d + sqr(A) * e;
		double pC = a * sqr(C) - A * C * d + sqr(A) * f;
		double pD = A * b * B * C - 2 * sqr(A) * c * C - A * sqr(B) * d + sqr(A) * B * e;
		double B24AC = sqr(pB) - 4 * pA * pC;

		if ((B24AC >= 0) && (fabs(2 * pA) > DOUBLE_ZERO) && (fabs(2 * A * pA) > DOUBLE_ZERO))
		{
			p1.x = (pD - B * sqrt(B24AC)) / (2 * A * pA);
			p1.y = (-pB + sqrt(B24AC)) / (2 * pA);

			p2.x = (pD + B * sqrt(B24AC)) / (2 * A * pA);
			p2.y = -(pB + sqrt(B24AC)) / (2 * pA);

			rgIntersection.push_back(p1);
			rgIntersection.push_back(p2);
		}
	}
	else
	{
		double pA = a * sqr(B);
		double pB = b * B * C - sqr(B) * d;
		double pC = c * sqr(C) + B * (B * f - C * e);
		double B24AC = sqr(pB) - 4 * pA * pC;

		if ((B24AC >= 0) && (fabs(B) > DOUBLE_ZERO) && (fabs(pA * 2) > DOUBLE_ZERO))
		{
			p1.x = (pB - sqrt(B24AC)) / (2 * pA);
			p1.y = -C / B;

			p2.x = (pB + sqrt(B24AC)) / (2 * pA);
			p2.y = -C / B;

			rgIntersection.push_back(p1);
			rgIntersection.push_back(p2);
		}
	}
}

void CalcIntersection_Segment_To_Ellipse(PrimitiveSegment* lpSeg, PrimitiveEllipse* lpEllipse, std::vector<PrimitivePoint>& rgIntersection)
{
	std::vector<PrimitivePoint> rgCandidate;

	CalcIntersection_Line_To_Ellipse(lpSeg, lpEllipse, rgCandidate);

	for (unsigned int i = 0; i < rgCandidate.size(); i++)
	{
		if (IsPointInSegmentRange(rgCandidate[i], lpSeg))
		{
			rgIntersection.push_back(rgCandidate[i]);
		}
	}
}

void CalcIntersection_Segment_To_Arc(PrimitiveSegment* lpSeg, PrimitiveArc* lpArc, std::vector<PrimitivePoint>& rgIntersection)
{
	std::vector<PrimitivePoint> rgCandidate;

	CalcIntersection_Line_To_Ellipse(lpSeg, (PrimitiveEllipse*)lpArc, rgCandidate);

	for (unsigned int i = 0; i < rgCandidate.size(); i++)
	{
		if (IsPointInSegmentRange(rgCandidate[i], lpSeg) && IsPointInArcRange(rgCandidate[i], lpArc))
		{
			rgIntersection.push_back(rgCandidate[i]);
		}
	}
}

void CalcIntersection_Ellipse_To_Ellipse(PrimitiveEllipse* lpEllipse1, PrimitiveEllipse* lpEllipse2, std::vector<PrimitivePoint>& rgIntersection)
{
	PrimitivePoint		pt;
	std::vector<double>	rgDistance;
	std::vector<double> rgMinPoint;

	double cx = lpEllipse1->center.x;
	double cy = lpEllipse1->center.y;
	double a = (lpEllipse1->rightBottom.x - lpEllipse1->leftTop.x) / 2;
	double b = (lpEllipse1->rightBottom.y - lpEllipse1->leftTop.y) / 2;
	double cosT = cos(lpEllipse1->rotateAngle);
	double sinT = sin(lpEllipse1->rotateAngle);
	double dt = PI / 360;
	double tx, ty;

	for (double t = 0; t < PI * 2.5; t += dt)
	{
		tx = a * cos(t);
		ty = b * sin(t);
		pt.x = cx + tx * cosT - ty * sinT;
		pt.y = cy + tx * sinT + ty * cosT;

		rgDistance.push_back(CalcDistance_Point_To_Ellipse(pt, lpEllipse2));
	}

	unsigned int i, j;

	i = 0;

	while (i < rgDistance.size())
	{
		i++;

		while (i < rgDistance.size())
		{
			if ((rgDistance[i] < 3) && (rgDistance[i] - rgDistance[i - 1]) < 0)
			{
				break;
			}
			i++;
		}

		while (i < rgDistance.size())
		{
			if (rgDistance[i] - rgDistance[i - 1] > 0)
			{
				break;
			}
			i++;
		}

		if ((i < rgDistance.size()) && (rgDistance[i] < 1))
		{
			rgMinPoint.push_back(dt * (i - 1));
		}
	}

	for (i = 0; i < rgMinPoint.size(); i++)
	{
		tx = a * cos(rgMinPoint[i]);
		ty = b * sin(rgMinPoint[i]);
		pt.x = cx + tx * cosT - ty * sinT;
		pt.y = cy + tx * sinT + ty * cosT;

		j = 0;

		for (j = 0; j < rgIntersection.size(); j++)
		{
			if (CalcDistance_Point_To_Point(pt, rgIntersection[j]) < 0.5)
			{
				break;
			}
		}

		if (j == rgIntersection.size())
		{
			rgIntersection.push_back(pt);
		}
	}
}

void CalcIntersection_Ellipse_To_Arc(PrimitiveEllipse* lpEllipse, PrimitiveArc* lpArc, std::vector<PrimitivePoint>& rgIntersection)
{
	std::vector<PrimitivePoint> rgCandidate;

	CalcIntersection_Ellipse_To_Ellipse(lpEllipse, (PrimitiveEllipse*)lpArc, rgCandidate);

	for (unsigned int i = 0; i < rgCandidate.size(); i++)
	{
		if (IsPointInArcRange(rgCandidate[i], lpArc))
		{
			rgIntersection.push_back(rgCandidate[i]);
		}
	}
}

void CalcIntersection_Arc_To_Arc(PrimitiveArc* lpArc1, PrimitiveArc* lpArc2, std::vector<PrimitivePoint>& rgIntersection)
{
	std::vector<PrimitivePoint> rgCandidate;

	CalcIntersection_Ellipse_To_Ellipse((PrimitiveEllipse*)lpArc1, (PrimitiveEllipse*)lpArc2, rgCandidate);

	for (unsigned int i = 0; i < rgCandidate.size(); i++)
	{
		if (IsPointInArcRange(rgCandidate[i], lpArc1) && IsPointInArcRange(rgCandidate[i], lpArc2))
		{
			rgIntersection.push_back(rgCandidate[i]);
		}
	}
}

unsigned int CalcRelation_Segment_To_Segment(PrimitiveSegment* lpSeg1, PrimitiveSegment* lpSeg2, double DistanceThreshold, double AngleThreshold)
{
	unsigned int uiRelation = 0;
	std::vector<PrimitivePoint> rgIntersection;

	//Conjoint_0
	if ((CalcDistance_Point_To_Point(lpSeg1->startPoint, lpSeg2->startPoint) <= DistanceThreshold) ||
		(CalcDistance_Point_To_Point(lpSeg1->endPoint, lpSeg2->startPoint) <= DistanceThreshold))
	{
		uiRelation |= RELATION_CONJOINT_0;
		goto PERPENDICULAR;
	}

	//Conjoint_1
	if ((CalcDistance_Point_To_Point(lpSeg1->startPoint, lpSeg2->endPoint) <= DistanceThreshold) ||
		(CalcDistance_Point_To_Point(lpSeg1->endPoint, lpSeg2->endPoint) <= DistanceThreshold))
	{
		uiRelation |= RELATION_CONJOINT_1;
		goto PERPENDICULAR;
	}

	//Tangent
	if (CalcDistance_Point_To_Line(lpSeg1->startPoint, lpSeg2) <= DistanceThreshold)
	{
		double ratio = CalcProjectPointRatio(lpSeg1->startPoint, lpSeg2);

		if ((0 < ratio) && (ratio < 1))
		{
			uiRelation |= RELATION_TANGENT;
			goto PERPENDICULAR;
		}
	}

	//Tangent
	if (CalcDistance_Point_To_Line(lpSeg1->endPoint, lpSeg2) <= DistanceThreshold)
	{
		double ratio = CalcProjectPointRatio(lpSeg1->endPoint, lpSeg2);

		if ((0 < ratio) && (ratio < 1))
		{
			uiRelation |= RELATION_TANGENT;
			goto PERPENDICULAR;
		}
	}

	CalcIntersection_Line_To_Line(lpSeg1, lpSeg2, rgIntersection);

	//Intersected
	if (rgIntersection.size() == 1)
	{
		if (IsPointInSegmentRange(rgIntersection[0], lpSeg1) && IsPointInSegmentRange(rgIntersection[0], lpSeg2))
		{
			uiRelation |= RELATION_INTERSECTED;
		}
	}

	//Intersected
	if ((uiRelation & RELATION_TANGENT) == 0)
	{
		if (CalcDistance_Point_To_Line(lpSeg2->startPoint, lpSeg1) <= DistanceThreshold)
		{
			double ratio = CalcProjectPointRatio(lpSeg2->startPoint, lpSeg1);

			if ((0 < ratio) && (ratio < 1))
			{
				uiRelation |= RELATION_INTERSECTED;
			}
		}

		if (CalcDistance_Point_To_Line(lpSeg2->endPoint, lpSeg1) <= DistanceThreshold)
		{
			double ratio = CalcProjectPointRatio(lpSeg2->endPoint, lpSeg1);

			if ((0 < ratio) && (ratio < 1))
			{
				uiRelation |= RELATION_INTERSECTED;
			}
		}
	}

PERPENDICULAR:

	double angle1 = CalcAngle_Line_To_X(lpSeg1);
	double angle2 = CalcAngle_Line_To_X(lpSeg2);

	//Perpendicular
	if (fabs(fabs(angle1 - angle2) - PI / 2) <= AngleThreshold)
	{
		if ((uiRelation & RELATION_INTERSECTED) != 0)
		{
			uiRelation |= RELATION_PERPENDICULAR;
		}
		else
		{
			double MaxAllowedDistance = min(CalcDistance_Point_To_Point(lpSeg1->startPoint, lpSeg1->endPoint), CalcDistance_Point_To_Point(lpSeg2->startPoint, lpSeg2->endPoint)) / 4;
			double ratio;

			if (CalcDistance_Point_To_Line(lpSeg1->startPoint, lpSeg2) <= MaxAllowedDistance)
			{
				ratio = CalcProjectPointRatio(lpSeg1->startPoint, lpSeg2);
				if ((-0.25 <= ratio) && (ratio <= 1.25))
				{
					uiRelation |= RELATION_PERPENDICULAR;
				}
			}

			if (CalcDistance_Point_To_Line(lpSeg1->endPoint, lpSeg2) <= MaxAllowedDistance)
			{
				ratio = CalcProjectPointRatio(lpSeg1->endPoint, lpSeg2);
				if ((-0.25 <= ratio) && (ratio <= 1.25))
				{
					uiRelation |= RELATION_PERPENDICULAR;
				}
			}

			if (CalcDistance_Point_To_Line(lpSeg2->startPoint, lpSeg1) <= MaxAllowedDistance)
			{
				ratio = CalcProjectPointRatio(lpSeg2->startPoint, lpSeg1);
				if ((-0.25 <= ratio) && (ratio <= 1.25))
				{
					uiRelation |= RELATION_PERPENDICULAR;
				}
			}

			if (CalcDistance_Point_To_Line(lpSeg2->endPoint, lpSeg1) <= MaxAllowedDistance)
			{
				ratio = CalcProjectPointRatio(lpSeg2->endPoint, lpSeg1);
				if ((-0.25 <= ratio) && (ratio <= 1.25))
				{
					uiRelation |= RELATION_PERPENDICULAR;
				}
			}
		}
	}

	//Parallel
	if (fabs(angle1 - angle2) <= AngleThreshold)
	{
		PrimitivePoint		startPt, endPt;
		PrimitiveSegment*	lpSeg;

		if (CalcDistance_Point_To_Point(lpSeg1->startPoint, lpSeg1->endPoint) < CalcDistance_Point_To_Point(lpSeg2->startPoint, lpSeg2->endPoint))
		{
			lpSeg	= lpSeg2;
			startPt	= lpSeg1->startPoint;
			endPt	= lpSeg1->endPoint;
		}
		else
		{
			lpSeg	= lpSeg1;
			startPt	= lpSeg2->startPoint;
			endPt	= lpSeg2->endPoint;
		}

		double startRatio	= min(CalcProjectPointRatio(startPt, lpSeg), CalcProjectPointRatio(endPt, lpSeg));
		double endRatio		= max(CalcProjectPointRatio(startPt, lpSeg), CalcProjectPointRatio(endPt, lpSeg));

		if ((startRatio <= 1) && (endRatio >= 0))
		{
			startRatio	= max(0, startRatio);
			endRatio	= min(1, endRatio);

			if (endRatio - startRatio >= 0.5)
			{
				double d = (CalcDistance_Point_To_Line(startPt, lpSeg) + CalcDistance_Point_To_Line(endPt, lpSeg)) / 2;
				double l = CalcDistance_Point_To_Point(lpSeg->startPoint, lpSeg->endPoint);

				d = min(d, l) / 2;

				if (fabs(angle1 - angle2) < (AngleThreshold * (0.5 + d / l)))
				{
					uiRelation |= RELATION_PARALLEL;
				}
			}
		}
	}

	double A, B, C, d;

	CalcLineParam(lpSeg2, A, B, C);
	
	if (B < 0)
	{
		A = -A;
		B = -B;
		C = -C;
	}

	if ((fabs(B) < DOUBLE_ZERO) && (A < 0))
	{
		A = -A;
		B = -B;
		C = -C;
	}

	if ((uiRelation & RELATION_CONJOINT_0) == RELATION_CONJOINT_0)
	{
		if (CalcDistance_Point_To_Point(lpSeg1->startPoint, lpSeg2->startPoint) < CalcDistance_Point_To_Point(lpSeg1->endPoint, lpSeg2->startPoint))
		{
			d = A * lpSeg1->endPoint.x + B * lpSeg1->endPoint.y + C;
		}
		else
		{
			d = A * lpSeg1->startPoint.x + B * lpSeg1->startPoint.y + C;
		}

		if (fabs(d) > DOUBLE_ZERO)
		{
			if (d > 0)
			{
				uiRelation |= RELATION_POSITIVE;
			}
			else
			{
				uiRelation |= RELATION_NEGATIVE;
			}
		}
	}

	if ((uiRelation & RELATION_CONJOINT_1) == RELATION_CONJOINT_1)
	{
		if (CalcDistance_Point_To_Point(lpSeg1->startPoint, lpSeg2->endPoint) < CalcDistance_Point_To_Point(lpSeg1->endPoint, lpSeg2->endPoint))
		{
			d = A * lpSeg1->endPoint.x + B * lpSeg1->endPoint.y + C;
		}
		else
		{
			d = A * lpSeg1->startPoint.x + B * lpSeg1->startPoint.y + C;
		}

		if (fabs(d) > DOUBLE_ZERO)
		{
			if (d > 0)
			{
				uiRelation |= RELATION_POSITIVE;
			}
			else
			{
				uiRelation |= RELATION_NEGATIVE;
			}
		}
	}

	if ((uiRelation & RELATION_TANGENT) == RELATION_TANGENT)
	{
		if (CalcDistance_Point_To_Line(lpSeg1->startPoint, lpSeg2) < CalcDistance_Point_To_Line(lpSeg1->endPoint, lpSeg2))
		{
			d = A * lpSeg1->endPoint.x + B * lpSeg1->endPoint.y + C;
		}
		else
		{
			d = A * lpSeg1->startPoint.x + B * lpSeg1->startPoint.y + C;
		}

		if (fabs(d) > DOUBLE_ZERO)
		{
			if (d > 0)
			{
				uiRelation |= RELATION_POSITIVE;
			}
			else
			{
				uiRelation |= RELATION_NEGATIVE;
			}
		}
	}

	return uiRelation;
}

unsigned int CalcRelation_Segment_To_Ellipse(PrimitiveSegment* lpSeg, PrimitiveEllipse* lpEllipse, double DistanceThreshold, double AngleThreshold)
{
	unsigned int	uiRelation = 0;
	std::vector<PrimitivePoint> rgIntersection;

	CalcIntersection_Segment_To_Ellipse(lpSeg, lpEllipse, rgIntersection);

	if (rgIntersection.size() != 0)
	{
		uiRelation |= RELATION_INTERSECTED;
	}

	if ((CalcDistance_Point_To_Ellipse(lpSeg->startPoint, lpEllipse) <= DistanceThreshold) ||
		(CalcDistance_Point_To_Ellipse(lpSeg->endPoint, lpEllipse) <= DistanceThreshold))
	{
		uiRelation |= RELATION_TANGENT;
	}

	if (CalcDistance_Point_To_Line(lpEllipse->center, lpSeg) <= DistanceThreshold)
	{
		uiRelation |= RELATION_CONCENTRIC;
	}

	double a, b, c, d, e, f;

	CalcEllipseParam(lpEllipse, a, b, c, d, e, f);

	if (a < 0)
	{
		a = -a;	b = -b;
		c = -c;	d = -d;
		e = -e;	f = -f;
	}

	double d1, d2;

	d1 = a * sqr(lpSeg->startPoint.x) + b * lpSeg->startPoint.x * lpSeg->startPoint.y + c * sqr(lpSeg->startPoint.y) + d * lpSeg->startPoint.x + e * lpSeg->startPoint.y + f;
	d2 = a * sqr(lpSeg->endPoint.x) + b * lpSeg->endPoint.x * lpSeg->endPoint.y + c * sqr(lpSeg->endPoint.y) + d * lpSeg->endPoint.x + e * lpSeg->endPoint.y + f;

	if ((uiRelation & RELATION_INTERSECTED) == 0)
	{
		if ((fabs(d1) > DOUBLE_ZERO) && (fabs(d2) > DOUBLE_ZERO))
		{
			if ((d1 > 0) && (d2 > 0))
			{
				uiRelation |= RELATION_OUTSIDE;
			}
			else if ((d1 < 0) && (d2 <0))
			{
				uiRelation |= RELATION_INSIDE;
			}
		}
	}

	if (((uiRelation & RELATION_TANGENT) == RELATION_TANGENT))
	{
		if ((CalcDistance_Point_To_Ellipse(lpSeg->startPoint, lpEllipse) <= DistanceThreshold) &&
			(CalcDistance_Point_To_Ellipse(lpSeg->endPoint, lpEllipse) <= DistanceThreshold))
		{
			uiRelation |= RELATION_INSIDE;
		}
		else
		{
			if (CalcDistance_Point_To_Ellipse(lpSeg->startPoint, lpEllipse) < CalcDistance_Point_To_Ellipse(lpSeg->endPoint, lpEllipse))
			{
				if (fabs(d2) > DOUBLE_ZERO)
				{
					if (d2 > 0)
					{
						uiRelation |= RELATION_OUTSIDE;
					}
					else
					{
						uiRelation |= RELATION_INSIDE;
					}
				}
			}
			else
			{
				if (fabs(d1) > DOUBLE_ZERO)
				{
					if (d1 > 0)
					{
						uiRelation |= RELATION_OUTSIDE;
					}
					else
					{
						uiRelation |= RELATION_INSIDE;
					}
				}
			}
		}
	}

	return uiRelation;
}

unsigned int CalcRelation_Ellipse_To_Ellipse(PrimitiveEllipse* lpEllipse1, PrimitiveEllipse* lpEllipse2, double DistanceThreshold, double AngleThreshold)
{
	unsigned int uiRelation = 0;
	std::vector<PrimitivePoint> rgIntersection;

	double a1 = lpEllipse1->x_axis;
	double b1 = lpEllipse1->y_axis;
	double a2 = lpEllipse2->x_axis;
	double b2 = lpEllipse2->y_axis;

	CalcIntersection_Ellipse_To_Ellipse(lpEllipse1, lpEllipse2, rgIntersection);

	if (rgIntersection.size() != 0)
	{
		uiRelation |= RELATION_INTERSECTED;
	}

	if (CalcDistance_Point_To_Point(lpEllipse1->center, lpEllipse2->center) <= DistanceThreshold)
	{
		uiRelation |= RELATION_CONCENTRIC;
	}

	if ((uiRelation & RELATION_INTERSECTED) == 0)
	{
		double a, b, c, d, e, f;

		CalcEllipseParam(lpEllipse2, a, b, c, d, e, f);

		if (a < 0)
		{
			a = -a;	b = -b;
			c = -c;	d = -d;
			e = -e;	f = -f;
		}

		double dist = a * sqr(lpEllipse1->center.x) + b * lpEllipse1->center.x * lpEllipse1->center.y + c * sqr(lpEllipse1->center.y) + d * lpEllipse1->center.x + e * lpEllipse1->center.y + f;

		if (fabs(dist) > DOUBLE_ZERO)
		{
			if (dist > 0)
			{
				uiRelation |= RELATION_OUTSIDE;
			}
		}
	}

	return uiRelation;
}

unsigned int CalcRelation_Ellipse_To_Arc(PrimitiveEllipse* lpEllipse, PrimitiveArc* lpArc, double DistanceThreshold, double AngleThreshold)
{
	unsigned int uiRelation = 0;
	std::vector<PrimitivePoint> rgIntersection;

	double a1 = lpEllipse->x_axis;
	double b1 = lpEllipse->y_axis;
	double a2 = lpArc->x_axis;
	double b2 = lpArc->y_axis;

	CalcIntersection_Ellipse_To_Arc(lpEllipse, lpArc, rgIntersection);

	if (rgIntersection.size() != 0)
	{
		uiRelation |= RELATION_INTERSECTED;
	}

	if (CalcDistance_Point_To_Point(lpEllipse->center, lpArc->center) <= DistanceThreshold)
	{
		uiRelation |= RELATION_CONCENTRIC;
	}

	if ((CalcDistance_Point_To_Ellipse(lpArc->startPoint, lpEllipse) <= DistanceThreshold) ||
		(CalcDistance_Point_To_Ellipse(lpArc->endPoint, lpEllipse) <= DistanceThreshold))
	{
		uiRelation |= RELATION_TANGENT;
	}

	return uiRelation;
}

unsigned int CalcRelation_Segment_To_Arc(PrimitiveSegment* lpSeg, PrimitiveArc* lpArc, double DistanceThreshold, double AngleThreshold)
{
	unsigned int uiRelation = 0;
	std::vector<PrimitivePoint> rgIntersection;

	CalcIntersection_Segment_To_Arc(lpSeg, lpArc, rgIntersection);

	if (rgIntersection.size() != 0)
	{
		uiRelation |= RELATION_INTERSECTED;
	}

	if (CalcDistance_Point_To_Line(lpArc->startPoint, lpSeg) <= DistanceThreshold)
	{
		double ratio = CalcProjectPointRatio(lpArc->startPoint, lpSeg);

		if ((0 < ratio) && (ratio < 1))
		{
			uiRelation |= RELATION_INTERSECTED;
		}
	}

	if (CalcDistance_Point_To_Line(lpArc->endPoint, lpSeg) <= DistanceThreshold)
	{
		double ratio = CalcProjectPointRatio(lpArc->endPoint, lpSeg);

		if ((0 < ratio) && (ratio < 1))
		{
			uiRelation |= RELATION_INTERSECTED;
		}
	}

	if ((CalcDistance_Point_To_Point(lpSeg->startPoint, lpArc->startPoint) <= DistanceThreshold) ||
		(CalcDistance_Point_To_Point(lpSeg->endPoint, lpArc->startPoint) <= DistanceThreshold))
	{
		uiRelation |= RELATION_CONJOINT_0;
	}

	if ((CalcDistance_Point_To_Point(lpSeg->startPoint, lpArc->endPoint) <= DistanceThreshold) ||
		(CalcDistance_Point_To_Point(lpSeg->endPoint, lpArc->endPoint) <= DistanceThreshold))
	{
		uiRelation |= RELATION_CONJOINT_1;
	}

	if ((CalcDistance_Point_To_Ellipse(lpSeg->startPoint, (PrimitiveEllipse*)lpArc) <= DistanceThreshold) && 
		IsPointInArcRange(lpSeg->startPoint, lpArc))
	{
		uiRelation |= RELATION_TANGENT;
	}

	if ((CalcDistance_Point_To_Ellipse(lpSeg->endPoint, (PrimitiveEllipse*)lpArc) <= DistanceThreshold) && 
		IsPointInArcRange(lpSeg->endPoint, lpArc))
	{
		uiRelation |= RELATION_TANGENT;
	}

	if (CalcDistance_Point_To_Line(lpArc->center, lpSeg) <= DistanceThreshold)
	{
		uiRelation |= RELATION_CONCENTRIC;
	}

	return uiRelation;
}

unsigned int CalcRelation_Arc_To_Segment(PrimitiveArc* lpArc, PrimitiveSegment* lpSeg, double DistanceThreshold, double AngleThreshold)
{
	unsigned int uiRelation = 0;
	std::vector<PrimitivePoint> rgIntersection;

	CalcIntersection_Segment_To_Arc(lpSeg, lpArc, rgIntersection);

	if (rgIntersection.size() != 0)
	{
		uiRelation |= RELATION_INTERSECTED;
	}

	if ((CalcDistance_Point_To_Ellipse(lpSeg->startPoint, (PrimitiveEllipse*)lpArc) <= DistanceThreshold) && 
		IsPointInArcRange(lpSeg->startPoint, lpArc))
	{
		uiRelation |= RELATION_INTERSECTED;
	}

	if ((CalcDistance_Point_To_Ellipse(lpSeg->endPoint, (PrimitiveEllipse*)lpArc) <= DistanceThreshold) && 
		IsPointInArcRange(lpSeg->endPoint, lpArc))
	{
		uiRelation |= RELATION_INTERSECTED;
	}

	if ((CalcDistance_Point_To_Point(lpArc->startPoint, lpSeg->startPoint) <= DistanceThreshold) ||
		(CalcDistance_Point_To_Point(lpArc->endPoint, lpSeg->startPoint) <= DistanceThreshold))
	{
		uiRelation |= RELATION_CONJOINT_0;
	}

	if ((CalcDistance_Point_To_Point(lpArc->startPoint, lpSeg->endPoint) <= DistanceThreshold) ||
		(CalcDistance_Point_To_Point(lpArc->endPoint, lpSeg->endPoint) <= DistanceThreshold))
	{
		uiRelation |= RELATION_CONJOINT_1;
	}

	if (CalcDistance_Point_To_Line(lpArc->startPoint, lpSeg) <= DistanceThreshold)
	{
		double ratio = CalcProjectPointRatio(lpArc->startPoint, lpSeg);

		if ((0 < ratio) && (ratio < 1))
		{
			uiRelation |= RELATION_TANGENT;
		}
	}

	if (CalcDistance_Point_To_Line(lpArc->endPoint, lpSeg) <= DistanceThreshold)
	{
		double ratio = CalcProjectPointRatio(lpArc->endPoint, lpSeg);

		if ((0 < ratio) && (ratio < 1))
		{
			uiRelation |= RELATION_TANGENT;
		}
	}

	if (CalcDistance_Point_To_Line(lpArc->center, lpSeg) <= DistanceThreshold)
	{
		uiRelation |= RELATION_CONCENTRIC;
	}

	return uiRelation;
}

unsigned int CalcRelation_Arc_To_Arc(PrimitiveArc* lpArc1, PrimitiveArc* lpArc2, double DistanceThreshold, double AngleThreshold)
{
	unsigned int uiRelation = 0;
	std::vector<PrimitivePoint> rgIntersection;

	double a1 = lpArc1->x_axis;
	double b1 = lpArc1->y_axis;
	double a2 = lpArc2->x_axis;
	double b2 = lpArc2->y_axis;

	CalcIntersection_Arc_To_Arc(lpArc1, lpArc2, rgIntersection);

	if (rgIntersection.size() != 0)
	{
		uiRelation |= RELATION_INTERSECTED;
	}

	if ((CalcDistance_Point_To_Point(lpArc1->startPoint, lpArc2->startPoint) <= DistanceThreshold) ||
		(CalcDistance_Point_To_Point(lpArc1->endPoint, lpArc2->startPoint) <= DistanceThreshold))
	{
		uiRelation |= RELATION_CONJOINT_0;
	}

	if ((CalcDistance_Point_To_Point(lpArc1->startPoint, lpArc2->endPoint) <= DistanceThreshold) ||
		(CalcDistance_Point_To_Point(lpArc1->endPoint, lpArc2->endPoint) <= DistanceThreshold))
	{
		uiRelation |= RELATION_CONJOINT_1;
	}

	if ((CalcDistance_Point_To_Ellipse(lpArc1->startPoint, (PrimitiveEllipse*)lpArc2) <= DistanceThreshold) && 
		IsPointInArcRange(lpArc1->startPoint, lpArc2))
	{
		uiRelation |= RELATION_TANGENT;
	}

	if ((CalcDistance_Point_To_Ellipse(lpArc1->endPoint, (PrimitiveEllipse*)lpArc2) <= DistanceThreshold) && 
		IsPointInArcRange(lpArc1->endPoint, lpArc2))
	{
		uiRelation |= RELATION_TANGENT;
	}

	if (CalcDistance_Point_To_Point(lpArc1->center, lpArc2->center) <= DistanceThreshold)
	{
		uiRelation |= RELATION_CONCENTRIC;
	}

	return uiRelation;
}

unsigned int CalcRelation(PrimitiveStroke* lpStroke1, PrimitiveStroke* lpStroke2,  double DistanceThreshold, double AngleThreshold)
{
	unsigned int uiRelation = 0;

	if (lpStroke1->strokeType == PRIMITIVE_STROKE_SEGMENT)
	{
		if (lpStroke2->strokeType == PRIMITIVE_STROKE_SEGMENT)
		{
			uiRelation = CalcRelation_Segment_To_Segment((PrimitiveSegment*)lpStroke1, (PrimitiveSegment*)lpStroke2, DistanceThreshold, AngleThreshold);
		}
		else if (lpStroke2->strokeType == PRIMITIVE_STROKE_ELLIPSE)
		{
			uiRelation = CalcRelation_Segment_To_Ellipse((PrimitiveSegment*)lpStroke1, (PrimitiveEllipse*)lpStroke2, DistanceThreshold, AngleThreshold);
		}
		else if (lpStroke2->strokeType == PRIMITIVE_STROKE_ARC)
		{
			uiRelation = CalcRelation_Segment_To_Arc((PrimitiveSegment*)lpStroke1, (PrimitiveArc*)lpStroke2, DistanceThreshold, AngleThreshold);
		}
	}
	else if (lpStroke1->strokeType == PRIMITIVE_STROKE_ELLIPSE)
	{
		if (lpStroke2->strokeType == PRIMITIVE_STROKE_SEGMENT)
		{
			uiRelation = CalcRelation_Segment_To_Ellipse((PrimitiveSegment*)lpStroke2, (PrimitiveEllipse*)lpStroke1, DistanceThreshold, AngleThreshold);
		}
		else if (lpStroke2->strokeType == PRIMITIVE_STROKE_ELLIPSE)
		{
			uiRelation = CalcRelation_Ellipse_To_Ellipse((PrimitiveEllipse*)lpStroke1, (PrimitiveEllipse*)lpStroke2, DistanceThreshold, AngleThreshold);
		}
		else if (lpStroke2->strokeType == PRIMITIVE_STROKE_ARC)
		{
			uiRelation = CalcRelation_Ellipse_To_Arc((PrimitiveEllipse*)lpStroke1, (PrimitiveArc*)lpStroke2, DistanceThreshold, AngleThreshold);
		}
	}
	else if (lpStroke1->strokeType == PRIMITIVE_STROKE_ARC)
	{
		if (lpStroke2->strokeType == PRIMITIVE_STROKE_SEGMENT)
		{
			uiRelation = CalcRelation_Arc_To_Segment((PrimitiveArc*)lpStroke1, (PrimitiveSegment*)lpStroke2, DistanceThreshold, AngleThreshold);
		}
		else if (lpStroke2->strokeType == PRIMITIVE_STROKE_ELLIPSE)
		{
			uiRelation = CalcRelation_Ellipse_To_Arc((PrimitiveEllipse*)lpStroke2, (PrimitiveArc*)lpStroke1, DistanceThreshold, AngleThreshold);
		}
		else if (lpStroke2->strokeType == PRIMITIVE_STROKE_ARC)
		{
			uiRelation = CalcRelation_Arc_To_Arc((PrimitiveArc*)lpStroke1, (PrimitiveArc*)lpStroke2, DistanceThreshold, AngleThreshold);
		}
	}

	return uiRelation;
}
