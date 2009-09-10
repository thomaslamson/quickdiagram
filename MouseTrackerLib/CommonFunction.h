#ifndef __COMMON_FUNCTION_H__
#define __COMMON_FUNCTION_H__

extern double PI;
extern double DOUBLE_ZERO;

//**************************************************
//Calculate the project point of (x0, y0) to a line
//
//	 Ax + By + C = 0
//	-Bx + Ay + D = 0
//	 D = Bx0 - Ay0
//
//Make sure A * A + B * B <> 0
//**************************************************
POINT CalcProjectPoint(double A, double B, double C, POINT pt);
void CalcLineParam(POINT pt1, POINT pt2, double &A, double &B, double &C);
bool CalcIntersectPoint(double A1, double B1, double C1, double A2, double B2, double C2, POINT &pt);
double CalcAngleVectorToX(POINT startPoint, POINT endPoint);
double RegularizeAngle(double angle);
void MergeAngleRange(double &startAngle, double &endAngle, double angle1, double angle2);
void RegularizeAngleRange(double &startAngle, double &endAngle);
double ConvertToEllipseAngle(double angle, double a, double b);
double DistributionSizeToLine(POINT pt1, POINT pt2, POINT* rgPoints, int nPoints);
void CalcEllipsePointByAngle(double cx, double cy, double a, double b, double rotateAngle, double angle, double &x, double &y);

#endif