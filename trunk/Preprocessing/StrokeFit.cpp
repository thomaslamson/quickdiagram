#include "stdafx.h"

#include <Windows.h>
#include <math.h>
#include <vector>

#include "LineFit.h"
#include "EllipseFit.h"
#include "Matrix.h"
#include "StrokeFit.h"
#include "CommonFunction.h"

double SEGMENT_ERROR_THRESHOLD	= 2;		//If the error of a fitted segment is smaller than this threshold, the possibility of arc fitting is ignored.
double ARC_ERROR_THREASHOLD		= 3;		//The error of a fitted arc should be smaller than this threshold.
double ARC_MINIMAL_LENGTH		= 30;		//The length of a fitted arc should be larger than this threshold.
double ARC_MINIMAL_CURVE		= 0.15;		//The distance from the top of the fitted arc to the chord should be larger than this threshold.
double STROKE_MINIMAL_LENGTH	= 20;		//The length of a fitted stroke (segment or arc) should be larger than this threshold.
double MINIMAL_TURNING_ANGLE	= 0.4 * PI;	//The turning angle of the corner detected in rough segmentation should be larget than this threshold.
double SEGMENTATION_PENALTY		= 1;		//The penalty parameter used in the dynamic programming.

typedef BasicStroke (*EstimateStrokeFunction)(Matrix<double> &rgDistance, std::vector<POINT> &rgCurve, int iStartPt, int iEndPt);

void GetFittingParam(double& Segment_Error_Threshold, double& Arc_Error_Threshold, double& Arc_Min_Length, double& Arc_Min_Curve, double& Stroke_Min_Length, double& Min_Turning_Angle, double& Segmentation_Penalty)
{
	Segment_Error_Threshold = SEGMENT_ERROR_THRESHOLD;
	Arc_Error_Threshold		= ARC_ERROR_THREASHOLD;
	Arc_Min_Length			= ARC_MINIMAL_LENGTH;
	Arc_Min_Curve			= ARC_MINIMAL_CURVE;
	Stroke_Min_Length		= STROKE_MINIMAL_LENGTH;
	Min_Turning_Angle		= MINIMAL_TURNING_ANGLE;
	Segmentation_Penalty	= SEGMENTATION_PENALTY;
}

void SetFittingParam(double Segment_Error_Threshold, double Arc_Error_Threshold, double Arc_Min_Length, double Arc_Min_Curve, double Stroke_Min_Length, double Min_Turning_Angle, double Segmentation_Penalty)
{
	SEGMENT_ERROR_THRESHOLD	= Segment_Error_Threshold;
	ARC_ERROR_THREASHOLD	= Arc_Error_Threshold;
	ARC_MINIMAL_LENGTH		= Arc_Min_Length;
	ARC_MINIMAL_CURVE		= Arc_Min_Curve;
	STROKE_MINIMAL_LENGTH	= Stroke_Min_Length;
	MINIMAL_TURNING_ANGLE	= Min_Turning_Angle;
	SEGMENTATION_PENALTY	= Segmentation_Penalty;
}

BasicStroke EstimateStroke_Segment(Matrix<double> &rgDistance, std::vector<POINT> &rgCurve, int iStartPt, int iEndPt)
{
	BasicStroke	segment;

	segment.error		= 1e+50;
	segment.iStartPoint	= iStartPt;
	segment.iEndPoint	= iEndPt;
	segment.iStrokeType	= PRIMITIVE_SEGMENT;

	SegmentFitting_Perpendicular(&rgCurve[iStartPt], iEndPt - iStartPt + 1, segment.segment.startPoint, segment.segment.endPoint, segment.error);

	return segment;
}

