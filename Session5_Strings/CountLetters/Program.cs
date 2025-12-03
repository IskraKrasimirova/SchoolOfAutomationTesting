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

                // Alternatively, to count only letters, uncomment the following line and comment out the above foreach
                // CountLettersOnly(sentence, charsCounts);

                foreach (var item in charsCounts)
                {
                    Console.WriteLine($"'{item.Key}' has occurred {item.Value} times");
                }
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
                        charsCounts[key] = 0;
                    }

                    charsCounts[key]++;
                }
            }
        }
    }
}
// Example input:  I’m doing my best to learn "C#". Äöü 222 Привет, свят. äÖÜ 你好.