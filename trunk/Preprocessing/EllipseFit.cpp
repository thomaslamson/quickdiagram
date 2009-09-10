#include "stdafx.h"

#include <Windows.h>
#include <math.h>
#include <vector>

#include "Matrix.h"
#include "CommonFunction.h"

void ROTATE(Matrix<double> &a, int i, int j, int k, int l, double tau, double s) 
{
	double g, h;

	g = a[i][j];
	h = a[k][l];
	a[i][j] = g - s * (h + g * tau);
	a[k][l] = h + s * (g - h * tau);
}

void jacobi(Matrix<double> &a, int n, double* d, Matrix<double> &v, int nrot)      
{
	int		i, j, iq, ip;
	double	tresh, theta, tau, t, sm, s, h, g, c;
	double* b = new double[n + 1];
	double* z = new double[n + 1];

	for (ip = 1; ip <= n; ip++) 
	{
		ZeroMemory(v[ip], sizeof(double) * (n + 1));
		v[ip][ip]=1.0;
	}

	for (ip = 1; ip <= n; ip++) 
	{
		b[ip] = d[ip] = a[ip][ip];
		z[ip] = 0.0;
	}

	nrot = 0;

	for (i = 1; i <= 50; i++) 
	{
		sm = 0.0;

		for (ip = 1; ip <= n - 1; ip++) 
		{
			for (iq = ip + 1; iq <= n; iq++)
			{
				sm += fabs(a[ip][iq]);
			}
		}

		if (sm == 0.0) 
		{
			delete[] b;
			delete[] z;
			return;
		}

		if (i < 4)
		{
			tresh = 0.2 * sm / (n * n);
		}
		else
		{
			tresh = 0.0;
		}

		for (ip = 1;ip <= n - 1; ip++) 
		{
			for (iq = ip + 1; iq <= n; iq++) 
			{
				g = 100.0 * fabs(a[ip][iq]);
				if ((i > 4) && (fabs(d[ip]) + g == fabs(d[ip])) && (fabs(d[iq]) + g == fabs(d[iq])))
				{
					a[ip][iq] = 0.0;
				}
				else if (fabs(a[ip][iq]) > tresh) 
				{
					h = d[iq] - d[ip];

					if (fabs(h) + g == fabs(h))
					{
						t = (a[ip][iq]) / h;
					}
					else
					{
						theta = 0.5 * h / (a[ip][iq]);
						t = 1.0 / (fabs(theta) + sqrt(1.0 + theta * theta));
						if (theta < 0.0)
						{
							t = -t;
						}
					}

					c	= 1.0 / sqrt(1 + t * t);
					s	= t * c;
					tau	= s / (1.0 + c);
					h	= t * a[ip][iq];

					z[ip] -= h;
					z[iq] += h;
					d[ip] -= h;
					d[iq] += h;
					a[ip][iq]=0.0;

					for (j = 1;j <= ip - 1; j++)
					{
						ROTATE(a, j, ip, j, iq, tau, s);
					}

					for (j = ip + 1; j <= iq - 1; j++)
					{
						ROTATE(a, ip, j, j, iq, tau, s);
					}

					for (j = iq + 1; j <= n; j++)
					{
						ROTATE(a, ip, j, iq, j, tau, s);
					}

					for (j = 1; j <= n; j++)
					{
						ROTATE(v, j, ip, j, iq, tau, s);
					}

					++nrot;
				}
			}
		}

		for (ip = 1; ip <= n; ip++) 
		{
			b[ip] += z[ip];
			d[ip] = b[ip];
			z[ip] = 0.0;
		}
	}

	delete[] b;
	delete[] z;
}

//********************************************************************************
// Perform the Cholesky decomposition
// Return the lower triangular matrix L such that L*L'=A
//********************************************************************************
void choldc(Matrix<double> &a, int n, Matrix<double> &l)
{
	int		i, j, k;
	double	sum;
	double*	p = new double[n + 1];

	for (i = 1; i <= n; i++)
	{
		for (j = i; j <= n; j++)
		{
			for (sum=a[i][j],k=i-1;k>=1;k--)
			{
				sum -= a[i][k] * a[j][k];
			}

			if (i == j)
			{
				if (sum>=0.0)
				{
					p[i] = sqrt(sum);
				}
			}
			else 
			{
				a[j][i] = sum/p[i];
			}
		}
	}       

	for (i=1; i<=n; i++)
	{
		for (j=i; j<=n; j++)
		{
			if (i==j)
			{
				l[i][i] = p[i];
			}
			else
			{
				l[j][i] = a[j][i];
				l[i][j] = 0.0;
			}  
		}
	}

	delete []p;
}