BasicStroke EstimateStroke_Segment_Arc(Matrix<double> &rgDistance, std::vector<POINT> &rgCurve, int iStartPt, int iEndPt)
{
	BasicStroke	segment = EstimateStroke_Segment(rgDistance, rgCurve, iStartPt, iEndPt);

	if ((segment.error < SEGMENT_ERROR_THRESHOLD) || (rgDistance[iStartPt][iEndPt] < ARC_MINIMAL_LENGTH))
	{
		return segment;
	}

	BasicStroke	arc;

	arc.error		= 1e+50;
	arc.iStartPoint	= iStartPt;
	arc.iEndPoint	= iEndPt;
	arc.iStrokeType	= PRIMITIVE_ARC;

	EllipticalArcFitting(&rgCurve[iStartPt], iEndPt - iStartPt + 1, arc.arc.rcBounding, arc.arc.rotateAngle, arc.arc.startAngle, arc.arc.endAngle, arc.error);

	double distSize = DistributionSizeToLine(segment.segment.startPoint, segment.segment.endPoint, &rgCurve[iStartPt], iEndPt - iStartPt + 1);
	double chordLen = CalcDistanceP2P(segment.segment.startPoint, segment.segment.endPoint);

	if ((arc.error < ARC_ERROR_THREASHOLD) && (arc.error < segment.error) && 
		(distSize / chordLen > ARC_MINIMAL_CURVE))
	{
		return arc;
	}
	else
	{
		return segment;
	}
}

double StrokeAngle(BasicStroke &stroke)
{
	double angle = 0;

	switch (stroke.iStrokeType) 
	{
	case PRIMITIVE_SEGMENT:
		{
			angle = atan2((double)(stroke.segment.endPoint.y - stroke.segment.startPoint.y), (double)(stroke.segment.endPoint.x - stroke.segment.startPoint.x));
			if (angle < 0)
			{
				angle += 2 * PI;
			}
		}
		break;
	case PRIMITIVE_ARC:
		break;
	}

	return angle;
}

double AngleWeight(BasicStroke &stroke1, BasicStroke &stroke2)
{
	double	angle = PI;

	switch (stroke1.iStrokeType)
	{
	case PRIMITIVE_SEGMENT:
		{
			switch (stroke2.iStrokeType)
			{
			case PRIMITIVE_SEGMENT:
				{
					double angle1 = CalcAngleVectorToX(stroke1.segment.startPoint, stroke1.segment.endPoint);
					double angle2 = CalcAngleVectorToX(stroke2.segment.startPoint, stroke2.segment.endPoint);

					angle = RegularizeAngle(angle2 - angle1);
				}
				break;
			case PRIMITIVE_ARC:
				break;
			}
		}
		break;
	case PRIMITIVE_ARC:
		{
			switch (stroke2.iStrokeType)
			{
			case PRIMITIVE_SEGMENT:
				break;
			case PRIMITIVE_ARC:
				break;
			}
		}
		break;
	}

	return (1 - (fabs(angle) / PI));
//	return (1 - 2 * pow(fabs(angle) / PI, 20));
}

bool StrokeFitting_DynamicProgramming(std::vector<POINT> &rgPoints, std::vector<BasicStroke> &rgResult, Matrix<double> &rgDistance, EstimateStrokeFunction EstimateStroke, double SegmentationPenalty)
{
	int		n = (int)rgPoints.size();
	int		iLastRow = 0;
	bool	bContinue;
	double	err;

	BasicStroke			stroke;
	Matrix<double>			rgErrors(n, n);
	Matrix<BasicStroke>	rgStrokes(n, n);
	Matrix<BasicStroke> rgFits(n, n);

	//Dynamic programming for segmentation
	for (int i = 0; i < n; i++)
	{
		bContinue	= false;
		iLastRow	= i;

		for (int j = i + 1; j < n; j++)
		{
			if (i == 0)
			{
				rgFits[i][j]	=	EstimateStroke(rgDistance, rgPoints, 0, j);
				rgStrokes[i][j] =	rgFits[i][j];
				rgErrors[i][j]	=	rgStrokes[i][j].error + 
									SegmentationPenalty * (1 - (rgDistance[i][j] / rgDistance[0][n - 1]));
				bContinue = true;
			}
			else
			{
				rgErrors[i][j] = 1e+50;

				if (rgDistance[i][j] < STROKE_MINIMAL_LENGTH)
				{
					continue;
				}

				for (int k = i; k < j; k++)
				{
					if (rgErrors[i - 1][k] < rgErrors[i - 1][n - 1])
					{
						if (rgFits[k][j].iStrokeType == PRIMITIVE_UNKNOWN)
						{
							rgFits[k][j] = EstimateStroke(rgDistance, rgPoints, k, j);
						}

						stroke	=	rgFits[k][j];
						err		=	rgErrors[i - 1][k]	+ 
									stroke.error		+
									SegmentationPenalty * AngleWeight(rgStrokes[i - 1][k], stroke) +
									SegmentationPenalty * (1 - (min(rgDistance[0][k], rgDistance[k][j]) / rgDistance[0][n - 1]));

						if (err < rgErrors[i][j])
						{
							rgErrors[i][j]	= err;
							rgStrokes[i][j]	= stroke;
						}
					}
				}

				if (rgErrors[i][j] < rgErrors[i - 1][n - 1])
				{
					bContinue = true;
				}
			}
		}

		if (!bContinue) break;
	}

	//Find the segmentation with minimal error 
	double	minErr	= 1e+50;
	int		minSplit= -1;

	for (int i = 0; i <= iLastRow; i++)
	{
		if (rgErrors[i][n - 1] < minErr)
		{
			minErr	= rgErrors[i][n - 1];
			minSplit= i;
		}
	}

	if (minSplit == -1)
	{
		return false;
	}

	n = n - 1;
	for (int i = minSplit; i >= 0; i--)
	{
		rgResult.insert(rgResult.begin(), rgStrokes[i][n]);
		n = rgStrokes[i][n].iStartPoint;
	}

	return true;
}

