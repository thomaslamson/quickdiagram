#include "stdafx.h"

#include <windows.h>
#include <process.h>
#include <vector>
#include <math.h>

#include "CommonFunction.h"
#include "MouseTrack.h"

MouseTracker::MouseTracker()
{
	unsigned int iThreadID;

	InitializeCriticalSection(&m_csPoints);

	m_hTimer	= CreateEvent(NULL, TRUE, FALSE, NULL);
	m_hThread	= (HANDLE)_beginthreadex(NULL, 0, this->MouseTrackThread, this, 0, &iThreadID);
	m_bTracking	= FALSE;
	m_iInterval	= 10;
}

MouseTracker::~MouseTracker()
{
	SetEvent(m_hTimer);
	WaitForSingleObject(m_hThread, INFINITE);

	CloseHandle(m_hTimer);
	CloseHandle(m_hThread);

	DeleteCriticalSection(&m_csPoints);
}

void MouseTracker::StartTracker(BOOL bClearPrevious)
{
	if (bClearPrevious)
	{
		m_rgPoints.clear();
	}

	m_bTracking = TRUE;
}

void MouseTracker::StopTracker()
{
	m_bTracking = FALSE;
}

void MouseTracker::GetData_Raw(std::vector<TrackPoint> &rgPoints, HWND hWnd)
{
	POINT		pt;
	TrackPoint	ptTrack;

	EnterCriticalSection(&m_csPoints);

	for (unsigned int i = 0; i < m_rgPoints.size(); i++)
	{
		pt.x = m_rgPoints[i].x;
		pt.y = m_rgPoints[i].y;

		if (hWnd != NULL)
		{
			ScreenToClient(hWnd, &pt);
		}

		ptTrack.x		= pt.x;
		ptTrack.y		= pt.y;
		ptTrack.time	= m_rgPoints[i].time;

		rgPoints.push_back(ptTrack);
	}

	LeaveCriticalSection(&m_csPoints);
}

void MouseTracker::GetData_No_Redundent(std::vector<TrackPoint> &rgPoints, HWND hWnd)
{
	POINT	pt;
	TrackPoint	ptTrack;

	EnterCriticalSection(&m_csPoints);

	for (unsigned int i = 0; i < m_rgPoints.size(); i++)
	{
		pt.x = m_rgPoints[i].x;
		pt.y = m_rgPoints[i].y;

		if (hWnd != NULL)
		{
			ScreenToClient(hWnd, &pt);
		}

		if (i > 0)	//Remove redundent points
		{
			if ((pt.x == rgPoints.back().x) && (pt.y == rgPoints.back().y))
			{
				continue;
			}
		}

		ptTrack.x		= pt.x;
		ptTrack.y		= pt.y;
		ptTrack.time	= m_rgPoints[i].time;

		rgPoints.push_back(ptTrack);
	}

	LeaveCriticalSection(&m_csPoints);
}

void MouseTracker::GetData_No_Collinear(std::vector<TrackPoint> &rgPoints, HWND hWnd, double CollinearThreshold)
{
	std::vector<TrackPoint>	rgCleanedPoints;
	TrackPoint	pt1, pt2;
	POINT		p1, p2;
	double		A, B, C, A2B2;

	GetData_No_Redundent(rgCleanedPoints, hWnd);

	if (rgCleanedPoints.size() >= 2)
	{
		pt1 = rgCleanedPoints[0];
		pt2 = rgCleanedPoints[1];

		rgPoints.push_back(pt1);

		p1.x = pt1.x;
		p1.y = pt1.y;
		p2.x = pt2.x;
		p2.y = pt2.y;
		CalcLineParam(p1, p2, A, B, C);

		A2B2	= sqrt(A * A + B * B) * CollinearThreshold;
		pt1		= pt2;

		for (unsigned int i = 2; i < rgCleanedPoints.size(); i++)
		{
			pt2		= rgCleanedPoints[i];

			if (fabs(A * pt2.x + B * pt2.y + C) > A2B2)
			{
				rgPoints.push_back(pt1);

				p1.x = pt1.x;
				p1.y = pt1.y;
				p2.x = pt2.x;
				p2.y = pt2.y;
				CalcLineParam(p1, p2, A, B, C);

				A2B2	= sqrt(A * A + B * B) * CollinearThreshold;
				pt1		= pt2;
			}

			pt1 = pt2;
		}

		rgPoints.push_back(pt1);
	}
}

BOOL MouseTracker::IsTracking()
{
	return m_bTracking;
}

unsigned int __stdcall MouseTracker::MouseTrackThread(void* lpParam)
{
	MouseTracker*	lpThis = (MouseTracker*)lpParam;
	TrackPoint		ptTrack;
	POINT			pt;

	while (WaitForSingleObject(lpThis->m_hTimer, lpThis->m_iInterval) == WAIT_TIMEOUT)
	{
		if (lpThis->m_bTracking)
		{
			if (GetCursorPos(&pt))
			{
				ptTrack.time	= GetTickCount();
				ptTrack.x		= pt.x;
				ptTrack.y		= pt.y;

				EnterCriticalSection(&lpThis->m_csPoints);
				lpThis->m_rgPoints.push_back(ptTrack);
				LeaveCriticalSection(&lpThis->m_csPoints);
			}
		}
	}

	return 0;
}
