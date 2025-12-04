using System.Text;

namespace CountLetters
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
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

                sentence = sentence.Trim();

                var charsCounts = new Dictionary<string, int>(StringComparer.CurrentCultureIgnoreCase);

                foreach (var ch in sentence)
                {
                    var key = ch.ToString();

                    if (charsCounts.ContainsKey(key))
                    {
                        charsCounts[key]++;
                    }
                    else
                    {
                        charsCounts[key] = 1;
                    }
                }

                // For counting only letters, uncomment the following line and comment out the above foreach
                // CountLettersOnly(sentence, charsCounts);

                foreach (var item in charsCounts)
                {
                    Console.WriteLine($"'{item.Key}' has occurred {item.Value} times");
                }

                // Count ASCII characters case-insensitive
                //CountAsciiCaseInsensitive(sentence);
            }
        }

        private static void CountLettersOnly(string sentence, Dictionary<string, int> charsCounts)
        {
            foreach (var ch in sentence)
            {
                var key = ch.ToString();

                if (char.IsLetter(ch))
                {
                    if (!charsCounts.ContainsKey(key))
                    {
                        charsCounts.Add(key, 0);
                    }

                    charsCounts[key]++;
                }
            }
        }

        // Count ASCII characters case-insensitive
        private static void CountAsciiCaseInsensitive(string sentence)
        {
            var counts = new int[256]; // ASCII range 0–255

            foreach (var ch in sentence)
            {
                if (ch < 256) 
                {
                    var normalized = char.ToUpperInvariant(ch);
                    counts[normalized]++;
                }
            }

            for (int i = 0; i < counts.Length; i++)
            {
                if (counts[i] > 0)
                {
                    Console.WriteLine($"'{(char)i}' has occurred {counts[i]} times");
                }
            }
        }
    }
}
// Example input:  I’m doing my best to learn "C#". Äöü 222 Привет, свят. äÖÜ 你好.