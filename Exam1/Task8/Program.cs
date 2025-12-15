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

            for (int i = 2; i <= number; i++)
            {
                if (IsPrime(i))
                {
                    while (number % i == 0)
                    {
                        dividers.Add(i);
                        number = number / i;
                    }
                }
            }

            Console.WriteLine($"The prime dividers of {initialNumber} are: {string.Join(" ", dividers)}");
            Console.WriteLine($"{initialNumber} = {string.Join("x", dividers)}");
        }

        private static bool IsPrime(int number)
        {
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            int boundary = (int)Math.Sqrt(number);

            for (int i = 3; i <= boundary; i += 2)
            {
                if (number % i == 0) return false;
            }

            return true;
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