void MergeSegmentToSegment(std::vector<POINT> &rgPoints, BasicStroke &stroke1, BasicStroke &stroke2)
{
	double	A, B, C;
	double	A1, B1, C1;
	double	A2, B2, C2;
	POINT	pt, pt1, pt2;

	if ((stroke1.segment.endPoint.x != stroke2.segment.startPoint.x) ||
		(stroke1.segment.endPoint.y != stroke2.segment.startPoint.y))
	{
		CalcLineParam(stroke1.segment.startPoint, stroke1.segment.endPoint, A1, B1, C1);
		CalcLineParam(stroke2.segment.startPoint, stroke2.segment.endPoint, A2, B2, C2);
		if (CalcIntersectPoint(A1, B1, C1, A2, B2, C2, pt2))
		{
			CalcLineParam(stroke1.segment.endPoint, stroke2.segment.startPoint, A, B, C);
			pt.x = (rgPoints[stroke1.iEndPoint].x + rgPoints[stroke2.iStartPoint].x) / 2;
			pt.y = (rgPoints[stroke1.iEndPoint].y + rgPoints[stroke2.iStartPoint].y) / 2;
			pt1 = CalcProjectPoint(A, B, C, pt);

			if ((pt1.x - pt.x) * (pt1.x - pt.x) + (pt1.y - pt.y) * (pt1.y - pt.y) <
				(pt2.x - pt.x) * (pt2.x - pt.x) + (pt2.y - pt.y) * (pt2.y - pt.y))
			{
				pt = pt1;
			}
			else
			{
				pt = pt2;
			}
		}
		else
		{
			pt = stroke1.segment.endPoint;	//These two segment are parallel, simply connect them together.
		}

		stroke1.segment.endPoint	= pt;
		stroke2.segment.startPoint	= pt;
	}
}

void MergeSegmentToArc(BasicStroke &stroke1, BasicStroke &stroke2)
{
	double x1, y1, x2, y2;
	double cx, cy, a, b;

	cx	= (stroke2.arc.rcBounding.left + stroke2.arc.rcBounding.right) / 2;
	cy	= (stroke2.arc.rcBounding.top + stroke2.arc.rcBounding.bottom) / 2;
	a	= (stroke2.arc.rcBounding.right - stroke2.arc.rcBounding.left) / 2;
	b	= (stroke2.arc.rcBounding.bottom - stroke2.arc.rcBounding.top) / 2;

	CalcEllipsePointByAngle(cx, cy, a, b, stroke2.arc.rotateAngle, ConvertToEllipseAngle(stroke2.arc.startAngle, a, b), x1, y1);
	CalcEllipsePointByAngle(cx, cy, a, b, stroke2.arc.rotateAngle, ConvertToEllipseAngle(stroke2.arc.endAngle, a, b), x2, y2);

	if (((x1 - stroke1.segment.endPoint.x) * (x1 - stroke1.segment.endPoint.x) + (y1 - stroke1.segment.endPoint.y) * (y1 - stroke1.segment.endPoint.y)) <
		((x2 - stroke1.segment.endPoint.x) * (x2 - stroke1.segment.endPoint.x) + (y2 - stroke1.segment.endPoint.y) * (y2 - stroke1.segment.endPoint.y)))
	{
		stroke1.segment.endPoint.x = (LONG)x1;
		stroke1.segment.endPoint.y = (LONG)y1;
	}
	else
	{
		stroke1.segment.endPoint.x = (LONG)x2;
		stroke1.segment.endPoint.y = (LONG)y2;
	}
}