//********************************************************************************
// Calculate the inverse matrix of TB and store it in InvB
//********************************************************************************
bool inverse(Matrix<double> &TB, Matrix<double> &InvB, int N) 
{  
	int		i, j, k, p, q;
	int		npivot;

	double	mult;
	double	D,temp;
	double	maxpivot;
	double	eps = 10e-20;

	Matrix<double>	B(N + 1,N + 2);
	Matrix<double>	A(N + 1,2 * N + 2);
	Matrix<double>	C(N + 1,N + 1);

	for (k = 1; k <= N; k++)
	{
		memcpy(B[k], TB[k], sizeof(double) * (N + 1));
	}

	for (k = 1; k <= N; k++)
	{
		ZeroMemory(A[k], sizeof(double) * (2 * N + 2));
		memcpy(A[k], B[k], sizeof(double) * (N + 2));
		A[k][k + N + 1] = 1;
	}

	for (k = 1; k <= N; k++)
	{
		maxpivot	= fabs((double)A[k][k]);
		npivot		= k;

		for (i = k; i <= N; i++)
		{
			if (maxpivot < fabs((double)A[i][k]))
			{
				maxpivot	= fabs((double)A[i][k]);
				npivot		= i;
			}
		}

		if (maxpivot >= eps)
		{
			if (npivot!=k)
			{
				for (j = k; j <= 2 * N + 1; j++)
				{
					temp			= A[npivot][j];
					A[npivot][j]	= A[k][j];
					A[k][j]			= temp;
				} ;
			}
		
			D = A[k][k];

			for (j = 2 * N + 1; j >= k; j--)
			{
				A[k][j] = A[k][j] / D;
			}

			for (i = 1;i <= N; i++)
			{
				if (i != k)
				{
					mult = A[i][k];
					for (j = 2 * N + 1; j >= k; j--)
					{
						A[i][j] = A[i][j] - mult * A[k][j];
					}
				}
			}
		}
		else
		{
			return false;
		}
	}

	for (k = 1, p = 1; k <= N; k++, p++)
	{
		for (j = N + 2, q = 1; j <= 2 * N + 1; j++, q++)
		{
			InvB[p][q] = A[k][j];
		}
	}

	return true;
}

//********************************************************************************
// Get the result _res of matrix _A multiplies _B
//********************************************************************************
void AperB(Matrix<double> &_A, Matrix<double> &_B, Matrix<double> &_res, int _righA, int _colA, int _righB, int _colB) 
{
	int	p, q, l;

	for (p = 1;p <= _righA; p++)
	{
		for (q = 1; q <= _colB; q++)
		{
			_res[p][q] = 0.0;
			for (l = 1; l <= _colA; l++)
			{
				_res[p][q] += _A[p][l] * _B[l][q];
			}
		}
	}
}                                                 

//********************************************************************************
// Get the result _res of matrix _A's transpose multiplies _B
//********************************************************************************
void A_TperB(Matrix<double> &_A, Matrix<double> &_B, Matrix<double> &_res, int _righA, int _colA, int _righB, int _colB) 
{
	int	p, q, l;

	for (p = 1;p <= _colA; p++)
	{
		for (q = 1; q <= _colB; q++)
		{
			_res[p][q] = 0.0;
			for (l = 1; l <= _righA; l++)
			{
				_res[p][q] += _A[l][p] * _B[l][q];
			}
		}                                            
	}
}

//********************************************************************************
// Get the result _res of matrix _A multiplies _B's transpose
//********************************************************************************
void AperB_T(Matrix<double> &_A, Matrix<double> &_B, Matrix<double> &_res, int _righA, int _colA, int _righB, int _colB) 
{
	int	p, q, l;

	for (p = 1; p <= _colA; p++)
	{
		for (q = 1; q <= _colB; q++)
		{ 
			_res[p][q] = 0.0;
			for (l = 1;l <= _righA; l++)
			{
				_res[p][q] += _A[p][l] * _B[q][l];
			}
		}
	}
}

