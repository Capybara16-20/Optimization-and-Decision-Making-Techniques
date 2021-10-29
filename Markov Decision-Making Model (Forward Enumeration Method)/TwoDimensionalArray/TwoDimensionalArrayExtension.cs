namespace TwoDimensionalArray
{
    public static class TwoDimensionalArrayExtension
    {
        const int rowsIndex = 0;
        const int columnsIndex = 1;

        public static int GetRowsCount(this double[,] array)
        {
            return array.GetLength(rowsIndex);
        }

        public static int GetColumnsCount(this double[,] array)
        {
            return array.GetLength(columnsIndex);
        }

        public static int GetRowsCount(this int[,] array)
        {
            return array.GetLength(rowsIndex);
        }

        public static int GetColumnsCount(this int[,] array)
        {
            return array.GetLength(columnsIndex);
        }
    }
}
