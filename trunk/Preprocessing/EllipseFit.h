#ifndef __ELLIPSE_FIT_H__
#define __ELLIPSE_FIT_H__

//********************************************************************************
//  The ellipse function used here is
//		ax^2 + bxy +cy^2 + dx + ey + f = 0
//
//  The least squares fitting algorithm used in this file is taken from
//		http://www.hpl.hp.com/personal/Maurizio_Pilu/research/ellipse.htm
//
//  Please refer to the above url for further detail
//********************************************************************************

bool EllipseFitting(POINT* rgPoints, int nPoints, double &a, double &b, double &c, double &d, double &e, double &f);
bool EllipticalArcFitting(POINT* rgPoints, int nPoints, RECT &rcBounding, double &dRotateAngle, double &dStartAngle, double &dEndAngle, double &error);

#endif