bool EllipseFitting(POINT* rgPoints, int nPoints, double &a, double &b, double &c, double &d, double &e, double &f)
{
	int		i, j;
	int		np = nPoints;
	int		nrot=0;

	Matrix<double>	D(np+1,7);	//Design matrix
	Matrix<double>	S(7,7);		//Scatter matrix
	Matrix<double>	Const(7,7);	//Constraint matrix
	Matrix<double>	temp(7,7);
	Matrix<double>	L(7,7);		//The lower triangular matrix L * L' = S
	Matrix<double>	C(7,7);
	Matrix<double>	invL(7,7);	//Inverse matrix of L
	Matrix<double>	V(7,7);		//The eigen vectors
	Matrix<double>	sol(7,7);	//The GVE solution

	double	ev[7];		//The eigen value
	double	pvec[7];

	if (np < 6)
	{
		return false;
	}

	//Build design matrix D
	for (i = 0; i < np; i++)
	{ 
		D[i][1]	= rgPoints[i].x * rgPoints[i].x;
		D[i][2] = rgPoints[i].x * rgPoints[i].y;
		D[i][3] = rgPoints[i].y * rgPoints[i].y;
		D[i][4] = rgPoints[i].x;
		D[i][5] = rgPoints[i].y;
		D[i][6] = 1.0;
	}

	//Build Scatter matrix S
	A_TperB(D, D, S, np, 6, np, 6);

	//Build 6*6 constraint matrix
	Const[1][3] = -2;
	Const[2][2] = 1;
	Const[3][1] = -2;

	//Sovle generalized eigen system
	choldc(S, 6, L);
	if (!inverse(L, invL, 6))
	{
		return false;
	}
	AperB_T(Const, invL, temp, 6, 6, 6, 6);
	AperB(invL, temp, C, 6, 6, 6, 6);
	jacobi(C, 6, ev, V, nrot);
	A_TperB(invL, V, sol, 6, 6, 6, 6);

	//Normalize the solution
	for (j = 1; j <= 6; j++)
	{
		double mod = 0.0;

		for (i = 1; i <= 6; i++)
		{
			mod += sol[i][j] * sol[i][j];
		}

		mod = sqrt(mod);

		for (i = 1 ; i <= 6; i++)
		{
		 	sol[i][j] /= mod;
		}
	}

	double	zero	= 10e-20;
	int		solind	= 0;

	//Find the only negative eigen value
	for (i = 1; i <= 6; i++)
	{
		if ((ev[i] < 0) && (fabs(ev[i]) > zero))
		{
			solind = i;
		}
	}

	//Get the fitted parameters
	for (j = 1; j <= 6; j++)
	{
		pvec[j] = sol[j][solind];
	}		

	a = pvec[1];
	b = pvec[2];
	c = pvec[3];
	d = pvec[4];
	e = pvec[5];
	f = pvec[6];

	return true;
}

double CalcErrorToEllipse(POINT* rgPoints, int nPoints, double tx, double ty, double fa, double fc, double dRotateAngle)
{
	double	cosT, sinT, error;
	double	x1, y1, x2, y2;
	double	x, y, k;
	double	px, py;
	double	dx, dy;
	int		nStep, nSample;

	cosT	= cos(-dRotateAngle);
	sinT	= sin(-dRotateAngle);
	error	= 0;
	nSample	= 0;

	for (int i = 1; i < nPoints; i++)
	{
		x1 = (rgPoints[i - 1].x - tx) * cosT - (rgPoints[i - 1].y - ty) * sinT;
		y1 = (rgPoints[i - 1].x - tx) * sinT + (rgPoints[i - 1].y - ty) * cosT;
		x2 = (rgPoints[i].x - tx) * cosT - (rgPoints[i].y - ty) * sinT;
		y2 = (rgPoints[i].x - tx) * sinT + (rgPoints[i].y - ty) * cosT;

		nStep	= (int)max(fabs(x1 - x2), fabs(y1 - y2));
		dx		= (x1 - x2) / nStep;
		dy		= (y1 - y2) / nStep;
		x		= x1;
		y		= y1;

		for (int j = 0; j <= nStep; j++)
		{
			if (fabs(x) > DOUBLE_ZERO)
			{
				k	= y / x;
				if (x > 0)
				{
					px	= fa * fc / sqrt(fc * fc + fa * fa * k * k);
				}
				else
				{
					px	= -fa * fc / sqrt(fc * fc + fa * fa * k * k);
				}
				py = k * px;
			}
			else
			{
				if (y > 0)
				{
					px = 0;
					py = fc;
				}
				else
				{
					px = 0;
					py = -fc;
				}
			}

			px -= x;
			py -= y;

			error += sqrt(px * px + py * py);

			x += dx;
			y += dy;

			nSample++;
		}
	}

	return error / nSample;
}

