#ifndef __MOUSE_TRACK_H__
#define __MOUSE_TRACK_H__

typedef struct tagTrackPoint
{
	int		x;
	int		y;
	LONG	time;
}
TrackPoint;

class MouseTracker
{
public:
	MouseTracker();
	~MouseTracker();
public:
	void StartTracker(BOOL bClearPrevious);
	void StopTracker();
	void GetData_Raw(std::vector<TrackPoint> &rgPoints, HWND hWnd);
	void GetData_No_Redundent(std::vector<TrackPoint> &rgPoints, HWND hWnd);
	void GetData_No_Collinear(std::vector<TrackPoint> &rgPoints, HWND hWnd, double CollinearThreshold = 0.5);
	BOOL IsTracking();
private:
	static unsigned int __stdcall MouseTrackThread(void* lpParam);
public:
	int						m_iInterval;
private:
	BOOL					m_bTracking;
	HANDLE					m_hTimer;
	HANDLE					m_hThread;
	CRITICAL_SECTION		m_csPoints;
	std::vector<TrackPoint>	m_rgPoints;
};

#endif