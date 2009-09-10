// Tracker.h : Declaration of the CTracker

#pragma once
#include "resource.h"       // main symbols
#include <vector>
#include "MouseTrack.h"

// ITracker
[
	object,
	uuid("62881EF1-7089-40EE-A5F7-31FBD2F7BD70"),
	dual,	helpstring("ITracker Interface"),
	pointer_default(unique)
]
__interface ITracker : IDispatch
{
	[id(1), helpstring("method StartTracker")] HRESULT StartTracker([in] VARIANT_BOOL ClearPreviousTrack);
	[id(2), helpstring("method StopTracker")] HRESULT StopTracker(void);
	[propget, id(3), helpstring("property IsTracking")] HRESULT IsTracking([out, retval] VARIANT_BOOL* pVal);
	[id(4), helpstring("method GetTrack_Raw")] HRESULT GetTrack_Raw([in] LONGLONG hWnd, [out,retval] BSTR* TrackXML);
	[id(5), helpstring("method GetTrack_No_Redundent")] HRESULT GetTrack_No_Redundent([in] LONGLONG hWnd, [out,retval] BSTR* TrackXML);
	[id(6), helpstring("method GetTrack_No_Collinear")] HRESULT GetTrack_No_Collinear([in] LONGLONG hWnd, [in] DOUBLE CollinearThreshold, [out,retval] BSTR* TrackXML);
};



// CTracker

[
	coclass,
	threading("apartment"),
	vi_progid("MouseTrackerLib.Tracker"),
	progid("MouseTrackerLib.Tracker.1"),
	version(1.0),
	uuid("098069F8-F34C-44D6-93DA-7D64BD744381"),
	helpstring("Tracker Class")
]
class ATL_NO_VTABLE CTracker : 
	public ITracker
{
public:
	CTracker()
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
	STDMETHOD(StartTracker)(VARIANT_BOOL ClearPreviousTrack);
	STDMETHOD(StopTracker)(void);
	STDMETHOD(get_IsTracking)(VARIANT_BOOL* pVal);
	STDMETHOD(GetTrack_Raw)(LONGLONG hWnd, BSTR* TrackXML);
	STDMETHOD(GetTrack_No_Redundent)(LONGLONG hWnd, BSTR* TrackXML);
	STDMETHOD(GetTrack_No_Collinear)(LONGLONG hWnd, DOUBLE CollinearThreshold, BSTR* TrackXML);

private:
	void EncodeToXML(std::vector<TrackPoint> rgPoints, BSTR* TrackXML);
private:
	MouseTracker	m_tracker;
};

