namespace Task1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter non-negative integer number: ");
            var number = Console.ReadLine()!;
            int[] countDigits = new int[10];

            for (int i = 0; i < number.Length; i++)
            {
                countDigits[int.Parse(number[i].ToString())]++;
            }

            var digitsRepeated = countDigits.Count(x => x > 1);

            Console.WriteLine($"Distinct digits count is {digitsRepeated}.");
            Console.WriteLine("Frequency:");

            for (int i = 0; i < countDigits.Length; i++)
            {
                Console.WriteLine($"{i} is present {countDigits[i]} times.");
            }
        }
    }
}
//Task 1 — Count Digit Duplications - 5 Points
//Description:
//Read a non-negative integer as text input.
//Count how many distinct digits appear more than once.
//Also print the frequency of each digit (0–9).
//Example:
//Input:
//120450201
//Output:
//Distinct digits count is 3.
//Frequency:
//0 is present 3 times.
//1 is present 2 times.
//2 is present 2 times.
//3 is present 0 times.....
//9 is present 0 times
