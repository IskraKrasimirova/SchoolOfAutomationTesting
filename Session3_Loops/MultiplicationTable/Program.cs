namespace MultiplicationTable
{
    internal class Program
    {
        static void Main(string[] args)
        {
            for (int i = 1; i <= 5; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    Console.WriteLine($"{i} * {j} = {i * j}");
                    //Console.Write($"{i * j,4}"); // print in table format
                }

                Console.WriteLine();
            }
        }
    }
}
