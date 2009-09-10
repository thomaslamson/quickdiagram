// StrokeRecognition.h : Declaration of the CStrokeRecognition

#pragma once
#include "resource.h"       // main symbols
#include <vector>
#include "StrokeFit.h"

// IStrokeRecognition
[
	object,
	uuid("5E2C7AFC-8414-47B1-AC3A-F7A28D4DB45B"),
	dual,	helpstring("IStrokeRecognition Interface"),
	pointer_default(unique)
]
__interface IStrokeRecognition : IDispatch
{
	[id(1), helpstring("method RecognizeStroke")] HRESULT RecognizeStroke([in] BSTR MouseTrack, [out,retval] BSTR* StrokeResult);
	[propget, id(2), helpstring("property RecognitionParam")] HRESULT RecognitionParam([out, retval] BSTR* pVal);
	[propput, id(2), helpstring("property RecognitionParam")] HRESULT RecognitionParam([in] BSTR newVal);
};



// CStrokeRecognition

[
	coclass,
	threading("apartment"),
	vi_progid("Preprocessing.StrokeRecognition"),
	progid("Preprocessing.StrokeRecognition.1"),
	version(1.0),
	uuid("E0454D4C-0289-40E6-A55C-FA5A6E4BD7D6"),
	helpstring("StrokeRecognition Class")
]
class ATL_NO_VTABLE CStrokeRecognition : 
	public IStrokeRecognition
{
public:
	CStrokeRecognition()
	{
	}


	DECLARE_PROTECT_FINAL_CONSTRUCT()

	HRESULT FinalConstruct()
	{
		return S_OK;
	}
	
	void FinalRelease() 
	{
	}

public:

	STDMETHOD(RecognizeStroke)(BSTR MouseTrack, BSTR* StrokeResult);
	STDMETHOD(get_RecognitionParam)(BSTR* pVal);
	STDMETHOD(put_RecognitionParam)(BSTR newVal);

private:

	void DecodePointFromXML(std::vector<POINT> &rgPoints, BSTR strXML);
	void EncodeStrokeToXML(std::vector<BasicStroke> &rgStrokes, BSTR* StrokeResult);
};

