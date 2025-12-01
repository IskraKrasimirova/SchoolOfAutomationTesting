namespace MiddleCharacter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string? input;

            while (true)
            {
                Console.Write("Enter string input (or type Exit to exit the program): ");
                input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Input cannot be empty. Please try again.");
                    continue;
                }

                if (string.Equals(input, "Exit", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("End of program.");
                    break;
                }

                var middleChar = FindMiddleCharacter(input);

                Console.WriteLine($"Middle character(s) of the string {input} is '{middleChar}'");
            }
        }

        private static string FindMiddleCharacter(string input)
        {
            var length = input.Length;
            var middleIndex = length / 2;
            string middleChar;

            if (length % 2 == 0)
            {
                middleChar = input.Substring(middleIndex - 1, 2);
            }
            else
            {
                middleChar = input[middleIndex].ToString();
            }

            return middleChar;
        }
    }
}