void MergeArcToSegment(BasicStroke &stroke1, BasicStroke &stroke2)
{
	double x1, y1, x2, y2;
	double cx, cy, a, b;

	cx	= (stroke1.arc.rcBounding.left + stroke1.arc.rcBounding.right) / 2;
	cy	= (stroke1.arc.rcBounding.top + stroke1.arc.rcBounding.bottom) / 2;
	a	= (stroke1.arc.rcBounding.right - stroke1.arc.rcBounding.left) / 2;
	b	= (stroke1.arc.rcBounding.bottom - stroke1.arc.rcBounding.top) / 2;

	CalcEllipsePointByAngle(cx, cy, a, b, stroke1.arc.rotateAngle, ConvertToEllipseAngle(stroke1.arc.startAngle, a, b), x1, y1);
	CalcEllipsePointByAngle(cx, cy, a, b, stroke1.arc.rotateAngle, ConvertToEllipseAngle(stroke1.arc.endAngle, a, b), x2, y2);

	if (((x1 - stroke2.segment.startPoint.x) * (x1 - stroke2.segment.startPoint.x) + (y1 - stroke2.segment.startPoint.y) * (y1 - stroke2.segment.startPoint.y)) <
		((x2 - stroke2.segment.startPoint.x) * (x2 - stroke2.segment.startPoint.x) + (y2 - stroke2.segment.startPoint.y) * (y2 - stroke2.segment.startPoint.y)))
	{
		stroke2.segment.startPoint.x = (LONG)x1;
		stroke2.segment.startPoint.y = (LONG)y1;
	}
	else
	{
		stroke2.segment.startPoint.x = (LONG)x2;
		stroke2.segment.startPoint.y = (LONG)y2;
	}
}

void MergeAdjacentStroke(std::vector<POINT> &rgPoints, std::vector<BasicStroke> &rgResult)
{
	for (unsigned int i = 0; i < rgResult.size() - 1; i++)
	{
		switch(rgResult[i].iStrokeType) 
		{
		case PRIMITIVE_SEGMENT:
			{
				switch(rgResult[i + 1].iStrokeType) 
				{
				case PRIMITIVE_SEGMENT:
					{
						MergeSegmentToSegment(rgPoints, rgResult[i], rgResult[i + 1]);
					}
					break;
				case PRIMITIVE_ARC:
					{
						MergeSegmentToArc(rgResult[i], rgResult[i + 1]);
					}
					break;
				}
			}
			break;
		case PRIMITIVE_ARC:
			{
				switch(rgResult[i + 1].iStrokeType) 
				{
				case PRIMITIVE_SEGMENT:
					{
						MergeArcToSegment(rgResult[i], rgResult[i + 1]);
					}
					break;
				case PRIMITIVE_ARC:
					{
					}
					break;
				}
			}
			break;
		}
	}
}

void CloseStrokes(std::vector<BasicStroke> &rgResult)
{
	if (rgResult.size() >= 3)
	{
		if ((rgResult.front().iStrokeType == PRIMITIVE_SEGMENT) && (rgResult.back().iStrokeType == PRIMITIVE_SEGMENT))
		{
			double	A1, B1, C1, A2, B2, C2;
			POINT	pt;
			BasicStroke *lpStroke1 = &(rgResult.front());
			BasicStroke *lpStroke2 = &(rgResult.back());

			switch (lpStroke1->iStrokeType)
			{
			case PRIMITIVE_SEGMENT:
				switch (lpStroke2->iStrokeType)
				{
				case PRIMITIVE_SEGMENT:
					{
						if (CalcDistanceP2P(lpStroke1->segment.startPoint, lpStroke2->segment.endPoint) < STROKE_MINIMAL_LENGTH)
						{
							CalcLineParam(lpStroke1->segment.startPoint, lpStroke1->segment.endPoint, A1, B1, C1);
							CalcLineParam(lpStroke2->segment.startPoint, lpStroke2->segment.endPoint, A2, B2, C2);

							if (CalcIntersectPoint(A1, B1, C1, A2, B2, C2, pt))
							{
								lpStroke1->segment.startPoint = pt;
								lpStroke2->segment.endPoint = pt;

							}
						}
					}
					break;
				case PRIMITIVE_ARC:
					{
					}
					break;
				}
				break;
			case PRIMITIVE_ARC:
				switch (lpStroke2->iStrokeType)
				{
				case PRIMITIVE_SEGMENT:
					break;
				}
				break;
			}
		}
	}
}

