namespace PrimaryNumbers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int number;

            while (true)
            {
                Console.Write("Enter an integer number greater than 1: ");
                var input = Console.ReadLine();

                if (int.TryParse(input, out number))
                {
                    if (number > 1 && number <= int.MaxValue)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"The number must be between 1 and {int.MaxValue}. Please try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer number.");
                }
            }

            Console.WriteLine($"Primary numbers between 1 and {number}:");

            for (int i = 2; i <= number; i++)
            {
                bool isPrime = true;

                for (int j = 2; j <= Math.Sqrt(i); j++)
                {
                    if (i % j == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }

                if (isPrime)
                {
                    Console.Write($"{i} ");
                }
            }
        }
    }
}
