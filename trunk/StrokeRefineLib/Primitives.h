#ifndef __PRIMITIVES_H__
#define __PRIMITIVES_H__

#include <vector>
#include <string>
#include <math.h>

//Code added by KONG

#include <comutil.h>

#pragma comment(lib, "comsuppw.lib")
#pragma comment(lib, "comsupp.lib")
#pragma comment(lib, "kernel32.lib")
//end

extern double PI;
extern double DOUBLE_ZERO;

#define PRIMITIVE_STROKE_SEGMENT	1
#define PRIMITIVE_STROKE_ELLIPSE	2
#define PRIMITIVE_STROKE_ARC		3

class TransformationMatrix
{
public:
	TransformationMatrix();
public:
	void SetIdentityMatrix();						//Reset current matrix to identity matrix
	void Offset(double offset_X, double offset_Y);	//Offset current matrix
	void Rotate(double angle);						//Rotate current matrix, counterclockwise
	void Skew(double angle_X, double angle_Y);		//Skew current matrix
	void Scale(double scale_X, double scale_Y);		//Scale current matrix
	bool Inverse();									//Calculate the inverse matrix of current matrix
public:
	TransformationMatrix & operator = (const TransformationMatrix &rvalue);	//Assignment between matrixes
	double* operator[](int idx);					//Return the row specified by idx
private:
	void MultiplyMatrix(double m[3][3]);
private:
	double	matrix[3][3];
};

class PrimitivePoint
{
public:
	PrimitivePoint();
public:
	bool operator==(PrimitivePoint& value);
	void Transform(TransformationMatrix &matrix);
public:
	double	x;
	double	y;
};

class PrimitiveStroke
{
public:
	PrimitiveStroke();
public:
	_declspec(property(get = get_strokeType)) int strokeType;
	_declspec(property(get = get_sketchLength, put = put_sketchLength)) double sketchLength;
public:
	int get_strokeType();
	double get_sketchLength();
	void put_sketchLength(double value);
public:
	std::vector<PrimitivePoint>	m_rgPoints;
protected:
	int		m_iStrokeType;
	double	m_dSketchLength;
};

class PrimitiveSegment : public PrimitiveStroke
{
public:
	PrimitiveSegment();
public:
	_declspec(property(get = get_startPoint, put = put_startPoint)) PrimitivePoint startPoint;
	_declspec(property(get = get_endPoint, put = put_endPoint)) PrimitivePoint endPoint;
public:
	PrimitivePoint& get_startPoint();
	PrimitivePoint& get_endPoint();
	void put_startPoint(PrimitivePoint value);
	void put_endPoint(PrimitivePoint value);
	void Transform(TransformationMatrix &matrix);
};

class PrimitiveEllipse : public PrimitiveStroke
{
public:
	PrimitiveEllipse();
public:
	_declspec(property(get = get_leftTop, put = put_leftTop)) PrimitivePoint leftTop;
	_declspec(property(get = get_rightBottom, put = put_rightBottom)) PrimitivePoint rightBottom;
	_declspec(property(get = get_rotateAngle, put = put_rotateAngle)) double rotateAngle;
	_declspec(property(get = get_center)) PrimitivePoint center;
	_declspec(property(get = get_x_axis)) double x_axis;
	_declspec(property(get = get_y_axis)) double y_axis;
public:
	double get_x_axis();
	double get_y_axis();
	PrimitivePoint get_center();
	PrimitivePoint& get_leftTop();
	PrimitivePoint& get_rightBottom();
	double& get_rotateAngle();
	void put_leftTop(PrimitivePoint value);
	void put_rightBottom(PrimitivePoint value);
	void put_rotateAngle(double value);
private:
	double	m_rotateAngle;
};

class PrimitiveArc : public PrimitiveEllipse
{
public:
	PrimitiveArc();
public:
	_declspec(property(get = get_startPoint, put = put_startPoint)) PrimitivePoint startPoint;
	_declspec(property(get = get_endPoint, put = put_endPoint)) PrimitivePoint endPoint;
public:
	PrimitivePoint& get_startPoint();
	PrimitivePoint& get_endPoint();
	void put_startPoint(PrimitivePoint value);
	void put_endPoint(PrimitivePoint value);
};

class GraphicObject
{
public:
	std::string		m_id;
	std::vector<PrimitiveSegment>	m_rgSegments;
	std::vector<PrimitiveEllipse>	m_rgEllipses;
	std::vector<PrimitiveArc>		m_rgArcs;
public:
	_declspec(property(get = get_primitiveStroke)) PrimitiveStroke* rgStrokes[];
	_declspec(property(get = get_strokeCount)) int Count;
public:
	GraphicObject& operator=(GraphicObject& value);
public:
	PrimitiveStroke* get_primitiveStroke(int i);
	int get_strokeCount();
};

GraphicObject* LoadFromFile(char* szFileName);

#endif