bool StrokeFitting_Internal(std::vector<POINT> &rgPoints, std::vector<BasicStroke> &rgResult, double SegmentationPenalty)
{
	int				n = (int)rgPoints.size();
	int				nArcs;
	bool			bRet = true;
	double*			rgLen;
	double			dx, dy;
	double			errSeg, errSegArc, TotalLength;
	Matrix<double>	rgDistance(n, n);
	std::vector<BasicStroke> rgSeg, rgSegArc;

	//If there are too few points, just regard it as failure
	if (rgPoints.size() < 2)
	{
		return false;
	}

	//Calculate distance between two points
	rgLen = new double[n];
	ZeroMemory(rgLen, sizeof(double) * n);

	for (int i = 0; i < n - 1; i++)
	{
		dx = rgPoints[i + 1].x - rgPoints[i].x;
		dy = rgPoints[i + 1].y - rgPoints[i].y;
		rgLen[i] = sqrt(dx * dx + dy * dy);
	}

	for (int i = 0; i < n; i++)
	{
		for (int j = i + 1; j < n; j++)
		{
			rgDistance[i][j] = rgDistance[i][j - 1] + rgLen[j - 1];
		}
	}

	delete[] rgLen;

	//Least-Squares Fitting with Dynamic Programming with segment and arc
	bRet &= StrokeFitting_DynamicProgramming(rgPoints, rgSegArc, rgDistance, EstimateStroke_Segment_Arc, SegmentationPenalty);
	//Least-Squares Fitting with Dynamic Programming with segment
	bRet &= StrokeFitting_DynamicProgramming(rgPoints, rgSeg, rgDistance, EstimateStroke_Segment, SegmentationPenalty);

	TotalLength = rgDistance[0][rgPoints.size() - 1];
	nArcs		= 0;
	//Calculate error for segment-only fitting
	errSeg = 0;
	for (unsigned int i = 0; i < rgSeg.size(); i++)
	{
		errSeg += rgSeg[i].error * rgDistance[rgSeg[i].iStartPoint][rgSeg[i].iEndPoint] / TotalLength;
	}
	//Calculate error for fitting with segment and arc
	errSegArc = 0;
	for (unsigned int i = 0; i < rgSegArc.size(); i++)
	{
		errSegArc += rgSegArc[i].error * rgDistance[rgSegArc[i].iStartPoint][rgSegArc[i].iEndPoint] / TotalLength;
		if (rgSegArc[i].iStrokeType == PRIMITIVE_ARC)
		{
			nArcs++;
		}
	}
	//Make a double-check with the two fitting results
	if ((errSeg < errSegArc) && (rgSeg.size() <= rgSegArc.size() + nArcs))
	{
		rgResult.assign(rgSeg.begin(), rgSeg.end());
	}
	else
	{
		rgResult.assign(rgSegArc.begin(), rgSegArc.end());
	}

	//Assign length to each stroke
	for (unsigned int i = 0; i < rgResult.size(); i++)
	{
		rgResult[i].length = rgDistance[rgResult[i].iStartPoint][rgResult[i].iEndPoint];
	}

	return bRet;
}

