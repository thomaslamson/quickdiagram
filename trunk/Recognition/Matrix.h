template <class TYPE>
class Matrix
{
public:
	Matrix(int row,int col)
	{
		m_row = row;
		m_col = col;

		m_matrix = new TYPE*[m_row];

		for (int i = 0; i < m_row; i++)
		{
			m_matrix[i] = new TYPE[m_col];
			ZeroMemory(m_matrix[i], sizeof(TYPE) * m_col);
		}
	}

	~Matrix()
	{
		for (int i = 0; i < m_row; i++)
		{
			delete[] m_matrix[i];
		}
		delete[] m_matrix;
	}

	TYPE* operator [](int row)
	{
		return m_matrix[row];
	}

	int Rows()
	{
		return m_row;
	}

	int Columns()
	{
		return m_col;
	}

private:
	TYPE**		m_matrix;
	int			m_row;
	int			m_col;
};
