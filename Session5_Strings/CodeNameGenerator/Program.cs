using System.Text;

namespace CodeNameGenerator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string? firstName;
            string? lastName;

            while (true)
            {
                Console.Write("Enter first name (at least 2 letters): ");
                firstName = Console.ReadLine();

                Console.Write("Enter last name (at least 3 letters): ");
                lastName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
                {
                    Console.WriteLine("End of program.");
                    break;
                }

                if (firstName.Length < 2)
                {
                    Console.WriteLine("First name cannot be less than 2 letters long. Please try again.");
                    continue;
                }

                if (lastName.Length < 3)
                {
                    Console.WriteLine("Last name cannot be less than 3 letters long. Please try again.");
                    continue;
                }

                firstName = firstName.Trim();
                lastName = lastName.Trim();

                var codeName = CreateCodeName(firstName, lastName);
                Console.WriteLine($"Code name: {codeName}");
                
                var shiftedCodeName = ShiftLetters(codeName, 3);
                Console.WriteLine($"Shifted code name: {shiftedCodeName}");
            }
        }

        private static string CreateCodeName(string firstName, string lastName)
        {
            var modifiedFirstName = ReverseString(firstName.Substring(0, 2));
            var modifiedLastName = ReverseString(lastName.Substring(lastName.Length - 3));

            return (modifiedFirstName + modifiedLastName).ToUpper();
        }

        private static string ReverseString(string input)
        {
            char[] charArray = input.ToCharArray();
            Array.Reverse(charArray);

            return new string(charArray);
        }

        //private static string ShiftLetters(string input, int shift)
        //{
        //    var sb = new StringBuilder();
        //    char shiftedChar;

        //    foreach (var ch in input)
        //    {
        //        if (ch == 'X')
        //        {
        //            shiftedChar = 'A';
        //        }
        //        else if (ch == 'Y')
        //        {
        //            shiftedChar = 'B';
        //        }
        //        else if (ch == 'Z')
        //        {
        //            shiftedChar = 'C';
        //        }
        //        else if (ch >= 'A' && ch <= 'W')
        //        {
        //            shiftedChar = (char)(ch + shift);
        //        }
        //        else // All other characters remain unchanged
        //        {
        //            shiftedChar = ch;
        //        }

        //        // More general approach using modulo
        //        // Works for any shift value. Assumes input is uppercase letters A-Z
        //        // All other characters remain unchanged
        //        // 'A' = 65, 'Z' = 90 => 0 to 25, 26 letters
        //        //if (ch >= 'A' && ch <= 'Z')
        //        //{
        //        //    int baseCode = 'A';
        //        //    int currentIndex = ch - baseCode;  // maps A->0, B->1, ..., Z->25
        //        //    int offset = (currentIndex + shift) % 26;  // 0-25 range
        //        //    shiftedChar = (char)(baseCode + offset);
        //        //}
        //        //else
        //        //{
        //        //    shiftedChar = ch;
        //        //}

        //        sb.Append(shiftedChar);
        //    }

        //    return sb.ToString();
        //}

        // General approach using modulo and works for any shift value
        // Assumes input is uppercase letters A-Z
        // All other characters remain unchanged
        // Using a defined alphabet string for clarity
        private static string ShiftLetters(string input, int shift)
        {
            const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var alphabetCount = alphabet.Length;
            var sb = new StringBuilder();

            foreach (var ch in input)
            {
                if (alphabet.Contains(ch))
                {
                    var index = alphabet.IndexOf(ch);              
                    var newIndex = (index + shift) % alphabetCount; 
                    sb.Append(alphabet[newIndex]);
                }
                else
                {
                    sb.Append(ch); 
                }
            }

            return sb.ToString();
        }
    }
}
//Write a method CreateCodeName(string firstName, string lastName).
//Rules:
//1.Take the first 2 letters of the first name.
//2.	Change the position of the 2 letters (the second becomes first and vice versa), example ‘ab’->’ba’
//3.	Take the last 3 letters of the last name.
//4.	Change the positions of the first and third letter, example: “sad” -> “das”.
//5.	Concatenate both results from 2. and 4.
//6.	Convert the result to uppercase.
//7.	Bonus** Shift each letter 3 letters to the right, example:
//“ANI” -> “DQL” 
//if it happens that the letter is X, Y or Z -> start from the beginning of the alphabet
//“ZORO” -> “CRUR”
