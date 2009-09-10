// PrimitiveRecognition.h : Declaration of the CPrimitiveRecognition

#pragma once

#include <vector>

#include "resource.h"       // main symbols
#include "Matrix.h"
#include "Relation.h"
#include "Similarity.h"

// IPrimitiveRecognition
[
	object,
	uuid("88869CCD-F59E-4985-B4D8-EAE83C0D0F67"),
	dual,	helpstring("IPrimitiveRecognition Interface"),
	pointer_default(unique)
]
__interface IPrimitiveRecognition : IDispatch
{
	[id(1), helpstring("method LoadTemplates")] HRESULT LoadTemplates([in] BSTR TemplatePath);
	[id(2), helpstring("method RecognizePrimitive")] HRESULT RecognizeObjects([in] BSTR strokeXML, [out,retval] BSTR* resultXML);
};



// CPrimitiveRecognition

[
	coclass,
	threading("apartment"),
	vi_progid("Recognition.PrimitiveRecognition"),
	progid("Recognition.PrimitiveRecognition.1"),
	version(1.0),
	uuid("D8E8E148-F05C-4862-B6BC-A55C28E97F86"),
	helpstring("PrimitiveRecognition Class")
]
class ATL_NO_VTABLE CPrimitiveRecognition : 
	public IPrimitiveRecognition
{
public:
	CPrimitiveRecognition()
	{
	}


	DECLARE_PROTECT_FINAL_CONSTRUCT()

	HRESULT FinalConstruct()
	{
		return S_OK;
	}
	
	void FinalRelease() 
	{
		FreeTemplates();
	}

public:
	STDMETHOD(LoadTemplates)(BSTR TemplatePath);
	STDMETHOD(RecognizeObjects)(BSTR strokeXML, BSTR* resultXML);
private:
	void LoadTemplates(char* szPath);
	void FreeTemplates();
private:
	std::vector<GraphicObject*>			m_rgTemplates;
	std::vector<Matrix<unsigned int>*>	m_rgRelations;
};

