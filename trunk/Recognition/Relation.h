#ifndef __RELATION_H__
#define __RELATION_H__

#include "Primitives.h"

#define RELATION_INTERSECTED	0x001	//0000.0000.0001	1
#define RELATION_TANGENT		0x003	//0000.0000.0011	3
#define RELATION_CONJOINT		0x005	//0000.0000.0100	5
#define RELATION_CONJOINT_0		0x00d	//0000.0000.1101	13
#define RELATION_CONJOINT_1		0x015	//0000.0001.0101	21
#define RELATION_PARALLEL		0x020	//0000.0010.0000	32
#define RELATION_PERPENDICULAR	0x040	//0000.0100.0000	64
#define RELATION_CONCENTRIC		0x080	//0000.1000.0000	128
#define RELATION_POSITIVE		0x100	//0001.0000.0000	256
#define RELATION_NEGATIVE		0x200	//0010.0000.0000	512
#define RELATION_INSIDE			0x400	//0100.0000.0000	1024
#define RELATION_OUTSIDE		0x800	//1000.0000.0000	2048

void CalcLineParam(PrimitiveSegment* lpSeg, double &A, double &B, double &C);
void CalcEllipseParam(PrimitiveEllipse* lpEllipse, double &a, double &b, double &c, double &d, double &e, double &f);

PrimitivePoint CalcProjectPoint(PrimitivePoint pt, PrimitiveSegment* lpSeg);
double CalcPointRatio(PrimitivePoint pt, PrimitiveSegment* lpSeg);
double CalcPointRatio(PrimitivePoint pt, PrimitiveSegment* lpSeg);
double CalcProjectPointRatio(PrimitivePoint pt, PrimitiveSegment* lpSeg);

double CalcEllipseAngle(PrimitiveEllipse* lpEllipse, double angle);
PrimitivePoint CalcEllipsePointByAngle(PrimitiveEllipse* lpEllipse, double angle);

double CalcAngle_Line_To_X(PrimitiveSegment* lpSeg);
double CalcAngle_Vector_To_X(PrimitivePoint pt);

double CalcDistance_Point_To_Point(PrimitivePoint pt1, PrimitivePoint pt2);
double CalcDistance_Point_To_Line(PrimitivePoint pt, PrimitiveSegment* lpSeg);
double CalcDistance_Point_To_Ellipse(PrimitivePoint pt, PrimitiveEllipse* lpEllipse);
double CalcDistance_Elliptic_Arc(PrimitiveEllipse* lpEllipse, PrimitivePoint startPoint, PrimitivePoint endPoint);

bool IsPointInSegmentRange(PrimitivePoint pt, PrimitiveSegment* lpSeg);

void CalcIntersection_Line_To_Line(PrimitiveSegment* lpSeg1, PrimitiveSegment* lpSeg2, std::vector<PrimitivePoint>& rgIntersection);
void CalcIntersection_Segment_To_Segment(PrimitiveSegment* lpSeg1, PrimitiveSegment* lpSeg2, std::vector<PrimitivePoint>& rgIntersection);
void CalcIntersection_Line_To_Ellipse(PrimitiveSegment* lpSeg, PrimitiveEllipse* lpEllipse, std::vector<PrimitivePoint>& rgIntersection);
void CalcIntersection_Segment_To_Ellipse(PrimitiveSegment* lpSeg, PrimitiveEllipse* lpEllipse, std::vector<PrimitivePoint>& rgIntersection);
void CalcIntersection_Segment_To_Arc(PrimitiveSegment* lpSeg, PrimitiveArc* lpArc, std::vector<PrimitivePoint>& rgIntersection);
void CalcIntersection_Ellipse_To_Ellipse(PrimitiveEllipse* lpEllipse1, PrimitiveEllipse* lpEllipse2, std::vector<PrimitivePoint>& rgIntersection);
void CalcIntersection_Ellipse_To_Arc(PrimitiveEllipse* lpEllipse, PrimitiveArc* lpArc, std::vector<PrimitivePoint>& rgIntersection);
void CalcIntersection_Arc_To_Arc(PrimitiveArc* lpArc1, PrimitiveArc* lpArc2, std::vector<PrimitivePoint>& rgIntersection);

unsigned int CalcRelation(PrimitiveStroke* lpStroke1, PrimitiveStroke* lpStroke2, double DistanceThreshold, double AngleThreshold);

#endif