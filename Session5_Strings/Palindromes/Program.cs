using System.Text;

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

                input = input.Trim();

                var filteredInput = RemovePunctuationAndWhitespaces(input);

                // with LINQ
                //var filteredInput = new string(input.Where(c => !char.IsWhiteSpace(c) && !IsPunctuation(c)).ToArray());

                var isPalindrome = IsPalindrome(filteredInput);

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

        private static bool IsPunctuation(char ch)
        {
            char[] punctuationChars = ['.', ',', '!', '?', ';', ':', '-', '\'', '\"'];

            return punctuationChars.Contains(ch);
        }

        private static string RemovePunctuationAndWhitespaces(string input)
        {
            var sb = new StringBuilder();

            foreach (var ch in input)
            {
                if (!IsPunctuation(ch) && !char.IsWhiteSpace(ch))
                {
                    sb.Append(ch);
                }
            }

            return sb.ToString();
        }

        private static bool IsPalindrome(string input)
        {
            var reversed = new string(input.Reverse().ToArray());

            return string.Equals(input, reversed, StringComparison.OrdinalIgnoreCase);
        }
    }
}
// Palindromes do not account for spaces, punctuation, or capitalization.
/*
    Evil is a name of a foeman, as I live.
    No lemon, no melon.
    Mr. Owl ate my metal worm.
    Dammit, I’m mad!
    Sir, I demand, I am a maid named Iris.
 */