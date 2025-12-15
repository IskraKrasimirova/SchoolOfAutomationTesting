namespace Task2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter positive integer number: ");
            int n = int.Parse(Console.ReadLine()!);
            int[] fibonacci = new int[n+1];

            fibonacci[0] = 0;
            fibonacci[1] = 1;
            int count = 2;

            for (int i = 2; i <= n; i++)
            {
                fibonacci[i] = fibonacci[i - 1] + fibonacci[i - 2];

                if (fibonacci[i] > n)
                {
                    break;
                }

                count++;
            }

            for (int i = 0; i < count; i++)
            {
                Console.Write($"{fibonacci[i]} ");
            }
        }
    }
}
//Task 2 — Fibonacci Numbers up to N - 7 Points
//Description:
//Read an integer N. Print all Fibonacci numbers less than or equal to N.
//Fibonacci sequence: 0, 1, 1, 2, 3, 5, 8, 13, …
//Example:
//Input:
//20
//Output:
//0 1 1 2 3 5 8 13
