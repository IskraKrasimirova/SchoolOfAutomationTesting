namespace Task3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter an integer number: ");
            var number = Console.ReadLine()!;

            if (number[0] == '-')
            {
                number = number.Substring(1);
            }

            Console.WriteLine($"The count of digits is {number.Length}");
        }
    }
}
//Task 3 — Count Digits in a Number - 5 Points
//Description:
//Read an integer (positive or negative).
//Print how many digits it contains (the minus sign does not count).
//Example:
//Input:
//-1200
//Output:
//4
