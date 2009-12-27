using System;
using System.Collections.Generic;
using System.Text;

namespace EEDomain.MatrixCalculation
{
    public class Matrix
    {
        double[,] A;
        //m行n列
        int m, n;
        string name;
        public Matrix(int am, int an)
        {
            m = am;
            n = an;
            A = new double[m, n];
            name = "Result";
        }
        public Matrix(int am, int an, string aName)
        {
            m = am;
            n = an;
            A = new double[m, n];
            name = aName;
        }

        public int getM
        {
            get { return m; }
        }
        public int getN
        {
            get { return n; }
        }
        public double[,] Detail
        {
            get { return A; }
            set { A = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
    class MatrixOperator
    {
        MatrixOperator()
        { }
        /// <summary>
        /// 矩阵加法
        /// </summary>
        /// <param name="Ma"></param>
        /// <param name="Mb"></param>
        /// <returns></returns>
        public static Matrix MatrixAdd(Matrix Ma, Matrix Mb)
        {
            int m = Ma.getM;
            int n = Ma.getN;
            int m2 = Mb.getM;
            int n2 = Mb.getN;

            if ((m != m2) || (n != n2))
            {
                Exception myException = new Exception("数组维数不匹配");
                throw myException;
            }

            Matrix Mc = new Matrix(m, n);
            double[,] c = Mc.Detail;
            double[,] a = Ma.Detail;
            double[,] b = Mb.Detail;

            int i, j;
            for (i = 0; i < m; i++)
                for (j = 0; j < n; j++)
                    c[i, j] = a[i, j] + b[i, j];

            return Mc;
        }
        /// <summary>
        /// 矩阵减法
        /// </summary>
        /// <param name="Ma"></param>
        /// <param name="Mb"></param>
        /// <returns></returns>
        public static Matrix MatrixSub(Matrix Ma, Matrix Mb)
        {
            int m = Ma.getM;
            int n = Ma.getN;
            int m2 = Mb.getM;
            int n2 = Mb.getN;

            if ((m != m2) || (n != n2))
            {
                Exception myException = new Exception("数组维数不匹配");
                throw myException;
            }

            Matrix Mc = new Matrix(m, n);
            double[,] c = Mc.Detail;
            double[,] a = Ma.Detail;
            double[,] b = Mb.Detail;

            int i, j;
            for (i = 0; i < m; i++)
                for (j = 0; j < n; j++)
                    c[i, j] = a[i, j] - b[i, j];

            return Mc;
        }
        /// <summary>
        /// 矩阵打印
        /// </summary>
        /// <param name="Ma"></param>
        /// <returns></returns>
        public static string MatrixPrint(Matrix Ma)
        {
            string s;
            s = Ma.Name + ":\n";

            int m = Ma.getM;
            int n = Ma.getN;

            double[,] a = Ma.Detail;

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    s += a[i, j].ToString("0.0000") + "\t";
                }
                s += "\n";
            }
            return s;
        }
        /// <summary>
        /// 矩阵乘法
        /// </summary>
        /// <param name="Ma"></param>
        /// <param name="Mb"></param>
        /// <returns></returns>
        public static Matrix MatrixMulti(Matrix Ma, Matrix Mb)
        {
            int m = Ma.getM;
            int n = Ma.getN;
            int m2 = Mb.getM;
            int n2 = Mb.getN;

            if (n != m2)
            {
                Exception myException = new Exception("数组维数不匹配");
                throw myException;
            }

            Matrix Mc = new Matrix(m, n2);
            double[,] c = Mc.Detail;
            double[,] a = Ma.Detail;
            double[,] b = Mb.Detail;

            int i, j, k;
            for (i = 0; i < m; i++)
                for (j = 0; j < n2; j++)
                {
                    c[i, j] = 0;
                    for (k = 0; k < n; k++)
                        c[i, j] += a[i, k] * b[k, j];
                }

            return Mc;
        }
        /// <summary>
        /// 矩阵数乘
        /// </summary>
        /// <param name="k"></param>
        /// <param name="Ma"></param>
        /// <returns></returns>
        public static Matrix MatrixSimpleMulti(double k, Matrix Ma)
        {
            int n = Ma.getN;
            int m = Ma.getM;
            Matrix Mc = new Matrix(m, n);
            double[,] c = Mc.Detail;
            double[,] a = Ma.Detail;

            int i, j;
            for (i = 0; i < m; i++)
                for (j = 0; j < n; j++)
                    c[i, j] = a[i, j] * k;

            return Mc;
        }
        /// <summary>
        /// 矩阵转置
        /// </summary>
        /// <param name="Ma"></param>
        /// <param name="Mb"></param>
        /// <returns></returns>
        public static Matrix MatrixTrans(Matrix Ma)
        {
            int m = Ma.getM;
            int n = Ma.getN;
            Matrix Mc = new Matrix(n, m);
            double[,] c = Mc.Detail;
            double[,] a = Ma.Detail;

            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    c[i, j] = a[j, i];

            return Mc;
        }
        /// <summary>
        /// 矩阵求逆(高斯法)
        /// </summary>
        /// <param name="Ma"></param>
        /// <returns></returns>
        public static Matrix MatrixInv(Matrix Ma)
        {
            int m = Ma.getM;
            int n = Ma.getN;
            if (m != n)
            {
                Exception myException = new Exception("数组维数不匹配");
                throw myException;
            }
            Matrix Mc = new Matrix(m, n);
            double[,] a0 = Ma.Detail;
            double[,] a = (double[,])a0.Clone();
            double[,] b = Mc.Detail;

            int i, j, row, k;
            double max, temp;

            //单位矩阵
            for (i = 0; i < n; i++)
            {
                b[i, i] = 1;
            }
            for (k = 0; k < n; k++)
            {
                max = 0; row = k;
                //找最大元，其所在行为row
                for (i = k; i < n; i++)
                {
                    temp = Math.Abs(a[i, k]);
                    if (max < temp)
                    {
                        max = temp;
                        row = i;
                    }

                }
                if (max == 0)
                {
                    Exception myException = new Exception("没有逆矩阵");
                    throw myException;
                }
                //交换k与row行
                if (row != k)
                {
                    for (j = 0; j < n; j++)
                    {
                        temp = a[row, j];
                        a[row, j] = a[k, j];
                        a[k, j] = temp;

                        temp = b[row, j];
                        b[row, j] = b[k, j];
                        b[k, j] = temp;
                    }

                }

                //首元化为1
                for (j = k + 1; j < n; j++) a[k, j] /= a[k, k];
                for (j = 0; j < n; j++) b[k, j] /= a[k, k];

                a[k, k] = 1;

                //k列化为0
                //对a
                for (j = k + 1; j < n; j++)
                {
                    for (i = 0; i < k; i++) a[i, j] -= a[i, k] * a[k, j];
                    for (i = k + 1; i < n; i++) a[i, j] -= a[i, k] * a[k, j];
                }
                //对b
                for (j = 0; j < n; j++)
                {
                    for (i = 0; i < k; i++) b[i, j] -= a[i, k] * b[k, j];
                    for (i = k + 1; i < n; i++) b[i, j] -= a[i, k] * b[k, j];
                }
                for (i = 0; i < n; i++) a[i, k] = 0;
                a[k, k] = 1;
            }

            return Mc;
        }
        /// <summary>
        /// 矩阵求逆(伴随矩阵法)
        /// </summary>
        /// <param name="Ma"></param>
        /// <returns></returns>
        public static Matrix MatrixInvByCom(Matrix Ma)
        {
            double d = MatrixOperator.MatrixDet(Ma);
            if (d == 0)
            {
                Exception myException = new Exception("没有逆矩阵");
                throw myException;
            }
            Matrix Ax = MatrixOperator.MatrixCom(Ma);
            Matrix An = MatrixOperator.MatrixSimpleMulti((1.0 / d), Ax);
            return An;
        }
        /// <summary>
        /// 对应行列式的代数余子式矩阵
        /// </summary>
        /// <param name="Ma"></param>
        /// <returns></returns>
        public static Matrix MatrixSpa(Matrix Ma, int ai, int aj)
        {
            int m = Ma.getM;
            int n = Ma.getN;
            if (m != n)
            {
                Exception myException = new Exception("数组维数不匹配");
                throw myException;
            }
            int n2 = n - 1;
            Matrix Mc = new Matrix(n2, n2);
            double[,] a = Ma.Detail;
            double[,] b = Mc.Detail;

            //左上
            for (int i = 0; i < ai; i++)
                for (int j = 0; j < aj; j++)
                {
                    b[i, j] = a[i, j];
                }
            //右下
            for (int i = ai; i < n2; i++)
                for (int j = aj; j < n2; j++)
                {
                    b[i, j] = a[i + 1, j + 1];
                }
            //右上
            for (int i = 0; i < ai; i++)
                for (int j = aj; j < n2; j++)
                {
                    b[i, j] = a[i, j + 1];
                }
            //左下
            for (int i = ai; i < n2; i++)
                for (int j = 0; j < aj; j++)
                {
                    b[i, j] = a[i + 1, j];
                }
            //符号位
            if ((ai + aj) % 2 != 0)
            {
                for (int i = 0; i < n2; i++)
                    b[i, 0] = -b[i, 0];

            }
            return Mc;
        }
        /// <summary>
        /// 矩阵的行列式
        /// </summary>
        /// <param name="Ma"></param>
        /// <returns></returns>
        public static double MatrixDet(Matrix Ma)
        {
            int m = Ma.getM;
            int n = Ma.getN;
            if (m != n)
            {
                Exception myException = new Exception("数组维数不匹配");
                throw myException;
            }
            double[,] a = Ma.Detail;
            if (n == 1) return a[0, 0];

            double D = 0;
            for (int i = 0; i < n; i++)
            {
                D += a[1, i] * MatrixDet(MatrixSpa(Ma, 1, i));
            }
            return D;
        }
        /// <summary>
        /// 矩阵的伴随矩阵
        /// </summary>
        /// <param name="Ma"></param>
        /// <returns></returns>
        public static Matrix MatrixCom(Matrix Ma)
        {
            int m = Ma.getM;
            int n = Ma.getN;
            Matrix Mc = new Matrix(m, n);
            double[,] c = Mc.Detail;
            double[,] a = Ma.Detail;

            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                    c[i, j] = MatrixDet(MatrixSpa(Ma, j, i));

            return Mc;
        }
    }
}