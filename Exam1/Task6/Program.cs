namespace Task6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter integer numbers separated by space: ");
            int[] numbers = Console.ReadLine()!
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            int arrLength = numbers.Length;

            for (int i = 0; i < arrLength/2; i++)
            {
                // Swap elements
                var temp = numbers[i];
                numbers[i] = numbers[arrLength - 1 - i];
                numbers[arrLength - 1 - i] = temp;
            }

            Console.WriteLine(string.Join(" ", numbers));
        }
    }
}
//Task 6 — Reverse an Array (In-Place) - 7 Points
//Description:
//Read an array of N integers.
//Reverse the array in place by swapping elements.
//Print the reversed array. Do not use .Reverse(); Hint: Do it with for loop.
//Example:
//Input:
//5
//1 2 3 4 5
//Output:
//5 4 3 2 1
