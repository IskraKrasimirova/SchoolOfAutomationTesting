namespace CompareStrings2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter first string: ");
            var firstString = Console.ReadLine();

            Console.Write("Enter second string: ");
            var secondString = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(firstString) && !string.IsNullOrWhiteSpace(secondString))
            {
                Console.WriteLine("Select an option for comparison: Press 1 for case-sensitive, 2 for case-insensitive");

                var option = Console.ReadLine();

                if (option == "1" || option == "2")
                {
                    CompareStrings(firstString, secondString, option == "1" ? true : false);
                }
                else
                {
                    Console.WriteLine("Invalid option selected.");
                }
            }
            else
            {
                Console.WriteLine("Input strings cannot be null or empty.");
            }
        }

        private static void CompareStrings(string? firstString, string? secondString, bool isCaseSensitive)
        {
            bool areEqual;

            if (isCaseSensitive)
            {
                areEqual = string.Equals(firstString, secondString);
            }
            else
            {
                areEqual = string.Equals(firstString, secondString, StringComparison.InvariantCultureIgnoreCase);
            }

            if (areEqual)
            {
                Console.WriteLine($"The strings '{firstString}' and '{secondString}' are equal via {(isCaseSensitive ? "case-sensitive" : "case-insensitive")} comparison");
            }
            else
            {
                Console.WriteLine($"The strings '{firstString}' and '{secondString}' are not equal.");
            }
        }
    }
}
