namespace Task4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter at least two integer numbers separated by space: ");
            string[] input = Console.ReadLine()!.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            int arrLength = input.Length;
            int[] numbers = new int[arrLength];

            for (int i = 0; i < arrLength; i++)
            {
                numbers[i] = int.Parse(input[i]);
            }

            SortAscending(numbers);

            int minNumber = numbers[0];
            int maxNumber = numbers[arrLength - 1];
            int secondMin = numbers[1];
            int secondMax = numbers[arrLength - 2];

            // Use LINQ
            //int[] numbers = Console.ReadLine()!
            //    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
            //    .Select(int.Parse)
            //    .ToArray();

            //var sortedAsc = numbers.OrderBy(x => x).ToArray();
            //int minNumber = sortedAsc.First();
            //int maxNumber = sortedAsc.Last();
            //int secondMin = sortedAsc[1];
            //int secondMax = sortedAsc[sortedAsc.Length - 2];
            //Console.WriteLine($"Sorted ascending numbers: {string.Join(" ", sortedAsc)}");

            Console.WriteLine($"Sorted ascending numbers: {string.Join(" ", numbers)}");
            Console.WriteLine($"Min: {minNumber}");
            Console.WriteLine($"Max: {maxNumber}");
            Console.WriteLine($"SecondMin: {(minNumber == secondMin ? "None" : secondMin)}");
            Console.WriteLine($"SecondMax: {(maxNumber == secondMax ? "None" : secondMax)}");
        }

        private static void SortAscending(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                var minIdx = FindMinIndex(array, i);
                (array[minIdx], array[i]) = (array[i], array[minIdx]);
            }
        }

        private static int FindMinIndex(int[] array, int startIndex)
        {
            int minIndex = startIndex;

            for (int i = startIndex + 1; i < array.Length; i++)
            {
                if (array[i] < array[minIndex])
                {
                    minIndex = i;
                }
            }

            return minIndex;
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
