namespace Interview2
{
    public static class SortedMatrix
    {
        // Return a bool indicating if a given value is present in a matrix that has the following properties:
        // matrix[a, b] < matrix[a + 1, b] for all a, b that are valid indicies in the matrix
        // matrix[a, b] < matrix[a, b + 1] for all a, b that are valid indicies in the matrix
        public static bool FindValue(int[,] matrix, int value)
        {
            Point minimum = new Point { Row = 0, Column = 0 };
            Point maximum = new Point { Row = matrix.GetLength(0) - 1, Column = matrix.GetLength(1) - 1 };
            Point middle = new Point();

            while (minimum.Row <= maximum.Row && minimum.Column <= maximum.Column)
            {
                middle.Row = (minimum.Row + maximum.Row) / 2;
                middle.Column = (minimum.Column + maximum.Column) / 2;

                if (matrix[middle.Row, middle.Column] > value)
                {
                    maximum.Row = middle.Row - 1;
                    maximum.Column = middle.Column - 1;
                }
                else if (matrix[middle.Row, middle.Column] < value)
                {
                    minimum.Row = middle.Row + 1;
                    minimum.Column = middle.Column + 1;
                }
                else
                {
                    return true;
                }
            }

            if (BinarySearchRow(matrix, minimum.Row, value))
            {
                return true;
            }
            else if (BinarySearchColumn(matrix, minimum.Column, value))
            {
                return true;
            }

            return false;
        }

        // Perform a binary search on a specific row in a matrix
        private static bool BinarySearchRow(int[,] matrix, int row, int value)
        {
            int minimum = 0;
            int maximum = matrix.GetLength(1) - 1;

            while (minimum <= maximum)
            {
                int middle = (minimum + maximum) / 2;

                if (matrix[row, middle] > value)
                {
                    maximum = middle - 1;
                }
                else if (matrix[row, middle] < value)
                {
                    minimum = middle + 1;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        // Perform a binary search on a specific column in a matrix
        private static bool BinarySearchColumn(int[,] matrix, int column, int value)
        {
            int minimum = 0;
            int maximum = matrix.GetLength(0) - 1;

            while (minimum <= maximum)
            {
                int middle = (minimum + maximum) / 2;

                if (matrix[middle, column] > value)
                {
                    maximum = middle - 1;
                }
                else if (matrix[middle, column] < value)
                {
                    minimum = middle + 1;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        private struct Point
        {
            public int Row;
            public int Column;
        }
    }
}
