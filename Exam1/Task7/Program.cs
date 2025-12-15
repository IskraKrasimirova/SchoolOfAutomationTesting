namespace Task7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the number of integer numbers: ");
            var n = int.Parse(Console.ReadLine()!);

            Console.Write("Enter integer numbers separated by space: ");
            int[] numbers = Console.ReadLine()!
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            if (numbers.Length != n)
            {
                Console.WriteLine("Number of elements is not correct.");
                return;
            }

            int groupsCount = 1; // even the array has 1 element, it has 1 group

            for (int i = 1; i < numbers.Length; i++)
            {
                if (numbers[i]!= numbers[i-1])
                {
                    groupsCount++;
                }
            }

            Console.WriteLine(groupsCount);
        }
    }
}
//Task 7 — Count Runs of Equal Numbers - 7 Points
//Description:
//Read N and an array of N integers.
//Count how many groups (runs) of consecutive equal numbers there are.
//Example:
//Input:
//8
//1 1 2 2 2 3 1 1
//Output:
//4
