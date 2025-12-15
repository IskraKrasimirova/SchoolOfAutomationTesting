namespace Task5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter text: ");
            var text = Console.ReadLine()!.ToLower();
            var vowels = "aeiou";

            int vowelsCount = 0;
            int consonantsCount = 0;
            int digitsCount = 0;
            int othersCount = 0;

            foreach (var ch in text)
            {
                if (char.IsLetter(ch))
                {
                    if (vowels.Contains(ch))
                    {
                        vowelsCount++;
                    }
                    else
                    {
                        consonantsCount++;
                    }
                }
                else if (char.IsDigit(ch))
                {
                    digitsCount++;
                }
                else
                {
                    othersCount++;
                }
            }

            Console.WriteLine($"Vowels: {vowelsCount}");
            Console.WriteLine($"Consonants: {consonantsCount}");
            Console.WriteLine($"Digits: {digitsCount}");
            Console.WriteLine($"Other: {othersCount}");
        }
    }
}
//Task 5 — Count Vowels, Consonants, Digits, and Others - 7 Points
//Description:
//Read a text line. Count and print: -Vowels: A, E, I, O, U (case-insensitive) - Consonants: all other letters - Digits: 0–9 - Other: spaces, punctuation, etc.
//Example:
//Input:
//Hi! 2025 - 11 - 24
//Output:
//Vowels: 1
//Consonants: 1
//Digits: 8
//Other: 4
