#ifndef __STROKE_FIT_H__
#define __STROKE_FIT_H__

#define PRIMITIVE_UNKNOWN	0
#define PRIMITIVE_SEGMENT	1
#define PRIMITIVE_ARC		2

typedef struct tagSegmentStroke
{
	POINT	startPoint;
	POINT	endPoint;
}
SegmentStroke;

typedef struct tagArcStroke
{
	RECT	rcBounding;
	double	startAngle;
	double	endAngle;
	double	rotateAngle;
}
ArcStroke;

typedef struct tagBasicStroke
{
	int		iStrokeType;
	int		iStartPoint;
	int		iEndPoint;
	double	error;
	double	length;

	union
	{
		SegmentStroke	segment;
		ArcStroke		arc;
	};
}
BasicStroke;

bool StrokeFitting(std::vector<POINT> &rgPoints, std::vector<BasicStroke> &rgResult);
void GetFittingParam(double& Segment_Error_Threshold, double& Arc_Error_Threshold, double& Arc_Min_Length, double& Arc_Min_Curve, double& Stroke_Min_Length, double& Min_Turning_Angle, double& Segmentation_Penalty);
void SetFittingParam(double Segment_Error_Threshold, double Arc_Error_Threshold, double Arc_Min_Length, double Arc_Min_Curve, double Stroke_Min_Length, double Min_Turning_Angle, double Segmentation_Penalty);

#endif