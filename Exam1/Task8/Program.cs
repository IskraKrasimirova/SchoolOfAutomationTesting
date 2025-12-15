namespace Task8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter an integer number (greater or equal to 2): ");
            var number = int.Parse(Console.ReadLine()!);
            var dividers = new List<int>();
            var initialNumber = number;

            var boundary = (int)Math.Sqrt(number);

            for (int i = 2; i <= boundary; i++)
            {
                while (number % i == 0)
                {
                    dividers.Add(i);
                    number = number / i;
                }
            }

            if (number > 1)
            {
                dividers.Add(number);
            }

            Console.WriteLine($"The prime dividers of {initialNumber} are: {string.Join(" ", dividers)}");
            Console.WriteLine($"{initialNumber} = {string.Join("x", dividers)}");
        }
    }
}
//Task 12 — Prime Factorization - Bonus Task - 15 points
//Description:
//Read an integer x (x ≥ 2).
//Print its prime factors in non-decreasing order.
//Example:
//Input:
//84
//Output:
//2 2 3 7
//84 = 2×2×3×7
//All factors are prime and listed in ascending order.
