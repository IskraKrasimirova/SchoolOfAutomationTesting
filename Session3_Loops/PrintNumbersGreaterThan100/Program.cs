namespace PrintNumbersGreaterThan100
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int number = 0;

            while (number <= 100)
            {
                Console.Write("Enter an integer number greater than 100: ");
                var input = Console.ReadLine();

                if (int.TryParse(input, out number) && number > 100)
                {
                    for (int i = 1; i <= number; i++)
                    {
                        Console.WriteLine(i);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }
            }
        }
    }
}
