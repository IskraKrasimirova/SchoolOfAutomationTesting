namespace MatrixExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[,] matrix = new int[3, 4];

            // set values in matrix
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = row + col;
                }     
            }

            // print matrix values
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    Console.Write($"{matrix[row, col]} ");
                }

                Console.WriteLine();
            }
        }
    }
}