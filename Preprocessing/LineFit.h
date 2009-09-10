#ifndef __LINE_FIT_H__
#define __LINE_FIT_H__

//********************************************************************************
//  The line function used here is
//		y = a + bx
//
//  The least squares fitting algorithm used in this file is taken from
//		http://mathworld.wolfram.com/LeastSquaresFitting.html
//		http://mathworld.wolfram.com/LeastSquaresFittingPerpendicularOffsets.html
//
//  Please refer to the above url for further detail
//********************************************************************************

bool LineFitting_Perpendicular(POINT* rgPoints, int nPoints, double &a1, double &b1, double &a2, double &b2);
bool LineFitting_Vertical(POINT* rgPoints, int nPoints, double &a, double &b);

bool SegmentFitting_Perpendicular(POINT* rgPoints, int nPoints, POINT &pt1, POINT &pt2, double &error);
bool SegmentFitting_Vertical(POINT* rgPoints, int nPoints, POINT &pt1, POINT &pt2, double &error);

#endif