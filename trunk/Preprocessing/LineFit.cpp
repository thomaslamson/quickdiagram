#include "stdafx.h"

#include <Windows.h>
#include <math.h>
#include <vector>

#include "CommonFunction.h"

double CalcErrorToLine(POINT* rgPoints, int nPoints, double A, double B, double C)
{
	double	A2B2 = sqrt(A * A + B * B);
	double	error = 0;

	for (int i = 0; i < nPoints; i++)
	{
		error += fabs(A * rgPoints[i].x + B * rgPoints[i].y + C);
	}

	return error / A2B2 / nPoints;
}

bool LineFitting_Perpendicular(POINT* rgPoints, int nPoints, double &a1, double &b1, double &a2, double &b2)
{
	double	avgX, avgY, sumX2, sumY2, sumXY;

	if (nPoints < 2) return false;

	avgX  = 0;
	avgY  = 0;
	sumX2 = 0;
	sumY2 = 0;
	sumXY = 0;

	for (int i = 0; i < nPoints; i++)
	{
		avgX  += rgPoints[i].x;
		avgY  += rgPoints[i].y;
		sumX2 += rgPoints[i].x * rgPoints[i].x;
		sumY2 += rgPoints[i].y * rgPoints[i].y;
		sumXY += rgPoints[i].x * rgPoints[i].y;
	}
	avgX /= nPoints;
	avgY /= nPoints;

	double B = nPoints * avgX * avgY - sumXY;

	if (fabs(B) < DOUBLE_ZERO)
	{
		return false;
	}

	B = ((sumY2 - nPoints * avgY * avgY) - (sumX2 - nPoints * avgX * avgX)) / (2 * B);

	//Case 1
	b1 = -B + sqrt(B * B + 1);
	a1 = avgY - b1 * avgX;

	//Case 2
	b2 = -B - sqrt(B * B + 1);
	a2 = avgY - b2 * avgX;

	return true;
}

bool LineFitting_Vertical(POINT* rgPoints, int nPoints, double &a, double &b)
{
	double	avgX, avgY, sumX2, sumXY;

	if (nPoints < 2) return false;

	avgX  = 0;
	avgY  = 0;
	sumX2 = 0;
	sumXY = 0;

	for (int i = 0; i < nPoints; i++)
	{
		avgX  += rgPoints[i].x;
		avgY  += rgPoints[i].y;
		sumX2 += rgPoints[i].x * rgPoints[i].x;
		sumXY += rgPoints[i].x * rgPoints[i].y;
	}
	avgX /= nPoints;
	avgY /= nPoints;

	double B = sumX2 - nPoints * avgX * avgX;

	if (fabs(B) < DOUBLE_ZERO)
	{
		return false;
	}

	a = (avgY * sumX2 - avgX * sumXY) / B;
	b = (sumXY - nPoints * avgX * avgY) / B;

	return true;
}

bool SegmentFitting_Perpendicular(POINT* rgPoints, int nPoints, POINT &pt1, POINT &pt2, double &error)
{
	double a1, b1, a2, b2;
	double A, B, C;

	if (nPoints < 2)
	{
		return false;
	}

	if (LineFitting_Perpendicular(rgPoints, nPoints, a1, b1, a2, b2))	//y = a + bx
	{
		double error1 = 0;
		double error2 = 0;

		error1 = CalcErrorToLine(rgPoints, nPoints, b1, -1, a1);
		error2 = CalcErrorToLine(rgPoints, nPoints, b2, -1, a2);

		if (error1 < error2)
		{
			A = b1;
			B = -1;
			C = a1;
			error = error1;
		}
		else
		{
			A = b2;
			B = -1;
			C = a2;
			error = error2;
		}
	}
	else
	{
		pt1 = rgPoints[0];
		pt2 = rgPoints[nPoints - 1];
		error = 0;

		return true;
	}

	pt1 = rgPoints[0];
	pt1 = CalcProjectPoint(A, B, C, pt1);

	pt2 = rgPoints[nPoints - 1];
	pt2 = CalcProjectPoint(A, B, C, pt2);

	return true;
}

bool SegmentFitting_Vertical(POINT* rgPoints, int nPoints, POINT &pt1, POINT &pt2, double &error)
{
	double a, b;
	double A, B, C;

	if (nPoints < 2)
	{
		return false;
	}

	if (LineFitting_Vertical(rgPoints, nPoints, a, b))	//y = a + bx
	{
		A = b;
		B = -1;
		C = a;
	}
	else
	{
		pt1 = rgPoints[0];
		pt2 = rgPoints[nPoints - 1];
		error = 0;

		return true;
	}

	pt1 = rgPoints[0];
	pt1 = CalcProjectPoint(A, B, C, pt1);

	pt2 = rgPoints[nPoints - 1];
	pt2 = CalcProjectPoint(A, B, C, pt2);

	error = CalcErrorToLine(rgPoints, nPoints, A, B, C);

	return true;
}
