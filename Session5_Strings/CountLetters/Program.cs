namespace CountLetters
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string? sentence;

            while (true)
            {
                Console.Write("Enter sentence (or type Exit to exit the program): ");
                sentence = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(sentence))
                {
                    Console.WriteLine("Input cannot be empty. Please try again.");
                    continue;
                }

                if (string.Equals(sentence, "Exit", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("End of program.");
                    break;
                }

                var letterCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                CountLettersAndSymbolsInSentence(sentence, letterCounts);

                foreach (var item in letterCounts)
                {
                    Console.WriteLine($"'{item.Key}' has occurred {item.Value} times");
                }
            }
        }

        private static void CountLettersAndSymbolsInSentence(string sentence, Dictionary<string, int> lettersCounts)
        {
            foreach (var ch in sentence)
            {
                var key = ch.ToString();

                if (key == string.Empty)
                {
                    continue;
                }

                if (lettersCounts.ContainsKey(key))
                {
                    lettersCounts[key]++;
                }
                else
                {
                    lettersCounts[key] = 1;
                }
            }
        }
    }
}
