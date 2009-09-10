// StrokeRefinement.h : Declaration of the CStrokeRefinement

#pragma once

#include <vector>

#include "resource.h"       // main symbols
#include "Relation.h"
#include "StrokeRefine.h"

// IStrokeRefinement
[
	object,
	uuid("FDAE69D6-93E3-4815-9FB7-78457D9B7FED"),
	dual,	helpstring("IStrokeRefinement Interface"),
	pointer_default(unique)
]
__interface IStrokeRefinement : IDispatch
{
	[id(1), helpstring("method Refine_MergeVertex")] HRESULT Refine_MergeVertex([in] BSTR StrokeXML, [in] DOUBLE DistanceThreshold, [out,retval] BSTR* ResultXML);
};



// CStrokeRefinement

[
	coclass,
	threading("apartment"),
	vi_progid("StrokeRefineLib.StrokeRefinement"),
	progid("StrokeRefineLib.StrokeRefinement.1"),
	version(1.0),
	uuid("1A512878-0A46-4EB9-9BED-7F902AF26A5F"),
	helpstring("StrokeRefinement Class")
]
class ATL_NO_VTABLE CStrokeRefinement : 
	public IStrokeRefinement
{
public:
	CStrokeRefinement()
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
	STDMETHOD(Refine_MergeVertex)(BSTR StrokeXML, DOUBLE DistanceThreshold, BSTR* ResultXML);
private:
	void DecodeStrokeFromXML(std::vector<PrimitiveStroke*> &rgStrokes, BSTR StrokeXML);
	void EncodeStrokeToXML(std::vector<PrimitiveStroke*> &rgStrokes, BSTR* ResultXML);
	void CreateStrokeListFromXML(std::vector<PrimitiveStroke*> &rgStrokes, IXMLDOMNode* pCandidate);
	PrimitiveStroke* CreateStrokeFromXML(IXMLDOMNode* lpNode);
};

