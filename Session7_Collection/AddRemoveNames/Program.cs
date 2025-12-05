
namespace AddRemoveNames
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a list of names separated by a comma:");
            var names = Console.ReadLine()!
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(name => name.Trim())
                .Where(name => !string.IsNullOrEmpty(name))
                .ToList();

            PrintList(names);

            Console.WriteLine("Enter a name to remove from the list:");
            var nameToRemove = Console.ReadLine()!.Trim();

            RemoveName(names, nameToRemove);
            PrintList(names);
        }

        private static void RemoveName(List<string> names, string nameToRemove)
        {
            if (names.Remove(nameToRemove))
            {
                Console.WriteLine($"Removed '{nameToRemove}' from the list.");
            }
            else
            {
                Console.WriteLine($"Name '{nameToRemove}' not found in the list.");
            }
        }

        private static void PrintList(List<string> names)
        {
            string message;

            if (names.Count == 0)
            {
                message = "The list is empty.";
            }
            else
            {
                message = $"The list has {names.Count} names: {string.Join(", ", names)}";
            }

            Console.WriteLine(message);
        }
    }
}
