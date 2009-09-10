// Tracker.cpp : Implementation of CTracker

#include "stdafx.h"
#include "Tracker.h"


// CTracker


STDMETHODIMP CTracker::StartTracker(VARIANT_BOOL ClearPreviousTrack)
{
	m_tracker.StartTracker(ClearPreviousTrack);

	return S_OK;
}

STDMETHODIMP CTracker::StopTracker(void)
{
	m_tracker.StopTracker();

	return S_OK;
}

STDMETHODIMP CTracker::get_IsTracking(VARIANT_BOOL* pVal)
{
	*pVal = m_tracker.IsTracking();

	return S_OK;
}

STDMETHODIMP CTracker::GetTrack_Raw(LONGLONG hWnd, BSTR* TrackXML)
{
	std::vector<TrackPoint> rgPoints;

	m_tracker.GetData_Raw(rgPoints, (HWND)hWnd);
	EncodeToXML(rgPoints, TrackXML);

	return S_OK;
}

STDMETHODIMP CTracker::GetTrack_No_Redundent(LONGLONG hWnd, BSTR* TrackXML)
{
	std::vector<TrackPoint> rgPoints;

	m_tracker.GetData_No_Redundent(rgPoints, (HWND)hWnd);
	EncodeToXML(rgPoints, TrackXML);

	return S_OK;
}

STDMETHODIMP CTracker::GetTrack_No_Collinear(LONGLONG hWnd, DOUBLE CollinearThreshold, BSTR* TrackXML)
{
	std::vector<TrackPoint> rgPoints;

	m_tracker.GetData_No_Collinear(rgPoints, (HWND)hWnd, CollinearThreshold);
	EncodeToXML(rgPoints, TrackXML);

	return S_OK;
}

void CTracker::EncodeToXML(std::vector<TrackPoint> rgPoints, BSTR* TrackXML)
{
	CComBSTR	xml;
	char		szTmp[1024];

	xml = "<?xml version=\"1.0\"?>";
	sprintf(szTmp, "<input ptnum=\"%d\">", rgPoints.size());
	xml += szTmp;

	for (unsigned int i = 0; i < rgPoints.size(); i++)
	{
		sprintf(szTmp, "<packet x=\"%d\" y=\"%d\" t=\"%d\"/>", rgPoints[i].x, rgPoints[i].y, rgPoints[i].time);
		xml += szTmp;
	}

	xml += "</input>";

	xml.CopyTo(TrackXML);
}
