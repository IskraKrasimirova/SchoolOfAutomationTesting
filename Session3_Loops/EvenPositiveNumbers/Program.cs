namespace EvenPositiveNumbers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Enter an integer number between -1000 and 999 (Enter 999 for Exit): ");
                var input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Input cannot be empty or whitespace.");
                    continue;
                }

                if (int.TryParse(input, out int number))
                {
                    if (number == 999)
                    {
                        Console.WriteLine("End of program.");
                        break;
                    }

                    if (number < -1000 || number > 999)
                    {
                        Console.WriteLine($"The number must be between -1000 and 999.");
                        continue;
                    }

                    Console.WriteLine($"Even non-negative numbers between {number} and 999: ");

                    for (int i = Math.Max(number, 0); i < 999; i++)
                    {
                        if (i % 2 == 0)
                        {
                            Console.Write($"{i} ");
                        }
                    }

                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer number.");
                }
            }
        }
    }
}
