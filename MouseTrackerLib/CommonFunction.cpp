#include "stdafx.h"

#include <windows.h>
#include <math.h>

double PI			= 3.141592653589793238462643383279502884197169399;
double DOUBLE_ZERO	= 1e-20;

void CalcLineParam(POINT pt1, POINT pt2, double &A, double &B, double &C)
{
	A = pt1.y - pt2.y;
	B = pt2.x - pt1.x;
	C = pt1.x * pt2.y - pt2.x * pt1.y;
}

POINT CalcProjectPoint(double A, double B, double C, POINT pt)
{
	double D = B * pt.x - A * pt.y;
	double A2B2 = A * A + B * B;

	pt.x = 0;
	pt.y = 0;

	if (fabs(A2B2) > DOUBLE_ZERO)
	{
		pt.x = (LONG)(-(A * C - B * D) / A2B2);
		pt.y = (LONG)(-(A * D + B * C) / A2B2);
	}

	return pt;
}

bool CalcIntersectPoint(double A1, double B1, double C1, double A2, double B2, double C2, POINT &pt)
{
	double d = A2 * B1 - A1 * B2;

	if (d < DOUBLE_ZERO)
	{
		return false;
	}

	pt.x = (LONG)((B2 * C1 - B1 * C2) / d);
	pt.y = -(LONG)((A2 * C1 - A1 * C2) / d);

	return true;
}

double CalcAngleVectorToX(POINT startPoint, POINT endPoint)
{
	return atan2((double)(endPoint.y - startPoint.y), (double)(endPoint.x - startPoint.x));
}

double RegularizeAngle(double angle)
{
	while (angle < 0)
	{
		angle += 2 * PI;
	}

	while (angle > 2 * PI)
	{
		angle -= 2 * PI;
	}

	if (angle > PI)
	{
		angle -= 2 * PI;
	}

	return angle;
}


void RegularizeAngleRange(double &startAngle, double &endAngle)
{
	while (startAngle > 2 * PI)
	{
		startAngle -= 2 * PI;
	}
	while (startAngle < 0)
	{
		startAngle += 2 * PI;
	}
	while (endAngle > 2 * PI)
	{
		endAngle -= 2 * PI;
	}
	while (endAngle < 0)
	{
		endAngle += 2 * PI;
	}

	double a = min(startAngle, endAngle);
	double b = max(startAngle, endAngle);

	if (b - a > PI)
	{
		startAngle	= b;
		endAngle	= a + 2 * PI;
	}
	else
	{
		startAngle	= a;
		endAngle	= b;
	}
}

void MergeAngleRange(double &startAngle, double &endAngle, double angle1, double angle2)
{
	double angle = endAngle - startAngle;

	angle1 -= startAngle;
	angle2 -= startAngle;

	while (angle1 < 0) 
	{
		angle1 += 2 * PI;
		angle2 += 2 * PI;
	}

	while (angle1 > 2 * PI)
	{
		angle1 -= 2 * PI;
		angle2 -= 2 * PI;
	}

	if (angle1 <= angle)
	{
		if (angle2 > angle)
		{
			endAngle = startAngle + angle2;
		}
	}
	else
	{
		if (angle2 >= 2 * PI)
		{
			startAngle -= (2 * PI - angle1);
		}
		else
		{
			double d1 = angle1 - angle;
			double d2 = 2 * PI - angle2;

			if (d1 < d2)
			{
				endAngle = startAngle + angle2;
			}
			else
			{
				startAngle -= (2 * PI - angle1);
			}
		}
	}

	while (startAngle < 0)
	{
		startAngle	+= 2 * PI;
		endAngle	+= 2 * PI;
	}
}

double ConvertToEllipseAngle(double angle, double a, double b)
{
	double tx = a * cos(angle);
	double ty = a * sin(angle) * a / b;

	return atan2(ty, tx);
}

double DistributionSizeToLine(POINT pt1, POINT pt2, POINT* rgPoints, int nPoints)
{
	double A, B, C;
	double A2B2;
	double minDist, maxDist, dist;

	CalcLineParam(pt1, pt2, A, B, C);
	A2B2 = sqrt(A * A + B * B);

	for (int i = 0; i < nPoints; i++)
	{
		dist = (A * rgPoints[i].x + B * rgPoints[i].y + C) / A2B2;

		if (i == 0)
		{
			minDist = dist;
			maxDist = dist;
		}
		else
		{
			minDist = min(minDist, dist);
			maxDist = max(maxDist, dist);
		}
	}

	return maxDist - minDist;
}

void CalcEllipsePointByAngle(double cx, double cy, double a, double b, double rotateAngle, double angle, double &x, double &y)
{
	double cosT = cos(rotateAngle);
	double sinT = sin(rotateAngle);
	double tx	= a * cos(angle);
	double ty	= b * sin(angle);

	x	= (int)(cx + tx * cosT - ty * sinT);
	y	= (int)(cy + tx * sinT + ty * cosT);
}