double CalcArcPointAngle(double x, double y, double cx, double cy, double a, double b, double rotateAngle)
{
	double dx	= x - cx;
	double dy	= y - cy;
	double cosT	= cos(-rotateAngle);
	double sinT	= sin(-rotateAngle);
	double tx	= dx * cosT - dy * sinT;
	double ty	= (dx * sinT + dy * cosT);
	double ang	= atan2(ty, tx);

	if (ang < 0)
	{
		ang += 2 * PI;
	}

	return ang;
}

void CalcArcAngle(POINT* rgPoints, int nPoints, double cx, double cy, double a, double b, double dRotateAngle, double &dStartAngle, double &dEndAngle)
{
	double orgAngle, curAngle;
	double angle1, angle2;

	dStartAngle = dEndAngle = 0;

	if (nPoints >= 2)
	{
		dStartAngle	= CalcArcPointAngle(rgPoints[0].x, rgPoints[0].y, cx, cy, a, b, dRotateAngle);
		dEndAngle	= CalcArcPointAngle(rgPoints[1].x, rgPoints[1].y, cx, cy, a, b, dRotateAngle);
		orgAngle	= dEndAngle;

		RegularizeAngleRange(dStartAngle, dEndAngle);

		for (int i = 2; i < nPoints; i++)
		{
			curAngle = CalcArcPointAngle(rgPoints[i].x, rgPoints[i].y, cx, cy, a, b, dRotateAngle);
			angle1 = orgAngle;
			angle2 = curAngle;
			RegularizeAngleRange(angle1, angle2);
			MergeAngleRange(dStartAngle, dEndAngle, angle1, angle2);
			orgAngle = curAngle;
		}
	}
}

bool EllipticalArcFitting(POINT* rgPoints, int nPoints, RECT &rcBounding, double &dRotateAngle, double &dStartAngle, double &dEndAngle, double &error)
{
	double	a, b, c, d, e, f;
	double	fa, fc, ff;
	double	tx, ty, td;
	double	cosT, sinT;

	if (!EllipseFitting(rgPoints, nPoints, a, b, c, d, e, f))
	{
		return false;
	}

	td = b * b - 4 * a * c;
	tx = (2 * c * d - b * e) / td;
	ty = (2 * a * e - b * d) / td;
	ff = a * tx * tx + c * ty * ty + b * tx * ty + d * tx + e * ty + f;

	td = sqrt(2.0);
	fa = fabs(a - c);
	fc = sqrt(fa * fa + b * b);

	cosT	= sqrt(1.0 - fa / fc) * (fa * fa + fa * fc) / b / (a - c) / td;
	sinT	= sqrt(1.0 - fa / fc) / td;
	fa		= -ff / (a * cosT * cosT + b * cosT * sinT + c * sinT * sinT);
	fc		= -ff / (c * cosT * cosT - b * cosT * sinT + a * sinT * sinT);

	if ((fa < 0) || (fc < 0))
	{
		return false;
	}

	fa = sqrt(fa);
	fc = sqrt(fc);

	dRotateAngle		= atan2(sinT, cosT);
	rcBounding.left		= (LONG)(tx - fa);
	rcBounding.right	= (LONG)(tx + fa);
	rcBounding.top		= (LONG)(ty - fc);
	rcBounding.bottom	= (LONG)(ty + fc);

	CalcArcAngle(rgPoints, nPoints, tx, ty, fa, fc, dRotateAngle, dStartAngle, dEndAngle);

	error = CalcErrorToEllipse(rgPoints, nPoints, tx, ty, fa, fc, dRotateAngle);

	return true;
}