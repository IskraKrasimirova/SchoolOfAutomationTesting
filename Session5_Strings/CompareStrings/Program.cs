namespace CompareStrings
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter first string: ");
            var firstString = Console.ReadLine();

            Console.Write("Enter second string: ");
            var secondString = Console.ReadLine();

            if (string.Equals(firstString, secondString, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("The strings are equal (ignoring case sensitivity).");
            }
            else
            {
                Console.WriteLine("The strings are not equal.");
            }
        }
    }
}

// Compare two strings ignoring case