bool StrokeFitting(std::vector<POINT> &rgPoints, std::vector<BasicStroke> &rgResult)
{
	int				n = (int)rgPoints.size();
	double			dx, dy;
	double*			rgLen;
	Matrix<double>	rgDistance(n, n);

	//If there are too few points, just regard it as failure
	if (rgPoints.size() < 2)
	{
		return false;
	}

	//Calculate distance between two points
	rgLen = new double[n];
	ZeroMemory(rgLen, sizeof(double) * n);

	for (int i = 0; i < n - 1; i++)
	{
		dx = rgPoints[i + 1].x - rgPoints[i].x;
		dy = rgPoints[i + 1].y - rgPoints[i].y;
		rgLen[i] = sqrt(dx * dx + dy * dy);
	}

	for (int i = 0; i < n; i++)
	{
		for (int j = i + 1; j < n; j++)
		{
			rgDistance[i][j] = rgDistance[i][j - 1] + rgLen[j - 1];
		}
	}

	delete[] rgLen;

	//First check whether the input can be an ellipse or an elliptical arc
	if (rgDistance[0][rgPoints.size() - 1] > ARC_MINIMAL_LENGTH)
	{
		BasicStroke segment = EstimateStroke_Segment(rgDistance, rgPoints, 0, (int)rgPoints.size() - 1);
		BasicStroke	arc;

		arc.error		= 1e+50;
		arc.iStartPoint	= 0;
		arc.iEndPoint	= (int)rgPoints.size() - 1;
		arc.iStrokeType	= PRIMITIVE_ARC;

		EllipticalArcFitting(&rgPoints[0], (int)rgPoints.size(), arc.arc.rcBounding, arc.arc.rotateAngle, arc.arc.startAngle, arc.arc.endAngle, arc.error);

		double distSize = DistributionSizeToLine(segment.segment.startPoint, segment.segment.endPoint, &rgPoints[0], (int)rgPoints.size());
		double chordLen = CalcDistanceP2P(segment.segment.startPoint, segment.segment.endPoint);

		if (distSize / chordLen > ARC_MINIMAL_CURVE)
		{
			if ((arc.error < ARC_ERROR_THREASHOLD / 2) || 
				((arc.arc.endAngle - arc.arc.startAngle > 1.9 * PI) && (arc.error < ARC_ERROR_THREASHOLD * 1.2)))
			{
				rgResult.push_back(arc);
				return true;
			}
		}
	}

	std::vector<int>				rgSplit;
	std::vector<POINT>				rgTempPoints;
	std::vector<BasicStroke>		rgTempResult;

	rgSplit.push_back(0);

	//Roughly split user's input
	for (int i = 1; i < n - 1; i++)
	{
		int	j, k;
		
		j = i - 1;
		while ((rgDistance[j][i] < STROKE_MINIMAL_LENGTH) && (j > 0))
		{
			j--;
		}

		k = i + 1;
		while ((rgDistance[i][k] < STROKE_MINIMAL_LENGTH) && (k < n - 1))
		{
			k++;
		}

		if ((rgDistance[j][i] >= STROKE_MINIMAL_LENGTH) && (rgDistance[i][k] >= STROKE_MINIMAL_LENGTH))
		{
			POINT	pt1, pt2, pt3, pt4;
			double	err1, err2;

			SegmentFitting_Perpendicular(&rgPoints[j], i - j + 1, pt1, pt2, err1);
			SegmentFitting_Perpendicular(&rgPoints[i], k - i + 1, pt3, pt4, err2);

			if ((err1 < SEGMENT_ERROR_THRESHOLD) && (err2 < SEGMENT_ERROR_THRESHOLD))
			{
				double angle = RegularizeAngle(CalcAngleVectorToX(pt1, pt2) - CalcAngleVectorToX(pt3, pt4));

				if (fabs(angle) > MINIMAL_TURNING_ANGLE)
				{
					rgSplit.push_back(i);
					i = k - 1;
				}
			}
		}
	}

	rgSplit.push_back(n - 1);

	for (unsigned int i = 1; i < rgSplit.size() - 1; i++)
	{
		if ((rgDistance[rgSplit[i - 1]][rgSplit[i]] < STROKE_MINIMAL_LENGTH) ||
			(rgDistance[rgSplit[i]][rgSplit[i + 1]] < STROKE_MINIMAL_LENGTH))
		{
			rgSplit.erase(rgSplit.begin() + i);
			i--;
		}
	}
	

	for (unsigned int i = 0; i < rgSplit.size() - 1; i++)
	{
		rgTempPoints.clear();
		rgTempResult.clear();

		for (int j = rgSplit[i]; j <= rgSplit[i + 1]; j++)
		{
			rgTempPoints.push_back(rgPoints[j]);
		}

		StrokeFitting_Internal(rgTempPoints, rgTempResult, SEGMENTATION_PENALTY);

		for (unsigned int j = 0; j < rgTempResult.size(); j++)
		{
			BasicStroke	stroke = rgTempResult[j];

			stroke.iStartPoint	+= rgSplit[i];
			stroke.iEndPoint	+= rgSplit[i];
			rgResult.push_back(stroke);
		}
	}

	//Merge adjacent strokes together
	MergeAdjacentStroke(rgPoints, rgResult);

	//If the sketch is a closed figure, just close it.
	CloseStrokes(rgResult);

	return true;
}