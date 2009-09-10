#ifndef __SIMILARITY_H__
#define __SIMILARITY_H__

typedef struct tagRecognitionResult
{
	double	dSimilarity;
	double	dRotate;
}
RECOG_RESULT;

RECOG_RESULT CalcSimilarity(GraphicObject* lpObj, Matrix<unsigned int>* lpRelModel, std::vector<PrimitiveStroke*> *lpStrokes, Matrix<unsigned int>* lpRelUser);

#endif