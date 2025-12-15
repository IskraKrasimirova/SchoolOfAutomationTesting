namespace Task6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter an integer number (array length): ");
            var n = int.Parse(Console.ReadLine()!);

            Console.Write("Enter integer numbers separated by space: ");
            string[] input = Console.ReadLine()!.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            int arrLength = input.Length;

            if (arrLength != n)
            {
                Console.WriteLine("Number of entered elements is not correct.");
                return;
            }

            int[] numbers = new int[arrLength];

            for (int i = 0; i < arrLength; i++)
            {
                numbers[i] = int.Parse(input[i]);
            }

            // Use LINQ
            //int[] numbers = Console.ReadLine()!
            //    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
            //    .Select(int.Parse)
            //    .ToArray();

            //var arrLength = numbers.Length;

            for (int i = 0; i < arrLength/2; i++)
            {
                // Swap elements
                var temp = numbers[i];
                numbers[i] = numbers[arrLength - 1 - i];
                numbers[arrLength - 1 - i] = temp;
                //// Use tuple to swap values
                //(numbers[arrLength - 1 - i], numbers[i]) = (numbers[i], numbers[arrLength - 1 - i]);
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
//5 -> Why this number input is needed?
//1 2 3 4 5
//Output:
//5 4 3 2 1
