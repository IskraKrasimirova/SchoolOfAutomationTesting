namespace Palindromes
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

                // IsStringPalindrome(input); // Original method without case sensitivity option, by default ignores case

                Console.Write("Ignore case? (y/n): ");
                var ignoreCaseChoice = Console.ReadLine()?.ToLower();

                while (ignoreCaseChoice != "y" && ignoreCaseChoice != "n")
                {
                    Console.Write("Invalid choice. Please enter 'y' or 'n': ");
                    ignoreCaseChoice = Console.ReadLine()?.ToLower();
                }

                if (ignoreCaseChoice == "y")
                {
                    IsStringPalindrome(input, true);
                }
                else
                {
                    IsStringPalindrome(input, false);
                }
            }
        }

        private static void IsStringPalindrome(string input)
        {
            var reversed = new string(input.Reverse().ToArray());

            if (string.Equals(input, reversed, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"'{input}' is a palindrome");
            }
            else
            {
                Console.WriteLine($"'{input}' is not a palindrome");
            }
        }

        private static void IsStringPalindrome(string input, bool isIgnoreCase)
        {
            var reversed = new string(input.Reverse().ToArray());

            bool isPalindrome;

            if (isIgnoreCase)
            {
                isPalindrome = string.Equals(input, reversed, StringComparison.OrdinalIgnoreCase);
            }
            else
            {
                isPalindrome = input == reversed;
            }

            if (isPalindrome)
            {
                Console.WriteLine($"'{input}' is a palindrome");
            }
            else
            {
                Console.WriteLine($"'{input}' is not a palindrome");
            }
        }
    }
}