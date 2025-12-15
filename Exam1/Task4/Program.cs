namespace Task4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter at least two integer numbers separated by space: ");
            int[] numbers = Console.ReadLine()!
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var sortedAsc = numbers.OrderBy(x => x).ToArray();
            int minNumber = sortedAsc.First();
            int maxNumber = sortedAsc.Last();
            int secondMin = sortedAsc[1];
            int secondMax = sortedAsc[sortedAsc.Length - 2];

            Console.WriteLine($"Sorted ascending numbers: {string.Join(" ", sortedAsc)}");
            Console.WriteLine($"Min: {minNumber}");
            Console.WriteLine($"Max: {maxNumber}");
            Console.WriteLine($"SecondMin: {(minNumber == secondMin ? "None" : secondMin)}");
            Console.WriteLine($"SecondMax: {(maxNumber == secondMax ? "None" : secondMax)}");
        }
    }
}
//Task 4 — Find Min, Max, and Second Min/Max - 7 Points
//Description:
//In the console read more than 2 integers for comparison. Find and print: -The smallest number(Min) -The second smallest number (SecondMin) - The largest number (Max) - The second largest number (SecondMax)
//If the min and second min or the max and second max are the same number write for that comparison "None.". Example:
//Input:
//4 4 2 9 9
//Output:
//Min: 2
//SecondMin: 4
//Max: 9
//SecondMax: NONE
