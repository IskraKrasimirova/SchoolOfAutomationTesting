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

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Input cannot be empty or whitespace.");
                    continue;
                }

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
                if (IsPrime(i))
                {
                    Console.Write($"{i} ");
                }
            }
        }

        private static bool IsPrime(int number)
        {
            if (number == 2) return true;
            if ((number % 2) == 0) return false;

            int boundary = (int)Math.Sqrt(number);

            for (int i = 3; i <= boundary; i += 2)
            {
                if ((number % i) == 0) return false;
            }

            return true;
        }

        //private static bool IsPrime(int number)
        //{
        //    bool isPrime = true;

        //    if (number == 2)
        //    {
        //        isPrime = true;
        //    }
        //    else if ((number % 2) == 0)
        //    {
        //        isPrime = false;
        //    }
        //    else
        //    {
        //        int boundary = (int)Math.Sqrt(number);

        //        for (int i = 3; i <= boundary; i += 2)
        //        {
        //            if ((number % i) == 0)
        //            {
        //                isPrime = false;
        //                break;
        //            }
        //        }
        //    }

        //    return isPrime;
        //}
    }
}
